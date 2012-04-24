using System;
using Microsoft.SPOT;
using System.Threading;
using XMath = ElzeKool.exMath;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Stasis.Software.Netduino
{
	public class StasisController
	{
		const int displayCount = 50;
		const int maxMotorOutput = 150;   // max motor value is actually 100, but sin(output) rarely reaches 1.0 -BZ (4/22/12)
		const int saturation = 30;
        const int stableRange = 1;
        int counter = displayCount;
        int motorValue = 0;
        double nonLinearOuput = 0.0;
        DateTime lastDateTime = DateTime.Now;

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
		private PID pidLogic = new PID(proportionalConstant: 2.2, integrationConstant: 0.005, derivativeConstant: 1);      // Current value for nonlinear output // Linear Controller: P ~ 5.75 -BZ (4/22/12)
		private MedianFilter IRFilter = new MedianFilter(3);
        private MedianFilter accelFilter = new MedianFilter(3);
        private Sensors.Analog5DOF imu = new Sensors.Analog5DOF(Pins.GPIO_PIN_A0, Pins.GPIO_PIN_A1, Pins.GPIO_PIN_A2, Pins.GPIO_PIN_A3);

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bot"></param>
		public StasisController(StasisRobot bot)
		{
			this.Robot = bot;
			this.pidLogic.SetPoint = 91.75;        // slightly tilted; should calibrate at the beginning of each run -BZ (4/12/12)
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
			motorValue = 0;

			// Let the robot update it's state
			this.Robot.Update();
            this.imu.Update();

			// Filter tilt values
            this.IRFilter.AddValue(this.Robot.Tilt);      // takes 5 fps
            //this.accelFilter.AddValue(this.imu.Acceleration.Z);
            
			// Update the PID 
            this.pidLogic.Update(IRFilter.Value);
            //this.pidLogic.Update(this.Robot.Tilt);  // unfiltered value

			// Apply Nonlinear Map
            nonLinearOuput = maxMotorOutput * XMath.Sin(this.pidLogic.Output / 180 * System.Math.PI);     // takes 5 fps

			// Setup motors for new PID output
            //if (this.Robot.Tilt > (this.pidLogic.SetPoint - saturation) && this.Robot.Tilt < (this.pidLogic.SetPoint + saturation))
            if (this.IRFilter.Value > (this.pidLogic.SetPoint - saturation) && this.IRFilter.Value < (this.pidLogic.SetPoint + saturation))
			{
                motorValue = (int)System.Math.Round(nonLinearOuput); //(int)System.Math.Round(this.pidLogic.Output);

                // Decaying function for integration error
                if (this.IRFilter.Value > (this.pidLogic.SetPoint - stableRange) && this.IRFilter.Value < (this.pidLogic.SetPoint + stableRange))
                {
                    this.pidLogic.accumulativeError /= 2;
                }
			}

			this.Robot.LeftMotor.Speed = this.Robot.RightMotor.Speed = motorValue;

            // Toggle Debug display
            if (false) 
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

                    //Debug.Print(fps.ToString());
                    Debug.Print(motorValue + "\t:\t" + this.Robot.Tilt + "\t:\t" + this.pidLogic.accumulativeError + "\t:\t" + this.pidLogic.DerivativeError);
                    //Debug.Print(fps + ">>\t" + motorValue + "\t:\t" + this.Robot.Tilt + "\t:\t" + imu.Acceleration.ToString() + "\t:\t" + imu.RotationRate.ToString());

                    lastDateTime = DateTime.Now;
                    counter = displayCount;
                }
            }
		}
	}
}
