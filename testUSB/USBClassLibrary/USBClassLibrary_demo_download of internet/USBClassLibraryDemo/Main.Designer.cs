namespace USBClassLibraryDemo
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ConnectionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DeviceTypeLabel = new System.Windows.Forms.Label();
            this.FriendlyNameLabel = new System.Windows.Forms.Label();
            this.DeviceTypeTextBox = new System.Windows.Forms.TextBox();
            this.FriendlyNameTextBox = new System.Windows.Forms.TextBox();
            this.DeviceManufacturerTextBox = new System.Windows.Forms.TextBox();
            this.DeviceDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DeviceManufacturerLabel = new System.Windows.Forms.Label();
            this.DeviceDescriptionLabel = new System.Windows.Forms.Label();
            this.DeviceLocationTextBox = new System.Windows.Forms.TextBox();
            this.DeviceClassTextBox = new System.Windows.Forms.TextBox();
            this.DeviceLocationLabel = new System.Windows.Forms.Label();
            this.DeviceClassLabel = new System.Windows.Forms.Label();
            this.DevicePhysicalObjectNameTextBox = new System.Windows.Forms.TextBox();
            this.DevicePathTextBox = new System.Windows.Forms.TextBox();
            this.DevicePhysicalObjectNameLabel = new System.Windows.Forms.Label();
            this.DevicePathLabel = new System.Windows.Forms.Label();
            this.MyDeviceLabel = new System.Windows.Forms.Label();
            this.VIDTextBox = new System.Windows.Forms.TextBox();
            this.PIDTextBox = new System.Windows.Forms.TextBox();
            this.VIDLabel = new System.Windows.Forms.Label();
            this.PIDLabel = new System.Windows.Forms.Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SerialPortTextBox = new System.Windows.Forms.TextBox();
            this.SerialPortLabel = new System.Windows.Forms.Label();
            this.SerialPortCheckBox = new System.Windows.Forms.CheckBox();
            this.MITextBox = new System.Windows.Forms.TextBox();
            this.MILabel = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(634, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ConnectionToolStripStatusLabel
            // 
            this.ConnectionToolStripStatusLabel.Name = "ConnectionToolStripStatusLabel";
            this.ConnectionToolStripStatusLabel.Size = new System.Drawing.Size(79, 17);
            this.ConnectionToolStripStatusLabel.Text = "Disconnected";
            // 
            // DeviceTypeLabel
            // 
            this.DeviceTypeLabel.AutoSize = true;
            this.DeviceTypeLabel.Location = new System.Drawing.Point(13, 45);
            this.DeviceTypeLabel.Name = "DeviceTypeLabel";
            this.DeviceTypeLabel.Size = new System.Drawing.Size(65, 13);
            this.DeviceTypeLabel.TabIndex = 1;
            this.DeviceTypeLabel.Text = "DeviceType";
            // 
            // FriendlyNameLabel
            // 
            this.FriendlyNameLabel.AutoSize = true;
            this.FriendlyNameLabel.Location = new System.Drawing.Point(13, 77);
            this.FriendlyNameLabel.Name = "FriendlyNameLabel";
            this.FriendlyNameLabel.Size = new System.Drawing.Size(71, 13);
            this.FriendlyNameLabel.TabIndex = 2;
            this.FriendlyNameLabel.Text = "FriendlyName";
            // 
            // DeviceTypeTextBox
            // 
            this.DeviceTypeTextBox.Location = new System.Drawing.Point(107, 41);
            this.DeviceTypeTextBox.Name = "DeviceTypeTextBox";
            this.DeviceTypeTextBox.ReadOnly = true;
            this.DeviceTypeTextBox.Size = new System.Drawing.Size(515, 20);
            this.DeviceTypeTextBox.TabIndex = 0;
            this.DeviceTypeTextBox.TabStop = false;
            // 
            // FriendlyNameTextBox
            // 
            this.FriendlyNameTextBox.Location = new System.Drawing.Point(107, 73);
            this.FriendlyNameTextBox.Name = "FriendlyNameTextBox";
            this.FriendlyNameTextBox.ReadOnly = true;
            this.FriendlyNameTextBox.Size = new System.Drawing.Size(515, 20);
            this.FriendlyNameTextBox.TabIndex = 0;
            this.FriendlyNameTextBox.TabStop = false;
            // 
            // DeviceManufacturerTextBox
            // 
            this.DeviceManufacturerTextBox.Location = new System.Drawing.Point(123, 137);
            this.DeviceManufacturerTextBox.Name = "DeviceManufacturerTextBox";
            this.DeviceManufacturerTextBox.ReadOnly = true;
            this.DeviceManufacturerTextBox.Size = new System.Drawing.Size(499, 20);
            this.DeviceManufacturerTextBox.TabIndex = 0;
            this.DeviceManufacturerTextBox.TabStop = false;
            // 
            // DeviceDescriptionTextBox
            // 
            this.DeviceDescriptionTextBox.Location = new System.Drawing.Point(107, 105);
            this.DeviceDescriptionTextBox.Name = "DeviceDescriptionTextBox";
            this.DeviceDescriptionTextBox.ReadOnly = true;
            this.DeviceDescriptionTextBox.Size = new System.Drawing.Size(515, 20);
            this.DeviceDescriptionTextBox.TabIndex = 0;
            this.DeviceDescriptionTextBox.TabStop = false;
            // 
            // DeviceManufacturerLabel
            // 
            this.DeviceManufacturerLabel.AutoSize = true;
            this.DeviceManufacturerLabel.Location = new System.Drawing.Point(13, 141);
            this.DeviceManufacturerLabel.Name = "DeviceManufacturerLabel";
            this.DeviceManufacturerLabel.Size = new System.Drawing.Size(104, 13);
            this.DeviceManufacturerLabel.TabIndex = 6;
            this.DeviceManufacturerLabel.Text = "DeviceManufacturer";
            // 
            // DeviceDescriptionLabel
            // 
            this.DeviceDescriptionLabel.AutoSize = true;
            this.DeviceDescriptionLabel.Location = new System.Drawing.Point(13, 109);
            this.DeviceDescriptionLabel.Name = "DeviceDescriptionLabel";
            this.DeviceDescriptionLabel.Size = new System.Drawing.Size(94, 13);
            this.DeviceDescriptionLabel.TabIndex = 5;
            this.DeviceDescriptionLabel.Text = "DeviceDescription";
            // 
            // DeviceLocationTextBox
            // 
            this.DeviceLocationTextBox.Location = new System.Drawing.Point(107, 201);
            this.DeviceLocationTextBox.Name = "DeviceLocationTextBox";
            this.DeviceLocationTextBox.ReadOnly = true;
            this.DeviceLocationTextBox.Size = new System.Drawing.Size(515, 20);
            this.DeviceLocationTextBox.TabIndex = 0;
            this.DeviceLocationTextBox.TabStop = false;
            // 
            // DeviceClassTextBox
            // 
            this.DeviceClassTextBox.Location = new System.Drawing.Point(107, 169);
            this.DeviceClassTextBox.Name = "DeviceClassTextBox";
            this.DeviceClassTextBox.ReadOnly = true;
            this.DeviceClassTextBox.Size = new System.Drawing.Size(515, 20);
            this.DeviceClassTextBox.TabIndex = 0;
            this.DeviceClassTextBox.TabStop = false;
            // 
            // DeviceLocationLabel
            // 
            this.DeviceLocationLabel.AutoSize = true;
            this.DeviceLocationLabel.Location = new System.Drawing.Point(13, 205);
            this.DeviceLocationLabel.Name = "DeviceLocationLabel";
            this.DeviceLocationLabel.Size = new System.Drawing.Size(82, 13);
            this.DeviceLocationLabel.TabIndex = 10;
            this.DeviceLocationLabel.Text = "DeviceLocation";
            // 
            // DeviceClassLabel
            // 
            this.DeviceClassLabel.AutoSize = true;
            this.DeviceClassLabel.Location = new System.Drawing.Point(13, 173);
            this.DeviceClassLabel.Name = "DeviceClassLabel";
            this.DeviceClassLabel.Size = new System.Drawing.Size(66, 13);
            this.DeviceClassLabel.TabIndex = 9;
            this.DeviceClassLabel.Text = "DeviceClass";
            // 
            // DevicePhysicalObjectNameTextBox
            // 
            this.DevicePhysicalObjectNameTextBox.Location = new System.Drawing.Point(158, 265);
            this.DevicePhysicalObjectNameTextBox.Name = "DevicePhysicalObjectNameTextBox";
            this.DevicePhysicalObjectNameTextBox.ReadOnly = true;
            this.DevicePhysicalObjectNameTextBox.Size = new System.Drawing.Size(464, 20);
            this.DevicePhysicalObjectNameTextBox.TabIndex = 0;
            this.DevicePhysicalObjectNameTextBox.TabStop = false;
            // 
            // DevicePathTextBox
            // 
            this.DevicePathTextBox.Location = new System.Drawing.Point(107, 233);
            this.DevicePathTextBox.Name = "DevicePathTextBox";
            this.DevicePathTextBox.ReadOnly = true;
            this.DevicePathTextBox.Size = new System.Drawing.Size(515, 20);
            this.DevicePathTextBox.TabIndex = 0;
            this.DevicePathTextBox.TabStop = false;
            // 
            // DevicePhysicalObjectNameLabel
            // 
            this.DevicePhysicalObjectNameLabel.AutoSize = true;
            this.DevicePhysicalObjectNameLabel.Location = new System.Drawing.Point(13, 269);
            this.DevicePhysicalObjectNameLabel.Name = "DevicePhysicalObjectNameLabel";
            this.DevicePhysicalObjectNameLabel.Size = new System.Drawing.Size(139, 13);
            this.DevicePhysicalObjectNameLabel.TabIndex = 14;
            this.DevicePhysicalObjectNameLabel.Text = "DevicePhysicalObjectName";
            // 
            // DevicePathLabel
            // 
            this.DevicePathLabel.AutoSize = true;
            this.DevicePathLabel.Location = new System.Drawing.Point(13, 237);
            this.DevicePathLabel.Name = "DevicePathLabel";
            this.DevicePathLabel.Size = new System.Drawing.Size(63, 13);
            this.DevicePathLabel.TabIndex = 13;
            this.DevicePathLabel.Text = "DevicePath";
            // 
            // MyDeviceLabel
            // 
            this.MyDeviceLabel.AutoSize = true;
            this.MyDeviceLabel.Location = new System.Drawing.Point(13, 14);
            this.MyDeviceLabel.Name = "MyDeviceLabel";
            this.MyDeviceLabel.Size = new System.Drawing.Size(61, 13);
            this.MyDeviceLabel.TabIndex = 17;
            this.MyDeviceLabel.Text = "My Device:";
            // 
            // VIDTextBox
            // 
            this.VIDTextBox.Location = new System.Drawing.Point(135, 10);
            this.VIDTextBox.MaxLength = 4;
            this.VIDTextBox.Name = "VIDTextBox";
            this.VIDTextBox.Size = new System.Drawing.Size(75, 20);
            this.VIDTextBox.TabIndex = 1;
            this.VIDTextBox.Leave += new System.EventHandler(this.VIDTextBox_Leave);
            // 
            // PIDTextBox
            // 
            this.PIDTextBox.Location = new System.Drawing.Point(264, 11);
            this.PIDTextBox.MaxLength = 4;
            this.PIDTextBox.Name = "PIDTextBox";
            this.PIDTextBox.Size = new System.Drawing.Size(75, 20);
            this.PIDTextBox.TabIndex = 2;
            this.PIDTextBox.Leave += new System.EventHandler(this.PIDTextBox_Leave);
            // 
            // VIDLabel
            // 
            this.VIDLabel.AutoSize = true;
            this.VIDLabel.Location = new System.Drawing.Point(104, 14);
            this.VIDLabel.Name = "VIDLabel";
            this.VIDLabel.Size = new System.Drawing.Size(25, 13);
            this.VIDLabel.TabIndex = 20;
            this.VIDLabel.Text = "VID";
            // 
            // PIDLabel
            // 
            this.PIDLabel.AutoSize = true;
            this.PIDLabel.Location = new System.Drawing.Point(233, 14);
            this.PIDLabel.Name = "PIDLabel";
            this.PIDLabel.Size = new System.Drawing.Size(25, 13);
            this.PIDLabel.TabIndex = 21;
            this.PIDLabel.Text = "PID";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(457, 7);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(65, 23);
            this.UpdateButton.TabIndex = 4;
            this.UpdateButton.Text = "Update!";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // SerialPortTextBox
            // 
            this.SerialPortTextBox.Location = new System.Drawing.Point(107, 297);
            this.SerialPortTextBox.Name = "SerialPortTextBox";
            this.SerialPortTextBox.ReadOnly = true;
            this.SerialPortTextBox.Size = new System.Drawing.Size(515, 20);
            this.SerialPortTextBox.TabIndex = 0;
            this.SerialPortTextBox.TabStop = false;
            // 
            // SerialPortLabel
            // 
            this.SerialPortLabel.AutoSize = true;
            this.SerialPortLabel.Location = new System.Drawing.Point(13, 301);
            this.SerialPortLabel.Name = "SerialPortLabel";
            this.SerialPortLabel.Size = new System.Drawing.Size(55, 13);
            this.SerialPortLabel.TabIndex = 23;
            this.SerialPortLabel.Text = "Serial Port";
            // 
            // SerialPortCheckBox
            // 
            this.SerialPortCheckBox.AutoSize = true;
            this.SerialPortCheckBox.Location = new System.Drawing.Point(528, 11);
            this.SerialPortCheckBox.Name = "SerialPortCheckBox";
            this.SerialPortCheckBox.Size = new System.Drawing.Size(94, 17);
            this.SerialPortCheckBox.TabIndex = 5;
            this.SerialPortCheckBox.Text = "Get Serial Port";
            this.SerialPortCheckBox.UseVisualStyleBackColor = true;
            // 
            // MITextBox
            // 
            this.MITextBox.Location = new System.Drawing.Point(379, 10);
            this.MITextBox.MaxLength = 2;
            this.MITextBox.Name = "MITextBox";
            this.MITextBox.Size = new System.Drawing.Size(50, 20);
            this.MITextBox.TabIndex = 3;
            this.MITextBox.Leave += new System.EventHandler(this.MITextBox_Leave);
            // 
            // MILabel
            // 
            this.MILabel.AutoSize = true;
            this.MILabel.Location = new System.Drawing.Point(354, 15);
            this.MILabel.Name = "MILabel";
            this.MILabel.Size = new System.Drawing.Size(19, 13);
            this.MILabel.TabIndex = 27;
            this.MILabel.Text = "MI";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 369);
            this.Controls.Add(this.MILabel);
            this.Controls.Add(this.MITextBox);
            this.Controls.Add(this.SerialPortCheckBox);
            this.Controls.Add(this.SerialPortTextBox);
            this.Controls.Add(this.SerialPortLabel);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.PIDLabel);
            this.Controls.Add(this.VIDLabel);
            this.Controls.Add(this.PIDTextBox);
            this.Controls.Add(this.VIDTextBox);
            this.Controls.Add(this.MyDeviceLabel);
            this.Controls.Add(this.DevicePhysicalObjectNameTextBox);
            this.Controls.Add(this.DevicePathTextBox);
            this.Controls.Add(this.DevicePhysicalObjectNameLabel);
            this.Controls.Add(this.DevicePathLabel);
            this.Controls.Add(this.DeviceLocationTextBox);
            this.Controls.Add(this.DeviceClassTextBox);
            this.Controls.Add(this.DeviceLocationLabel);
            this.Controls.Add(this.DeviceClassLabel);
            this.Controls.Add(this.DeviceManufacturerTextBox);
            this.Controls.Add(this.DeviceDescriptionTextBox);
            this.Controls.Add(this.DeviceManufacturerLabel);
            this.Controls.Add(this.DeviceDescriptionLabel);
            this.Controls.Add(this.FriendlyNameTextBox);
            this.Controls.Add(this.DeviceTypeTextBox);
            this.Controls.Add(this.FriendlyNameLabel);
            this.Controls.Add(this.DeviceTypeLabel);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Main";
            this.Text = "USB Class Library Demo";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private USBClassLibrary.USBClass USBPort;
        private USBClassLibrary.USBClass.DeviceProperties USBDeviceProperties;
        bool MyUSBDeviceConnected;
        private const uint MyDeviceVID = 0X04D8; //Microchip ICD2 VID
        private const uint MyDevicePID = 0X8001; //Microchip ICD2 PID
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionToolStripStatusLabel;
        private System.Windows.Forms.Label DeviceTypeLabel;
        private System.Windows.Forms.Label FriendlyNameLabel;
        private System.Windows.Forms.TextBox DeviceTypeTextBox;
        private System.Windows.Forms.TextBox FriendlyNameTextBox;
        private System.Windows.Forms.TextBox DeviceManufacturerTextBox;
        private System.Windows.Forms.TextBox DeviceDescriptionTextBox;
        private System.Windows.Forms.Label DeviceManufacturerLabel;
        private System.Windows.Forms.Label DeviceDescriptionLabel;
        private System.Windows.Forms.TextBox DeviceLocationTextBox;
        private System.Windows.Forms.TextBox DeviceClassTextBox;
        private System.Windows.Forms.Label DeviceLocationLabel;
        private System.Windows.Forms.Label DeviceClassLabel;
        private System.Windows.Forms.TextBox DevicePhysicalObjectNameTextBox;
        private System.Windows.Forms.TextBox DevicePathTextBox;
        private System.Windows.Forms.Label DevicePhysicalObjectNameLabel;
        private System.Windows.Forms.Label DevicePathLabel;
        private System.Windows.Forms.Label MyDeviceLabel;
        private System.Windows.Forms.TextBox VIDTextBox;
        private System.Windows.Forms.TextBox PIDTextBox;
        private System.Windows.Forms.Label VIDLabel;
        private System.Windows.Forms.Label PIDLabel;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.TextBox SerialPortTextBox;
        private System.Windows.Forms.Label SerialPortLabel;
        private System.Windows.Forms.CheckBox SerialPortCheckBox;
        private System.Windows.Forms.Label MILabel;
        private System.Windows.Forms.TextBox MITextBox;
    }
}

