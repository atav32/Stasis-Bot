using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;

namespace Stasis.Software.Netduino.Extensions
{
	public static class AnalogInputExtensions
	{
		public static double ReadVoltage(this AnalogInput input, double referenceVoltage = Environment.AnalogReferenceVoltage)
		{
			return (double)input.Read() * (referenceVoltage / 1023.0);
		}
	}
}
