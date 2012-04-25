using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.NetMicroFramework.Tools.MFDeployTool.Engine;
using System.Diagnostics;
using System.IO;

namespace StasisCommandCenter
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// Wifi Monitor for the Robot
		/// </summary>
		private WiFiMonitorConnection wifiMonitor = new WiFiMonitorConnection();

		/// <summary>
		/// USB monitor for live data from the robot
		/// </summary>
		private USBMonitor usbMonitor = new USBMonitor();

		/// <summary>
		/// 
		/// </summary>
		private MFDeploy mfDeployEngine = new MFDeploy();

		/// <summary>
		/// Writer for the current logging session
		/// </summary>
		private StreamWriter logWriter = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			this.mfDeployEngine.OnDeviceListUpdate += new EventHandler<EventArgs>(MFDeployEngine_OnDeviceListUpdate);
			this.UpdateNetduinoList();

			// Listen to monitors
			this.wifiMonitor.Connected += new EventHandler(Monitor_Connected);
			this.wifiMonitor.Disconnected += new EventHandler(Monitor_Disconnected);
			this.wifiMonitor.MessageReceived += new WiFiMonitorConnection.MessageReceivedEventHandler(Monitor_MessageReceived);

			this.usbMonitor.Connected += new EventHandler(USBMonitor_Connected);
			this.usbMonitor.Disconnected += new EventHandler(USBMonitor_Disconnected);
			this.usbMonitor.MessageReceived += new USBMonitor.MessageReceivedEventHandler(USBMonitor_MessageReceived);
			this.usbMonitor.DataReceived += new USBMonitor.DataReceivedEventHandler(USBMonitor_DataReceived);
			this.usbMonitor.DataLabelsReceived += new USBMonitor.DataLabelsReceivedEventHandler(USBMonitor_DataLabelsReceived);

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

		void USBMonitor_DataLabelsReceived(object sender, USBMonitor.DataLabelsReceivedEventArgs e)
		{
			
		}

		private void USBMonitor_DataReceived(object sender, USBMonitor.DataReceivedEventArgs e)
		{
			if (this.logWriter != null)
			{
				string output = e.Timestamp.Ticks.ToString();
				foreach (var value in e.Values)
				{
					output += "," + value;
				}
				this.logWriter.WriteLine(output);
			}
		}

		private void MFDeployEngine_OnDeviceListUpdate(object sender, EventArgs e)
		{
			this.UpdateNetduinoList();
		}

		private void UpdateNetduinoList()
		{
			this.netduinoSelect.Items.Clear();
			foreach (var obj in this.mfDeployEngine.DeviceList)
			{
				MFPortDefinition port = (MFPortDefinition)obj;
				if (port.Name.Contains("Netduino"))
				{
					this.netduinoSelect.Items.Add(port);
				}
			}

			if (this.netduinoSelect.Items.Count > 0)
			{
				this.netduinoSelect.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void USBMonitor_MessageReceived(object sender, USBMonitor.MessageReceivedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate
				{
					this.debugTextbox.Text += e.Text;
					if (this.scrollOnUpdateCheck.Checked)
					{
						this.debugTextbox.SelectionStart = this.debugTextbox.TextLength;
						this.debugTextbox.ScrollToCaret();
					}
				}));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void USBMonitor_Disconnected(object sender, EventArgs e)
		{
			this.panelRight.Enabled = false;
			this.connectToUSBButton.Enabled = this.netduinoSelect.Enabled = true;
			this.disconnectUSBButton.Enabled = false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void USBMonitor_Connected(object sender, EventArgs e)
		{
			this.panelRight.Enabled = true;
			this.connectToUSBButton.Enabled = this.netduinoSelect.Enabled = false;
			this.disconnectUSBButton.Enabled = true;
		}

		private void HandleReportLoopSpeed(WiFiMonitorConnection.Message msg)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate
				{
					this.loopSpeedLabel.Text = "LOOP SPEED: " + msg.Values[0].ToString();
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
			this.anglePIDControls.Enabled = this.angularVelocityPIDControls.Enabled = false;
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
			this.anglePIDControls.Enabled = this.angularVelocityPIDControls.Enabled = true;
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
			this.wifiMonitor.Connect(ipAddressTextbox.Text, 2000);
		}

		/// <summary>
		/// Disconnect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void disconnectButton_Click(object sender, EventArgs e)
		{
			this.wifiMonitor.Disconnect();
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
			this.wifiMonitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.SetPID, new double[] { 1, p, i, d, s, g }));
			this.pid1PValue.BackColor = this.pid1IValue.BackColor = this.pid1DValue.BackColor = this.pid1SetPointValue.BackColor = this.pid1GainValue.BackColor = Color.OrangeRed;
		}

		/// <summary>
		/// Gets PID values for the main PID controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getPID1Button_Click(object sender, EventArgs e)
		{
			this.wifiMonitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.GetPID, new double[] { 1 }));
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
			this.wifiMonitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.SetPID, new double[] { 2, p, i, d, s, g }));
			this.pid2PValue.BackColor = this.pid2IValue.BackColor = this.pid2DValue.BackColor = this.pid2SetPointValue.BackColor = this.pid2GainValue.BackColor = Color.OrangeRed;
		}

		/// <summary>
		/// Gets PID values for the delta Angle PID controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getPID2Button_Click(object sender, EventArgs e)
		{
			this.wifiMonitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.GetPID, new double[] { 2 }));
		}

		private void connectToUSBButton_Click(object sender, EventArgs e)
		{
			this.usbMonitor.Connect(this.netduinoSelect.SelectedItem as MFPortDefinition);
		}

		private void disconnectUSBButton_Click(object sender, EventArgs e)
		{
			this.usbMonitor.Disconnect();
		}

		private void startLoggingButton_Click(object sender, EventArgs e)
		{
			var file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\StasisLogs\\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

			if (Directory.Exists(Path.GetDirectoryName(file)) == false)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(file));
			}

			this.logWriter = new StreamWriter(File.OpenWrite(file));
			this.logWriter.AutoFlush = true;

			this.startLoggingButton.Enabled = false;
			this.stopLoggingButton.Enabled = true;
		}

		private void stopLoggingButton_Click(object sender, EventArgs e)
		{
			this.logWriter.Close();

			this.startLoggingButton.Enabled = true;
			this.stopLoggingButton.Enabled = false;

			this.logWriter = null;
		}
	}
}
