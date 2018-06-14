using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TBMails
{
    class BaseDados
    {
        private List<BD> _ListaBD;

        public List<BD> ListaBaseDados{ get { refresh_links(); return _ListaBD; } }

        //---------Node-------------------------
        static protected string File = "BaseDados.xml";
        static protected string MainNode = "BaseDados";
        static protected string BDNode = "BD";
        //------------------------------------------

        public BaseDados() { refresh_links(); }
        
        public void CheckBD(string BDName){
        }
      
     
       
     
       public void refresh_links()
        {
            //atualizar lista de links
            _ListaBD = new List<BD>();
            XDocument doc = XDocument.Load(File);
            foreach (var l in doc.Descendants(BDNode))
            {
                int IdL = (int)l.Attribute("Id");

                _ListaBD.Add(new BD(IdL));
            }
        }
  
           
       public class BD {
          
            //-----Var-------------------
            private int _IdBD;
            private string _path;
            private string _name;
            private Ligacoes _Ligacoes;
            public int Id { get { return _IdBD; } }
            public string Path { get { return _path; } }
            public string Name { get { return _name; } }

            public Ligacoes Ligacoes { get { return _Ligacoes; } }
            //---------------------------------------------

            public BD() { }
            public BD(int Id) { 
                _IdBD = Id;
                readBD();
                _Ligacoes = new Ligacoes(Id);
                
            }
            public void readBD() {
                XDocument doc = XDocument.Load(File);
                XElement selectedElement = doc.Descendants(MainNode).Elements(BDNode).Where(x => (int)x.Attribute("Id") == _IdBD).FirstOrDefault();
                _path = selectedElement.Element("Path").Value;
                _name = selectedElement.Element("Name").Value;

            }
            public  void new_BD(string name,string linkPath)
            {
                //gravar BD
                XDocument doc = XDocument.Load(File);
                XElement nName = new XElement("Name");
                nName.Value = name;
                XElement nPath = new XElement("Path");
                nPath.Value = linkPath;
                XElement nLigacoes = new XElement("Ligacoes");
                int nID = doc.Element(MainNode).Elements().Count() + 1;
                XElement nElem = new XElement(BDNode, new XAttribute("Id", nID));
                nElem.Add(nName);
                nElem.Add(nPath);
                nElem.Add(nLigacoes);

                doc.Element(MainNode).Add(nElem);
                doc.Save(File);
                this._IdBD =nID;
                _Ligacoes = new Ligacoes(_IdBD);
                //readBD();
            }
            public void update_BD(string name,string linkPath, int idLink)
            {
                //actualizar BD
                XDocument doc = XDocument.Load(File);
                XElement selectedElement = doc.Descendants(MainNode).Elements(BDNode).Where(x => (int)x.Attribute("Id") == idLink).FirstOrDefault();
                selectedElement.Element("Name").Value = name;
                selectedElement.Element("Path").Value = linkPath;
                doc.Save(File);
                readBD();
                
            }
            public  void remove_BD(int idLink)
            {
                // Remove Bd
                XDocument doc = XDocument.Load(File);
                XElement selectedElement = doc.Descendants(MainNode).Elements(BDNode).Where(x => (int)x.Attribute("Id") == idLink).FirstOrDefault();
                selectedElement.Remove();
                doc.Save(File);

            }
           public void Criar_Ligacoes(string[] Names){
               _Ligacoes = new Ligacoes(_IdBD);
               _Ligacoes.CriarLigacoes(Names);
    
           }
        }
    }

}
