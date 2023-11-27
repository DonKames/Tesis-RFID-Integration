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
            btnReadOnce = new Button();
            btnStopRead = new Button();
            btnStartRead = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            grpBoxConnection.SuspendLayout();
            grpBoxLecturas.SuspendLayout();
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
            textBox1.Size = new Size(528, 143);
            textBox1.TabIndex = 3;
            // 
            // grpBoxLecturas
            // 
            grpBoxLecturas.Controls.Add(btnReadOnce);
            grpBoxLecturas.Controls.Add(btnStopRead);
            grpBoxLecturas.Controls.Add(btnStartRead);
            grpBoxLecturas.Location = new Point(218, 12);
            grpBoxLecturas.Name = "grpBoxLecturas";
            grpBoxLecturas.Size = new Size(322, 149);
            grpBoxLecturas.TabIndex = 4;
            grpBoxLecturas.TabStop = false;
            grpBoxLecturas.Text = "Lecturas";
            // 
            // btnReadOnce
            // 
            btnReadOnce.Location = new Point(6, 51);
            btnReadOnce.Name = "btnReadOnce";
            btnReadOnce.Size = new Size(181, 23);
            btnReadOnce.TabIndex = 2;
            btnReadOnce.Text = "Lectura Unica";
            btnReadOnce.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(552, 450);
            Controls.Add(grpBoxLecturas);
            Controls.Add(textBox1);
            Controls.Add(grpBoxConnection);
            Name = "Form1";
            Text = "MainForm";
            grpBoxConnection.ResumeLayout(false);
            grpBoxConnection.PerformLayout();
            grpBoxLecturas.ResumeLayout(false);
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
    }
}