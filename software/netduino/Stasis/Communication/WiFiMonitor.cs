using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace Stasis.Software.Netduino.Communication
{
	/// <summary>
	/// WiFi Monitor for control / data logging over WiFi to the PC
	/// </summary>
	public class WiFiMonitor
	{

		/// <summary>
		/// Serial port used to communicate with the connected wiFly module
		/// </summary>
		private SerialPort _port = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="comPort"></param>
		/// <param name="baudRate"></param>
		public WiFiMonitor(string comPort, int baudRate)
		{
			this._port = new SerialPort(comPort, baudRate);
			this._port.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
			this._port.Open();
		}

		/// <summary>
		/// Received some data from PC, see if we need to handle some commands
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			
		}
	}
}
