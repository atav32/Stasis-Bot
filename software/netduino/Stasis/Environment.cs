using System;
using Microsoft.SPOT;

namespace Stasis.Software.Netduino
{
	public static class Environment
	{
		/// <summary>
		/// Gets the current analog reference voltage setting
		/// </summary>
		public const double AnalogReferenceVoltage = 5.0;

		/// <summary>
		/// Gets the number of steps (resolution) over the ADC input range
		/// </summary>
		public const double ADCResolution = 1023.0; 

	}
}
