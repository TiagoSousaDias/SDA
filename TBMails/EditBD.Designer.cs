namespace TBMails
{
    partial class EditBD
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
            this.gravar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nomeBD = new System.Windows.Forms.TextBox();
            this.ligacoesList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nomeLig = new System.Windows.Forms.TextBox();
            this.addLigacao = new System.Windows.Forms.Button();
            this.pathTXT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rmvLig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gravar
            // 
            this.gravar.Location = new System.Drawing.Point(476, 245);
            this.gravar.Name = "gravar";
            this.gravar.Size = new System.Drawing.Size(75, 23);
            this.gravar.TabIndex = 5;
            this.gravar.Text = "Gravar";
            this.gravar.UseVisualStyleBackColor = true;
            this.gravar.Click += new System.EventHandler(this.gravar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome:";
            // 
            // nomeBD
            // 
            this.nomeBD.Location = new System.Drawing.Point(98, 26);
            this.nomeBD.Name = "nomeBD";
            this.nomeBD.Size = new System.Drawing.Size(453, 20);
            this.nomeBD.TabIndex = 1;
            // 
            // ligacoesList
            // 
            this.ligacoesList.FormattingEnabled = true;
            this.ligacoesList.Location = new System.Drawing.Point(98, 73);
            this.ligacoesList.Name = "ligacoesList";
            this.ligacoesList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ligacoesList.Size = new System.Drawing.Size(418, 134);
            this.ligacoesList.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ligações:";
            // 
            // nomeLig
            // 
            this.nomeLig.Location = new System.Drawing.Point(98, 213);
            this.nomeLig.Name = "nomeLig";
            this.nomeLig.Size = new System.Drawing.Size(280, 20);
            this.nomeLig.TabIndex = 3;
            // 
            // addLigacao
            // 
            this.addLigacao.Location = new System.Drawing.Point(384, 211);
            this.addLigacao.Name = "addLigacao";
            this.addLigacao.Size = new System.Drawing.Size(167, 23);
            this.addLigacao.TabIndex = 4;
            this.addLigacao.Text = "Adicionar Ligação";
            this.addLigacao.UseVisualStyleBackColor = true;
            this.addLigacao.Click += new System.EventHandler(this.addLigacao_Click);
            // 
            // pathTXT
            // 
            this.pathTXT.Location = new System.Drawing.Point(98, 50);
            this.pathTXT.Name = "pathTXT";
            this.pathTXT.Size = new System.Drawing.Size(453, 20);
            this.pathTXT.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Caminho:";
            // 
            // rmvLig
            // 
            this.rmvLig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rmvLig.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rmvLig.Location = new System.Drawing.Point(522, 76);
            this.rmvLig.Name = "rmvLig";
            this.rmvLig.Size = new System.Drawing.Size(29, 26);
            this.rmvLig.TabIndex = 9;
            this.rmvLig.Text = "-";
            this.rmvLig.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rmvLig.UseVisualStyleBackColor = true;
            this.rmvLig.Click += new System.EventHandler(this.rmvLig_Click);
            // 
            // EditBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 297);
            this.Controls.Add(this.rmvLig);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pathTXT);
            this.Controls.Add(this.addLigacao);
            this.Controls.Add(this.nomeLig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ligacoesList);
            this.Controls.Add(this.nomeBD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gravar);
            this.Name = "EditBD";
            this.Text = "EditBD";
            this.Load += new System.EventHandler(this.EditBD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button gravar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nomeBD;
        private System.Windows.Forms.ListBox ligacoesList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nomeLig;
        private System.Windows.Forms.Button addLigacao;
        private System.Windows.Forms.TextBox pathTXT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button rmvLig;
    }
}