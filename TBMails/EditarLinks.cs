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
    public partial class EditarLinks : Form
    {
        private Links.Link _link;
        private BaseDados _Bds;
        private bool ativado;
        public bool Criado{get{  return ativado;   }  }

        public EditarLinks(int id)
        {
            InitializeComponent();

            _link = new Links.Link(id);
            RefreshList();
            RefreshMails();
            pathTxt.Text = _link.Path;
            nomeTxt.Text = _link.Name;
            init();
        }
        public EditarLinks()
        {
            InitializeComponent();
            _link = new Links.Link();
            init();

        }
        private void init() {
            _Bds = new BaseDados();
            bdcb.DataSource = _Bds.ListaBaseDados;
            bdcb.DisplayMember = "Name";
            bdcb.ValueMember = "Id";

            ativado = true;
        }

         List<Ligacoes.Ligacao> tmpList =new List<Ligacoes.Ligacao>();
        private void button2_Click(object sender, EventArgs e)
        {
            if (_link.Id == 0)
            {
                BaseDados.BD item = bdcb.SelectedItem as BaseDados.BD;
                tmpList.Add(ligacoesCb.SelectedItem as Ligacoes.Ligacao);
                RefreshList();
            }
            else
            {
                _link.addLigacao(ligacoesCb.SelectedItem as Ligacoes.Ligacao, bdcb.SelectedItem as BaseDados.BD);

                RefreshList();
            }
        }

      
        private void addtxt_DoubleClick(object sender, EventArgs e)
        {
            selectFile.ShowDialog();
        }

        private void selectFile_FileOk(object sender, CancelEventArgs e)
        {
            pathTxt.Text = selectFile.FileName;
            nomeTxt.Text = System.IO.Path.GetFileNameWithoutExtension(selectFile.FileName);
        }
        private void RefreshList()
        {
            if (_link.Id == 0)
            {
                listLinks.DataSource = null;
                listLinks.DataSource = tmpList;
                listLinks.ValueMember = "NameBD";
                listLinks.DisplayMember = "Name";

            }
            else
            {
                listLinks.DataSource = null;
                listLinks.DataSource = _link.Ligacoes;
                listLinks.DisplayMember = "Name";
                listLinks.ValueMember = "Id";
            }
        }
        private void RefreshMails()
        {
            mailsList.DataSource = null;

            mailsList.DataSource = _link.ListaMails;
            mailsList.DisplayMember = "Email";
            mailsList.ValueMember = "Id";
        }
        private void removeLink_Click(object sender, EventArgs e)
        {
            //Links.Link item = listLinks.SelectedItem as Links.Link;
            //item.remove_link(item.Id);
            _link.removerLigacao(ligacoesCb.SelectedItem as Ligacoes.Ligacao);
            RefreshList();
        }

        private void addtxt_TextChanged(object sender, EventArgs e)
        {
            if (pathTxt.Text == "")
            {
                selectBT.Text = "Selecionar";

            }
            else
            {
                selectBT.Text = "Adicionar";
            }

        }

        private void bdcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLigacoes();
        }
        private void RefreshLigacoes()
        {


            BaseDados.BD item = bdcb.SelectedItem as BaseDados.BD;
            Ligacoes liga = new Ligacoes(item.Id);
            ligacoesCb.DataSource = liga.ListaLigacoes;
            ligacoesCb.DisplayMember = "Name";
            ligacoesCb.ValueMember = "Id";
            ligacoesCb.Refresh();
        }

        private void addMail_Click(object sender, EventArgs e)
        {
            if (_link.Id == 0) {
                mailsList.Items.Add(mailtxt.Text);
            }
            else
            {
                Links.Link.Mails.Mail mail = new Links.Link.Mails.Mail(_link.Id);
                mail.NovoMail(mailtxt.Text);
                RefreshMails();
            }
        }

        private void rmvMail_Click(object sender, EventArgs e)
        {
            if (_link.Id == 0) {
                mailsList.Items.Remove(mailsList.Items[mailsList.SelectedIndex]);
            }
            else
            {
                Links.Link.Mails.Mail mail = mailsList.SelectedItem as Links.Link.Mails.Mail;
                mail.RemoverMail();
                RefreshMails();
            }
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            if (_link.Id == 0)
            {
                registar();
            } else {
                gravar();
            }

        }

        private void gravar()
        {
            _link.update_link(pathTxt.Text, nomeTxt.Text);
            this.Close();
        }
        private void registar()
        {
           int n = mailsList.Items.Count;
           List<string> mails = new List<string>();
            for (int i = 0; i < n; i++)
            {
                mails.Add(mailsList.Items[i].ToString());
            }

            _link.new_link(pathTxt.Text,nomeTxt.Text, tmpList,mails);
            this.Close();
        }

        private void selectBT_Click(object sender, EventArgs e)
        {
            if (pathTxt.Text == "")
            {
                selectFile.ShowDialog();
            }
           
        }


    }
}
