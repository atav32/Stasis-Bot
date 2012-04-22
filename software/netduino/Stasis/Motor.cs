using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;

namespace Stasis.Software.Netduino
{
	public class Motor
	{
		/// <summary>
		/// Gets or sets the speed between -100 and 100
		/// </summary>
		public int Speed
		{
			get
			{
				return this.speed;
			}
			set
			{
				this.SetSpeed(value);
			}
		}

		/// <summary>
		/// Gets the current draw for this motor.
		/// </summary>
		public int Current
		{
			get
			{
				return this.currentSense.Read();
			}
		}

		/// <summary>
		/// Gets or sets whether the motor is reversed. This is the same as setting 
		/// all speeds to the negative of what they need to be (useful if have pair of 
		/// opposite facing motors driving a robot)
		/// </summary>
		public bool Reversed
		{
			get;
			set;
		}

		/// <summary>
		/// Speed between [-100, 100]
		/// </summary>
		private int speed = 0;

		/// <summary>
		/// PWM port for speed control
		/// </summary>
		private PWM speedPWM = null;

		/// <summary>
		/// Direction control pin A
		/// </summary>
		private OutputPort directionOutputA = null;

		/// <summary>
		/// Direction control pin B
		/// </summary>
		private OutputPort directionOutputB = null;

		/// <summary>
		/// Current sense pin
		/// </summary>
		private AnalogInput currentSense = null;

		/// <summary>
		/// Sets speed to given value. Support function for "Speed" property.
		/// </summary>
		/// <param name="value"></param>
		private void SetSpeed(int value)
		{
			// Are we reversed?
			if (this.Reversed)
			{
				value *= -1;
			}

			// Set direction pins
			if (value == 0)
			{
				this.directionOutputA.Write(false);
				this.directionOutputB.Write(false);
			}
			else if (value > 0)
			{
				this.directionOutputA.Write(true);
				this.directionOutputB.Write(false);
			}
			else if (value < 0)
			{
				this.directionOutputA.Write(false);
				this.directionOutputB.Write(true);
			}

			uint val = (uint)System.Math.Abs(value);
			if (val > 100)
			{
				val = 100;
			}

			// Change PWM to new speed
			this.speedPWM.SetDutyCycle(val);

			// Save speed
			this.speed = value;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pwmPin"></param>
		/// <param name="directionPinA"></param>
		/// <param name="directionPinB"></param>
		/// <param name="currentSensePin"></param>
		public Motor(Cpu.Pin pwmPin, Cpu.Pin directionPinA, Cpu.Pin directionPinB, Cpu.Pin currentSensePin)
		{
			this.speedPWM = new PWM(pwmPin);
			this.directionOutputA = new OutputPort(directionPinA, false);
			this.directionOutputB = new OutputPort(directionPinB, false);
			this.currentSense = new AnalogInput(currentSensePin);
		}

	}
}
