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
		public int Velocity
		{
			get
			{
				return this.velocity;
			}
			set
			{
				this.SetVelocity(value);
			}
		}

		/// <summary>
		/// Gets the average velocity since last call to Update
		/// </summary>
        public double MeasuredVelocity
        {
            get;
            private set;
        }

		/// <summary>
		/// Gets the displacement since the last call to Update
		/// </summary>
		public double MeasuredDisplacement
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the current draw for this motor.
		/// </summary>
		public int Current
		{
			get
			{
				return 0;// return this.currentSense.Read();
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
		private int velocity = 0;

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
		/// Encoder channel A input
		/// </summary>
		private InterruptPort encoderChannelA = null;

		/// <summary>
		/// Encoder channel B input
		/// </summary>
		private InputPort encoderChannelB = null;

		/// <summary>
		/// Current encoder direction
		/// </summary>
		private double encoderCounter = 0;

		/// <summary>
		/// 
		/// </summary>
		private DateTime lastUpdateDateTime = DateTime.MinValue;

		/// <summary>
		/// Sets speed to given value. Support function for "Speed" property.
		/// </summary>
		/// <param name="value"></param>
		private void SetVelocity(int value)
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
			this.velocity = value;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pwmPin"></param>
		/// <param name="directionPinA"></param>
		/// <param name="directionPinB"></param>
		/// <param name="currentSensePin"></param>
		public Motor(Cpu.Pin pwmPin, Cpu.Pin directionPinA, Cpu.Pin directionPinB, Cpu.Pin encoderA, Cpu.Pin encoderB)
		{
			this.speedPWM = new PWM(pwmPin);
			this.directionOutputA = new OutputPort(directionPinA, false);
			this.directionOutputB = new OutputPort(directionPinB, false);
            this.encoderChannelA = new InterruptPort(encoderA, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeHigh);
			this.encoderChannelB = new InputPort(encoderB, false, Port.ResistorMode.Disabled);

			//this.encoderChannelA.OnInterrupt += new NativeEventHandler(encoder_OnInterrupt);

			this.MeasuredVelocity = this.MeasuredDisplacement = 0;
		}

		/// <summary>
		/// Update state for the motor
		/// </summary>
		/// <returns></returns>
		public void Update()
		{
			if (this.lastUpdateDateTime != DateTime.MinValue)
			{
				var circumference = 0.037698;
				this.MeasuredDisplacement = (encoderCounter / 6533.0) * circumference;

				var timeDiff = DateTime.Now - this.lastUpdateDateTime;
				this.MeasuredVelocity = this.MeasuredDisplacement / ((double)timeDiff.Ticks / (double)TimeSpan.TicksPerSecond);
			}

			this.lastUpdateDateTime = DateTime.Now;
		}

		/// <summary>
		/// Handle encoder shit
		/// </summary>
		/// <param name="data1"></param>
		/// <param name="data2"></param>
		/// <param name="time"></param>
        void encoder_OnInterrupt(uint port, uint state, DateTime time)
        {
			if (state == 0)
			{
				if (this.encoderChannelB.Read() == true)
				{
					encoderCounter++;
				}
				else
				{
					encoderCounter--;
				}
			}
			else
			{
				if (this.encoderChannelB.Read() == true)
				{
					encoderCounter--;
				}
				else
				{
					encoderCounter++;
				}
			}
        }
	}
}
