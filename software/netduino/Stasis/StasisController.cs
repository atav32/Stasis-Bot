using System;
using Microsoft.SPOT;
using System.Threading;

namespace Stasis.Software.Netduino
{
	public class StasisController
	{
        const int DISPLAY_COUNT = 50;
        const int SATURATION = 20;

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
		private PID pidLogic = new PID(proportionalConstant:5.75);
        private MedianFilter medianFilter = new MedianFilter(5);
        
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot)
		{
			this.Robot = bot;
            this.pidLogic.SetPoint = 90;
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
		DateTime lastDateTime = DateTime.Now;

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{
            int motorValue = 0;

			// Let the robot update it's state
			this.Robot.Update();

            // Filter tile values
            this.medianFilter.AddValue(this.Robot.Tilt);

			// Update the PID 
			this.pidLogic.Update(medianFilter.Value);

			// Setup motors for new PID output
            if (this.medianFilter.Value > (this.pidLogic.SetPoint - SATURATION) && this.medianFilter.Value < (this.pidLogic.SetPoint + SATURATION))
                motorValue = (int)System.Math.Round(this.pidLogic.Output);

			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = motorValue;

            if (counter > 0)
            {
                counter--;
            }
            else
            {
                var diff = (double)(DateTime.Now - lastDateTime).Ticks;
                double fps = (100.0 / diff) * TimeSpan.TicksPerSecond;

                Debug.Print(motorValue + " >> " + this.Robot.FrontDistanceSensor.Distance + "\t:\t" + this.Robot.RearDistanceSensor.Distance + "\t:\t" + this.medianFilter.Value);

                lastDateTime = DateTime.Now;
                counter = DISPLAY_COUNT;
            }
		}

	}
}
