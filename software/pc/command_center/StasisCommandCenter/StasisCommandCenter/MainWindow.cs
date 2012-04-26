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
			this.pid1SetPointValue.DataBindings.Add("Value", Properties.Settings.Default, "PID1_SetPoint", false, DataSourceUpdateMode.OnPropertyChanged);

			this.pid2PValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_P", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2IValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_I", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2DValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_D", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid2SetPointValue.DataBindings.Add("Value", Properties.Settings.Default, "PID2_SetPoint", false, DataSourceUpdateMode.OnPropertyChanged);

			this.pid3PValue.DataBindings.Add("Value", Properties.Settings.Default, "PID3_P", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid3IValue.DataBindings.Add("Value", Properties.Settings.Default, "PID3_I", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid3DValue.DataBindings.Add("Value", Properties.Settings.Default, "PID3_D", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid3SetPointValue.DataBindings.Add("Value", Properties.Settings.Default, "PID3_SetPoint", false, DataSourceUpdateMode.OnPropertyChanged);

			this.pid4PValue.DataBindings.Add("Value", Properties.Settings.Default, "PID4_P", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid4IValue.DataBindings.Add("Value", Properties.Settings.Default, "PID4_I", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid4DValue.DataBindings.Add("Value", Properties.Settings.Default, "PID4_D", false, DataSourceUpdateMode.OnPropertyChanged);
			this.pid4SetPointValue.DataBindings.Add("Value", Properties.Settings.Default, "PID4_SetPoint", false, DataSourceUpdateMode.OnPropertyChanged);

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
					this.pid1PValue.Value = (decimal)msg.Values[0];
					this.pid1IValue.Value = (decimal)msg.Values[1];
					this.pid1DValue.Value = (decimal)msg.Values[2];
					this.pid1SetPointValue.Value = (decimal)msg.Values[3];

					this.pid2PValue.Value = (decimal)msg.Values[4];
					this.pid2IValue.Value = (decimal)msg.Values[5];
					this.pid2DValue.Value = (decimal)msg.Values[6];
					this.pid2SetPointValue.Value = (decimal)msg.Values[7];

					this.pid3PValue.Value = (decimal)msg.Values[8];
					this.pid3IValue.Value = (decimal)msg.Values[9];
					this.pid3DValue.Value = (decimal)msg.Values[10];
					this.pid3SetPointValue.Value = (decimal)msg.Values[11];

					this.pid4PValue.Value = (decimal)msg.Values[12];
					this.pid4IValue.Value = (decimal)msg.Values[13];
					this.pid4DValue.Value = (decimal)msg.Values[14];
					this.pid4SetPointValue.Value = (decimal)msg.Values[15];

					this.pid1PValue.BackColor = this.pid1IValue.BackColor = this.pid1DValue.BackColor = this.pid1SetPointValue.BackColor = Color.YellowGreen;
					this.pid2PValue.BackColor = this.pid2IValue.BackColor = this.pid2DValue.BackColor = this.pid2SetPointValue.BackColor = Color.YellowGreen;
					this.pid3PValue.BackColor = this.pid3IValue.BackColor = this.pid3DValue.BackColor = this.pid3SetPointValue.BackColor = Color.YellowGreen;
					this.pid4PValue.BackColor = this.pid4IValue.BackColor = this.pid4DValue.BackColor = this.pid4SetPointValue.BackColor = Color.YellowGreen;
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
			this.leftPanel.Enabled = false;
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
			this.leftPanel.Enabled = true;
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

		private void setPIDButton_Click(object sender, EventArgs e)
		{
			double p1 = (double)this.pid1PValue.Value;
			double i1 = (double)this.pid1IValue.Value;
			double d1 = (double)this.pid1DValue.Value;
			double s1 = (double)this.pid1SetPointValue.Value;
			
			double p2 = (double)this.pid2PValue.Value;
			double i2 = (double)this.pid2IValue.Value;
			double d2 = (double)this.pid2DValue.Value;
			double s2 = (double)this.pid2SetPointValue.Value;
			
			double p3 = (double)this.pid3PValue.Value;
			double i3 = (double)this.pid3IValue.Value;
			double d3 = (double)this.pid3DValue.Value;
			double s3 = (double)this.pid3SetPointValue.Value;
			
			double p4 = (double)this.pid4PValue.Value;
			double i4 = (double)this.pid4IValue.Value;
			double d4 = (double)this.pid4DValue.Value;
			double s4 = (double)this.pid4SetPointValue.Value;

			this.wifiMonitor.SendMessage(new WiFiMonitorConnection.Message(WiFiMonitorConnection.MessageType.SetPID, new double[] { p1, i1, d1, s1, p2, i2, d2, s2, p3, i3, d3, s3, p4, i4, d4, s4 }));
			this.pid1PValue.BackColor = this.pid1IValue.BackColor = this.pid1DValue.BackColor = this.pid1SetPointValue.BackColor = Color.OrangeRed;
			this.pid2PValue.BackColor = this.pid2IValue.BackColor = this.pid2DValue.BackColor = this.pid2SetPointValue.BackColor = Color.OrangeRed;
			this.pid3PValue.BackColor = this.pid3IValue.BackColor = this.pid3DValue.BackColor = this.pid3SetPointValue.BackColor = Color.OrangeRed;
			this.pid4PValue.BackColor = this.pid4IValue.BackColor = this.pid4DValue.BackColor = this.pid4SetPointValue.BackColor = Color.OrangeRed;
		}

		private void getPIDButton_Click(object sender, EventArgs e)
		{

		}
	}
}
