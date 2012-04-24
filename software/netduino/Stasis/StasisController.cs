using System;
using Microsoft.SPOT;
using System.Threading;
using XMath = ElzeKool.exMath;
using SecretLabs.NETMF.Hardware.Netduino;
using Stasis.Software.Netduino.Extensions;
using Stasis.Software.Netduino.Communication;

namespace Stasis.Software.Netduino
{
	public class StasisController
	{
		private const int NonLinearControllerConstant = 150;   // max motor value is actually 100, but sin(output) rarely reaches 1.0 -BZ (4/22/12)
		private const int MotorSaturationAngle = 30;
		private const int StableAngleRange = 1;
		
		double nonLinearOuput = 0.0;

		/// <summary>
		/// Gets the balbot being controlled
		/// </summary>
		public StasisRobot Robot
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the average (over 1 second) loop speed in iterations per second
		/// </summary>
		public int LoopSpeed
		{
			get;
			private set;
		}

		/// <summary>
		/// PID for motor speed control
		/// </summary>
		private PID pidLogic = new PID(proportionalConstant: 2.2, integrationConstant: 0.005, derivativeConstant: 1);      // Current value for nonlinear output // Linear Controller: P ~ 5.75 -BZ (4/22/12)
		
		/// <summary>
		/// IR data median filter
		/// </summary>
		private MedianFilter irFilter = new MedianFilter(3);

		/// <summary>
		/// Accelerometer data median filter
		/// </summary>
		private MedianFilter accelFilter = new MedianFilter(3);

		/// <summary>
		/// Some state for the loop speed calculations
		/// </summary>
		private DateTime lastDateTime = DateTime.Now;
		private int loopSpeedCounter = 0;

		/// <summary>
		/// Wifi Monitor instance
		/// </summary>
		private WiFiMonitor wifiMonitor = new WiFiMonitor(SerialPorts.COM1, 230400);
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot)
		{
			this.Robot = bot;
			this.pidLogic.SetPoint = 91.75;        // slightly tilted; should calibrate at the beginning of each run -BZ (4/12/12)
			this.wifiMonitor.MessageReceived += new WiFiMonitor.MessageReceivedEventHandler(WifiMonitor_MessageReceived);
		}

		private void WifiMonitor_MessageReceived(object sender, WiFiMonitor.MessageReceivedEventArgs e)
		{
			if (e.Message.Type == WiFiMonitor.MessageType.SetPID)
			{
				if (e.Message.Values.Length >= 5)
				{
					if (e.Message.Values[0] == 1)
					{
						// Set PID stuff for angle PID controller
						this.pidLogic.ProportionalConstant = e.Message.Values[1];
						this.pidLogic.IntegrationConstant = e.Message.Values[2];
						this.pidLogic.DerivativeConstant = e.Message.Values[3];
						this.pidLogic.SetPoint = e.Message.Values[4];
					}
					else if (e.Message.Values[0] == 2)
					{
						// Set PID stuff for delta Angle PID controller
					}
				}
			}
			if (e.Message.Type == WiFiMonitor.MessageType.GetPID || e.Message.Type == WiFiMonitor.MessageType.SetPID)
			{
				if (e.Message.Values[0] == 1)
				{
					// Send out PID values for Angle PID
					this.wifiMonitor.SendMessage(new WiFiMonitor.Message(WiFiMonitor.MessageType.GetPID, new double[] { 1, pidLogic.ProportionalConstant, pidLogic.IntegrationConstant, pidLogic.DerivativeConstant, pidLogic.SetPoint }));
				}
				else if (e.Message.Values[0] == 2)
				{
					// Send out PID values for delta angle PID
				}
			}
		}

		/// <summary>
		/// Samples 100 values while the robot is vertical
		/// </summary>
		public void Calibrate()
		{
			// Setup averaging filter for 100 samples
			var averagingFilter = new MovingAverageFilter(100);

			// Read in tilt 100 times to get offset from 0 when the
			// robot is being held at 0 degrees.
			for (int i = 0; i < 100; i++)
			{
				// Update state/sensors
				this.Robot.Update();

				// Add to filter
				averagingFilter.AddValue(this.Robot.Tilt);
			}

			// Set point for the PID is the tilt angle when the robot
			// is vertical.
			this.pidLogic.SetPoint = 91;
		}

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{
			// Motors are 0 by default
			int motorSpeed = 0;

			// Let the robot update it's state
			this.Robot.Update();

			// Filter tilt values
			this.irFilter.AddValue(this.Robot.Tilt);      // takes 5 fps
			//this.accelFilter.AddValue(this.imu.Acceleration.Z);

			// Update the PID 
			this.pidLogic.Update(irFilter.Value);
			//this.pidLogic.Update(this.Robot.Tilt);  // unfiltered value

			// Apply Nonlinear Map
			nonLinearOuput = NonLinearControllerConstant * XMath.Sin(this.pidLogic.Output / 180 * System.Math.PI);     // takes 5 fps

			// Setup motors for new PID output
			//if (this.Robot.Tilt > (this.pidLogic.SetPoint - saturation) && this.Robot.Tilt < (this.pidLogic.SetPoint + saturation))
			if (this.irFilter.Value > (this.pidLogic.SetPoint - MotorSaturationAngle) && this.irFilter.Value < (this.pidLogic.SetPoint + MotorSaturationAngle))
			{
				motorSpeed = (int)System.Math.Round(nonLinearOuput); //(int)System.Math.Round(this.pidLogic.Output);

				// Decaying function for integration error
				if (this.irFilter.Value > (this.pidLogic.SetPoint - StableAngleRange) && this.irFilter.Value < (this.pidLogic.SetPoint + StableAngleRange))
				{
					this.pidLogic.accumulativeError /= 2;
				}
			}

			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = motorSpeed;

			// Toggle Debug display
			if (true)
			{
				var now = DateTime.Now;
				var diff = (now - lastDateTime).Ticks;
				if (diff > TimeSpan.TicksPerSecond)
				{
					this.LoopSpeed = this.loopSpeedCounter;
					this.loopSpeedCounter = 0;
					this.lastDateTime = now;
					Debug.Print(this.LoopSpeed.ToString());
					this.wifiMonitor.SendMessage(new WiFiMonitor.Message(WiFiMonitor.MessageType.GetLoopSpeed, new double[] { this.LoopSpeed }));
				}
				else
				{
					this.loopSpeedCounter++;
				}
			}
		}
	}
}
