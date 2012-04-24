using System;
using Microsoft.SPOT;
using System.IO.Ports;
using Stasis.Software.Netduino.Extensions;

namespace Stasis.Software.Netduino.Communication
{
	/// <summary>
	/// WiFi Monitor for control / data logging over WiFi to the PC
	/// </summary>
	public partial class WiFiMonitor
	{
		/// <summary>
		/// Event raised when a new message has been received from the PC
		/// </summary>
		public event MessageReceivedEventHandler MessageReceived = delegate { };

		/// <summary>
		/// Serial port used to communicate with the connected wiFly module
		/// </summary>
		private SerialPort _port = null;

		/// <summary>
		/// Running raw message buffer
		/// </summary>
		private byte[] rawMessage = new byte[64];

		/// <summary>
		/// Raw message length so far
		/// </summary>
		private int rawMessageLength = 0;

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
		/// Sends a message back to PC
		/// </summary>
		/// <param name="message"></param>
		public void SendMessage(Message message)
		{
			this._port.Write(new byte[] { 0xFF, 0xFE, (byte)message.Type }, 0, 3);
			this._port.Write(message.Data, 0, message.Data.Length);
			this._port.Write(new byte[] { 0xFF, 0xFE }, 0, 2);
		}

		/// <summary>
		/// Received some data from PC, see if we need to handle some commands
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			while (this._port.BytesToRead > 0)
			{
				byte b = this._port.ReadByte();

				if (this.rawMessageLength < 2)
				{
					if (b == 0xFF)
					{
						this.rawMessage[this.rawMessageLength++] = b;
					}
					else
					{
						this.rawMessageLength = 0;
					}
				}
				else
				{
					this.rawMessage[this.rawMessageLength++] = b;
					if (b == 0xFF && this.rawMessage[this.rawMessageLength - 2] == 0xFE)
					{
						// Received full message
						Message m = new Message(this.rawMessage, this.rawMessageLength);

						// Let people know 
						this.MessageReceived(this, new MessageReceivedEventArgs(m));

						// Reset message
						this.rawMessageLength = 0;
					}
				}
			}
		}
	}
}
