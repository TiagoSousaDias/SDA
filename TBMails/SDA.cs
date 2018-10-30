using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace TBMails
{
    public partial class SDA : Form
    {
        //private EditarLinks editForm;
        //private EditBD editBDForm;
        private Dashboard dashboardform;
        private Download downloadForm;
        private TBs tbsForm;
        //private Links lista;
        //private BaseDados _BaseDados;
        private string tempPath = "";
        private string MainFolderPath = "";

        Serializer ser = new Serializer();
        public AutomatismosClass autos;
        List<Alertas> ListaAlertas = new List<Alertas>();

        public SDA() => InitializeComponent();

        private void TBMail_Load(object sender, EventArgs e)
        {
            setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
            mainPathtxt.Text = setts.MainFolder;
            //caso o ficheiro seja apagado descomentar linhas abaixo
            //GetAutomatismo();
            //File.WriteAllText(@"Automatismo.xml", (string)ser.Serialize<AutomatismosClass>(AutoStruture));
            RefreshListTB();
           
        }

        private void GetGraphics()
        {
            foreach (AutomatismosClass.Automatismo a in autos.Automatismos)
            {
                foreach (AutomatismosClass.Automatismo.Ficheiro f in a.Ficheiros)
                {
                    foreach (AutomatismosClass.Automatismo.Ficheiro.Consulta item in f.Ligacoes)
                    {
                        if (item.Range != null && item.Range != "") item.Image = GetImageFromExcel(a.Path + "\\01. Script\\" + f.Nome, item.Range);
                    }
                }
            }
        }
        private void RefreshListTB()
        {
            autos = ser.Deserialize<AutomatismosClass>((string)File.ReadAllText(@"Automatismo.xml"));
            fileList.DataSource = null;
            fileList.DataSource = autos.Automatismos;
            fileList.DisplayMember = "Nome";
            fileList.ValueMember = "Path";
        }

        private void dashboardBT_Click(object sender, EventArgs e)
        {
            if (this.dashboardform == (null) || dashboardform.IsDisposed)
            {
                createDashboard();

            }
            ckdBT.BackColor = defaultColor;
            tbsBT.BackColor = defaultColor;
            downloadBT.BackColor = defaultColor;
            dashboardBT.BackColor = selectedColor;
            if (!dashboardform.Visible) dashboardform.Show();
            if (downloadForm != (null)) { if (downloadForm.Visible) downloadForm.Hide(); }
            if (tbsForm != (null)) { if (tbsForm.Visible) tbsForm.Hide(); }
        }
        private void dashboardform_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Do your stuff here.

        }
        private void createDashboard()
        {
            dashboardform = new Dashboard();
            dashboardform.Size = this.Size;
            dashboardform.Location = new System.Drawing.Point(0, 40);
            dashboardform.TopLevel = false;
            dashboardform.Parent = this;
            dashboardform.Anchor = AnchorStyles.Top & AnchorStyles.Bottom & AnchorStyles.Right & AnchorStyles.Left;
            dashboardform.BringToFront();
            dashboardform.FormClosing += new FormClosingEventHandler(this.dashboardform_FormClosing);
            dashboardform.VisibleChanged += new EventHandler(this.RefreshAlertas);
            dashboardform.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
        }
        private void createDownload()
        {
            downloadForm = new Download();
            downloadForm.Size = this.Size;
            downloadForm.Location = new System.Drawing.Point(0, 40);
            downloadForm.TopLevel = false;
            downloadForm.Parent = this;
            downloadForm.Anchor = AnchorStyles.Top & AnchorStyles.Bottom & AnchorStyles.Right & AnchorStyles.Left;
            downloadForm.BringToFront();

        }
        private void createTBs()
        {
            tbsForm = new TBs();
            tbsForm.Size = this.Size;
            tbsForm.Location = new System.Drawing.Point(0, 40);
            tbsForm.TopLevel = false;
            tbsForm.Parent = this;
            tbsForm.Anchor = AnchorStyles.Top & AnchorStyles.Bottom & AnchorStyles.Right & AnchorStyles.Left;
            tbsForm.BringToFront();

        }
        private WebDownload webdownform;
        private void createWeb()
        {
            webdownform = new WebDownload();
            webdownform.Size = this.Size;
            webdownform.Location = new System.Drawing.Point(0, 40);
            webdownform.TopLevel = false;
            webdownform.Parent = this;
            webdownform.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;

            webdownform.BringToFront();

        }
        private void checkFile_Click(object sender, EventArgs e)
        {
            //RefreshAutos();
        }

        public Settings setts;
        AutomatismosClass AutoStruture;
        private void GetAutomatismo()
        {
            List<string> Folders = SearchFolder.GetDirectories(setts.MainFolder);
            AutoStruture = new AutomatismosClass();
            AutoStruture.Automatismos = new List<AutomatismosClass.Automatismo>();
            foreach (var Folder in Folders)
            {
                try
                {
                    string[] ck = Folder.Split('\\');
                    ck = ck[ck.Length - 1].Split('.');
                    if (ck[0].Length < 5 && typeof(int) == Int32.Parse(ck[0]).GetType()) {
                     AutomatismosClass.Automatismo tempAuto = GetFilesForStruct(Folder);
                        if(tempAuto.Ficheiros.Count > 0)   AutoStruture.Automatismos.Add(GetFilesForStruct(Folder));
                    }
                }
                catch (Exception)
                {
                    //throw;
                }
            }
        }

        private AutomatismosClass.Automatismo GetFilesForStruct(string Folder)
        {
            string[] AutoNameTemp = Folder.Split('\\');
            List<string> Folders = SearchFolder.GetDirectories(Folder , "*Script");
            AutomatismosClass.Automatismo item = new AutomatismosClass.Automatismo() { Nome = AutoNameTemp[AutoNameTemp.Length - 1], Path = Folder, Ficheiros = new List<AutomatismosClass.Automatismo.Ficheiro>() };

            foreach (var file in new DirectoryInfo(@"" + Folders[0]).GetFiles("Auto*").Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)))
            {
                item.Ficheiros.Add(new AutomatismosClass.Automatismo.Ficheiro()
                {
                    Nome = file.Name,
                    Ligacoes = GetLigacoesFromFile(file.FullName),
                    ConfigHorario_Range = ""
          
                });
                ListaAlertas.Add(new Alertas() { Ficheiro = file.Name, Campo = "EmailsGC", Alerta = TipoAlerta.Aviso });
                ListaAlertas.Add(new Alertas() { Ficheiro = file.Name, Campo = "EmailsRE", Alerta = TipoAlerta.Aviso });
                ListaAlertas.Add(new Alertas() { Ficheiro = file.Name, Campo = "EmailsSup", Alerta = TipoAlerta.Aviso });

            }
            return item;
        }
        private List<AutomatismosClass.Automatismo.Ficheiro.Consulta> GetLigacoesFromFile(string file)
        {
            ExcelLink Tb = new ExcelLink(file,true);
            List<AutomatismosClass.Automatismo.Ficheiro.Consulta> Ligs = new List<AutomatismosClass.Automatismo.Ficheiro.Consulta>();
            Range r = Tb.GetRange("C6", "R25");
            foreach (Range item in r.Rows)
            {

                try
                {

               
                if (item.Cells[1, 3].Value == null) break;
                    AutomatismosClass.Automatismo.Ficheiro.Consulta Lig = new AutomatismosClass.Automatismo.Ficheiro.Consulta()
                    {
                        Nome = item.Cells[1, 3].Value,
                        Ok = item.Cells[1, 1].Value,
                        VOlBD_Export = item.Cells[1, 4].Value,
                        Seq_Data_Report = item.Cells[1, 5].Value,
                        Range = (autos.Automatismos.FirstOrDefault(x =>x.Ficheiros.Contains( x.Ficheiros.FirstOrDefault(y => y.Ligacoes.Contains(y.Ligacoes.FirstOrDefault(z => z.Nome == item.Cells[1, 3].Value))))).Ficheiros.FirstOrDefault(x => x.Ligacoes.Contains(x.Ligacoes.FirstOrDefault(z => z.Nome == item.Cells[1, 3].Value))).Ligacoes.FirstOrDefault(x=>x.Nome == item.Cells[1, 3].Value).Range),
                   // Image = (Range != null )
                    DA_Mod = item.Cells[1, 9].Value.ToString("dd/MM/yyyy"),
                    FO_Mod = item.Cells[1, 15].Value,
                    FO_Trans = (item.Cells[1, 13].value == null ? "" : item.Cells[1, 13].Value.ToString("dd/MM/yyyy"))
                };
                Ligs.Add(Lig);
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            Tb.closeFile(false);

            return Ligs;
        }

        private void runScriptBT_Click(object sender, EventArgs e)
        {
            //GetAutomatismo();
            RefreshAutos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAutomatismo();

            if (!autos.Equals(AutoStruture))
            {
                File.WriteAllText(@"Automatismo.xml", (string)ser.Serialize<AutomatismosClass>(AutoStruture));

            }
            RefreshListTB();
        }
        private void RefreshAlertas(object sender, EventArgs e)
        {
            dashboardform.avisos.DataSource = null;
            dashboardform.avisos.DataSource = ListaAlertas;
            dashboardform.avisos.DisplayMember = "Text";
            // dashboardform.lastFiles.ValueMember = "Path";
            foreach (AutomatismosClass.Automatismo item in autos.Automatismos)
            {
                foreach (AutomatismosClass.Automatismo.Ficheiro  file in item.Ficheiros)
                {
                    foreach (AutomatismosClass.Automatismo.Ficheiro.Consulta c in file.Ligacoes)
                    {
                        Image i;
                        try { i = c.Image;  } catch (Exception) { i = null; }
                        if (i != null)
                        {
                            PictureBox pic = new PictureBox();
                            pic.Image = i;
                            pic.Size = i.Size;
                            dashboardform.graphList.Controls.Add(pic);
                        }
                    }
                    
                }
            }
        }

        private void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //tbfileslist.Items.Clear();
                detalhesTB.Visible = true;
                detalhesTB.Enabled = true;
                AutomatismosClass.Automatismo tb = fileList.SelectedItem as AutomatismosClass.Automatismo;
                MainFolderPath = tb.Path + "\\01. Script\\";
                tbfileslist.DataSource = tb.Ficheiros;
                tbfileslist.DisplayMember = "Nome";
                pathTxt.Text = tb.Path;
                ResetTBViewer();
            }
            catch (Exception)
            {

            }

        }

        private void tbfileslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            webDownBt.Visible = true;
            webDownBt.Enabled = true;
            AutomatismosClass.Automatismo.Ficheiro file = tbfileslist.SelectedItem as AutomatismosClass.Automatismo.Ficheiro;

            tempPath = MainFolderPath+ file.Nome;
            ligacoeList.DataSource = file.Ligacoes;
            ligacoeList.DisplayMember = "Nome";
          
            if(file.ConfigHorario_Range != "" && file.ConfigHorario_Range !=  null) configHorpic.Image = GetImageFromExcel(tempPath,file.ConfigHorario_Range); configHorarioTxt.Text = file.ConfigHorario_Range;
        }
        private Image GetImageFromExcel(string path,string range)
        {
            Image i = null;
            ExcelLink teste = null;
            try
            {
                 teste = new ExcelLink(tempPath,true);
              
            }
            catch (Exception  )
            {
                MessageBox.Show("Ficheiro:" + path + "\n Não encontrado");
                ListaAlertas.Add(new Alertas() { Alerta = TipoAlerta.Erro, Ficheiro = path, TimeError = DateTime.Now });
               
                //throw ex ;
            }
          //  teste.copyChart();
            if (teste != null)
            {
                i = teste.CopyImage(range); rngAl.Visible = false;
                teste.closeFile(false);
            }
            else
            {
               // GetAutomatismo();
            }  
           
            return i;
        }
        private void ResetTBViewer()
        {
            webDownBt.Visible = false;
            webDownBt.Enabled = false;
            rngAl.Visible = false;
            rngLB.Visible = false;
            rngTxt.Visible = false;
            CKFtxt.Visible = false;
            ckLB.Visible = false;
            ckLB.Visible = false;
            pictureBox1.Image = null;
            FO_modtxt.Visible = false;
            FO_transTxt.Visible = false;
            DA_modtxt.Visible = false;
        }
        private void ShowConn()
        {
            rngLB.Visible = true;
            rngTxt.Visible = true;
            CKFtxt.Visible = true;
            ckLB.Visible = true;
            ckLB.Visible = true;
            CKFtxt.Visible = true;
            FO_modtxt.Visible = true;
            FO_transTxt.Visible = true;
            DA_modtxt.Visible = true;
        }
        private void ligacoeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                AutomatismosClass.Automatismo.Ficheiro.Consulta ligacao = ligacoeList.SelectedItem as AutomatismosClass.Automatismo.Ficheiro.Consulta;
                CKFtxt.Text = ligacao.Ok;
                FO_modtxt.Text = ligacao.FO_Mod;
                FO_transTxt.Text = ligacao.FO_Trans;
                DA_modtxt.Text = ligacao.DA_Mod;
                rngTxt.Text = ligacao.Range;
                if (ligacao.Image != null) pictureBox1.Image = ligacao.Image;
                else if (rngTxt.Text != "")
                {
                    pictureBox1.Image =  GetImageFromExcel(tempPath, rngTxt.Text); rngAl.Visible = false;
                    ligacao.Image = pictureBox1.Image;
                    ShowConn();
                }
                else
                {
                    ListaAlertas.Add(new Alertas() { Alerta = TipoAlerta.Valor_Falta, Campo = "Range", Ficheiro = "Consulta" + ligacao.Nome });
                    rngAl.Visible = true;
                    ShowConn();
                }
            }
            catch (Exception ex)    
            {
               Console.WriteLine( ex.Message);
            }
        }

        private void RefreshAutos()
        {
            List<string> Folders = SearchFolder.GetDirectories(setts.MainFolder);
            foreach (var folder in Folders)
            {
                AutomatismosClass.Automatismo t = autos.Automatismos.Find(x => x.Path.Equals(folder));
                if (t == null)
                {
                    //Adicionar Nova pasta á strutura
                }
                else
                {
                    foreach (var file in new DirectoryInfo(@"" + t.Path + "\\01. Script").GetFiles().Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)))
                    {
                        AutomatismosClass.Automatismo.Ficheiro f = t.Ficheiros.Find(x => x.Nome.Equals(file.Name));
                        if (f == null)
                        { //Adicionar Ficheiro

                        }
                        else
                        {
                            ExcelLink teste = new ExcelLink(t.Path + "\\01. Script\\" + f.Nome);
                            Range r = teste.GetRange("C6:E36", "");
                            foreach (Range item in r.Rows)
                            {
                                if (item.Cells[1, 3].Value == null) break;
                                AutomatismosClass.Automatismo.Ficheiro.Consulta lig = f.Ligacoes.Find(l => l.Nome.Equals(item.Cells[1, 3].Value));
                                if (lig == null)
                                { //Adicionar Consulta
                                }
                                else
                                {
                                    lig.Nome = item.Cells[1, 3].Value;
                                    lig.Ok = item.Cells[1, 1].Value;
                                    lig.VOlBD_Export = item.Cells[1, 4].Value;
                                    lig.Seq_Data_Report = item.Cells[1, 5].Value;
                                    lig.DA_Mod = item.Cells[1, 9].Value.ToString("dd/MM/yyyy");
                                    lig.FO_Mod = item.Cells[1, 15].Value;
                                    lig.FO_Trans = (item.Cells[1, 13].value == null ? "" : item.Cells[1, 13].Value.ToString("dd/MM/yyyy"));
                                    if (lig.Range != "") lig.Image = teste.CopyImage(r, lig.Range);
                                    teste.closeFile(false);
                                }

                            }
                        }
                    }

                }
            }


        }
        private void gravar_Click(object sender, EventArgs e)
        {
            AutomatismosClass.Automatismo tb = fileList.SelectedItem as AutomatismosClass.Automatismo;
            AutomatismosClass.Automatismo.Ficheiro f = tbfileslist.SelectedItem as AutomatismosClass.Automatismo.Ficheiro;
            AutomatismosClass.Automatismo.Ficheiro.Consulta c = ligacoeList.SelectedItem as AutomatismosClass.Automatismo.Ficheiro.Consulta;
            c.Range = rngTxt.Text;
            File.WriteAllText(@"Automatismo.xml", (string)ser.Serialize<AutomatismosClass>(autos));


        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.downloadForm == (null) || downloadForm.IsDisposed)
            {
                createDownload();
                downloadForm.pathUploadTxt.Text = setts.DownloadFolder;

            }
            ckdBT.BackColor = defaultColor;
            tbsBT.BackColor = defaultColor;
            downloadBT.BackColor = selectedColor;
            dashboardBT.BackColor = defaultColor;
            if (!downloadForm.Visible) downloadForm.Show();
            if (tbsForm != (null)) { if (tbsForm.Visible) tbsForm.Hide(); }
            if (dashboardform != (null)) { if (dashboardform.Visible) dashboardform.Hide(); }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.tbsForm == (null) || tbsForm.IsDisposed)
            {
                createTBs();

            }
            ckdBT.BackColor = defaultColor;
            tbsBT.BackColor = selectedColor;
            downloadBT.BackColor = defaultColor;
            dashboardBT.BackColor = defaultColor;
            if (!tbsForm.Visible) tbsForm.Show();
            if (downloadForm != (null)) { if (downloadForm.Visible) downloadForm.Hide(); }
            if (dashboardform != (null)) { if (dashboardform.Visible) dashboardform.Hide(); }
        }
        private Color selectedColor = System.Drawing.SystemColors.ActiveCaption;
        private Color defaultColor = System.Drawing.SystemColors.Control;
        private void button3_Click(object sender, EventArgs e)
        {
            ckdBT.BackColor = selectedColor;
            tbsBT.BackColor = defaultColor;
            downloadBT.BackColor = defaultColor;
            dashboardBT.BackColor = defaultColor;
            webDownBt.BackColor = defaultColor;
            if (tbsForm != (null)) { if (tbsForm.Visible) tbsForm.Hide(); }
            if (downloadForm != (null)) { if (downloadForm.Visible) downloadForm.Hide(); }
            if (dashboardform != (null)) { if (dashboardform.Visible) dashboardform.Hide(); }
            if (webdownform != (null)) { if (webdownform.Visible) webdownform.Hide(); }

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.webdownform == (null) || webdownform.IsDisposed) createWeb();

            ckdBT.BackColor = defaultColor;
            tbsBT.BackColor = defaultColor;
            downloadBT.BackColor = defaultColor;
            dashboardBT.BackColor = defaultColor;
            webDownBt.BackColor = selectedColor;

            if (!webdownform.Visible) webdownform.Show();
            if (tbsForm != (null)) { if (tbsForm.Visible) tbsForm.Hide(); }
            if (downloadForm != (null)) { if (downloadForm.Visible) downloadForm.Hide(); }
            if (dashboardform != (null)) { if (dashboardform.Visible) dashboardform.Hide(); }
            
        }
        private void mainPathBT_Click(object sender, EventArgs e)
        {
            if (mainPathtxt.Text != "") { mainPathDialog.SelectedPath = mainPathtxt.Text; }
            DialogResult result = mainPathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mainPathtxt.Text = mainPathDialog.SelectedPath;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            setts.MainFolder = mainPathtxt.Text;
            File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            AutomatismosClass.Automatismo tb = fileList.SelectedItem as AutomatismosClass.Automatismo;
            AutomatismosClass.Automatismo.Ficheiro f = tbfileslist.SelectedItem as AutomatismosClass.Automatismo.Ficheiro;
            f.ConfigHorario_Range = configHorarioTxt.Text;
            File.WriteAllText(@"Automatismo.xml", (string)ser.Serialize<AutomatismosClass>(autos));
        }

        private void AccoesGB_Enter(object sender, EventArgs e)
        {

        }
    }
}