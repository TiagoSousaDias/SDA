using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TBMails
{
    public partial class EditBD : Form
    {
        private BaseDados.BD _BD;
        private int IDBD;

        public EditBD(int ID)
        {
            InitializeComponent();
            IDBD = ID;
            _BD = new BaseDados.BD(ID);
            RefreshList();


        } 
        public EditBD()
        {
            InitializeComponent();
            _BD = new BaseDados.BD();
           
        }

        private void EditBD_Load(object sender, EventArgs e)
        {
            if (IDBD > 0)
            {
                nomeBD.Text = _BD.Name;
                pathTXT.Text = _BD.Path;
            }
        }

        private void addLigacao_Click(object sender, EventArgs e)
        {
            if (IDBD == 0)
            {      
                ligacoesList.Items.Add(nomeLig.Text);
            }
            else
            {
                Ligacoes.Ligacao novo = new Ligacoes.Ligacao(IDBD);
                novo.NovaLigacao(nomeLig.Text);
                RefreshList();
            }
        }
        private void RefreshList()
        {
            ligacoesList.DataSource = null;
            _BD.Ligacoes.refresh_links();
            ligacoesList.DataSource = _BD.Ligacoes.ListaLigacoes;
            ligacoesList.DisplayMember = "Name";
            ligacoesList.ValueMember = "Id";
        }

        private void gravar_Click(object sender, EventArgs e)
        {
            if (IDBD == 0) {
                _BD.new_BD(nomeBD.Text, pathTXT.Text);
                string[] Names = new string[ligacoesList.Items.Count ];
              
            // Ligacoes.Ligacao items = new Ligacoes.Ligacao();

                foreach (var item in ligacoesList.Items)
                {
                     _BD.Ligacoes.CriarLigacao(item.ToString());
                }
                this.Close();
               // Names[]);
                //_BD.Criar_Ligacoes(Names);
            } else {
                _BD.update_BD(nomeBD.Text, pathTXT.Text,_BD.Id);
                this.Close();
            }
            
        }

        private void rmvLig_Click(object sender, EventArgs e)
        {
            if (IDBD == 0) {
                ligacoesList.Items.Remove(ligacoesList.Items[ligacoesList.SelectedIndex]);

            }
            else
            {
                foreach (Ligacoes.Ligacao item in ligacoesList.SelectedItems)
                {
                    item.RemoverLigacao(item.Id);
                }
                //  Ligacoes.Ligacao item = ligacoesList.SelectedItem as Ligacoes.Ligacao;

                RefreshList();
            }
        }
    }
}
