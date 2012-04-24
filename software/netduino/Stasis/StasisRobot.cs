using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using XMath = ElzeKool.exMath;

namespace Stasis.Software.Netduino
{
	public class StasisRobot
	{
		/// <summary>
		/// Gets the left motor on the balbot.
		/// </summary>
		public Motor LeftMotor
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the right motor on the balbot.
		/// </summary>
		public Motor RightMotor
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the infrared ranger measuring distance to the ground on the front 
		/// side of teh balbot.
		/// </summary>
		public InfraredDistanceSensor FrontDistanceSensor
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the infrared ranger measuring distance to the ground on the rear 
		/// of the balbot.
		/// </summary>
		public InfraredDistanceSensor RearDistanceSensor
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the current tilt angle of the robot in degrees. This is updated on calls to 
		/// Think()
		/// </summary>
		public double Tilt
		{
			get;
			private set;
		}

        public double AngularVelocity
        {
            get;
            private set;
        }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="leftMotor"></param>
		/// <param name="rightMotor"></param>
		/// <param name="frontRanger"></param>
		/// <param name="rearRanger"></param>
		public StasisRobot(Motor leftMotor, Motor rightMotor, InfraredDistanceSensor frontRanger, InfraredDistanceSensor rearRanger)
		{
			this.LeftMotor = leftMotor;
			this.RightMotor = rightMotor;
			this.FrontDistanceSensor = frontRanger;
			this.RearDistanceSensor = rearRanger;
			this.RearDistanceSensor.Offset = 2.2;
		}

		/// <summary>
		/// Lets the robot update state
		/// </summary>
		public void Update()
		{
			// Update sensors
			this.FrontDistanceSensor.Update();
			this.RearDistanceSensor.Update();

			// Update tilt value
            var previousTilt = this.Tilt;
			this.Tilt = CalculateTiltAngleFromDistanceSensors(this.FrontDistanceSensor.Distance, this.RearDistanceSensor.Distance);
            this.AngularVelocity = this.Tilt - previousTilt;
		}

		/// <summary>
		/// Calculates the tilt angle from distance sensors. 
		/// </summary>
		/// <param name="front"></param>
		/// <param name="rear"></param>
		/// <returns></returns>
		private double CalculateTiltAngleFromDistanceSensors(double front, double rear)
		{
			// The calculation is over a triangle ABC
			// ∠BAC = Angle at the sensors
			// ∠ABC = Angle at the front of the robot
			// ∠ACB = Angle at the rear of the robot
			// ∠ADB = Angle at the base of the robot towards the front

			// Angle BAC is a constant 60 since both sensors are facing outwards at 30 from the 
			// vertical each.
			double BAC = XMath.PI / 3;

			// Angle BAD is a constant 30 since each sensor is facing outwards at 30 from the vertical
			double BAD = XMath.PI / 6;

			// Length AB is the distance from the front sensor + a constant distance from the front 
			// sensor to where the angle is actually made
			double AB = front + 3.519527;
			double AB2 = System.Math.Pow(AB, 2.0);

			// Length AC is the distance from the rear sensor + a constant distance from the front 
			// sensor to where the angle is actually made
			double AC = rear + 3.519527;
			double AC2 = System.Math.Pow(AC, 2.0);

			// We calculate CB^2 using law of cosines
			double CB2 = AB2 + AC2 - (2 * AB * AC * XMath.Cos(BAC));
			double CB = XMath.Sqrt(CB2);

			// Using CB^2 and AB, we can get angle ABC
			double ABC = XMath.Acos((AC2 - CB2 - AB2) / (-2.0 * CB * AB));

			// Now we know angle ADB ... also known as our tilt
			return (XMath.PI - BAD - ABC) * (180 / System.Math.PI);
		}
	}
}
