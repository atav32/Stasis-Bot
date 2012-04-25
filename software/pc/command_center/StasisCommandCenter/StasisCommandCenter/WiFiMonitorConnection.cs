using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace StasisCommandCenter
{
	public partial class WiFiMonitorConnection
	{
		/// <summary>
		/// 
		/// </summary>
		public event EventHandler Connected = delegate { };
		
		/// <summary>
		/// 
		/// </summary>
		public event EventHandler Disconnected = delegate { };

		/// <summary>
		/// Message receieved from the robot
		/// </summary>
		public event MessageReceivedEventHandler MessageReceived = delegate { };

		/// <summary>
		/// Connection to module
		/// </summary>
		private TcpClient connection = new TcpClient();

		/// <summary>
		/// Background read worker for the connection
		/// </summary>
		private BackgroundWorker worker;

		/// <summary>
		/// Constructor
		/// </summary>
		public WiFiMonitorConnection()
		{

		}

		/// <summary>
		/// Connect to the robot
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="port"></param>
		public void Connect(string ip, int port)
		{
			this.connection = new TcpClient();
			this.connection.Connect(ip, port);
			this.Connected(this, EventArgs.Empty);

			this.worker = new BackgroundWorker();
			this.worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerAsync();
		}

		/// <summary>
		/// Disconnect from the robot
		/// </summary>
		public void Disconnect()
		{
			this.worker.CancelAsync();
			this.connection.Close();
			this.Disconnected(this, EventArgs.Empty);
		}

		/// <summary>
		/// Sends a message back to robot
		/// </summary>
		/// <param name="message"></param>
		public void SendMessage(Message message)
		{
			string output = message.Type + ",";
			foreach (var val in message.Values)
			{
				output += val.ToString("N3") + ",";
			}
			output += "\r\n";
			byte[] outBytes = Encoding.UTF8.GetBytes(output);
			this.connection.GetStream().Write(outBytes, 0, outBytes.Length);
			this.connection.GetStream().Flush();
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			byte[] rawMessage = new byte[1024];
			int rawMessageLength = 0;
			while (this.connection.Connected && this.worker.CancellationPending == false)
			{
				if (this.connection.Available > 0)
				{
					byte b = (byte)this.connection.GetStream().ReadByte();

					rawMessage[rawMessageLength++] = b;
					if (b == 0x0A && rawMessage[rawMessageLength - 2] == 0x0D)
					{
						// Got a full message
						var m = new Message(rawMessage, rawMessageLength);

						// Let people know
						this.MessageReceived(this, new MessageReceivedEventArgs(m));

						// Reset message
						rawMessageLength = 0;
					}
				}
			}
		}
	}
}
