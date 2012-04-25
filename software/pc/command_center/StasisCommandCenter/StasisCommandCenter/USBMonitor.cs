using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.NetMicroFramework.Tools.MFDeployTool.Engine;

namespace StasisCommandCenter
{
	public class USBMonitor
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
		/// 
		/// </summary>
		public event MessageReceivedEventHandler MessageReceived = delegate { };

		public event DataLabelsReceivedEventHandler DataLabelsReceived = delegate { };

		public event DataReceivedEventHandler DataReceived = delegate { };

		/// <summary>
		/// Device we are monitoring
		/// </summary>
		private MFDevice _device = null;

		/// <summary>
		/// Connects to the device at the specified port
		/// </summary>
		/// <param name="port"></param>
		public void Connect(MFPortDefinition port)
		{
			this._device = new MFDeploy().Connect(port);
			this._device.OnDebugText += new EventHandler<DebugOutputEventArgs>(Device_OnDebugText);

			this.Connected(this, EventArgs.Empty);
		}

		public void Disconnect()
		{
			this._device.Dispose();

			this.Disconnected(this, EventArgs.Empty);
		}

		/// <summary>
		/// Debugging text received. See if it's data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Device_OnDebugText(object sender, DebugOutputEventArgs e)
		{
			if (e.Text.StartsWith("DATAL"))
			{
				this.DataLabelsReceived(this, new DataLabelsReceivedEventArgs(e.Text));
			}
			else if (e.Text.StartsWith("DATA"))
			{
				this.DataReceived(this, new DataReceivedEventArgs(e.Text));
			}
			else
			{
				this.MessageReceived(this, new MessageReceivedEventArgs(e.Text));
			}
		}

		public delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);

		/// <summary>
		/// Debug message event data
		/// </summary>
		public class DataReceivedEventArgs
		{
			/// <summary>
			/// Gets the text in the message from debug events
			/// </summary>
			public string RawText
			{
				get;
				private set;
			}

			public DateTime Timestamp
			{
				get;
				private set;
			}

			public double[] Values
			{
				get;
				private set;
			}

			public DataReceivedEventArgs(string text)
			{
				this.RawText = text;
				var parts = text.Split(new char[] { ',' });
				this.Values = new double[parts.Length - 1];
				for (int i = 1; i < parts.Length; i++)
				{
					this.Values[i - 1] = double.Parse(parts[i]);
				}
				this.Timestamp = DateTime.Now;
			}
		}

		public delegate void DataLabelsReceivedEventHandler(object sender, DataLabelsReceivedEventArgs e);

		/// <summary>
		/// Debug message event data
		/// </summary>
		public class DataLabelsReceivedEventArgs
		{
			/// <summary>
			/// Gets the text in the message from debug events
			/// </summary>
			public string RawText
			{
				get;
				private set;
			}

			public string[] Labels
			{
				get;
				private set;
			}

			public DataLabelsReceivedEventArgs(string text)
			{
				this.RawText = text;
				var parts = text.Split(new char[] { ',' });
				this.Labels = new string[parts.Length - 1];
				for (int i = 1; i < parts.Length; i++)
				{
					this.Labels[i - 1] = parts[i];
				}
			}
		}

		public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

		/// <summary>
		/// Debug message event data
		/// </summary>
		public class MessageReceivedEventArgs
		{
			/// <summary>
			/// Gets the text in the message from debug events
			/// </summary>
			public string Text
			{
				get;
				private set;
			}

			public MessageReceivedEventArgs(string text)
			{
				this.Text = text;
			}
		}
	}
}
