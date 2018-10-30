using OpenQA.Selenium.Chrome;
using System;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Security.Permissions;
using System.Runtime.InteropServices;

using CefSharp.WinForms;
using CefSharp;
using System.Threading;
using System.Collections.Generic;
using CefSharp.Example;
using System.IO;
using System.Linq;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Reflection;
using SmartDataAnalysis;
using Microsoft.VisualBasic.CompilerServices;

namespace TBMails
{
    //{

    //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    //[ComVisible(true)]
   
    public partial class WebDownload : Form
    {
        #region Vars
        Thread CGIScript, S3Script;
        Thread del;
        DateTime nextRefreshCGI, nextRefreshS3;
        bool ctrlDelCGI = true, ctrlDelS3 = true;
        DateTime delaytimeCGI = DateTime.Now.AddSeconds(10);
        DateTime delaytimeS3 = DateTime.Now.AddSeconds(10);
        IWebDriver CGI, S3,S3UP;
        bool ctrlCGI = true, ctrlS3 = true;
        private List<Alertas> CGIalertas,S3Alertas;
        private List<Settings.Links> files;
        private List<Settings.Clientes> Clientes;
        private List<Settings.UploadFile> UpFiles;
        private IniFile DownSetts;
        private Settings setts;
        private Serializer ser = new Serializer();
        BindingSource CGIAlertasSource = new BindingSource(), S3AlertasSource = new BindingSource();

        #endregion
        public WebDownload() => InitializeComponent();

