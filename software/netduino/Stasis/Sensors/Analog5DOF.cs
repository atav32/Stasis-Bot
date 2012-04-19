using System;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;
using SecretLabs.NETMF.Hardware;
using Microsoft.SPOT.Hardware;
using Stasis.Software.Netduino.Utility;

namespace Stasis.Software.Netduino.Sensors
{
	/// <summary>
	/// Analog 5DOF combo from Sparkfun http://www.sparkfun.com/products/11072
	/// </summary>
	public class Analog5DOF
	{
		/// <summary>
		/// Gets the acceleration recorded by the module
		/// </summary>
		public Vector Acceleration
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the rotation rate from the gyro
		/// </summary>
		public Vector RotationRate
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the absolute orientation/tilt/rotation of the module 
		/// </summary>
		public Vector Orientation
		{
			get;
			private set;
		}

		/// <summary>
		/// Analog inputs for each input
		/// </summary>
		private AnalogInput _xAccelInput = null;
		private AnalogInput _yAccelInput = null;
		private AnalogInput _zAccelInput = null;
		private AnalogInput _xGyroInput = null;
		private AnalogInput _yGyroInput = null;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="xAccelPin"></param>
		/// <param name="yAccelPin"></param>
		/// <param name="zAccelPin"></param>
		/// <param name="xGyroPin"></param>
		/// <param name="yGyroPin"></param>
		public Analog5DOF(Cpu.Pin xAccelPin = Cpu.Pin.GPIO_NONE, 
						  Cpu.Pin yAccelPin = Cpu.Pin.GPIO_NONE, 
						  Cpu.Pin zAccelPin = Cpu.Pin.GPIO_NONE, 
						  Cpu.Pin xGyroPin = Cpu.Pin.GPIO_NONE, 
						  Cpu.Pin yGyroPin = Cpu.Pin.GPIO_NONE)
		{
			if (xAccelPin != Cpu.Pin.GPIO_NONE)
			{
				_xAccelInput = new AnalogInput(xAccelPin);
			}
			if (yAccelPin != Cpu.Pin.GPIO_NONE)
			{
				_yAccelInput = new AnalogInput(yAccelPin);
			}
			if (zAccelPin != Cpu.Pin.GPIO_NONE)
			{
				_zAccelInput = new AnalogInput(zAccelPin);
			}
			if (xGyroPin != Cpu.Pin.GPIO_NONE)
			{
				_xGyroInput = new AnalogInput(xGyroPin);
			}
			if (yGyroPin != Cpu.Pin.GPIO_NONE)
			{
				_yGyroInput = new AnalogInput(yGyroPin);
			}
		}

		/// <summary>
		/// Update sensor values
		/// </summary>
		public void Update()
		{
			if (this._xAccelInput != null)
			{
				this.Acceleration.X = this._xAccelInput.Read();
			}
			if (this._yAccelInput != null)
			{
				this.Acceleration.Y = this._yAccelInput.Read();
			}
			if (this._zAccelInput != null)
			{
				this.Acceleration.Z = this._zAccelInput.Read();
			}
			if (this._xGyroInput != null)
			{
				this.RotationRate.X = this._xGyroInput.Read();
			}
			if (this._yGyroInput != null)
			{
				this.RotationRate.Y = this._yGyroInput.Read();
			}
		}
	}
}
