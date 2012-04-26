namespace StasisCommandCenter
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.loopSpeedLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolbar = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.ipAddressTextbox = new System.Windows.Forms.ToolStripTextBox();
			this.connectButton = new System.Windows.Forms.ToolStripButton();
			this.disconnectButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.netduinoSelect = new System.Windows.Forms.ToolStripComboBox();
			this.connectToUSBButton = new System.Windows.Forms.ToolStripButton();
			this.disconnectUSBButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.startLoggingButton = new System.Windows.Forms.ToolStripButton();
			this.stopLoggingButton = new System.Windows.Forms.ToolStripButton();
			this.leftPanel = new System.Windows.Forms.Panel();
			this.angularVelocityPIDControls = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.pid2SetPointValue = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.pid2DValue = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.pid2IValue = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.pid2PValue = new System.Windows.Forms.NumericUpDown();
			this.anglePIDControls = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pid1SetPointValue = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.pid1DValue = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.pid1IValue = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.pid1PValue = new System.Windows.Forms.NumericUpDown();
			this.panelRight = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.debugTextbox = new System.Windows.Forms.RichTextBox();
			this.scrollOnUpdateCheck = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pid3SetPointValue = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.pid3DValue = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.pid3IValue = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.pid3PValue = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.getPIDButton = new System.Windows.Forms.Button();
			this.setPIDButton = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.pid4SetPointValue = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.pid4DValue = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.pid4IValue = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.pid4PValue = new System.Windows.Forms.NumericUpDown();
			this.statusBar.SuspendLayout();
			this.toolbar.SuspendLayout();
			this.leftPanel.SuspendLayout();
			this.angularVelocityPIDControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid2SetPointValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2DValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2IValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2PValue)).BeginInit();
			this.anglePIDControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid1SetPointValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1DValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1IValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1PValue)).BeginInit();
			this.panelRight.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid3SetPointValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid3DValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid3IValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid3PValue)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid4SetPointValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid4DValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid4IValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid4PValue)).BeginInit();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.AutoSize = false;
			this.statusBar.BackColor = System.Drawing.Color.WhiteSmoke;
			this.statusBar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loopSpeedLabel});
			this.statusBar.Location = new System.Drawing.Point(0, 632);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(1284, 30);
			this.statusBar.TabIndex = 0;
			this.statusBar.Text = "statusStrip1";
			// 
			// loopSpeedLabel
			// 
			this.loopSpeedLabel.ForeColor = System.Drawing.Color.DimGray;
			this.loopSpeedLabel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
			this.loopSpeedLabel.Name = "loopSpeedLabel";
			this.loopSpeedLabel.Size = new System.Drawing.Size(75, 25);
			this.loopSpeedLabel.Text = "LOOP SPEED:";
			// 
			// toolbar
			// 
			this.toolbar.AutoSize = false;
			this.toolbar.BackColor = System.Drawing.Color.WhiteSmoke;
			this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ipAddressTextbox,
            this.connectButton,
            this.disconnectButton,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.netduinoSelect,
            this.connectToUSBButton,
            this.disconnectUSBButton,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.startLoggingButton,
            this.stopLoggingButton});
			this.toolbar.Location = new System.Drawing.Point(0, 0);
			this.toolbar.Name = "toolbar";
			this.toolbar.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
			this.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolbar.Size = new System.Drawing.Size(1284, 30);
			this.toolbar.TabIndex = 10;
			this.toolbar.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripLabel1.ForeColor = System.Drawing.Color.Gray;
			this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(48, 27);
			this.toolStripLabel1.Text = "WIFI LINK";
			// 
			// ipAddressTextbox
			// 
			this.ipAddressTextbox.Name = "ipAddressTextbox";
			this.ipAddressTextbox.Size = new System.Drawing.Size(100, 30);
			this.ipAddressTextbox.Text = "169.254.1.1";
			// 
			// connectButton
			// 
			this.connectButton.Image = ((System.Drawing.Image)(resources.GetObject("connectButton.Image")));
			this.connectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(72, 27);
			this.connectButton.Text = "Connect";
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// disconnectButton
			// 
			this.disconnectButton.Enabled = false;
			this.disconnectButton.Image = ((System.Drawing.Image)(resources.GetObject("disconnectButton.Image")));
			this.disconnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.disconnectButton.Name = "disconnectButton";
			this.disconnectButton.Size = new System.Drawing.Size(86, 27);
			this.disconnectButton.Text = "Disconnect";
			this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripLabel2.ForeColor = System.Drawing.Color.Gray;
			this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(46, 27);
			this.toolStripLabel2.Text = "USB LINK";
			// 
			// netduinoSelect
			// 
			this.netduinoSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.netduinoSelect.Name = "netduinoSelect";
			this.netduinoSelect.Size = new System.Drawing.Size(121, 30);
			// 
			// connectToUSBButton
			// 
			this.connectToUSBButton.Image = ((System.Drawing.Image)(resources.GetObject("connectToUSBButton.Image")));
			this.connectToUSBButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.connectToUSBButton.Name = "connectToUSBButton";
			this.connectToUSBButton.Size = new System.Drawing.Size(72, 27);
			this.connectToUSBButton.Text = "Connect";
			this.connectToUSBButton.Click += new System.EventHandler(this.connectToUSBButton_Click);
			// 
			// disconnectUSBButton
			// 
			this.disconnectUSBButton.Enabled = false;
			this.disconnectUSBButton.Image = ((System.Drawing.Image)(resources.GetObject("disconnectUSBButton.Image")));
			this.disconnectUSBButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.disconnectUSBButton.Name = "disconnectUSBButton";
			this.disconnectUSBButton.Size = new System.Drawing.Size(86, 27);
			this.disconnectUSBButton.Text = "Disconnect";
			this.disconnectUSBButton.Click += new System.EventHandler(this.disconnectUSBButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripLabel3.ForeColor = System.Drawing.Color.Gray;
			this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(45, 27);
			this.toolStripLabel3.Text = "LOGGING";
			// 
			// startLoggingButton
			// 
			this.startLoggingButton.Image = ((System.Drawing.Image)(resources.GetObject("startLoggingButton.Image")));
			this.startLoggingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.startLoggingButton.Name = "startLoggingButton";
			this.startLoggingButton.Size = new System.Drawing.Size(51, 27);
			this.startLoggingButton.Text = "Start";
			this.startLoggingButton.Click += new System.EventHandler(this.startLoggingButton_Click);
			// 
			// stopLoggingButton
			// 
			this.stopLoggingButton.Enabled = false;
			this.stopLoggingButton.Image = ((System.Drawing.Image)(resources.GetObject("stopLoggingButton.Image")));
			this.stopLoggingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopLoggingButton.Name = "stopLoggingButton";
			this.stopLoggingButton.Size = new System.Drawing.Size(51, 27);
			this.stopLoggingButton.Text = "Stop";
			this.stopLoggingButton.Click += new System.EventHandler(this.stopLoggingButton_Click);
			// 
			// leftPanel
			// 
			this.leftPanel.Controls.Add(this.getPIDButton);
			this.leftPanel.Controls.Add(this.groupBox2);
			this.leftPanel.Controls.Add(this.setPIDButton);
			this.leftPanel.Controls.Add(this.groupBox1);
			this.leftPanel.Controls.Add(this.angularVelocityPIDControls);
			this.leftPanel.Controls.Add(this.anglePIDControls);
			this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftPanel.Location = new System.Drawing.Point(0, 30);
			this.leftPanel.Name = "leftPanel";
			this.leftPanel.Padding = new System.Windows.Forms.Padding(10);
			this.leftPanel.Size = new System.Drawing.Size(375, 602);
			this.leftPanel.TabIndex = 11;
			// 
			// angularVelocityPIDControls
			// 
			this.angularVelocityPIDControls.Controls.Add(this.label5);
			this.angularVelocityPIDControls.Controls.Add(this.pid2SetPointValue);
			this.angularVelocityPIDControls.Controls.Add(this.label6);
			this.angularVelocityPIDControls.Controls.Add(this.pid2DValue);
			this.angularVelocityPIDControls.Controls.Add(this.label7);
			this.angularVelocityPIDControls.Controls.Add(this.pid2IValue);
			this.angularVelocityPIDControls.Controls.Add(this.label8);
			this.angularVelocityPIDControls.Controls.Add(this.pid2PValue);
			this.angularVelocityPIDControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.angularVelocityPIDControls.Enabled = false;
			this.angularVelocityPIDControls.Location = new System.Drawing.Point(10, 87);
			this.angularVelocityPIDControls.Name = "angularVelocityPIDControls";
			this.angularVelocityPIDControls.Size = new System.Drawing.Size(355, 76);
			this.angularVelocityPIDControls.TabIndex = 13;
			this.angularVelocityPIDControls.TabStop = false;
			this.angularVelocityPIDControls.Text = "Angular Velocity PID";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(264, 25);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "SETPOINT";
			// 
			// pid2SetPointValue
			// 
			this.pid2SetPointValue.DecimalPlaces = 4;
			this.pid2SetPointValue.Location = new System.Drawing.Point(267, 41);
			this.pid2SetPointValue.Name = "pid2SetPointValue";
			this.pid2SetPointValue.Size = new System.Drawing.Size(80, 25);
			this.pid2SetPointValue.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(178, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(15, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "D";
			// 
			// pid2DValue
			// 
			this.pid2DValue.DecimalPlaces = 4;
			this.pid2DValue.Location = new System.Drawing.Point(181, 41);
			this.pid2DValue.Name = "pid2DValue";
			this.pid2DValue.Size = new System.Drawing.Size(80, 25);
			this.pid2DValue.TabIndex = 4;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(92, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(10, 13);
			this.label7.TabIndex = 3;
			this.label7.Text = "I";
			// 
			// pid2IValue
			// 
			this.pid2IValue.DecimalPlaces = 4;
			this.pid2IValue.Location = new System.Drawing.Point(95, 41);
			this.pid2IValue.Name = "pid2IValue";
			this.pid2IValue.Size = new System.Drawing.Size(80, 25);
			this.pid2IValue.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(6, 25);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(14, 13);
			this.label8.TabIndex = 1;
			this.label8.Text = "P";
			// 
			// pid2PValue
			// 
			this.pid2PValue.DecimalPlaces = 4;
			this.pid2PValue.Location = new System.Drawing.Point(9, 41);
			this.pid2PValue.Name = "pid2PValue";
			this.pid2PValue.Size = new System.Drawing.Size(80, 25);
			this.pid2PValue.TabIndex = 0;
			// 
			// anglePIDControls
			// 
			this.anglePIDControls.Controls.Add(this.label4);
			this.anglePIDControls.Controls.Add(this.pid1SetPointValue);
			this.anglePIDControls.Controls.Add(this.label3);
			this.anglePIDControls.Controls.Add(this.pid1DValue);
			this.anglePIDControls.Controls.Add(this.label2);
			this.anglePIDControls.Controls.Add(this.pid1IValue);
			this.anglePIDControls.Controls.Add(this.label1);
			this.anglePIDControls.Controls.Add(this.pid1PValue);
			this.anglePIDControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.anglePIDControls.Enabled = false;
			this.anglePIDControls.Location = new System.Drawing.Point(10, 10);
			this.anglePIDControls.Name = "anglePIDControls";
			this.anglePIDControls.Size = new System.Drawing.Size(355, 77);
			this.anglePIDControls.TabIndex = 12;
			this.anglePIDControls.TabStop = false;
			this.anglePIDControls.Text = "Angle PID";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(264, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "SETPOINT";
			// 
			// pid1SetPointValue
			// 
			this.pid1SetPointValue.DecimalPlaces = 4;
			this.pid1SetPointValue.Location = new System.Drawing.Point(267, 41);
			this.pid1SetPointValue.Name = "pid1SetPointValue";
			this.pid1SetPointValue.Size = new System.Drawing.Size(80, 25);
			this.pid1SetPointValue.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(178, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(15, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "D";
			// 
			// pid1DValue
			// 
			this.pid1DValue.DecimalPlaces = 4;
			this.pid1DValue.Location = new System.Drawing.Point(181, 41);
			this.pid1DValue.Name = "pid1DValue";
			this.pid1DValue.Size = new System.Drawing.Size(80, 25);
			this.pid1DValue.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(92, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(10, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "I";
			// 
			// pid1IValue
			// 
			this.pid1IValue.DecimalPlaces = 4;
			this.pid1IValue.Location = new System.Drawing.Point(95, 41);
			this.pid1IValue.Name = "pid1IValue";
			this.pid1IValue.Size = new System.Drawing.Size(80, 25);
			this.pid1IValue.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(6, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "P";
			// 
			// pid1PValue
			// 
			this.pid1PValue.DecimalPlaces = 4;
			this.pid1PValue.Location = new System.Drawing.Point(9, 41);
			this.pid1PValue.Name = "pid1PValue";
			this.pid1PValue.Size = new System.Drawing.Size(80, 25);
			this.pid1PValue.TabIndex = 0;
			// 
			// panelRight
			// 
			this.panelRight.Controls.Add(this.groupBox3);
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelRight.Enabled = false;
			this.panelRight.Location = new System.Drawing.Point(375, 30);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(909, 602);
			this.panelRight.TabIndex = 12;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.debugTextbox);
			this.groupBox3.Controls.Add(this.scrollOnUpdateCheck);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(909, 602);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Debug";
			// 
			// debugTextbox
			// 
			this.debugTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.debugTextbox.DetectUrls = false;
			this.debugTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.debugTextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.debugTextbox.Location = new System.Drawing.Point(3, 21);
			this.debugTextbox.Name = "debugTextbox";
			this.debugTextbox.Size = new System.Drawing.Size(903, 561);
			this.debugTextbox.TabIndex = 0;
			this.debugTextbox.Text = "";
			// 
			// scrollOnUpdateCheck
			// 
			this.scrollOnUpdateCheck.AutoSize = true;
			this.scrollOnUpdateCheck.Checked = true;
			this.scrollOnUpdateCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.scrollOnUpdateCheck.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.scrollOnUpdateCheck.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scrollOnUpdateCheck.Location = new System.Drawing.Point(3, 582);
			this.scrollOnUpdateCheck.Name = "scrollOnUpdateCheck";
			this.scrollOnUpdateCheck.Size = new System.Drawing.Size(903, 17);
			this.scrollOnUpdateCheck.TabIndex = 1;
			this.scrollOnUpdateCheck.Text = "SCROLL ON UPDATE";
			this.scrollOnUpdateCheck.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.pid3SetPointValue);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.pid3DValue);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.pid3IValue);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.pid3PValue);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(10, 163);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(355, 78);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Displacement PID";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(264, 25);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(58, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "SETPOINT";
			// 
			// pid3SetPointValue
			// 
			this.pid3SetPointValue.DecimalPlaces = 4;
			this.pid3SetPointValue.Location = new System.Drawing.Point(267, 41);
			this.pid3SetPointValue.Name = "pid3SetPointValue";
			this.pid3SetPointValue.Size = new System.Drawing.Size(80, 25);
			this.pid3SetPointValue.TabIndex = 6;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(178, 25);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(15, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "D";
			// 
			// pid3DValue
			// 
			this.pid3DValue.DecimalPlaces = 4;
			this.pid3DValue.Location = new System.Drawing.Point(181, 41);
			this.pid3DValue.Name = "pid3DValue";
			this.pid3DValue.Size = new System.Drawing.Size(80, 25);
			this.pid3DValue.TabIndex = 4;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(92, 25);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(10, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "I";
			// 
			// pid3IValue
			// 
			this.pid3IValue.DecimalPlaces = 4;
			this.pid3IValue.Location = new System.Drawing.Point(95, 41);
			this.pid3IValue.Name = "pid3IValue";
			this.pid3IValue.Size = new System.Drawing.Size(80, 25);
			this.pid3IValue.TabIndex = 2;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(6, 25);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(14, 13);
			this.label12.TabIndex = 1;
			this.label12.Text = "P";
			// 
			// pid3PValue
			// 
			this.pid3PValue.DecimalPlaces = 4;
			this.pid3PValue.Location = new System.Drawing.Point(9, 41);
			this.pid3PValue.Name = "pid3PValue";
			this.pid3PValue.Size = new System.Drawing.Size(80, 25);
			this.pid3PValue.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.pid4SetPointValue);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.pid4DValue);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.pid4IValue);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.pid4PValue);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Enabled = false;
			this.groupBox2.Location = new System.Drawing.Point(10, 241);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(355, 79);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Velocity PID";
			// 
			// getPIDButton
			// 
			this.getPIDButton.Location = new System.Drawing.Point(10, 326);
			this.getPIDButton.Name = "getPIDButton";
			this.getPIDButton.Size = new System.Drawing.Size(89, 33);
			this.getPIDButton.TabIndex = 9;
			this.getPIDButton.Text = "Get";
			this.getPIDButton.UseVisualStyleBackColor = true;
			this.getPIDButton.Click += new System.EventHandler(this.getPIDButton_Click);
			// 
			// setPIDButton
			// 
			this.setPIDButton.Location = new System.Drawing.Point(105, 326);
			this.setPIDButton.Name = "setPIDButton";
			this.setPIDButton.Size = new System.Drawing.Size(260, 33);
			this.setPIDButton.TabIndex = 8;
			this.setPIDButton.Text = "Set";
			this.setPIDButton.UseVisualStyleBackColor = true;
			this.setPIDButton.Click += new System.EventHandler(this.setPIDButton_Click);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(264, 25);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(58, 13);
			this.label13.TabIndex = 7;
			this.label13.Text = "SETPOINT";
			// 
			// pid4SetPointValue
			// 
			this.pid4SetPointValue.DecimalPlaces = 4;
			this.pid4SetPointValue.Location = new System.Drawing.Point(267, 41);
			this.pid4SetPointValue.Name = "pid4SetPointValue";
			this.pid4SetPointValue.Size = new System.Drawing.Size(80, 25);
			this.pid4SetPointValue.TabIndex = 6;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(178, 25);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(15, 13);
			this.label14.TabIndex = 5;
			this.label14.Text = "D";
			// 
			// pid4DValue
			// 
			this.pid4DValue.DecimalPlaces = 4;
			this.pid4DValue.Location = new System.Drawing.Point(181, 41);
			this.pid4DValue.Name = "pid4DValue";
			this.pid4DValue.Size = new System.Drawing.Size(80, 25);
			this.pid4DValue.TabIndex = 4;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(92, 25);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(10, 13);
			this.label15.TabIndex = 3;
			this.label15.Text = "I";
			// 
			// pid4IValue
			// 
			this.pid4IValue.DecimalPlaces = 4;
			this.pid4IValue.Location = new System.Drawing.Point(95, 41);
			this.pid4IValue.Name = "pid4IValue";
			this.pid4IValue.Size = new System.Drawing.Size(80, 25);
			this.pid4IValue.TabIndex = 2;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(6, 25);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(14, 13);
			this.label16.TabIndex = 1;
			this.label16.Text = "P";
			// 
			// pid4PValue
			// 
			this.pid4PValue.DecimalPlaces = 4;
			this.pid4PValue.Location = new System.Drawing.Point(9, 41);
			this.pid4PValue.Name = "pid4PValue";
			this.pid4PValue.Size = new System.Drawing.Size(80, 25);
			this.pid4PValue.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1284, 662);
			this.Controls.Add(this.panelRight);
			this.Controls.Add(this.leftPanel);
			this.Controls.Add(this.toolbar);
			this.Controls.Add(this.statusBar);
			this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainWindow";
			this.Text = "Stasis Command Center";
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.toolbar.ResumeLayout(false);
			this.toolbar.PerformLayout();
			this.leftPanel.ResumeLayout(false);
			this.angularVelocityPIDControls.ResumeLayout(false);
			this.angularVelocityPIDControls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid2SetPointValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2DValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2IValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2PValue)).EndInit();
			this.anglePIDControls.ResumeLayout(false);
			this.anglePIDControls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid1SetPointValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1DValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1IValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1PValue)).EndInit();
			this.panelRight.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid3SetPointValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid3DValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid3IValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid3PValue)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid4SetPointValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid4DValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid4IValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid4PValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel loopSpeedLabel;
		private System.Windows.Forms.ToolStrip toolbar;
		private System.Windows.Forms.ToolStripTextBox ipAddressTextbox;
		private System.Windows.Forms.ToolStripButton connectButton;
		private System.Windows.Forms.ToolStripButton disconnectButton;
		private System.Windows.Forms.Panel leftPanel;
		private System.Windows.Forms.GroupBox angularVelocityPIDControls;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown pid2SetPointValue;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown pid2DValue;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown pid2IValue;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown pid2PValue;
		private System.Windows.Forms.GroupBox anglePIDControls;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown pid1SetPointValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown pid1DValue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown pid1IValue;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown pid1PValue;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripComboBox netduinoSelect;
		private System.Windows.Forms.ToolStripButton connectToUSBButton;
		private System.Windows.Forms.ToolStripButton disconnectUSBButton;
		private System.Windows.Forms.Panel panelRight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripButton startLoggingButton;
		private System.Windows.Forms.ToolStripButton stopLoggingButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button getPIDButton;
		private System.Windows.Forms.Button setPIDButton;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown pid4SetPointValue;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown pid4DValue;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown pid4IValue;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown pid4PValue;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown pid3SetPointValue;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown pid3DValue;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown pid3IValue;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown pid3PValue;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RichTextBox debugTextbox;
		private System.Windows.Forms.CheckBox scrollOnUpdateCheck;
	}
}

