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
        // Messy declaration of variables
		private const int displayCount = 50;        // number of cycles to skip before displaying debug once
		private const int maxMotorOutput = 150;     // max motor value is actually 100, but sin(output) rarely reaches 1.0 -BZ (4/22/12)
        private const int saturation = 30;          // +/- angle allowed from vertical
        private int counter = displayCount;         // loop cycle counter
        private int motorValue = 0;                 // dummy variable for motor variabls
        private double sinAnglePIDOutput = 0.0;					// dummy varaible for sin(angle PID output)
		private double sinAnglularVelocityPIDOutput = 0.0;      // dummy varaible for sin(angular velocity PID output)

		/// <summary>
		/// Gets the balbot being controlled
		/// </summary>
		public StasisRobot Robot
		{
			get;
			private set;
		}

        public PID DisplacementPID
        {
            get;
            private set;
        }

        public PID VelocityPID
        {
            get;
            private set;
        }
     
        public PID AnglePID
        {
            get;
            private set;
        }

        public PID AngularVelocityPID
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
		public StasisController(StasisRobot bot, 
                                double displacementSetPoint = 0.0,
                                double angleSetPoint = 90.5,               // slightly tilted; should calibrate at the beginning of each run -BZ (4/12/12)
                                double velocitySetPoint = 0.0,
                                double angularVelocitySetPoint = 0.0, 
                                double displacementProportionalValue = 0,
                                double velocityProportionalValue = 0,
                                double angleProportionalValue = 7.5,
								double angleIntegrationValue = 1.25,
								double angleDerivativeValue = 55.0,
                                double angularVelocityProportionalValue = 0,
                                int integratorWindow = 4)
		{
			this.Robot = bot;

			// PID
            this.DisplacementPID = new PID(setPoint: displacementSetPoint, proportionalConstant: displacementProportionalValue, integratorWindowSize: integratorWindow);
            this.VelocityPID = new PID(setPoint: velocitySetPoint, proportionalConstant: velocityProportionalValue, integratorWindowSize: integratorWindow);
            this.AnglePID = new PID(setPoint: angleSetPoint, proportionalConstant: angleProportionalValue, integrationConstant: angleIntegrationValue, derivativeConstant:angleDerivativeValue, integratorWindowSize: integratorWindow);
            this.AngularVelocityPID = new PID(setPoint: angularVelocitySetPoint, proportionalConstant: angularVelocityProportionalValue, integratorWindowSize: integratorWindow);

			this.wifiMonitor.MessageReceived += new WiFiMonitor.MessageReceivedEventHandler(WifiMonitor_MessageReceived);
		}

		private void WifiMonitor_MessageReceived(object sender, WiFiMonitor.MessageReceivedEventArgs e)
		{
			if (e.Message.Type == WiFiMonitor.MessageType.SetPID)
			{
				if (e.Message.Values.Length >= 16)
				{
					// Set PID stuff for angle PID controller
					this.AnglePID.ProportionalConstant = e.Message.Values[0];
					this.AnglePID.IntegrationConstant = e.Message.Values[1];
					this.AnglePID.DerivativeConstant = e.Message.Values[2];
					this.AnglePID.SetPoint = e.Message.Values[3];
					this.AnglePID.Reset();
						
					// Set PID stuff for delta Angle PID controller
					this.AngularVelocityPID.ProportionalConstant = e.Message.Values[4];
					this.AngularVelocityPID.IntegrationConstant = e.Message.Values[5];
					this.AngularVelocityPID.DerivativeConstant = e.Message.Values[6];
					this.AngularVelocityPID.SetPoint = e.Message.Values[7];
					this.AngularVelocityPID.Reset();

					// Set PID stuff for angle PID controller
					this.DisplacementPID.ProportionalConstant = e.Message.Values[8];
					this.DisplacementPID.IntegrationConstant = e.Message.Values[9];
					this.DisplacementPID.DerivativeConstant = e.Message.Values[10];
					this.DisplacementPID.SetPoint = e.Message.Values[11];
					this.DisplacementPID.Reset();
						
					// Set PID stuff for delta Angle PID controller
					this.VelocityPID.ProportionalConstant = e.Message.Values[12];
					this.VelocityPID.IntegrationConstant = e.Message.Values[13];
					this.VelocityPID.DerivativeConstant = e.Message.Values[14];
					this.VelocityPID.SetPoint = e.Message.Values[15];
					this.VelocityPID.Reset();
				}
			}
			if (e.Message.Type == WiFiMonitor.MessageType.GetPID || e.Message.Type == WiFiMonitor.MessageType.SetPID)
			{
				this.wifiMonitor.SendMessage(
					new WiFiMonitor.Message(WiFiMonitor.MessageType.GetPID, new double[] { 
						AnglePID.ProportionalConstant, 
						AnglePID.IntegrationConstant, 
						AnglePID.DerivativeConstant, 
						AnglePID.SetPoint,
						AngularVelocityPID.ProportionalConstant, 
						AngularVelocityPID.IntegrationConstant, 
						AngularVelocityPID.DerivativeConstant, 
						AngularVelocityPID.SetPoint,
						DisplacementPID.ProportionalConstant, 
						DisplacementPID.IntegrationConstant, 
						DisplacementPID.DerivativeConstant, 
						DisplacementPID.SetPoint,
						VelocityPID.ProportionalConstant, 
						VelocityPID.IntegrationConstant, 
						VelocityPID.DerivativeConstant, 
						VelocityPID.SetPoint,
					}));
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
				this.Robot.UpdateSensors();

				// Add to filter
				averagingFilter.AddValue(this.Robot.Angle);
			}

			// Set point for the PID is the tilt angle when the robot
			// is vertical.
            this.AnglePID.SetPoint = averagingFilter.Value;
		}

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{
			// Motors are 0 by default
			motorValue = 0;
			
			// Let the robot update it's sensors
			this.Robot.UpdateSensors();

			// Let the robot update it's state
			this.Robot.UpdateState();

			// Update the PID 
			this.DisplacementPID.Update(this.Robot.Displacement);
			this.VelocityPID.Update(this.Robot.Velocity);
			this.AnglePID.Update(this.Robot.Angle);
			this.AngularVelocityPID.Update(this.Robot.AngularVelocity);

			// Only bother updating motors if tilt is within a certain range of vertical
			if (this.Robot.Angle > (this.AnglePID.SetPoint - saturation) && this.Robot.Angle < (this.AnglePID.SetPoint + saturation))
			{
				// Update with combined output of angle and angular velocity control
				motorValue = (int)(this.DisplacementPID.Output + this.VelocityPID.Output + this.AnglePID.Output + this.AngularVelocityPID.Output);
			}

			// Set Motor Speed
			//this.Robot.LeftMotor.Velocity = 100;
			//this.Robot.RightMotor.Velocity = 100;
			this.Robot.LeftMotor.Velocity = this.Robot.RightMotor.Velocity = motorValue;

	
			if (this.VelocityPID.SetPoint != 0)
			{
				Debug.Print("DATA, " + motorValue + ",   " +
									this.Robot.Angle.ToString("N4") + ",   " +
									this.Robot.AngularVelocity.ToString("N4") + ",   " +
									this.Robot.Displacement.ToString("N4") + ",   " +
									this.AnglePID.Output.ToString("N4") + ",   " +
									this.AngularVelocityPID.Output.ToString("N4") + ",   " +
									this.DisplacementPID.Output.ToString("N4"));

			}

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
					Debug.Print("DATA," + LoopSpeed);
					//Debug.Print("DATA," + this.angleAverage.Value);
					//Debug.Print("DATA," + this.Robot.LeftMotor.MeasuredVelocity + "," + this.Robot.RightMotor.MeasuredVelocity);
					//Debug.Print("DATA," + this.Robot.LeftMotor.Velocity + "," + this.Robot.RightMotor.Velocity + "," + this.Robot.Angle + "," + this.Robot.AngularVelocity);

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
