namespace TBMails
{
    partial class Download
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.servertxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ateDP = new System.Windows.Forms.DateTimePicker();
            this.deDP = new System.Windows.Forms.DateTimePicker();
            this.specDate = new System.Windows.Forms.DateTimePicker();
            this.rangeRb = new System.Windows.Forms.RadioButton();
            this.specRb = new System.Windows.Forms.RadioButton();
            this.todayRB = new System.Windows.Forms.RadioButton();
            this.pathUploadTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Login Exchange";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(69, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(249, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pass";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.servertxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 136);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // servertxt
            // 
            this.servertxt.Location = new System.Drawing.Point(69, 20);
            this.servertxt.Name = "servertxt";
            this.servertxt.Size = new System.Drawing.Size(249, 20);
            this.servertxt.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Servidor:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Login Imap";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ateDP);
            this.groupBox2.Controls.Add(this.deDP);
            this.groupBox2.Controls.Add(this.specDate);
            this.groupBox2.Controls.Add(this.rangeRb);
            this.groupBox2.Controls.Add(this.specRb);
            this.groupBox2.Controls.Add(this.todayRB);
            this.groupBox2.Location = new System.Drawing.Point(362, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 136);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Range de Dados";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "até";
            // 
            // ateDP
            // 
            this.ateDP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ateDP.Location = new System.Drawing.Point(224, 84);
            this.ateDP.Name = "ateDP";
            this.ateDP.Size = new System.Drawing.Size(96, 20);
            this.ateDP.TabIndex = 3;
            // 
            // deDP
            // 
            this.deDP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.deDP.Location = new System.Drawing.Point(94, 84);
            this.deDP.Name = "deDP";
            this.deDP.Size = new System.Drawing.Size(96, 20);
            this.deDP.TabIndex = 3;
            // 
            // specDate
            // 
            this.specDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.specDate.Location = new System.Drawing.Point(94, 58);
            this.specDate.Name = "specDate";
            this.specDate.Size = new System.Drawing.Size(96, 20);
            this.specDate.TabIndex = 3;
            // 
            // rangeRb
            // 
            this.rangeRb.AutoSize = true;
            this.rangeRb.Location = new System.Drawing.Point(16, 84);
            this.rangeRb.Name = "rangeRb";
            this.rangeRb.Size = new System.Drawing.Size(72, 17);
            this.rangeRb.TabIndex = 2;
            this.rangeRb.TabStop = true;
            this.rangeRb.Text = "Range de";
            this.rangeRb.UseVisualStyleBackColor = true;
            // 
            // specRb
            // 
            this.specRb.AutoSize = true;
            this.specRb.Location = new System.Drawing.Point(16, 58);
            this.specRb.Name = "specRb";
            this.specRb.Size = new System.Drawing.Size(74, 17);
            this.specRb.TabIndex = 1;
            this.specRb.TabStop = true;
            this.specRb.Text = "Especifica";
            this.specRb.UseVisualStyleBackColor = true;
            // 
            // todayRB
            // 
            this.todayRB.AutoSize = true;
            this.todayRB.Checked = true;
            this.todayRB.Location = new System.Drawing.Point(16, 32);
            this.todayRB.Name = "todayRB";
            this.todayRB.Size = new System.Drawing.Size(47, 17);
            this.todayRB.TabIndex = 0;
            this.todayRB.TabStop = true;
            this.todayRB.Text = "Hoje";
            this.todayRB.UseVisualStyleBackColor = true;
            // 
            // pathUploadTxt
            // 
            this.pathUploadTxt.Location = new System.Drawing.Point(119, 22);
            this.pathUploadTxt.Name = "pathUploadTxt";
            this.pathUploadTxt.Size = new System.Drawing.Size(499, 20);
            this.pathUploadTxt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Caminho de upload:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(624, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Pasta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(711, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Gravar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 543);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pathUploadTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Download";
            this.Text = "Download";
            this.Load += new System.EventHandler(this.Download_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox servertxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker ateDP;
        private System.Windows.Forms.DateTimePicker deDP;
        private System.Windows.Forms.DateTimePicker specDate;
        private System.Windows.Forms.RadioButton rangeRb;
        private System.Windows.Forms.RadioButton specRb;
        private System.Windows.Forms.RadioButton todayRB;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox pathUploadTxt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}