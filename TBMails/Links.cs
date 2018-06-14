using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace TBMails
{
    class Links
    {
       // private event onItensChanged;
        private static String Ficheiro = "Lista_Links.xml";
        private static String MainTag = "Links";
        private static String MainLinkTag = "Link";
        private static String MainLigacaoTag = "Ligacoes";
        private static String LigacaoTag = "Ligacao";
        private static String MainMailTag = "Mails";
        private static String MailTag = "Mail";

        private List<Link> _ListaLinks;

        public Links()
        {
         //   _ListaLinks = new List<Link>();
            refresh_links();
        }

        
        public List<Link> ListaLinks { get { refresh_links(); return _ListaLinks; } } 
       
     
       public void refresh_links()
        {
            //atualizar lista de links
            _ListaLinks = new List<Link>();
            XDocument doc = XDocument.Load(Ficheiro);
            foreach (var l in doc.Descendants(MainLinkTag))
            {
                string path = (string)l.Attribute("Path");
                int IdL = (int)l.Attribute("Id");
                _ListaLinks.Add(new Link(IdL));
            }
        }

      public  class  Link
        {
           private int _IdLink;
           private string _path;
           private List<Mails.Mail> _listamails;
           private string _name;
           private List<Ligacoes.Ligacao> _ligacoes;
           public int Id { get { return _IdLink; }  }
           public string Path { get { return _path; } }
           public string Name { get { return _name; } }

           public List<Mails.Mail> ListaMails { get { getMails(); return _listamails; } }
           public List<Ligacoes.Ligacao> Ligacoes { get { getLigacoes();return _ligacoes; } }
           public Link() { _ligacoes = new List<Ligacoes.Ligacao>();  _listamails = new List<Mails.Mail>(); }
            public Link(int Id)
            {
                _IdLink = Id;
                getLinkInfo();
            }
            public void getLinkInfo()
            {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault();
                getMails();
                getLigacoes();
              _path = selectedElement.Attribute("Path").Value;
              _name = selectedElement.Attribute("Name").Value; 
            }
            private void getMails() {               
                _listamails = new List<Mails.Mail>();
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault();
                foreach (XElement item in selectedElement.Element(MainMailTag).Elements())
                {
                    int t = Int32.Parse(item.Attribute("Id").Value);
                    _listamails.Add(new Mails.Mail(t, _IdLink));
                }
            }
            private void getLigacoes() {
                _ligacoes = new List<Ligacoes.Ligacao>();
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault();
   
                foreach (XElement item in selectedElement.Element(MainLigacaoTag).Elements())
                {
                    int t = Int32.Parse(item.Attribute("IDReal").Value);
                    _ligacoes.Add(new Ligacoes.Ligacao(t, item.Attribute("BD").Value));
                }
            }

            public void new_link(string linkPath,string linkname, List<Ligacoes.Ligacao> listLig, List<string> mails)
            {
                //gravar link
                XDocument doc = XDocument.Load(Ficheiro);
                XElement newLink = new XElement(MainLinkTag,
                        new XAttribute("Id", doc.Element(MainTag).Elements().Count() + 1),
                        new XAttribute("Path", linkPath),
                        new XAttribute("Name", linkname)
                    );
                XElement newLig = new XElement(MainLigacaoTag);

                foreach (Ligacoes.Ligacao item in listLig)
                {
                    newLig.Add(new XElement(LigacaoTag, new XAttribute("Id",newLig.Elements(LigacaoTag).Count()+1), new XAttribute("BD", item.NameBD), new XAttribute("IDReal",item.Id)));
                }
                XElement newMails = new XElement(MainMailTag);
                foreach (string mail in mails)
                {
                    newMails.Add(new XElement(MailTag, new XAttribute("Id", newMails.Elements(MailTag).Count() + 1),mail));
                }
                newLink.Add(newLig);
                
                newLink.Add(newMails);
                doc.Element(MainTag).Add(newLink);
                doc.Save(Ficheiro);
            }
            public void update_link(string linkPath, string linkname)
            {
                //actualizar link
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink ).FirstOrDefault();
                selectedElement.Attribute("Path") .Value= linkPath;
                selectedElement.Attribute("Name").Value = linkname;
                doc.Save(Ficheiro);
            }
            public void remove_link(int idLink) {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == idLink).FirstOrDefault();
                selectedElement.Remove();
                doc.Save(Ficheiro);

            }

            public void addLigacao(Ligacoes.Ligacao liga, BaseDados.BD bd)
            {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault();
                XElement nElem = new XElement(LigacaoTag, new XAttribute("Id", selectedElement.Elements().Count() + 1), new XAttribute("BD", bd.Name), new XAttribute("IDReal", liga.Id));
                nElem.Value = liga.Name;
                selectedElement.Element(MainLigacaoTag).Add(nElem);
                doc.Save(Ficheiro);           
            }
            public void removerLigacao(Ligacoes.Ligacao liga)
            {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault();
                selectedElement.Elements(MailTag).Where(x => (int)x.Attribute("IDReal") == liga.Id).FirstOrDefault().Remove();
                doc.Save(Ficheiro);            
            }
         public class Mails{
              private List<Mail> _mails;
              public List<Mail> ListaMails {get {return _mails;}}
              public Mails() { }
              public Mails(int IdLink) { BuscarMails(IdLink); }
           
              private void BuscarMails(int id){
                  _mails = new List<Mail>();
                  XDocument doc = XDocument.Load(Ficheiro);
                  foreach (var l in doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == id).FirstOrDefault().Descendants(MainMailTag).Elements())
                  {
                      int IdL = (int)l.Attribute("Id");

                      _mails.Add(new Mail(id,IdL));
                  }
                 
              }
              public class Mail{
                  private int _IdLink;
                  private int _IDMail;
                  private string _Mail;
                  public int Id { get { return _IDMail; } }
                  public string Email { get { return _Mail; } }

                  public Mail(int id) { _IdLink=id; }
                  public Mail(int id, int IdLink) { _IDMail = id; _IdLink = IdLink; LerMail(); }

                  private void LerMail(){
                     XDocument doc = XDocument.Load(Ficheiro);
                    XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault();
                   _Mail = selectedElement.Element(MainMailTag).Elements(MailTag).Where(x => (int)x.Attribute("Id") == _IDMail).FirstOrDefault().Value;

                  }
                  public void NovoMail(string mail) {
                      XDocument doc = XDocument.Load(Ficheiro);
                      XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault().Element(MainMailTag);
                       XElement nElem = new XElement(MailTag, new XAttribute("Id",selectedElement.Elements().Count()+1));
                       nElem.Value = mail; 
                      selectedElement.Add(nElem);
                    doc.Save(Ficheiro);
                  }
                  public void RemoverMail()
                  {
                      XDocument doc = XDocument.Load(Ficheiro);
                      XElement selectedElement = doc.Descendants(MainTag).Elements(MainLinkTag).Where(x => (int)x.Attribute("Id") == _IdLink).FirstOrDefault().Element(MainMailTag);
                      selectedElement.Elements(MailTag).Where(x => (int)x.Attribute("Id") == _IDMail).FirstOrDefault().Remove();
                      doc.Save(Ficheiro);

                  }
              }
          }
        }
        
    }
}
