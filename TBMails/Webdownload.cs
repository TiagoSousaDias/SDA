using OpenQA.Selenium.Chrome;
using System;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using CefSharp.WinForms;
using CefSharp;
using System.Threading;
using System.Collections.Generic;
using CefSharp.Internals;
using CefSharp.Handler;
using CefSharp.Example;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace TBMails {
    //{
   
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]

    public partial class WebDownload : Form
    {
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);

        public WebDownload()
        {
            InitializeComponent();
            InitializeChromium();

        }
        private List<Alertas> alertas;
        private List<Settings.Links> files;
        private IniFile DownSetts;
        private Settings setts;
        private Serializer ser = new Serializer();
        string procName;
        private void WebDownload_Load(object sender, EventArgs e)
        {
             procName = Process.GetCurrentProcess().ProcessName;
            //
            setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
            
           
            alertas = new List<Alertas>();
            //alertas.Add(new Alertas() { Alerta = TipoAlerta.Alerta, Ficheiro = "Teste", Campo = "Teste.xlsx" });
            DownSetts = new IniFile("DownloadSettings.ini");
            userTxt.Text = DownSetts.Read("User", "Login");
            passTxt.Text = DownSetts.Read("Pass", "Login");
            timerTxt.Text = DownSetts.Read("Start");
            fimTxt.Text = DownSetts.Read("Finish");
            intervalotxt.Text = DownSetts.Read("Interval");
            deltxt.Text = DownSetts.Read("Apagar");

            pathDownloadTxt.Text = DownSetts.Read("Path");
            source.DataSource = alertas;
       
            dataGridView1.DataSource = source;
            dataGridView1.Columns[1].Visible = false;
           // dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Timestamp";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
            dataGridView1.Columns[4].Visible = false;
            source.Sort = "TimeError DESC";
            fillFiLes();
        }
        public ChromiumWebBrowser chromeBrowser;
        BindingSource source = new BindingSource();

        private void fillFiLes()
        {
            files = setts.DownloadLinks;
          //  files = DownSetts.GetKeys("Ficheiros");
            foreach (Settings.Links file in files)
            {
                filesDGV.Rows.Add(file.Link,bool.Parse(file.Active));
            }
        }
        public void InitializeChromium()
        {

            CefSettings settings = new CefSettings();
            settings.IgnoreCertificateErrors = true;
            // Initialize cef with the provided settings
            settings.CachePath = "/"; // cache path
            settings.PersistSessionCookies = true;
            // Initialize cef with the provided settings
            string cefError = string.Empty;


            // Cookies path
            Cef.Initialize(settings);
            Cef.GetGlobalCookieManager().SetStoragePath("/", false);

            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("https://172.18.146.117/login");
            chromeBrowser.BrowserSettings.ApplicationCache = CefState.Enabled;
            chromeBrowser.BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings.ImageLoading = CefState.Enabled;
            chromeBrowser.BrowserSettings.Javascript = CefState.Enabled;
            chromeBrowser.BrowserSettings.WebSecurity = CefState.Disabled;
            
            
            // Add it to the form and fill it to the form window.
            panel1.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", @pathDownloadTxt.Text);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", true);
            tt = new ChromeDriver(chromeOptions);
            tt.Navigate().GoToUrl(baseUrl);
           
        }
        IWebDriver tt;

        private delegate void SetChromeFocus(Control ctl);
      
        private void onSetFocusChrome( Control ctl)
        {
            ctl.Focus();
        }
      

        public void login()
        {

            //  IWebElement i = tt.FindElement(By.CssSelector(".ui.fluid.left.icon.input input"));
            // i.SendKeys(userTxt.Text);
            // i = tt.FindElements(By.CssSelector(".ui.fluid.left.icon.input input")).ToList;
            try
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> links = tt.FindElements(By.CssSelector(".ui.fluid.left.icon.input input"));
                links[0].SendKeys(userTxt.Text);
                links[1].SendKeys(passTxt.Text);
                links[0].Submit();

                BlockInput(true);
                ProcessHelper.SetFocusToExternalApp(procName);
                MethodInvoker mi = delegate {
                    chromeBrowser.Focus();
                };
                /*if (InvokeRequired)    */
                this.Invoke(mi);

                // SendKeys.SendWait("{TAB}");
                //  chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('ui fluid left icon input')[0].children[0].focus()");
                //  Thread.Sleep(3000);
                //  SendKeys.Send(userTxt.Text);
                //  Thread.Sleep(2000);

                ////  SendKeys.SendWait("{TAB}");
                //  chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('ui fluid left icon input')[1].children[0].focus()", passTxt.Text );
                //  SendKeys.Send(passTxt.Text);
                //  SendKeys.SendWait("{Enter}");
                //  Thread.Sleep(1000);

                DownSetts.Write("User", userTxt.Text, "Login");
                DownSetts.Write("Pass", passTxt.Text, "Login");
                BlockInput(false);
            }
            catch (Exception)
            {

               // throw;
            }
            

        }
        static string baseUrl = "https://172.18.146.117/";
        static string mainURL = baseUrl+"download/";
     
        
        bool ctrlTimer = true;
        public void download()
        {
            try
            {
                login();
            }
            catch (Exception)
            {

                //throw;
            } 
            setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
            if (nextRefresh > DateTime.Parse(fimTxt.Text))
            {
                nextRefresh = DateTime.Parse(timerTxt.Text);
            }
            else
            {
                nextRefresh = DateTime.Now.AddMinutes(Int32.Parse(intervalotxt.Text));
            }
            DownloadHandler downowner = new DownloadHandler( pathDownloadTxt.Text,ref alertas);
            chromeBrowser.DownloadHandler = downowner;
            foreach (Settings.Links url in setts.DownloadLinks.Where(x => x.Active == "true"))
            {
                //chromeBrowser.Load(mainURL + url.Link);
                //Thread.Sleep(3000);
                //if (chromeBrowser.Address != baseUrl+ "notauthorized") {
                //    chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('ui green mini button')[0].click()");
                //} else {
                //    string []u = url.Link.Split('/');
                //    u = u.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                //    alertas.Add(new Alertas() { Alerta = TipoAlerta.Erro, Ficheiro = u[u.Length-1] , TimeError= DateTime.Now});
                //}

                Thread.Sleep(8000);
                tt.Navigate().GoToUrl(mainURL + url.Link);
                Thread.Sleep(2000);
                IWebElement i = tt.FindElement(By.CssSelector(".ui.green.mini.button"));
                i.Click();
                Thread.Sleep(8000);
                
            }
            ctrlTimer = true;
            chromeBrowser.Load(baseUrl);
            delaytime = DateTime.Now.AddSeconds(30);
            alertas = downowner._lista;
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            login();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            t = new Thread(new ThreadStart(download));

            t.Start();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (pathDownloadTxt.Text != "") { folderBrowserDialog1.SelectedPath = pathDownloadTxt.Text; }
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pathDownloadTxt.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        Thread t;
        Thread del;
        DateTime nextRefresh;
        bool ctrlDel = true;
        DateTime delaytime = DateTime.Now.AddSeconds(10);
        private void timer1_Tick(object sender, EventArgs e)
        {
            //inicio
           
            if (ctrlTimer && DateTime.Now.ToShortTimeString() == DateTime.Parse(timerTxt.Text).ToShortTimeString())
            {
                ctrlTimer = false;

                login();
                t = new Thread(new ThreadStart(download));

                t.Start();
             //fim
            }else if (ctrlTimer && DateTime.Now.ToShortTimeString() == DateTime.Parse(fimTxt.Text).ToShortTimeString())
            {
                t.Abort();
                nextRefresh = DateTime.Now.AddHours(-2);
                delaytime = DateTime.Now.AddMinutes(1);
            }
            else
            {
                //intervalos
                if(ctrlTimer && DateTime.Now.ToShortTimeString() == nextRefresh.ToShortTimeString() )
                {
                    ctrlTimer = false;
                    t = new Thread(new ThreadStart(download));

                    t.Start();
                }
                else if (ctrlDel && DateTime.Now.ToShortTimeString() == DateTime.Parse(deltxt.Text).ToShortTimeString())
                {
                    ctrlDel = false;
                    del = new Thread(new ThreadStart(apagarFicheiros));
                    del.Start();
                }
                else
                {
                    if (t == null || !t.IsAlive) { 
                    if(DateTime.Now.Hour == delaytime.Hour && DateTime.Now.Minute == delaytime.Minute && DateTime.Now.Second == delaytime.Second  ) {
                        chromeBrowser.Load(baseUrl);
                        delaytime = DateTime.Now.AddSeconds(10);
                    }
                    }

                    if (t != null) if(t.IsAlive)  source.ResetBindings(false); source.Sort = "TimeError DESC"; 
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                DateTime timeDown = DateTime.Parse(timerTxt.Text);
                DateTime f = DateTime.Parse(fimTxt.Text);
                
                DateTime t = DateTime.Parse(deltxt.Text); 
                runScriptBT.Enabled = false;
                stopScriptBT.Enabled = true;
                DownSetts.Write("Start", timerTxt.Text);
                DownSetts.Write("Finish", fimTxt.Text);
                DownSetts.Write("Interval", intervalotxt.Text);
                DownSetts.Write("Apagar", deltxt.Text);
                if (pathDownloadTxt.Text == "") { throw new Exception("0"); }
                DownSetts.Write("Path", pathDownloadTxt.Text);

                timer1.Start();

            }
            catch (Exception ex)
            {
                string msg;
                if(ex.Message == "0") { msg = "Tem de preencher o caminho para descarregar!"; } else { msg = "Insira uma hora valida! ex: 13:00:00"; }
                MessageBox.Show(msg);

            }
            
        }

        

        private void apagarbt_Click(object sender, EventArgs e)
        {
           DialogResult msg = MessageBox.Show("Tem a certeza que prentende apagar os ficheiros?","Apagar ficheiros",MessageBoxButtons.YesNo);
            if (msg == DialogResult.Yes)
            {
                apagarFicheiros();
            }
        }
        private void apagarFicheiros()
        {
           
                foreach (var file in new DirectoryInfo(@pathDownloadTxt.Text).GetFiles())
                {
                    file.Delete();
                }
            ctrlDel = true;
        }

      
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                runScriptBT.Enabled = true;
                stopScriptBT.Enabled = false;
                timer1.Stop();
                ctrlTimer = true;
                if (t != null) t.Abort();
                if (del != null) del.Abort();

            }
            catch (Exception)
            {

                //throw;
            }
        }
        bool newRow = false;

        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            newRow = true;
            //            if (newRow) DownSetts.Write("Ficheiro[]", e.Value + ";" + "true","Ficheiros");newRow = false;
            rowIndex = e.Row.Index-1;
            
        }

        private void dataGridView2_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = true;
            
        }
        int rowIndex;
        string lastkeyval ="";

        private void filesDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                rowIndex = e.RowIndex;
                lastkeyval = filesDGV.Rows[rowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (Exception)
            {

                //throw;
            }

               
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (newRow) {
                setts.DownloadLinks.Add(new Settings.Links() { Link = filesDGV.Rows[rowIndex].Cells[e.ColumnIndex].Value.ToString(), Active = "true" });
                File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                newRow = false;
            }
            else
            {
                if (lastkeyval != "" && filesDGV.Rows[rowIndex].Cells[0].Value != null)
                {
                    if (lastkeyval != filesDGV.Rows[rowIndex].Cells[e.ColumnIndex].Value.ToString() && lastkeyval != "True" && lastkeyval != "False")
                    {
                       
                        setts.DownloadLinks[setts.DownloadLinks.IndexOf(setts.DownloadLinks.Where(x=>x.Link==lastkeyval).First())].Link = filesDGV.Rows[rowIndex].Cells[e.ColumnIndex].Value.ToString();
                        File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
                        lastkeyval = "";
                    }
                    else
                    {
                        setts.DownloadLinks[setts.DownloadLinks.IndexOf(setts.DownloadLinks.Where(x => x.Link == filesDGV.Rows[rowIndex].Cells[0].Value.ToString()).First())].Active = filesDGV.Rows[rowIndex].Cells[1].Value.ToString();
                        File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));

                        lastkeyval = "";
                    }

                }

            }
            RefreshFiles();
        }

        private void filesDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //DownSetts.DeleteKey(e.Row.Cells[0].Value.ToString(), "Ficheiros");
            setts.DownloadLinks.RemoveAt(setts.DownloadLinks.IndexOf(setts.DownloadLinks.Where(x => x.Link == e.Row.Cells[0].Value.ToString()).First()));
            File.WriteAllText(@"Settings.xml", (string)ser.Serialize<Settings>(setts));
        }

        private void RefreshFiles()
        {
            filesDGV.Rows.Clear();
            fillFiLes();
        }
    }
    
}
