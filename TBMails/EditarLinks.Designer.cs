namespace TBMails
{
    partial class EditarLinks
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ligacoesCb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bdcb = new System.Windows.Forms.ComboBox();
            this.removeLink = new System.Windows.Forms.Button();
            this.listLinks = new System.Windows.Forms.ListBox();
            this.editBT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.selectFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rmvMail = new System.Windows.Forms.Button();
            this.mailsList = new System.Windows.Forms.ListBox();
            this.addMail = new System.Windows.Forms.Button();
            this.mailtxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pathTxt = new System.Windows.Forms.TextBox();
            this.selectBT = new System.Windows.Forms.Button();
            this.btGravar = new System.Windows.Forms.Button();
            this.nomeTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ligacoesCb);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.bdcb);
            this.groupBox2.Controls.Add(this.removeLink);
            this.groupBox2.Controls.Add(this.listLinks);
            this.groupBox2.Controls.Add(this.editBT);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F);
            this.groupBox2.Location = new System.Drawing.Point(20, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 302);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editar Ligacoes";
            // 
            // ligacoesCb
            // 
            this.ligacoesCb.FormattingEnabled = true;
            this.ligacoesCb.Location = new System.Drawing.Point(103, 67);
            this.ligacoesCb.Name = "ligacoesCb";
            this.ligacoesCb.Size = new System.Drawing.Size(164, 27);
            this.ligacoesCb.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ligação:";
            // 
            // bdcb
            // 
            this.bdcb.FormattingEnabled = true;
            this.bdcb.Location = new System.Drawing.Point(103, 34);
            this.bdcb.Name = "bdcb";
            this.bdcb.Size = new System.Drawing.Size(164, 27);
            this.bdcb.TabIndex = 5;
            this.bdcb.SelectedIndexChanged += new System.EventHandler(this.bdcb_SelectedIndexChanged);
            // 
            // removeLink
            // 
            this.removeLink.Location = new System.Drawing.Point(275, 100);
            this.removeLink.Name = "removeLink";
            this.removeLink.Size = new System.Drawing.Size(84, 27);
            this.removeLink.TabIndex = 4;
            this.removeLink.Text = "Remover ";
            this.removeLink.UseVisualStyleBackColor = true;
            this.removeLink.Click += new System.EventHandler(this.removeLink_Click);
            // 
            // listLinks
            // 
            this.listLinks.FormattingEnabled = true;
            this.listLinks.ItemHeight = 19;
            this.listLinks.Location = new System.Drawing.Point(12, 100);
            this.listLinks.Name = "listLinks";
            this.listLinks.Size = new System.Drawing.Size(255, 194);
            this.listLinks.TabIndex = 3;
            // 
            // editBT
            // 
            this.editBT.Location = new System.Drawing.Point(275, 67);
            this.editBT.Name = "editBT";
            this.editBT.Size = new System.Drawing.Size(84, 27);
            this.editBT.TabIndex = 2;
            this.editBT.Text = "Adicionar";
            this.editBT.UseVisualStyleBackColor = true;
            this.editBT.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Base Dados:";
            // 
            // selectFile
            // 
            this.selectFile.DefaultExt = "xlsx";
            this.selectFile.FileName = "Ficheiro";
            this.selectFile.Title = "Selecionar Ficheiro";
            this.selectFile.FileOk += new System.ComponentModel.CancelEventHandler(this.selectFile_FileOk);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rmvMail);
            this.groupBox3.Controls.Add(this.mailsList);
            this.groupBox3.Controls.Add(this.addMail);
            this.groupBox3.Controls.Add(this.mailtxt);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 12F);
            this.groupBox3.Location = new System.Drawing.Point(391, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 302);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Editar Mails";
            // 
            // rmvMail
            // 
            this.rmvMail.Location = new System.Drawing.Point(275, 68);
            this.rmvMail.Name = "rmvMail";
            this.rmvMail.Size = new System.Drawing.Size(84, 27);
            this.rmvMail.TabIndex = 4;
            this.rmvMail.Text = "Remover ";
            this.rmvMail.UseVisualStyleBackColor = true;
            this.rmvMail.Click += new System.EventHandler(this.rmvMail_Click);
            // 
            // mailsList
            // 
            this.mailsList.FormattingEnabled = true;
            this.mailsList.ItemHeight = 19;
            this.mailsList.Location = new System.Drawing.Point(12, 68);
            this.mailsList.Name = "mailsList";
            this.mailsList.Size = new System.Drawing.Size(255, 213);
            this.mailsList.TabIndex = 3;
            // 
            // addMail
            // 
            this.addMail.Location = new System.Drawing.Point(275, 33);
            this.addMail.Name = "addMail";
            this.addMail.Size = new System.Drawing.Size(84, 27);
            this.addMail.TabIndex = 2;
            this.addMail.Text = "Adicionar";
            this.addMail.UseVisualStyleBackColor = true;
            this.addMail.Click += new System.EventHandler(this.addMail_Click);
            // 
            // mailtxt
            // 
            this.mailtxt.Location = new System.Drawing.Point(76, 33);
            this.mailtxt.Name = "mailtxt";
            this.mailtxt.Size = new System.Drawing.Size(191, 27);
            this.mailtxt.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mail:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Link:";
            // 
            // pathTxt
            // 
            this.pathTxt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathTxt.Location = new System.Drawing.Point(54, 24);
            this.pathTxt.Name = "pathTxt";
            this.pathTxt.Size = new System.Drawing.Size(511, 27);
            this.pathTxt.TabIndex = 6;
            // 
            // selectBT
            // 
            this.selectBT.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectBT.Location = new System.Drawing.Point(571, 23);
            this.selectBT.Name = "selectBT";
            this.selectBT.Size = new System.Drawing.Size(84, 27);
            this.selectBT.TabIndex = 8;
            this.selectBT.Text = "Selecionar";
            this.selectBT.UseVisualStyleBackColor = true;
            this.selectBT.Click += new System.EventHandler(this.selectBT_Click);
            // 
            // btGravar
            // 
            this.btGravar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGravar.Location = new System.Drawing.Point(661, 23);
            this.btGravar.Name = "btGravar";
            this.btGravar.Size = new System.Drawing.Size(84, 27);
            this.btGravar.TabIndex = 9;
            this.btGravar.Text = "Gravar";
            this.btGravar.UseVisualStyleBackColor = true;
            this.btGravar.Click += new System.EventHandler(this.btGravar_Click);
            // 
            // nomeTxt
            // 
            this.nomeTxt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomeTxt.Location = new System.Drawing.Point(73, 57);
            this.nomeTxt.Name = "nomeTxt";
            this.nomeTxt.Size = new System.Drawing.Size(214, 27);
            this.nomeTxt.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Nome:";
            // 
            // EditarLinks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 413);
            this.Controls.Add(this.nomeTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btGravar);
            this.Controls.Add(this.selectBT);
            this.Controls.Add(this.pathTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditarLinks";
            this.Text = "EditarLinks";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listLinks;
        private System.Windows.Forms.Button editBT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog selectFile;
        private System.Windows.Forms.Button removeLink;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button rmvMail;
        private System.Windows.Forms.ListBox mailsList;
        private System.Windows.Forms.Button addMail;
        private System.Windows.Forms.TextBox mailtxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pathTxt;
        private System.Windows.Forms.Button selectBT;
        private System.Windows.Forms.Button btGravar;
        private System.Windows.Forms.ComboBox ligacoesCb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox bdcb;
        private System.Windows.Forms.TextBox nomeTxt;
        private System.Windows.Forms.Label label5;
    }
}