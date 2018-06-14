namespace TBMails
{
    partial class TBs
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
            this.bccMails = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ccMails = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.reMails = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.reportsList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bccMails
            // 
            this.bccMails.Enabled = false;
            this.bccMails.FormattingEnabled = true;
            this.bccMails.Location = new System.Drawing.Point(956, 444);
            this.bccMails.Name = "bccMails";
            this.bccMails.Size = new System.Drawing.Size(229, 147);
            this.bccMails.TabIndex = 19;
            this.bccMails.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(953, 428);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Emails BCC:";
            this.label11.Visible = false;
            // 
            // ccMails
            // 
            this.ccMails.Enabled = false;
            this.ccMails.FormattingEnabled = true;
            this.ccMails.Location = new System.Drawing.Point(956, 271);
            this.ccMails.Name = "ccMails";
            this.ccMails.Size = new System.Drawing.Size(229, 147);
            this.ccMails.TabIndex = 17;
            this.ccMails.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(953, 255);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Emails CC:";
            this.label10.Visible = false;
            // 
            // reMails
            // 
            this.reMails.Enabled = false;
            this.reMails.FormattingEnabled = true;
            this.reMails.Location = new System.Drawing.Point(956, 98);
            this.reMails.Name = "reMails";
            this.reMails.Size = new System.Drawing.Size(229, 147);
            this.reMails.TabIndex = 15;
            this.reMails.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(953, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Emails RE:";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(953, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Emails:";
            this.label8.Visible = false;
            // 
            // reportsList
            // 
            this.reportsList.FormattingEnabled = true;
            this.reportsList.Location = new System.Drawing.Point(32, 67);
            this.reportsList.Name = "reportsList";
            this.reportsList.Size = new System.Drawing.Size(247, 524);
            this.reportsList.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "TB\'s";
            this.label1.Visible = false;
            // 
            // TBs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 695);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportsList);
            this.Controls.Add(this.bccMails);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ccMails);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.reMails);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TBs";
            this.Text = "TBs";
            this.Load += new System.EventHandler(this.TBs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox bccMails;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox ccMails;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox reMails;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox reportsList;
        private System.Windows.Forms.Label label1;
    }
}