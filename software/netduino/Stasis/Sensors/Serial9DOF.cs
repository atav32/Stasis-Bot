using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace Stasis.Software.Netduino
{
	public class Serial9DOF
	{
		/// <summary>
		/// Accelerometer value in the X-axis
		/// </summary>
		public int XAccel
		{
			get;
			private set;
		}

		/// <summary>
		/// Accelerometer value in the Y-axis
		/// </summary>
		public int YAccel
		{
			get;
			private set;
		}

		/// <summary>
		/// Accelerometer value in the Z-axis
		/// </summary>
		public int ZAccel
		{
			get;
			private set;
		}

		/// <summary>
		/// Serial port to communicate with the 9DOF board
		/// </summary>
		private SerialPort serialPort;

		/// <summary>
		/// Sync status. Done synching if == 2
		/// </summary>
		private int syncStatus = 0;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="port"></param>
		/// <param name="baud"></param>
		public Serial9DOF(string port, int baud)
		{
			this.serialPort = new SerialPort(port, baud);
			this.serialPort.Open();
			//this.serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
		}

		/// <summary>
		/// Update values from the 9DOF board
		/// </summary>
		public void Update()
		{
			byte[] data = new byte[64];
			int bytesRead = 0;

			// Write to the board so ti replies
			this.serialPort.Write(new byte[] { 0xE0 }, 0, 1);
			this.serialPort.Flush();

			
			// Wait for reply
			while (bytesRead < 14)
			{
				bytesRead += this.serialPort.Read(data, bytesRead, 14);
			}

			// Check packet
			bool validPacket = data[12] == 0xFF && data[13] == 0xFF;
			if (validPacket)
			{
				this.XAccel = (int)data[0] | ((int)data[1] << 8);
				this.YAccel = (int)data[2] | ((int)data[3] << 8);
				this.ZAccel = (int)data[4] | ((int)data[5] << 8);
				//Debug.Print(this.XAccel + ":" + this.YAccel + ":" + this.ZAccel);
			}
			//*/
		}

		/// <summary>
		/// Handle data received
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			//Debug.Print(".");
		}

	}
}
