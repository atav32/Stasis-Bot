using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StasisCommandCenter
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// Wifi Monitor for the Robot
		/// </summary>
		private WiFiMonitorConnection monitor = new WiFiMonitorConnection();

		/// <summary>
		/// Constructor
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			// Listen to monitor
			this.monitor.Connected += new EventHandler(Monitor_Connected);
			this.monitor.Disconnected += new EventHandler(Monitor_Disconnected);
			this.monitor.MessageReceived += new WiFiMonitorConnection.MessageReceivedEventHandler(Monitor_MessageReceived);

			this.pid1PValue.DataBindings.Add("Value", Properties.Settings.Default, "PID1_P", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid1IValue.DataBindings.Add("Value", Properties.Settings.Default, "PID1_I", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid1DValue.DataBindings.Add("Value", Properties.Settings.Default, "PID1_D", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid1GainValue.DataBindings.Add("Value", Properties.Settings.Default, "PID1_Gain", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid1SetPointValue.DataBindings.Add("Value", Properties.Settings.Default, "PID1_SetPoint", false, DataSourceUpdateMode.OnPropertyChanged);

			this.pid2PValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_P", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2IValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_I", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2DValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_D", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2GainValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_Gain", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2SetPointValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_SetPoint", false, DataSourceUpdateMode.OnPropertyChanged);

			Properties.Settings.Default.PropertyChanged += delegate
			{
				Properties.Settings.Default.Save();
			};
		}

		private void HandleReportLoopSpeed(WiFiMonitorConnection.Message msg)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate
				{
					this.loopSpeedLabel.Text = msg.Values[0].ToString();
				}));
			}
		}

		private void HandleReportPIDValues(WiFiMonitorConnection.Message msg)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate
				{
					if (msg.Values[0] == 1)
					{
						this.pid1PValue.Value = (decimal)msg.Values[1];
						this.pid1IValue.Value = (decimal)msg.Values[2];
						this.pid1DValue.Value = (decimal)msg.Values[3];
						this.pid1SetPointValue.Value = (decimal)msg.Values[4];
						this.pid1GainValue.Value = (decimal)msg.Values[5];
						this.pid1PValue.BackColor = this.pid1IValue.BackColor = this.pid1DValue.BackColor = this.pid1SetPointValue.BackColor = this.pid1GainValue.BackColor = Color.YellowGreen;
					}
					else if (msg.Values[0] == 2)
					{
						this.pid2PValue.Value = (decimal)msg.Values[1];
						this.pid2IValue.Value = (decimal)msg.Values[2];
						this.pid2DValue.Value = (decimal)msg.Values[3];
						this.pid2SetPointValue.Value = (decimal)msg.Values[4];
						this.pid2GainValue.Value = (decimal)msg.Values[5];
						this.pid2PValue.BackColor = this.pid2IValue.BackColor = this.pid2DValue.BackColor = this.pid2SetPointValue.BackColor = this.pid2GainValue.BackColor = Color.YellowGreen;
					}
				}));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Monitor_MessageReceived(object sender, WiFiMonitorConnection.MessageReceivedEventArgs e)
		{
			if (e.Message.Type == WiFiMonitorConnection.MessageType.GetLoopSpeed)
			{
				this.HandleReportLoopSpeed(e.Message);
			}
			else if (e.Message.Type == WiFiMonitorConnection.MessageType.GetPID)
			{
				this.HandleReportPIDValues(e.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Monitor_Disconnected(object sender, EventArgs e)
		{
			this.ipAddressTextbox.Enabled = this.connectButton.Enabled = true;
			this.disconnectButton.Enabled = false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Monitor_Connected(object sender, EventArgs e)
		{
			this.ipAddressTextbox.Enabled = this.connectButton.Enabled = false;
			this.disconnectButton.Enabled = true;

		}

		/// <summary>
		/// Connect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			this.monitor.Connect(ipAddressTextbox.Text, 2000);
		}

		/// <summary>
		/// Disconnect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void disconnectButton_Click(object sender, EventArgs e)
		{
			this.monitor.Disconnect();
		}

		/// <summary>
		/// Set PID values for the main PID controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setPID1Button_Click(object sender, EventArgs e)
		{
			double p = (double)this.pid1PValue.Value;
			double i = (double)this.pid1IValue.Value;
			double d = (double)this.pid1DValue.Value;
			double s = (double)this.pid1SetPointValue.Value;
			double g = (double)this.pid1GainValue.Value;
			this.monitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.SetPID, new double[] { 1, p, i, d, s, g }));
			this.pid1PValue.BackColor = this.pid1IValue.BackColor = this.pid1DValue.BackColor = this.pid1SetPointValue.BackColor = this.pid1GainValue.BackColor = Color.OrangeRed;
		}

		/// <summary>
		/// Gets PID values for the main PID controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getPID1Button_Click(object sender, EventArgs e)
		{
			this.monitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.GetPID, new double[] { 1 }));
		}

		/// <summary>
		/// Set PID values for the delta Angle PID controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setPID2Button_Click(object sender, EventArgs e)
		{
			double p = (double)this.pid2PValue.Value;
			double i = (double)this.pid2IValue.Value;
			double d = (double)this.pid2DValue.Value;
			double s = (double)this.pid2SetPointValue.Value;
			double g = (double)this.pid2GainValue.Value;
			this.monitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.SetPID, new double[] { 2, p, i, d, s, g }));
			this.pid2PValue.BackColor = this.pid2IValue.BackColor = this.pid2DValue.BackColor = this.pid2SetPointValue.BackColor = this.pid2GainValue.BackColor = Color.OrangeRed;
		}

		/// <summary>
		/// Gets PID values for the delta Angle PID controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getPID2Button_Click(object sender, EventArgs e)
		{
			this.monitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.GetPID, new double[] { 2 }));
		}
	}
}
