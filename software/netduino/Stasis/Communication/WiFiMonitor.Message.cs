using System;
using Microsoft.SPOT;
using Stasis.Software.Netduino.Extensions;
using System.Text;

namespace Stasis.Software.Netduino.Communication
{
	public partial class WiFiMonitor
	{

		public class Message
		{
			/// <summary>
			/// Gets the type of message received from the PC
			/// </summary>
			public string Type
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
			public Message(string type, double[] values)
			{
				this.Type = type;
				this.Values = values;
			}

			/// <summary>
			/// Constructor #2
			/// </summary>
			/// <param name="rawMessage"></param>
			/// <param name="rawMessageLength"></param>
			public Message(byte[] rawMessage, int rawMessageLength)
			{
				try
				{
					byte[] rawMessageRealSize = new byte[rawMessageLength];
					for (int i = 0; i < rawMessageLength; i++)
					{
						rawMessageRealSize[i] = rawMessage[i];
					}
					string s = new String(Encoding.UTF8.GetChars(rawMessageRealSize));
					var parts = s.Split(new char[] { ',' });
					this.Type = parts[0];
					if (this.Type.IndexOf('*') != -1)
					{
						var typeParts = this.Type.Split(new char[] { '*' });
						this.Type = typeParts[typeParts.Length - 1];
					}
					this.Values = new double[parts.Length - 2];
					for (int i = 1; i < parts.Length - 1; i++)
					{
						this.Values[i - 1] = double.Parse(parts[i]);
					}
				}
				catch (Exception ex)
				{
					this.Type = MessageType.Invalid;
				}
			}
		}

		/// <summary>
		/// Types of messages
		/// </summary>
		public class MessageType
		{
			public const string Invalid = "INVALID";
			public const string SetPID = "SPID";
			public const string GetPID = "GPID";
			public const string GetLoopSpeed = "GLS";
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
