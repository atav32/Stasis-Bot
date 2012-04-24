using System;
using Microsoft.SPOT;
using Stasis.Software.Netduino.Extensions;

namespace Stasis.Software.Netduino.Communication
{
	public partial class WiFiMonitor
	{

		public class Message
		{
			/// <summary>
			/// Gets the type of message received from the PC
			/// </summary>
			public MessageType Type
			{
				get;
				private set;
			}

			/// <summary>
			/// Gets the data in this message
			/// </summary>
			public byte[] Data
			{
				get;
				private set;
			}

			/// <summary>
			/// Gets the actual values from the message
			/// </summary>
			public double[] Values
			{
				get;
				private set;
			}

			/// <summary>
			/// Constructor #1
			/// </summary>
			/// <param name="type"></param>
			/// <param name="data"></param>
			public Message(MessageType type, double[] values)
			{
				this.Type = type;
				this.Values = values;
				this.Data = new byte[this.Values.Length * 2];
				int count = 0;
				for (int i = 0; i < this.Values.Length; i++)
				{
					int val = (int)System.Math.Round(values[i] * 1000);
					this.Data[count++] = val.GetLSB();
					this.Data[count++] = val.GetMSB();
				}
			}

			/// <summary>
			/// Constructor #2
			/// </summary>
			/// <param name="rawMessage"></param>
			/// <param name="rawMessageLength"></param>
			public Message(byte[] rawMessage, int rawMessageLength)
			{
				this.Type = (MessageType)rawMessage[2];
				if (rawMessageLength > 5)
				{
					// Has data
					this.Data = new byte[rawMessageLength - 5];
					for (int i = 0; i < rawMessageLength - 5; i++)
					{
						this.Data[i] = rawMessage[i + 3];
					}
					this.Values = new double[rawMessageLength / 2];
					int counter = 0;
					for (int i = 0; i < this.Values.Length; i++)
					{
						int lsb = this.Data[counter++];
						int msb = this.Data[counter++];
						this.Values[i] = lsb | (msb << 8);
					}
				}
			}
		}

		/// <summary>
		/// Types of messages
		/// </summary>
		public enum MessageType
		{
			ReportPID = 0x21,
			SetPID = 0x31,
			ReportLoopSpeed = 0x41,
		}

		/// <summary>
		/// Delegate for MessageReceived
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

		/// <summary>
		/// Event data for MessageRecevied
		/// </summary>
		public class MessageReceivedEventArgs
		{
			/// <summary>
			/// Gets the message that was received
			/// </summary>
			public Message Message
			{
				get;
				private set;
			}

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="message"></param>
			public MessageReceivedEventArgs(Message message)
			{
				this.Message = message;
			}

		}

	}
}
