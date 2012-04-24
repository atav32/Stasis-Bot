using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace Stasis.Software.Netduino.Extensions
{
	public static class SerialPortExtensions
	{
		public static byte ReadByte(this SerialPort port)
		{
			byte[] buffer = new byte[1];
			if (port.BytesToRead > 0 && port.Read(buffer, 0, 1) == 1)
			{
				return buffer[0];
			}
			else
			{
				return 0;
			}
		}

	}
}
