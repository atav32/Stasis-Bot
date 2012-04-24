using System;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;
using SecretLabs.NETMF.Hardware;
using Microsoft.SPOT.Hardware;
using Stasis.Software.Netduino.Utility;
using Stasis.Software.Netduino.Extensions;

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
		/// Supply voltage to electronics on the board in mV
		/// </summary>
		private const double _supplyVoltage = 2800;

		/// <summary>
		/// Zero G voltage output from the accelerometer in mV
		/// </summary>
		private const double _accelerometerZeroGVoltage = Analog5DOF._supplyVoltage / 2.0;

		/// <summary>
		/// Resolution of the accelerometer in mV/G (Accelerometer is +-3G)
		/// </summary>
		private const double _accelerometerResolution = (Analog5DOF._accelerometerZeroGVoltage / 3.0);

		/// <summary>
		/// Voltage output of the gyro when rotation rate = 0 deg/s, in mV
		/// </summary>
		private const double _gyroZeroRateVoltage = 1350.0;

		/// <summary>
		/// Resolution of the gyro in mV/deg/s
		/// </summary>s
		private const double _gyroResolution = 2.0;


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
			this.Acceleration = new Vector(0, 0, 0);
			this.RotationRate = new Vector(0, 0, 0);

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
				this.Acceleration.X = this.CalculateAcceleration(this._xAccelInput.ReadVoltage());
			}
			if (this._yAccelInput != null)
			{
				this.Acceleration.Y = this.CalculateAcceleration(this._yAccelInput.ReadVoltage());
			}
			if (this._zAccelInput != null)
			{
				this.Acceleration.Z = this.CalculateAcceleration(this._zAccelInput.ReadVoltage());
			}
			if (this._xGyroInput != null)
			{
				this.RotationRate.X = this.CalculateRotationRate(this._xGyroInput.ReadVoltage());
			}
			if (this._yGyroInput != null)
			{
				this.RotationRate.Y = this.CalculateRotationRate(this._yGyroInput.ReadVoltage());
			}
		}

		/// <summary>
		/// Calculates the current rotation rate for an axis when specified the 
		/// ADC reading from the output for that axis.
		/// </summary>
		/// <param name="adcVoltage"></param>
		/// <returns></returns>
		private double CalculateRotationRate(double adcVoltage)
		{
			var mV = (adcVoltage * 1000.0) - Analog5DOF._gyroZeroRateVoltage;
			return mV / Analog5DOF._gyroResolution;
		}

		/// <summary>
		/// Calculates the current acceleration for an axis when specified the 
		/// ADC reading from the output for that axis.
		/// </summary>
		/// <param name="adcVoltage"></param>
		/// <returns></returns>
		private double CalculateAcceleration(double adcVoltage)
		{
			var mV = (adcVoltage * 1000.0) - Analog5DOF._accelerometerZeroGVoltage;
			return mV / Analog5DOF._accelerometerResolution;
		}
	}
}
