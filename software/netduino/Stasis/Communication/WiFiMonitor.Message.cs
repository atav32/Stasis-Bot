using System;
using Microsoft.SPOT;

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
			/// Constructor #1
			/// </summary>
			/// <param name="type"></param>
			/// <param name="data"></param>
			public Message(MessageType type, byte[] data)
			{
				this.Type = type;
				this.Data = data;
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
