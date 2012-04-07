using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Stasis.Software.Netduino
{
	public class StasisRobot
	{
		/// <summary>
		/// Gets the left motor on the balbot
		/// </summary>
		public Motor LeftMotor
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the right motor on the balbot
		/// </summary>
		public Motor RightMotor
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the infrared ranger measuring vertical distance from the ground on the front 
		/// side of teh balbot
		/// </summary>
		public InfraredRanger FrontVerticalRanger
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the infrared ranger measuring vertical distance from the ground on the rear 
		/// of the balbot
		/// </summary>
		public InfraredRanger RearVerticalRanger
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
		public StasisRobot(Motor leftMotor, Motor rightMotor, InfraredRanger frontRanger, InfraredRanger rearRanger)
		{
			this.LeftMotor = leftMotor;
			this.RightMotor = rightMotor;
			this.FrontVerticalRanger = frontRanger;
			this.RearVerticalRanger = rearRanger;
		}
	}
}
