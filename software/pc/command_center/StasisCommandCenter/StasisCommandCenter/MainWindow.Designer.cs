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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.getPID1Button = new System.Windows.Forms.Button();
			this.setPID1Button = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.pid1SetPointValue = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.pid1DValue = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.pid1IValue = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.pid1PValue = new System.Windows.Forms.NumericUpDown();
			this.toolbar = new System.Windows.Forms.ToolStrip();
			this.ipAddressTextbox = new System.Windows.Forms.ToolStripTextBox();
			this.connectButton = new System.Windows.Forms.ToolStripButton();
			this.disconnectButton = new System.Windows.Forms.ToolStripButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.getPID2Button = new System.Windows.Forms.Button();
			this.setPID2Button = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.pid2SetPointValue = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.pid2DValue = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.pid2IValue = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.pid2PValue = new System.Windows.Forms.NumericUpDown();
			this.pid1GainValue = new System.Windows.Forms.NumericUpDown();
			this.pid2GainValue = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.statusBar.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid1SetPointValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1DValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1IValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1PValue)).BeginInit();
			this.toolbar.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid2SetPointValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2DValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2IValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2PValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1GainValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2GainValue)).BeginInit();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.AutoSize = false;
			this.statusBar.BackColor = System.Drawing.Color.WhiteSmoke;
			this.statusBar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loopSpeedLabel});
			this.statusBar.Location = new System.Drawing.Point(0, 348);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(464, 30);
			this.statusBar.TabIndex = 0;
			this.statusBar.Text = "statusStrip1";
			// 
			// loopSpeedLabel
			// 
			this.loopSpeedLabel.ForeColor = System.Drawing.Color.DimGray;
			this.loopSpeedLabel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
			this.loopSpeedLabel.Name = "loopSpeedLabel";
			this.loopSpeedLabel.Size = new System.Drawing.Size(118, 25);
			this.loopSpeedLabel.Text = "toolStripStatusLabel1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.pid1GainValue);
			this.groupBox1.Controls.Add(this.getPID1Button);
			this.groupBox1.Controls.Add(this.setPID1Button);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.pid1SetPointValue);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.pid1DValue);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.pid1IValue);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.pid1PValue);
			this.groupBox1.Location = new System.Drawing.Point(12, 37);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(442, 115);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Angle PID";
			// 
			// getPID1Button
			// 
			this.getPID1Button.Location = new System.Drawing.Point(9, 72);
			this.getPID1Button.Name = "getPID1Button";
			this.getPID1Button.Size = new System.Drawing.Size(80, 33);
			this.getPID1Button.TabIndex = 9;
			this.getPID1Button.Text = "Get";
			this.getPID1Button.UseVisualStyleBackColor = true;
			this.getPID1Button.Click += new System.EventHandler(this.getPID1Button_Click);
			// 
			// setPID1Button
			// 
			this.setPID1Button.Location = new System.Drawing.Point(95, 72);
			this.setPID1Button.Name = "setPID1Button";
			this.setPID1Button.Size = new System.Drawing.Size(338, 33);
			this.setPID1Button.TabIndex = 8;
			this.setPID1Button.Text = "Set";
			this.setPID1Button.UseVisualStyleBackColor = true;
			this.setPID1Button.Click += new System.EventHandler(this.setPID1Button_Click);
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
			// toolbar
			// 
			this.toolbar.AutoSize = false;
			this.toolbar.BackColor = System.Drawing.Color.WhiteSmoke;
			this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ipAddressTextbox,
            this.connectButton,
            this.disconnectButton});
			this.toolbar.Location = new System.Drawing.Point(0, 0);
			this.toolbar.Name = "toolbar";
			this.toolbar.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
			this.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolbar.Size = new System.Drawing.Size(464, 30);
			this.toolbar.TabIndex = 10;
			this.toolbar.Text = "toolStrip1";
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
			this.disconnectButton.Image = ((System.Drawing.Image)(resources.GetObject("disconnectButton.Image")));
			this.disconnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.disconnectButton.Name = "disconnectButton";
			this.disconnectButton.Size = new System.Drawing.Size(86, 27);
			this.disconnectButton.Text = "Disconnect";
			this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.pid2GainValue);
			this.groupBox2.Controls.Add(this.getPID2Button);
			this.groupBox2.Controls.Add(this.setPID2Button);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.pid2SetPointValue);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.pid2DValue);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.pid2IValue);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.pid2PValue);
			this.groupBox2.Location = new System.Drawing.Point(12, 158);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(442, 115);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Delta Angle PID";
			// 
			// getPID2Button
			// 
			this.getPID2Button.Location = new System.Drawing.Point(9, 72);
			this.getPID2Button.Name = "getPID2Button";
			this.getPID2Button.Size = new System.Drawing.Size(80, 33);
			this.getPID2Button.TabIndex = 9;
			this.getPID2Button.Text = "Get";
			this.getPID2Button.UseVisualStyleBackColor = true;
			this.getPID2Button.Click += new System.EventHandler(this.getPID2Button_Click);
			// 
			// setPID2Button
			// 
			this.setPID2Button.Location = new System.Drawing.Point(95, 72);
			this.setPID2Button.Name = "setPID2Button";
			this.setPID2Button.Size = new System.Drawing.Size(338, 33);
			this.setPID2Button.TabIndex = 8;
			this.setPID2Button.Text = "Set";
			this.setPID2Button.UseVisualStyleBackColor = true;
			this.setPID2Button.Click += new System.EventHandler(this.setPID2Button_Click);
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
			// pid1GainValue
			// 
			this.pid1GainValue.DecimalPlaces = 4;
			this.pid1GainValue.Location = new System.Drawing.Point(353, 41);
			this.pid1GainValue.Name = "pid1GainValue";
			this.pid1GainValue.Size = new System.Drawing.Size(80, 25);
			this.pid1GainValue.TabIndex = 10;
			// 
			// pid2GainValue
			// 
			this.pid2GainValue.DecimalPlaces = 4;
			this.pid2GainValue.Location = new System.Drawing.Point(353, 41);
			this.pid2GainValue.Name = "pid2GainValue";
			this.pid2GainValue.Size = new System.Drawing.Size(80, 25);
			this.pid2GainValue.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(350, 25);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(35, 13);
			this.label9.TabIndex = 12;
			this.label9.Text = "GAIN";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(353, 25);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(35, 13);
			this.label10.TabIndex = 13;
			this.label10.Text = "GAIN";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(464, 378);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.toolbar);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusBar);
			this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainWindow";
			this.Text = "Stasis Command Center";
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid1SetPointValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1DValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1IValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1PValue)).EndInit();
			this.toolbar.ResumeLayout(false);
			this.toolbar.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pid2SetPointValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2DValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2IValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2PValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid1GainValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pid2GainValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel loopSpeedLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown pid1PValue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown pid1IValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown pid1SetPointValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown pid1DValue;
		private System.Windows.Forms.Button setPID1Button;
		private System.Windows.Forms.ToolStrip toolbar;
		private System.Windows.Forms.ToolStripTextBox ipAddressTextbox;
		private System.Windows.Forms.ToolStripButton connectButton;
		private System.Windows.Forms.ToolStripButton disconnectButton;
		private System.Windows.Forms.Button getPID1Button;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button getPID2Button;
		private System.Windows.Forms.Button setPID2Button;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown pid2SetPointValue;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown pid2IValue;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown pid2PValue;
		private System.Windows.Forms.NumericUpDown pid2DValue;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown pid1GainValue;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown pid2GainValue;
	}
}

