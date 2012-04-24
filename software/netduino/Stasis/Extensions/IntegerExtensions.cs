using System;
using Microsoft.SPOT;

namespace Stasis.Software.Netduino.Extensions
{
	public static class IntegerExtensions
	{
		public static byte GetLSB(this int value)
		{
			return (byte)(value & 0xFF);
		}

		public static byte GetMSB(this int value)
		{
			return (byte)((value >> 8) & 0xFF);
		}

		public static byte[] GetBytes(this int value)
		{
			return new byte[] { value.GetLSB(), value.GetMSB() };
		}
	}
}
