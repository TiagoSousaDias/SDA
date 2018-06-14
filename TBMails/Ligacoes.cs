using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TBMails
{
    class Ligacoes
    {
        private static String Ficheiro = "BaseDados.xml";
        private static String MainTag = "BD";
        private static String MainLigacaoTag = "Ligacoes";
        private static String LigacaoTag = "Ligacao";

        private List<Ligacao> _Liga;
        public List<Ligacao> ListaLigacoes { get { refresh_links(); return _Liga; } }

        public Ligacoes(){  _Liga = new List<Ligacao>();}
        static int IDBD;
        static string _NameBD;
        public Ligacoes(int Id) {
            IDBD = Id;
            refresh_links();
        }
        public void refresh_links()
        {
            //atualizar lista de links
            _Liga = new List<Ligacao>();
            XDocument doc = XDocument.Load(Ficheiro);
              _NameBD =  doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault().Element("Name").Value;
            foreach (var l in doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault().Elements(MainLigacaoTag).FirstOrDefault().Elements(LigacaoTag))
            {
                int IdL = (int)l.Attribute("Id");

                _Liga.Add(new Ligacao(IdL));
            }
        }
        public void CriarLigacao(string name)
        {
            Ligacao item = new Ligacao();
        
            item.NovaLigacao(name);
            

        }
        public void CriarLigacoes(string[] names) {
            Ligacao item = new Ligacao();
            foreach (string name in names) {
                item.NovaLigacao(name);
            }
           
        }
       public class Ligacao {
            private int _ID;
            private string _Name;
            private string _lastRefreshed;
            private Boolean _Checked;
            public Ligacao(){}
            public Ligacao(int ID) { _ID = ID; LerLigacao(); }
            public Ligacao(int ID, string _NomeBD) {
                _ID = ID; 
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => x.Element("Name").Value == _NomeBD).FirstOrDefault();
                IDBD = Int32.Parse(selectedElement.Attribute("Id").Value);
                LerLigacao(); }

            public int Id { get { return _ID; } }      
            public Boolean Check {  get { return _Checked; }}
            public string LastRefreshed {  get { return _lastRefreshed; } }
            public string Name {  get { return _Name; }}
            public string NameBD { get { return _NameBD; } }

            private void LerLigacao() {
                try
                {

                    XDocument doc = XDocument.Load(Ficheiro);
                    XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault();
                    _Name = selectedElement.Element(MainLigacaoTag).Elements(LigacaoTag).Where(x => (int)x.Attribute("Id") == _ID).FirstOrDefault().Value;
                    _lastRefreshed = selectedElement.Element(MainLigacaoTag).Elements(LigacaoTag).Where(x => (int)x.Attribute("Id") == _ID).FirstOrDefault().Attribute("LastRefresh").Value;
                    string t = selectedElement.Element(MainLigacaoTag).Elements(LigacaoTag).Where(x => (int)x.Attribute("Id") == _ID).FirstOrDefault().Attribute("Refreshed").Value;
                    _Checked = (t == "False" ? false : true);
                }
                catch (Exception ex){ }
            }
            public void NovaLigacao(string Name) {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault();
                XElement newElem = new XElement(LigacaoTag, new XAttribute("Id", selectedElement.Element(MainLigacaoTag).Elements().Count() + 1), new XAttribute("Refreshed", "False"), new XAttribute("LastRefresh", "04/12/2017"));
                newElem.Value= Name;
                selectedElement.Element(MainLigacaoTag).Add(newElem);
                doc.Save(Ficheiro);
            }
            public void NovaLigacao(string[] Names)
            {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault();
                foreach (string Name in Names) { 
                
                    XElement newElem = new XElement(LigacaoTag, new XAttribute("Id", selectedElement.Element(MainLigacaoTag).Elements().Count() + 1), new XAttribute("Refreshed", "False"), new XAttribute("LastRefresh", "04/12/2017"));
                    newElem.Value = Name;
                    selectedElement.Element(MainLigacaoTag).Add(newElem);
                }
                doc.Save(Ficheiro);
                
            }
            public void UpdateLigacao(string Name,int idLig) {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault();
                selectedElement.Element(MainLigacaoTag).Elements(LigacaoTag).Where(x => (int)x.Attribute("Id") == idLig).FirstOrDefault().Value = Name;
                doc.Save(Ficheiro);
            }
            public void RemoverLigacao(int idLig)
            { 
              XDocument doc = XDocument.Load(Ficheiro);
              XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault();
                selectedElement.Element(MainLigacaoTag).Elements(LigacaoTag).Where(x => (int)x.Attribute("Id") == idLig).FirstOrDefault().Remove();
                doc.Save(Ficheiro);
            }
            public void RemoverLigacao(int []idLig)
            {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Descendants("BaseDados").Elements(MainTag).Where(x => (int)x.Attribute("Id") == IDBD).FirstOrDefault();
                foreach (int id in idLig) {
                    selectedElement.Element(MainLigacaoTag).Elements(LigacaoTag).Where(x => (int)x.Attribute("Id") == id).FirstOrDefault().Remove();

                }
                doc.Save(Ficheiro);
            }
            public void RemoverTodos() {
                XDocument doc = XDocument.Load(Ficheiro);
                XElement selectedElement = doc.Elements(MainTag).Where(x => (int)x.Attribute("Id") == _ID).FirstOrDefault();
                selectedElement.Element(MainLigacaoTag).RemoveAll();
                doc.Save(Ficheiro);
            }
        }
    }
}
