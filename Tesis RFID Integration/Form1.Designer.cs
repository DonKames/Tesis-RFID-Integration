namespace Tesis_RFID_Integration
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnScanUSB = new Button();
            grpBoxConnection = new GroupBox();
            btnConnect = new Button();
            lblUSB = new Label();
            cboBoxUSB = new ComboBox();
            textBox1 = new TextBox();
            grpBoxLecturas = new GroupBox();
            lblEPC = new Label();
            label1 = new Label();
            btnReadOnce = new Button();
            btnStopRead = new Button();
            btnStartRead = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox1 = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            grpBoxConfiguration = new GroupBox();
            lblReadingInWarehouse = new Label();
            lblWarehouseSetted = new Label();
            btnUpdateSettings = new Button();
            grpBoxConnection.SuspendLayout();
            grpBoxLecturas.SuspendLayout();
            groupBox1.SuspendLayout();
            grpBoxConfiguration.SuspendLayout();
            SuspendLayout();
            // 
            // btnScanUSB
            // 
            btnScanUSB.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnScanUSB.Location = new Point(6, 21);
            btnScanUSB.Name = "btnScanUSB";
            btnScanUSB.Size = new Size(188, 23);
            btnScanUSB.TabIndex = 0;
            btnScanUSB.Text = "Scan USB Antenna";
            btnScanUSB.UseVisualStyleBackColor = true;
            btnScanUSB.Click += btnScanUSB_Click;
            // 
            // grpBoxConnection
            // 
            grpBoxConnection.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            grpBoxConnection.Controls.Add(btnConnect);
            grpBoxConnection.Controls.Add(lblUSB);
            grpBoxConnection.Controls.Add(cboBoxUSB);
            grpBoxConnection.Controls.Add(btnScanUSB);
            grpBoxConnection.Location = new Point(12, 12);
            grpBoxConnection.Name = "grpBoxConnection";
            grpBoxConnection.Size = new Size(200, 149);
            grpBoxConnection.TabIndex = 2;
            grpBoxConnection.TabStop = false;
            grpBoxConnection.Text = "Conexión";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(6, 80);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(188, 23);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Conectar";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // lblUSB
            // 
            lblUSB.AutoSize = true;
            lblUSB.Location = new Point(6, 54);
            lblUSB.Name = "lblUSB";
            lblUSB.Size = new Size(34, 15);
            lblUSB.TabIndex = 1;
            lblUSB.Text = "USB: ";
            // 
            // cboBoxUSB
            // 
            cboBoxUSB.FormattingEnabled = true;
            cboBoxUSB.Location = new Point(46, 51);
            cboBoxUSB.Name = "cboBoxUSB";
            cboBoxUSB.Size = new Size(148, 23);
            cboBoxUSB.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 295);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(528, 143);
            textBox1.TabIndex = 3;
            // 
            // grpBoxLecturas
            // 
            grpBoxLecturas.Controls.Add(lblEPC);
            grpBoxLecturas.Controls.Add(label1);
            grpBoxLecturas.Controls.Add(btnReadOnce);
            grpBoxLecturas.Controls.Add(btnStopRead);
            grpBoxLecturas.Controls.Add(btnStartRead);
            grpBoxLecturas.Location = new Point(218, 140);
            grpBoxLecturas.Name = "grpBoxLecturas";
            grpBoxLecturas.Size = new Size(322, 149);
            grpBoxLecturas.TabIndex = 4;
            grpBoxLecturas.TabStop = false;
            grpBoxLecturas.Text = "Lecturas";
            // 
            // lblEPC
            // 
            lblEPC.AutoSize = true;
            lblEPC.Location = new Point(43, 84);
            lblEPC.Name = "lblEPC";
            lblEPC.Size = new Size(16, 15);
            lblEPC.TabIndex = 4;
            lblEPC.Text = "...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 84);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 3;
            label1.Text = "EPC:";
            // 
            // btnReadOnce
            // 
            btnReadOnce.Location = new Point(6, 51);
            btnReadOnce.Name = "btnReadOnce";
            btnReadOnce.Size = new Size(181, 23);
            btnReadOnce.TabIndex = 2;
            btnReadOnce.Text = "Lectura Unica";
            btnReadOnce.UseVisualStyleBackColor = true;
            btnReadOnce.Click += btnReadOnce_Click;
            // 
            // btnStopRead
            // 
            btnStopRead.Location = new Point(112, 21);
            btnStopRead.Name = "btnStopRead";
            btnStopRead.Size = new Size(75, 23);
            btnStopRead.TabIndex = 1;
            btnStopRead.Text = "Parar";
            btnStopRead.UseVisualStyleBackColor = true;
            btnStopRead.Click += btnStopRead_Click;
            // 
            // btnStartRead
            // 
            btnStartRead.Location = new Point(6, 21);
            btnStartRead.Name = "btnStartRead";
            btnStartRead.Size = new Size(75, 23);
            btnStartRead.TabIndex = 0;
            btnStartRead.Text = "Iniciar";
            btnStartRead.UseVisualStyleBackColor = true;
            btnStartRead.Click += btnStartRead_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(12, 167);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 122);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Pruebas";
            // 
            // button3
            // 
            button3.Location = new Point(6, 80);
            button3.Name = "button3";
            button3.Size = new Size(112, 23);
            button3.TabIndex = 2;
            button3.Text = "Set Active Mode";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(6, 51);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "getParams";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(6, 22);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "getTagBuffTest";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 51);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(120, 23);
            comboBox1.TabIndex = 5;
            // 
            // grpBoxConfiguration
            // 
            grpBoxConfiguration.Controls.Add(btnUpdateSettings);
            grpBoxConfiguration.Controls.Add(comboBox1);
            grpBoxConfiguration.Controls.Add(lblWarehouseSetted);
            grpBoxConfiguration.Controls.Add(lblReadingInWarehouse);
            grpBoxConfiguration.Location = new Point(218, 12);
            grpBoxConfiguration.Name = "grpBoxConfiguration";
            grpBoxConfiguration.Size = new Size(322, 122);
            grpBoxConfiguration.TabIndex = 6;
            grpBoxConfiguration.TabStop = false;
            grpBoxConfiguration.Text = "Configuración";
            // 
            // lblReadingInWarehouse
            // 
            lblReadingInWarehouse.AutoSize = true;
            lblReadingInWarehouse.Location = new Point(6, 25);
            lblReadingInWarehouse.Name = "lblReadingInWarehouse";
            lblReadingInWarehouse.Size = new Size(114, 15);
            lblReadingInWarehouse.TabIndex = 0;
            lblReadingInWarehouse.Text = "Leyendo en Bodega:";
            // 
            // lblWarehouseSetted
            // 
            lblWarehouseSetted.AutoSize = true;
            lblWarehouseSetted.Location = new Point(147, 25);
            lblWarehouseSetted.Name = "lblWarehouseSetted";
            lblWarehouseSetted.Size = new Size(85, 15);
            lblWarehouseSetted.TabIndex = 1;
            lblWarehouseSetted.Text = "Sin Determinar";
            // 
            // btnUpdateSettings
            // 
            btnUpdateSettings.Location = new Point(147, 51);
            btnUpdateSettings.Name = "btnUpdateSettings";
            btnUpdateSettings.Size = new Size(85, 23);
            btnUpdateSettings.TabIndex = 6;
            btnUpdateSettings.Text = "Actualizar";
            btnUpdateSettings.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(552, 450);
            Controls.Add(grpBoxConfiguration);
            Controls.Add(groupBox1);
            Controls.Add(grpBoxLecturas);
            Controls.Add(textBox1);
            Controls.Add(grpBoxConnection);
            Name = "Form1";
            Text = "MainForm";
            grpBoxConnection.ResumeLayout(false);
            grpBoxConnection.PerformLayout();
            grpBoxLecturas.ResumeLayout(false);
            grpBoxLecturas.PerformLayout();
            groupBox1.ResumeLayout(false);
            grpBoxConfiguration.ResumeLayout(false);
            grpBoxConfiguration.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnScanUSB;
        private GroupBox grpBoxConnection;
        private Label lblUSB;
        private ComboBox cboBoxUSB;
        private Button btnConnect;
        private TextBox textBox1;
        private GroupBox grpBoxLecturas;
        private Button btnReadOnce;
        private Button btnStopRead;
        private Button btnStartRead;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Label lblEPC;
        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
        private GroupBox grpBoxConfiguration;
        private Button btnUpdateSettings;
        private Label lblWarehouseSetted;
        private Label lblReadingInWarehouse;
    }
}