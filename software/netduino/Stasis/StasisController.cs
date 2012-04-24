using System;
using Microsoft.SPOT;
using System.Threading;
using XMath = ElzeKool.exMath;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Stasis.Software.Netduino
{
	public class StasisController
	{
		const int DISPLAY_COUNT = 50;
		const int MAX_MOTOR_OUTPUT = 150;   // max motor value is actually 100, but sin(output) rarely reaches 1.0 -BZ (4/22/12)
		const int SATURATION = 30;

		/// <summary>
		/// Gets the balbot being controlled
		/// </summary>
		public StasisRobot Robot
		{
			get;
			private set;
		}

		/// <summary>
		/// PID for motor speed control
		/// </summary>
		private PID pidLogic = new PID(proportionalConstant: 2.25);      // Current value for nonlinear output // Linear Controller: P ~ 5.75 -BZ (4/22/12)
		private MedianFilter medianFilter = new MedianFilter(3);

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot)
		{
			this.Robot = bot;
			this.pidLogic.SetPoint = 90.5;        // slightly tilted; should calibrate at the beginning of each run -BZ (4/12/12)
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

		int counter = DISPLAY_COUNT;
		int motorValue = 0;
		double nonLinearOuput = 0.0;
		DateTime lastDateTime = DateTime.Now;
		Sensors.Analog5DOF imu = new Sensors.Analog5DOF(Pins.GPIO_PIN_A0, Pins.GPIO_PIN_A1, Pins.GPIO_PIN_A2, Pins.GPIO_PIN_A3);

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{
			motorValue = 0;

			// Let the robot update it's state
			this.Robot.Update();

			// Filter tile values
			this.medianFilter.AddValue(this.Robot.Tilt);

			// Update the PID 
			this.pidLogic.Update(medianFilter.Value);

			// Apply Nonlinear Map
			nonLinearOuput = MAX_MOTOR_OUTPUT * XMath.Sin(this.pidLogic.Output / 180 * System.Math.PI);

			// Setup motors for new PID output
			if (this.medianFilter.Value > (this.pidLogic.SetPoint - SATURATION) && this.medianFilter.Value < (this.pidLogic.SetPoint + SATURATION))
			{
				motorValue = (int)System.Math.Round(nonLinearOuput); //(int)System.Math.Round(this.pidLogic.Output);
			}

			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = motorValue;
			imu.Update();

			// Display debug output every DISPLAY_COUNT cycles
			if (counter > 0)
			{
				counter--;
			}
			else
			{
				// Measuring the time delta of each cycle
				var diff = (double)(DateTime.Now - lastDateTime).Ticks;
				var fps = (DISPLAY_COUNT / diff) * TimeSpan.TicksPerSecond;

				//Debug.Print(fps.ToString());
				Debug.Print(fps + ">>" + motorValue + "\t:\t" + this.medianFilter.Value + "\t:\t" + imu.Acceleration.ToString());

				lastDateTime = DateTime.Now;
				counter = DISPLAY_COUNT;
			}
		}

	}
}
