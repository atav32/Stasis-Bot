using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

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
		public InfraredDistanceSensor(Cpu.Pin pin, int averagingWindow = 30)
		{
			this.sensorInput = new AnalogInput(pin);
			this.averagingFilter = new MovingAverageFilter(averagingWindow);
		}

		/// <summary>
		/// Updates distance value for this ranger and returns the new value
		/// </summary>
		public double Update()
		{
			this.averagingFilter.AddValue(this.sensorInput.Read());
			this.Distance = (this.averagingFilter.Value / 1023.0) * 5.0;

			return this.Distance;
		}
	}
}
