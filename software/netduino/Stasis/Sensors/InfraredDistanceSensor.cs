using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using Microsoft.SPOT.Hardware;
using Stasis.Software.Netduino.Extensions;

namespace Stasis.Software.Netduino
{
	public class InfraredDistanceSensor
	{
		/// <summary>
		/// Gets the distance in cm
		/// </summary>
		public double Distance
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the correction offset for this sensor
		/// </summary>
		public double Offset
		{
			get;
			set;
		}

		/// <summary>
		/// Analog pin where sensor is hooked up
		/// </summary>
		private AnalogInput sensorInput = null;

		/// <summary>
		/// Averaging filter for values 
		/// </summary>
		private MovingAverageFilter averagingFilter = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pin"></param>
		public InfraredDistanceSensor(Cpu.Pin pin, int averagingWindow = 10)
		{
			this.sensorInput = new AnalogInput(pin);
			this.averagingFilter = new MovingAverageFilter(averagingWindow);
		}

		/// <summary>
		/// Updates distance value for this ranger and returns the new value
		/// </summary>
		public double Update()
		{
			// Calculate voltage
			var voltage = (double)this.sensorInput.ReadVoltage();

			// Calculate distance using
			// http://tutorial.cytron.com.my/2011/08/10/project-7-%E2%80%93-analog-sensor-range-using-infrared-distance-sensor/
			this.averagingFilter.AddValue(1.0 / ((voltage - 0.1911) / 20.99));
			this.Distance = this.averagingFilter.Value + this.Offset;

			// Return new distance value
			return this.Distance;
		}
	}
}
