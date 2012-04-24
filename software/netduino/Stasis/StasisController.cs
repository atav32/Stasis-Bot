using System;
using Microsoft.SPOT;
using System.Threading;
using XMath = ElzeKool.exMath;
using SecretLabs.NETMF.Hardware.Netduino;

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
        private double nonLinearOuput = 0.0;        // dummy varaible for sin(PID output)
        private DateTime lastDateTime = DateTime.Now;

        /// <summary>
        /// PID for motor speed control
        /// </summary>
        private PID anglePID = new PID(proportionalConstant: 2);            // Current value for nonlinear sine output 
        private PID angularVelocityPID = new PID(proportionalConstant: 1);

        private MedianFilter angleIRFilter = new MedianFilter(3);
        private MedianFilter angularVelocityIRFilter = new MedianFilter(3);
        private MedianFilter accelFilter = new MedianFilter(3);

		/// <summary>
		/// Gets the balbot being controlled
		/// </summary>
		public StasisRobot Robot
		{
			get;
			private set;
		}

        /// <summary>
        /// Gain of angle PID controller
        /// </summary>
        public double Input1Gain
        {
            get;
            private set;
        }

        /// <summary>
        /// Gain of angular velocity PID controller
        /// </summary>
        public double Input2Gain
        {
            get;
            private set;
        }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot, double angleSetPoint = 91.6, double angularVelocitySetPoint = 0.0, double input1Gain = 0.5, double input2Gain = 100.0)
		{
			this.Robot = bot;
            this.anglePID.SetPoint = angleSetPoint;        // slightly tilted; should calibrate at the beginning of each run -BZ (4/12/12)
            this.angularVelocityPID.SetPoint = angularVelocitySetPoint;
            this.Input1Gain = input1Gain;
            this.Input2Gain = input2Gain;
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
            this.anglePID.SetPoint = averagingFilter.Value;
		}

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{
			motorValue = 0;

			// Let the robot update it's state
			this.Robot.Update();

			// Filter tilt values
			this.angleIRFilter.AddValue(this.Robot.Tilt);               // takes 5 fps
            this.angularVelocityIRFilter.AddValue(this.Robot.AngularVelocity);

			// Update the PID 
			this.anglePID.Update(this.angleIRFilter.Value);
            this.angularVelocityPID.Update(this.angularVelocityIRFilter.Value);

			// Apply Nonlinear Map
			nonLinearOuput = maxMotorOutput * XMath.Sin(this.anglePID.Output / 180 * System.Math.PI);     // takes 5 fps

			// Only bother updating motors if tilt is within a certain range of vertical
			if (this.angleIRFilter.Value > (this.anglePID.SetPoint - saturation) && this.angleIRFilter.Value < (this.anglePID.SetPoint + saturation))
			{
                // Update with combined output of angle and angular velocity control
				motorValue = (int)(System.Math.Round(nonLinearOuput) * this.Input1Gain + this.angularVelocityPID.Output * this.Input2Gain);
			}

            // Set Motor Speed
			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = motorValue;

			// Toggle Debug display
			if (true)
			{
				// Display debug output every displayCount cycles
				if (counter > 0)
				{
					counter--;
				}
				else
				{
					// Measuring the time delta of each cycle
					var diff = (double)(DateTime.Now - lastDateTime).Ticks;
					var fps = (displayCount / diff) * TimeSpan.TicksPerSecond;

					Debug.Print(fps + " >>\t" + motorValue + "\t:\t" + this.Robot.Tilt + "\t:\t" + this.Robot.AngularVelocity + "\t:\t" + nonLinearOuput + "\t:\t" + this.angularVelocityPID.Output);

					lastDateTime = DateTime.Now;
					counter = displayCount;
				}
			}
		}
	}
}
