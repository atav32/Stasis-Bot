using System;
using Microsoft.SPOT;
using System.Threading;
using XMath = ElzeKool.exMath;

namespace Stasis.Software.Netduino
{
	public class StasisController
	{
        const int DISPLAY_COUNT = 50;
        const int MAX_MOTOR_OUTPUT = 110;   // max motor value is actually 100, but sin(output) rarely reaches 1.0 -BZ (4/22/12)
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
		private PID pidLogic = new PID(proportionalConstant:3.5);      // Current value for nonlinear output // Linear Controller: P ~ 5.75 -BZ (4/22/12)
        private MedianFilter medianFilter = new MedianFilter(5);
        
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot)
		{
			this.Robot = bot;
            this.pidLogic.SetPoint = 91;        // slightly tilted; should calibrate at the beginning of each run -BZ (4/12/12)
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
                motorValue = (int)System.Math.Round(nonLinearOuput); //(int)System.Math.Round(this.pidLogic.Output);

			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = motorValue;

            // Display debug output every DISPLAY_COUNT cycles
            if (counter > 0)
            {
                counter--;
            }
            else
            {
                // Measuring the time delta of each cycle
                var diff = (double)(DateTime.Now - lastDateTime).Ticks;
                double fps = (100.0 / diff) * TimeSpan.TicksPerSecond;

                Debug.Print(motorValue + " >> " + this.Robot.FrontDistanceSensor.Distance + "\t:\t" + this.Robot.RearDistanceSensor.Distance + "\t:\t" + this.medianFilter.Value);

                lastDateTime = DateTime.Now;
                counter = DISPLAY_COUNT;
            }
		}

	}
}
