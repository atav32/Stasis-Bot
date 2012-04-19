using System;
using Microsoft.SPOT;
using System.Threading;

namespace Stasis.Software.Netduino
{
	public class StasisController
	{
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
		private PID pidLogic = new PID();

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot)
		{
			this.Robot = bot;
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
			this.pidLogic.SetPoint = averagingFilter.Value;
		}

		/// <summary>
		/// Performs one iteration of the controller
		/// </summary>
		public void Think()
		{
			// Let the robot update it's state
			this.Robot.Update();

			// Update the PID 
			this.pidLogic.Update(this.Robot.Tilt);

			// Setup motors for new PID output
			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = (int)System.Math.Round(this.pidLogic.Output);
		}

	}
}
