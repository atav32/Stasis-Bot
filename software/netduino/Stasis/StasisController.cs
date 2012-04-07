using System;
using Microsoft.SPOT;
using System.Threading;

namespace Stasis.Software.Netduino
{
	public class BalbotController
	{

		/// <summary>
		/// Gets the balbot being controlled
		/// </summary>
		public StasisRobot Balbot
		{
			get;
			private set;
		}

		/// <summary>
		/// Difference between front and rear rangers averaged over 10 sample window
		/// </summary>
		private MovingAverageFilter differenceFilter = new MovingAverageFilter(10);

        /// <summary>
        /// Filters out spikes of noise in a 7 sample window
        /// </summary>
        private MedianFilter noiseFilter = new MedianFilter(7);

		/// <summary>
		/// PID for motor speed control
		/// </summary>
		private PID pidLogic = new PID(0.0, 0.05, 0, 0.5);      // The motors act as the integrator, the I = 0

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public BalbotController(StasisRobot bot)
		{
			this.Balbot = bot;
		}

        /// <summary>
        /// Samples 100 values while the robot is vertical
        /// </summary>
        public void Calibrate()
        {
            double calibrationPoint = 0;

            for (int i = 0; i < 100; i++)
            {
                this.Think();
            }

            calibrationPoint = this.differenceFilter.Value;

            pidLogic.SetPoint = calibrationPoint;
        }

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{

			this.Balbot.FrontVerticalRanger.Update();
			this.Balbot.RearVerticalRanger.Update();

			double front = this.Balbot.FrontVerticalRanger.Distance;
			double back = this.Balbot.RearVerticalRanger.Distance;

            this.noiseFilter.AddValue(back - front);
			this.differenceFilter.AddValue(this.noiseFilter.Value);

			this.pidLogic.Update(this.differenceFilter.Value);
			

			Debug.Print((back - front) + "\t:\t" + this.differenceFilter.Value.ToString() + "\t:\t" + pidLogic.Output.ToString());
		}

        /// <summary>
        /// Move the motors
        /// </summary>
        public void Move()
        {
            this.Think();

            this.Balbot.LeftMotor.Speed = this.Balbot.RightMotor.Speed = (int)System.Math.Round(pidLogic.Output);
        }
		
	}
}