        private void WebDownload_Load(object sender, EventArgs e)
        {
            setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
            //setts.UploadFiles.Add( new Settings.UploadFile() {  Documento ="",Active=""});
            //File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
            CGIalertas = new List<Alertas>();
            S3Alertas = new List<Alertas>();
            DownSetts = new IniFile("DownloadSettings.ini");
         //   DownSetts.Write("CGI", "");
            LoadDownloadConfigs(userCGI, passCGI, InicioCGI, FimCGI, intervaloCGI, delCGI, pathDownloadCGITxt, "CGI");
            LoadDownloadConfigs(userS3, passS3, InicioS3, FimS3, intervaloS3, delS3, pathDownloadS3, "S3");
            LoadUploadConfigs(UserS3Up,PassS3Up,horaUpS3,"S3");

            PrepareDGV(alertasCGI,CGIAlertasSource, CGIalertas);
            PrepareDGV(alertasS3, S3AlertasSource, S3Alertas);
            
            fillFiLes();
            FillClients();
            FillUpS3Files();
        }
        private void PrepareDGV(DataGridView DGV, BindingSource source, List<Alertas> alertas)
        {
            source.DataSource = alertas;
            DGV.DataSource = source;
            DGV.Columns[1].Visible = false;
            DGV.Columns[3].HeaderText = "Timestamp";
            DGV.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
            DGV.Columns[4].Visible = false;
            source.Sort = "TimeError DESC";
        }
        private void fillFiLes()
        {
            files = setts.DownloadLinks;
            foreach (Settings.Links file in files)
            {
                filesDGV.Rows.Add(file.Link,bool.Parse(file.Active));
            }
        }
        private void FillClients()
        {
            Clientes = setts.ClientesList;
            foreach (Settings.Clientes cliente in Clientes)
            {
                ClientesS3.Rows.Add(cliente.Cliente, bool.Parse(cliente.Active));
            }
        }
        private void FillUpS3Files()
        {
            UpFiles = setts.UploadFiles;
            foreach (Settings.UploadFile file in UpFiles)
            {
                UploadS3DGV.Rows.Add(file.Documento, bool.Parse(file.Refresh), bool.Parse(file.Active));
            }
        }
        #region Common
        private void LoadDownloadConfigs(TextBox user, TextBox pass, TextBox inicio, TextBox fim, TextBox intervalo, TextBox del, TextBox caminho, string Portal)
        {
            user.Text = DownSetts.Read("User", Portal);
            pass.Text = DownSetts.Read("Pass", Portal);
            inicio.Text = DownSetts.Read("Start", Portal);
            fim.Text = DownSetts.Read("Finish", Portal);
            intervalo.Text = DownSetts.Read("Interval", Portal);
            del.Text = DownSetts.Read("Apagar", Portal);
            caminho.Text = DownSetts.Read("Path", Portal);
        }
        private void LoadUploadConfigs(TextBox user, TextBox pass, TextBox inicio, string Portal)
        {
            user.Text = DownSetts.Read("User", Portal);
            pass.Text = DownSetts.Read("Pass", Portal);
            inicio.Text = DownSetts.Read("Hora Upload", Portal);
        }
        private void SaveAndRun(System.Windows.Forms.Timer Timer, string Inicio, string Fim, string Del, string Intervalo, string caminho, Button Run, Button Stop, string Portal)
        {
            try
            {
                DateTime timeDown = DateTime.Parse(Inicio);
                DateTime f = DateTime.Parse(Fim);
                DateTime t = DateTime.Parse(Del);
                Run.Enabled = false;
                Stop.Enabled = true;
                DownSetts.Write("Start", Inicio, Portal );
                if(Fim != null)DownSetts.Write("Finish", Fim, Portal);
                if (Intervalo != null) DownSetts.Write("Interval", Intervalo,Portal);
                if (Del != null) DownSetts.Write("Apagar", Del, Portal);
                if (caminho == "") { throw new Exception("0"); }
                DownSetts.Write("Path", caminho, Portal);
                Timer.Start();
            }
            catch (Exception ex)
            {
                string msg;
                if (ex.Message == "0") { msg = Portal + "Tem de preencher o caminho para descarregar!"; } else { msg = Portal + "Insira uma hora valida! ex: 13:00:00"; }
                MessageBox.Show(msg);

            }
        }
        private void CorrerCGI(object sender, EventArgs e)
        {
            SaveAndRun(CGITimer,InicioCGI.Text, FimCGI.Text, delCGI.Text, intervaloCGI.Text, pathDownloadCGITxt.Text, RunCGI,StopCGI,"CGI");            
        }
        /// <summary>
        /// Função para apagar os ficheiros da pasta selecionada para transferencias
        /// </summary>
        /// <param name="caminho">Pasta dos ficheiros</param>
        private void apagarFicheiros(string caminho, string portal)
        {
            foreach (var file in new DirectoryInfo(caminho).GetFiles()) { file.Delete(); }
            if (portal == "CGI")
            {
                ctrlDelCGI = true;
            }
            else { ctrlDelS3 = true; }
        }
        private void StopTimer(System.Windows.Forms.Timer timer, Button Run, Button Stop, ref bool ctrl, Thread Script, Thread del)
        {
            try
            {
                Run.Enabled = true;
                Stop.Enabled = false;
                timer.Stop();
                ctrl = true;
                if (Script != null) Script.Abort();
                if (del != null) del.Abort();

            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void TimersCheck(IWebDriver Web, string url, string caminho, ref bool CTRL, string Inicio, string Fim, ref DateTime nextDown, DateTime delay, ref bool CTRLDel, ref Thread script, ref Thread del, string Portal)
        {
            try
            {
                //inicio
                MethodInfo mi = this.GetType().GetMethod("Download" + Portal);
                if (CTRL && DateTime.Now.ToShortTimeString() == DateTime.Parse(Inicio).ToShortTimeString())
                {
                    CTRL = false;
                    script = new Thread(new ThreadStart(delegate () { mi.Invoke(this, null); }));
                    script.Start();
                }//fim
                else if (CTRL && DateTime.Now.ToShortTimeString() == DateTime.Parse(Fim).ToShortTimeString())
                {
                    script.Abort();
                    nextDown = DateTime.Now.AddHours(-2);
                    delay = DateTime.Now.AddMinutes(1);
                }
                else
                { //intervalos
                    if (CTRL && DateTime.Now.ToShortTimeString() == nextDown.ToShortTimeString())
                    {
                        CTRL = false;
                        script = new Thread(new ThreadStart(delegate () { mi.Invoke(this, null); }));
                        script.Start();
                    }
                    else if (CTRLDel && DateTime.Now.ToShortTimeString() == DateTime.Parse(delCGI.Text).ToShortTimeString())
                    {
                        CTRLDel = false;
                        del = new Thread(new ThreadStart(delegate { apagarFicheiros(caminho, Portal); }));
                        del.Start();
                    }
                    else
                    {
                        if (script != null) if (script.IsAlive) CGIAlertasSource.ResetBindings(false); CGIAlertasSource.Sort = "TimeError DESC";
                    }
                }
            }catch (Exception ex) {WriteLogFile.WriteLog("Log.txt", String.Format("[{0}]:{1}->Function:TimerCheck():{2}", DateTime.Now, ex.Message, Portal)); }
        }
        #endregion
        #region Browsers
        public void InitializeChrome(ref IWebDriver Web, string url, string path)
        {
            if(Web == null)  Web = new ChromeDriver(System.IO.Directory.GetCurrentDirectory(), getChromeOptions(path));
            Web.Navigate().GoToUrl(url);
            
        }
        private ChromeOptions getChromeOptions(string DownPath)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.SetLoggingPreference(LogType.Driver, LogLevel.All);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("download.default_directory", DownPath);
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            return chromeOptions;
        }
        private void abrirCGI(object sender, EventArgs e) => InitializeChrome(ref CGI, DownSetts.Read("CGI"), DownSetts.Read("Path","CGI"));
        private void S3BrowserBT_Click(object sender, EventArgs e) => InitializeChrome(ref S3, DownSetts.Read("S3"), DownSetts.Read("Path", "S3"));
        #endregion
        #region CGI
        public void LoginCGI()
        {
            try
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> links = CGI.FindElements(By.CssSelector(".ui.fluid.left.icon.input input"));
                links[0].SendKeys(userCGI.Text);
                links[1].SendKeys(passCGI.Text);
                links[0].Submit();
                DownSetts.Write("User", userCGI.Text, "CGI");
                DownSetts.Write("Pass", passCGI.Text, "CGI");
            }
            catch (Exception ex ) { WriteLogFile.WriteLog("Log.txt", String.Format("[{0}]:{1}->Function:LoginCGI", DateTime.Now, ex.Message)); }
        }
        public void DownloadCGI()
        {
            string baseUrl = DownSetts.Read("CGI");
            string mainURL = baseUrl + "download/";
            try { CGI.Navigate().GoToUrl(baseUrl); LoginCGI(); Thread.Sleep(1000); } catch (Exception ex ) {
                WriteLogFile.WriteLog("Log.txt", String.Format("[{0}]:{1}->Function:DownloadCGI", DateTime.Now, ex.Message)); }
            setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
            if (nextRefreshCGI > DateTime.Parse(FimCGI.Text)) { nextRefreshCGI = DateTime.Parse(InicioCGI.Text); } else { nextRefreshCGI = DateTime.Now.AddMinutes(Int32.Parse(intervaloCGI.Text)); }
            try
            {          
                foreach (Settings.Links url in setts.DownloadLinks.Where(x => x.Active == "True"|| x.Active =="true"))
                {
                    CGI.Navigate().GoToUrl(mainURL + url.Link);Thread.Sleep(4000);     
                    IWebElement fileName = CGI.FindElement(By.CssSelector(".ui.small.animated.divided.selection.middle.aligned.list .item .header"));
                    string path = pathDownloadCGITxt.Text + "\\" + fileName.Text;
                    if (File.Exists(path)) { continue; }
                    IWebElement i = CGI.FindElement(By.CssSelector(".ui.green.mini.button"));
                    i.Click(); Thread.Sleep(4000);
                    if (File.Exists(path)) { CGIalertas.Add(new Alertas() { Alerta = TipoAlerta.Sucesso, Ficheiro = fileName.Text, TimeError = DateTime.Now }); }
                    else { CGIalertas.Add(new Alertas() { Alerta = TipoAlerta.Erro, Ficheiro = fileName.Text, TimeError = DateTime.Now }); }
                }
            }
            catch (Exception ex) { WriteLogFile.WriteLog("Log.txt", String.Format("[{0}]:{1}->Function:DownloadCGI", DateTime.Now, ex.Message)); }
            ctrlCGI = true;
            CGI.Navigate().GoToUrl(baseUrl);
            delaytimeCGI = DateTime.Now.AddSeconds(30);
          //  lastRunCGILB.Text = DateTime.Now.ToString();
            this.lastRunCGILB.BeginInvoke((MethodInvoker)delegate () { this.lastRunCGILB.Text = DateTime.Now.ToString(); });

        }
        /// <summary>
        /// Butão para remover ficheiros CGI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apagarbt_CGI(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show("Tem a certeza que prentende apagar os ficheiros?", "Apagar ficheiros", MessageBoxButtons.YesNo);
            if (msg == DialogResult.Yes) { apagarFicheiros(pathDownloadCGITxt.Text, "CGI"); }
        }
        private void pararCGI(object sender, EventArgs e) => StopTimer(CGITimer, RunCGI, StopCGI, ref ctrlCGI, CGIScript, del);
        private void LoginCGI_Click(object sender, EventArgs e) => LoginCGI();
        private void DownloadCGI_Click(object sender, EventArgs e)
        {
            CGIScript = new Thread(new ThreadStart(DownloadCGI));
            CGIScript.Start();
        }
        private void pathDownloadCGITxt_DoubleClick(object sender, EventArgs e)
        {
            if (pathDownloadCGITxt.Text != "") { folderBrowserDialog1.SelectedPath = pathDownloadCGITxt.Text; }
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pathDownloadCGITxt.Text = folderBrowserDialog1.SelectedPath;
                DownSetts.Write("CGI", pathDownloadCGITxt.Text, "CGI");

            }
        }
        private void CGITimer_Tick(object sender, EventArgs e) => TimersCheck(CGI, DownSetts.Read("CGI"), pathDownloadCGITxt.Text,
            ref ctrlCGI, InicioCGI.Text, FimCGI.Text, ref nextRefreshCGI, delaytimeCGI, ref ctrlDelCGI, ref CGIScript, ref del, "CGI");
        #region Grelha_CGI
        //Indice de linha
        string lastkeyvalCGI = "";
        bool newRowCGI = false;
        int rowIndexCGI,columnCGI;
        //Guarda o indice da linha que o utilizador está a criar
        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            newRowCGI = true;
            rowIndexCGI = e.Row.Index - 1;
        }
        //Define o valor defeito quando se cria uma linha nova
        private void fileList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = true;
        }
        
        //Evento no inicio de edição de celula
        private void filesDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                columnCGI = e.ColumnIndex;
                rowIndexCGI = e.RowIndex;
                lastkeyvalCGI = filesDGV.Rows[rowIndexCGI].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (Exception) { }
        }
        //Evento no final de edição de celula
        private void filelist_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Se nova linha
            if (newRowCGI)
            {
                //Adiciona a nova linha ao ficheiro de configuração
                setts.DownloadLinks.Add(new Settings.Links() { Link = filesDGV.Rows[rowIndexCGI].Cells[e.ColumnIndex].Value.ToString(), Active = "true" });
                File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                newRowCGI = false;
            }
            //Linha existente
            else
            {
                //Verifica se o valor foi alterado
                if (lastkeyvalCGI != "" && filesDGV.Rows[rowIndexCGI].Cells[0].Value != null)
                {
                    if (lastkeyvalCGI != filesDGV.Rows[rowIndexCGI].Cells[e.ColumnIndex].Value.ToString() && lastkeyvalCGI != "True" && lastkeyvalCGI != "False"){
                        setts.DownloadLinks[setts.DownloadLinks.IndexOf(setts.DownloadLinks.Where(x => x.Link == lastkeyvalCGI).First())].Link = filesDGV.Rows[rowIndexCGI].Cells[e.ColumnIndex].Value.ToString();
                        File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                        lastkeyvalCGI = "";
                    }else{
                        setts.DownloadLinks[setts.DownloadLinks.IndexOf(setts.DownloadLinks.Where(x => x.Link == filesDGV.Rows[rowIndexCGI].Cells[0].Value.ToString()).First())].Active = filesDGV.Rows[rowIndexCGI].Cells[1].Value.ToString();
                        File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                        lastkeyvalCGI = "";
                    }
                }
            }
            //RefreshFiles();
        }
        //Evento de apagar linha
        private void filesDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //DownSetts.DeleteKey(e.Row.Cells[0].Value.ToString(), "Ficheiros");
            setts.DownloadLinks.RemoveAt(setts.DownloadLinks.IndexOf(setts.DownloadLinks.Where(x => x.Link == e.Row.Cells[0].Value.ToString()).First()));
            File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
        }
        private void RefreshFiles()
        {
            try
            {
                filesDGV.Rows.Clear();
                fillFiLes();
            }
            catch (Exception)
            {

               // throw;
            }             
           
        }
        #endregion
        #endregion
        #region S3
        public void LoginS3(IWebDriver web)
        {
            try
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> links = web.FindElements(By.CssSelector("input.OSFillParent.Mandatory"));
                links[0].SendKeys(userS3.Text);
                links[1].SendKeys(passS3.Text);
                // links[0].Submit();
                web.FindElement(By.CssSelector(".Button.Is_Default.OSFillParent")).Click();
                DownSetts.Write("User", userS3.Text, "S3");
                DownSetts.Write("Pass", passS3.Text, "S3");
            }
            catch (Exception ex) { WriteLogFile.WriteLog("LogS3.txt", String.Format("[{0}]:{1}->Function:LoginS3", DateTime.Now, ex.Message)); }
        }
        private void RunS3_Click(object sender, EventArgs e) => SaveAndRun(S3Timer, InicioS3.Text, FimS3.Text, delS3.Text, intervaloS3.Text, pathDownloadS3.Text,
            RunS3, StopS3, "S3"); 
        private void StopS3_Click(object sender, EventArgs e) => StopTimer(S3Timer, RunS3, StopS3, ref ctrlS3, S3Script, del);    
        private void S3Timer_Tick(object sender, EventArgs e) => TimersCheck(S3, DownSetts.Read("S3"), pathDownloadS3.Text, ref ctrlS3, InicioS3.Text, FimS3.Text,
                ref nextRefreshS3, delaytimeS3, ref ctrlDelS3, ref S3Script, ref del, "S3");
        private void loginS3_Click(object sender, EventArgs e) => LoginS3(S3);
        public void DownloadS3()
        {
            try
            {
                setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
                if (nextRefreshS3 > DateTime.Parse(FimS3.Text)) { nextRefreshS3 = DateTime.Parse(InicioS3.Text); } else { nextRefreshS3 = DateTime.Now.AddMinutes(Int32.Parse(intervaloS3.Text)); }
                foreach (Settings.Clientes url in setts.ClientesList.Where(x => x.Active == "true"))
                {
                    try { S3.FindElement(By.Id("RandstadTheme_wt101_block_wtHeader_wt99_wt4_wtLogoutLink")).Click(); } catch {
                        //string uploadURL = "https://i.randstad.pt/PortalS3/UploadWizard.aspx?CloudClientId=4";
                        //S3.Navigate().GoToUrl(uploadURL);
                        
                        //foreach (Settings.UploadFile item in setts.UploadFiles)
                        //{
                        //    string tempFile = item.Documento;
                        //    if (tempFile.Contains("{Now}")) tempFile = tempFile.Replace("{Now}", DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy"));
                        //    S3.FindElement(By.CssSelector("#WebPatterns_wt3_block_wtMainContent_wt2_wtfiles input[type=file]")).SendKeys(tempFile);
                        // }
                    }
                    try { Thread.Sleep(1000); S3.Navigate().GoToUrl(DownSetts.Read("S3")); LoginS3(S3); } catch (Exception ex) { WriteLogFile.WriteLog("Log.txt", String.Format("[{0}]:{1}->Function:DownloadS3", DateTime.Now, ex.Message)); }
                    Thread.Sleep(1000);
                    S3.FindElement(By.Id("RandstadTheme_wt101_block_wtMainContent_WebPatterns_wt42_block_wtColumn1_wtStartDate")).Clear(); S3.FindElement(By.Id("RandstadTheme_wt101_block_wtMainContent_WebPatterns_wt42_block_wtColumn1_wtEndDate")).Clear();
                    S3.FindElement(By.Id("RandstadTheme_wt101_block_wtMainContent_WebPatterns_wt42_block_wtColumn1_wtStartDate")).SendKeys(DateTime.Now.Date.ToString("yyyy-MM-dd")); S3.FindElement(By.Id("RandstadTheme_wt101_block_wtMainContent_WebPatterns_wt42_block_wtColumn1_wtEndDate")).SendKeys(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    S3.FindElement(By.Id("RandstadTheme_wt101_block_wtMainContent_wt68")).Click(); //botao Filtrar
                
                    Thread.Sleep(1000);
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> options = S3.FindElements(By.CssSelector(".select2-results-dept-0.select2-result.select2-result-selectable"));
                    IWebElement item2 = options.Where(x => x.Text == url.Cliente).FirstOrDefault();
                    item2.Click();
                    Thread.Sleep(1000);
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> linhas = S3.FindElements(By.CssSelector("table.TableRecords.OSFillParent.OSAutoMarginTop tbody tr "));
                    foreach (IWebElement d in linhas){
                        IWebElement file = d.FindElement(By.CssSelector("td:nth-of-type(2)"));
                        if (File.Exists(pathDownloadS3.Text + "\\" + file.Text) ) {  continue; }
                        d.FindElement(By.CssSelector("td div:not(.OSAutoMarginTop) a")).Click();
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(1000);
                }
            }catch (Exception ex) { WriteLogFile.WriteLog("LogS3.txt", String.Format("[{0}]:{1}->Function:DownloadS3", DateTime.Now, ex.Message)); }     
            ctrlS3 = true;
            S3.Navigate().GoToUrl(DownSetts.Read("S3"));
            delaytimeS3 = DateTime.Now.AddSeconds(30);
           // lastRunS3LB.Text = DateTime.Now.ToString();

        }
        #region Grelha_S3
        string lastkeyvalS3 = "";
        bool newRowS3 = false;
        private void UploadS3BT_Click(object sender, EventArgs e)
        {
         
        }
        private void pathDownloadS3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (pathDownloadS3.Text != "") { folderBrowserDialog1.SelectedPath = pathDownloadS3.Text; }
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pathDownloadS3.Text = folderBrowserDialog1.SelectedPath;
                DownSetts.Write("Path", pathDownloadS3.Text, "S3");
            }
        }

        private void S3Up_Browser_Click(object sender, EventArgs e) => InitializeChrome(ref S3UP, DownSetts.Read("S3"), DownSetts.Read("Path", "S3"));

        private void S3UP_login_Click(object sender, EventArgs e) => LoginS3(S3UP);

        private void button7_Click(object sender, EventArgs e)
        {
            DownSetts.Write("Hora Upload", horaUpS3.Text, "S3");
            UploadS3();
        }
        String uploadDate;
        private void UploadS3()
        {
            try
            {
                LoginS3(S3UP);
                string uploadURL = "https://i.randstad.pt/PortalS3/UploadWizard.aspx?CloudClientId=4";
                //IWebElement i = S3.FindElement(By.Id("RandstadTheme_wt101_block_wtActions_wtbtnUpload"));
                //i.Click();
                S3UP.Navigate().GoToUrl(uploadURL);

                foreach (Settings.UploadFile item in setts.UploadFiles.Where(x => x.Refresh.ToLower() == "true"))
                {
                    try
                    {
                        string tempFile = item.Documento;
                        if (tempFile.Contains("{Now}")) tempFile = tempFile.Replace("{Now}", DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy"));
                        ExcelLink tempEx = new ExcelLink(tempFile); tempEx.RefreshAll();
                    }
                    catch (Exception ex)
                    {

                        continue;
                    }

                }
                foreach (Settings.UploadFile item in setts.UploadFiles.Where(x => x.Active == "true" || x.Active == "True"))
                {
                    try
                    {
                        string tempFile = item.Documento;
                        if (tempFile.Contains("{Now}")) tempFile = tempFile.Replace("{Now}", DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy"));
                        // if (item.Refresh.ToLower() == "true") { ExcelLink tempEx = new ExcelLink(tempFile); tempEx.RefreshAll();  }
                        S3UP.FindElement(By.CssSelector("#WebPatterns_wt3_block_wtMainContent_wt2_wtfiles input[type=file]")).SendKeys(tempFile);
                    }
                    catch (Exception ex)
                    {

                        continue;
                    }
                }
                //WebPatterns_wt3_block_wtMainContent_wt2_wtTempUploadedFileTable
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> t;
                do
                {
                    t = S3UP.FindElements(By.CssSelector(".qq-upload-list li"));

                } while (t.Count > 0);
                S3UP.FindElement(By.CssSelector(".qq-upload-button .button.Button")).Click();
                
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("LogS3.txt", String.Format("[{0}]:{1}->Function:Upload", DateTime.Now, ex.Message));
            }
            uploadDate = DateTime.Now.ToShortDateString();
        }

        private void S3UpTimer_Tick(object sender, EventArgs e)
        {
            
            if (horaUpS3.Text == DateTime.Now.ToShortTimeString()+":"+ DateTime.Now.Second && uploadDate != DateTime.Now.ToShortDateString()) UploadS3();
        }

        private void stopUpScript_Click(object sender, EventArgs e)
        {
            S3UpTimer.Stop();
        }

        private void runUpScript_Click(object sender, EventArgs e)
        {
            S3UpTimer.Start();
        }

        private void filesDGV_CellLeave(object sender, DataGridViewCellEventArgs e) => RefreshFiles(); 
      
        int rowIndexS3;
        private void downloadS3bt_Click(object sender, EventArgs e)
        {
           S3Script = new Thread(new ThreadStart(DownloadS3));
            S3Script.Start();
        }
        private void ClientesS3_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e) => e.Row.Cells[1].Value = true;
        private void ClientesS3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try{
                rowIndexS3 = e.RowIndex;
                lastkeyvalCGI = ClientesS3.Rows[rowIndexS3].Cells[e.ColumnIndex].Value.ToString();
            }catch (Exception) { }
        }
        private void ClientesS3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   //Se nova linha
            if (newRowS3)
            {   //Adiciona a nova linha ao ficheiro de configuração
                setts.ClientesList.Add(new Settings.Clientes() { Cliente = ClientesS3.Rows[rowIndexS3].Cells[e.ColumnIndex].Value.ToString(), Active = "true" });
                File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                newRowS3 = false;
            }//Linha existente
            else{ //Verifica se o valor foi alterado
                if (lastkeyvalS3 != "" && filesDGV.Rows[rowIndexS3].Cells[0].Value != null)
                {
                    if (lastkeyvalS3 != filesDGV.Rows[rowIndexS3].Cells[e.ColumnIndex].Value.ToString() && lastkeyvalS3 != "True" && lastkeyvalS3 != "False")
                    {
                        setts.ClientesList[setts.ClientesList.IndexOf(setts.ClientesList.Where(x => x.Cliente == lastkeyvalCGI).First())].Cliente = ClientesS3.Rows[rowIndexS3].Cells[e.ColumnIndex].Value.ToString();
                        File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                        lastkeyvalS3 = "";
                    }
                    else
                    {
                        setts.ClientesList[setts.ClientesList.IndexOf(setts.ClientesList.Where(x => x.Cliente == ClientesS3.Rows[rowIndexS3].Cells[0].Value.ToString()).First())].Active = ClientesS3.Rows[rowIndexS3].Cells[1].Value.ToString();
                        File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                        lastkeyvalS3 = "";
                    }
                }
            }
        }
        private void ClientesS3_UserAddedRow(object sender, DataGridViewRowEventArgs e) {
            newRowS3 = true;
            rowIndexS3 = e.Row.Index - 1;
        }
        private void ClientesS3_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            setts.ClientesList.RemoveAt(setts.ClientesList.IndexOf(setts.ClientesList.Where(x => x.Cliente == e.Row.Cells[0].Value.ToString()).First()));
            File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
        }
        #endregion
        #endregion
    }
}
