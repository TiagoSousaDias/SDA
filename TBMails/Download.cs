using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;
//using EAGetMail;
using System.IO;
//using OpenPop.Pop3;
//using OpenPop.Mime;
using System.Data;
//using Limilabs.Client.IMAP;
using System.Collections.Generic;
//using Limilabs.Mail;
//using Limilabs.Mail.MIME;
using ActiveUp.Net.Mail;

namespace TBMails
{
    
    public partial class Download : Form
    {
        BindingList<EmailMessage> emails = new BindingList<EmailMessage>();
        BindingSource bs;
        public Download()
        {
            InitializeComponent();
        

        }
        private void Download_Load(object sender, EventArgs e)
        {

        }
        private bool haveMore = true;
        private int pageSize = 50;
        ExchangeService service;
        ItemView view;

        FindItemsResults<Item> results;
        Thread ler;

        private void ConnectEWS()
        {
             service = new ExchangeService();
            service.UseDefaultCredentials = true;
            service.Credentials = new WebCredentials(textBox1.Text, textBox2.Text);
            service.Url = new Uri(servertxt.Text);

            string querystring;
            if (todayRB.Checked)
            {
                querystring = "received:today";
            }
            else if(specRb.Checked){
                querystring = "received:"+ specDate.Value.Date;
            } else {
                querystring = "received:>=" + deDP.Value.Date +" AND received:<=" + ateDP.Value.Date;
            }
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
          
            view = new ItemView(pageSize);
         //   view.PropertySet = new PropertySet(BasePropertySet.IdOnly,)
            results = service.FindItems(WellKnownFolderName.Inbox, querystring, view);
            if (results.Items.Count() > 0) {
                 ler = new Thread(new ThreadStart (this.lerMails));
               //- ler.IsBackground = true;
                ler.Start();
            }
           

        }
        //private void ConnectPop()
        //{
        //    Pop3Client pop3Client;
       
        //    pop3Client = new ActiveUp.Net.Mail.Pop3Client();
        //    pop3Client.Connect("pop3.smartcloudpt.pt", 995 ,true);
        //    pop3Client.Authenticate(textBox1.Text, textBox2.Text);
    
        //    bs = new BindingSource();
        //    int count = pop3Client.GetMessageCount();
            
        //    DataTable dtMessages = new DataTable();
        //    dtMessages.Columns.Add("MessageNumber");
        //    dtMessages.Columns.Add("From");
        //    dtMessages.Columns.Add("Subject");
        //    dtMessages.Columns.Add("DateSent");
        //    int counter = 0;
        //    for (int i = count; i >= 1; i--)
        //    {
        //        OpenPop.Mime.Message message = pop3Client.GetMessage(i);
        //        dtMessages.Rows.Add();
        //        dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageNumber"] = i;
        //        dtMessages.Rows[dtMessages.Rows.Count - 1]["From"] = message.Headers.From;
        //        dtMessages.Rows[dtMessages.Rows.Count - 1]["Subject"] = message.Headers.Subject;
        //        dtMessages.Rows[dtMessages.Rows.Count - 1]["DateSent"] = message.Headers.DateSent;
        //        counter++;
        //        if (counter > 5)
        //        {
        //         //   break;
        //        }
        //    }
        //    bs.DataSource = dtMessages;
       
        //}
        private void connectImap()
        {
            Imap4Client imap = new Imap4Client();
            imap.ConnectSsl(servertxt.Text, 993);
            imap.Login(textBox1.Text, textBox2.Text);
            ActiveUp.Net.Mail.Mailbox mbox = imap.SelectMailbox("Inbox");
            MessageCollection messages;
           // List<long> uids;
            if (todayRB.Checked)
            {
                //uids = imap.Search(Expression.Since(DateTime.Now));
                messages = mbox.SearchParse("SINCE " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"));
            }
            else if (specRb.Checked)
            {
                //uids = imap.Search(Expression.And(Expression.Since(specDate.Value),Expression.Before(specDate.Value)));
                messages = mbox.SearchParse("SINCE " + specDate.Value.ToString("dd-MMM-yyyy hh:mm:ss") + " NOT BEFORE " + specDate.Value.AddDays(-1).ToString("dd-MM-yyyy hh:mm:ss"));
            }
            else
            {
                //uids = imap.Search(Expression.And(Expression.Since(deDP.Value), Expression.Before(ateDP.Value)));
                messages = mbox.SearchParse("SINCE " + deDP.Value.ToString("dd-MMM-yyyy hh:mm:ss") + " NOT BEFORE " + ateDP.Value.AddDays(-1).ToString("dd-MM-yyyy hh:mm:ss"));
            }
            foreach (ActiveUp.Net.Mail.Message mail in messages)
            {
                foreach (MimePart item in mail.Attachments)
                {
                    item.StoreToFile(pathUploadTxt.Text);
                }
                Console.WriteLine(mail.Date +" - " +mail.Subject);
            }
         //   foreach (long uid in uids)
         //   {
         //       byte[] eml = imap.GetMessageByUID(uid);
         //       IMail message = new MailBuilder().CreateFromEml(eml);
         //       foreach (MimeData mime in message.Attachments)
                    
         //       {
                    
         //           mime.Save( pathUploadTxt.Text+"\\" + mime.SafeFileName);
                    
         //       }

         //       Console.WriteLine(message.Subject);
         //       Console.WriteLine(message.Text);
         //   }
         ////   MessageBox.Show("Leu emails");
         //   imap.Close(true);
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                bs = new BindingSource();
                bs.DataSource = emails;
                bs.DataSourceChanged += new EventHandler(refreshList);
                bs.ResetBindings(false);

                ConnectEWS();
            }
            else
            {
                MessageBox.Show("Preencha os dados de login");
            }
        }
        
        private void lerMails()
        {
            haveMore = true;
            while (haveMore)
            {
                results = service.FindItems(WellKnownFolderName.Inbox, view);
                
                foreach (var item in results.Items)
                {
                    EmailMessage message = EmailMessage.Bind(service, item.Id, new PropertySet(ItemSchema.Attachments));

                 
                        foreach(Microsoft.Exchange.WebServices.Data.Attachment attch in message.Attachments)
                        {
                            if(attch is FileAttachment)
                            {
                                FileAttachment file = attch as FileAttachment;
                            string path = setts.DownloadFolder;

                               
                                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                                if (!File.Exists(@path + "\\" + file.Name)) {
                                   file.Load(@path+"\\" + file.Name);
                                    Thread.Sleep(1000);
                                   if(file.Content != null) File.WriteAllBytes(@path + file.Name, file.Content);
                                }
                            }

                        }
                        emails.Add((EmailMessage)item);
                  
              
               
                }
              
                if (haveMore)
                {
                    view.Offset += pageSize;
                    Console.WriteLine("Mails Lidos");
                    ler.Abort();
          
                }
                else {
                    haveMore = false;
                    Console.WriteLine("Mails Lidos");
                    ler.Abort();
                  

                }
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
        private void refreshList(object sender,EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // ConnectPop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connectImap();
        }

  

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(pathUploadTxt.Text != "") { folderBrowserDialog1.SelectedPath = pathUploadTxt.Text; }
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                pathUploadTxt.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        Settings setts;
        private void button4_Click(object sender, EventArgs e)
        {
            Serializer ser = new Serializer();
            setts = ser.Deserialize<Settings>((string)File.ReadAllText(@"Settings.xml"));
            setts.DownloadFolder = pathUploadTxt.Text;
            File.WriteAllText(@"Settings.xml", ser.Serialize<Settings>(setts));
        }
    }
}
