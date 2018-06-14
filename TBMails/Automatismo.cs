using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TBMails
{
    [XmlRoot("Automatismos")]
    public class AutomatismosClass
    {
        [XmlElement("Automatismo")]
        public List<Automatismo> Automatismos { get; set; }
        public class Automatismo
        {
           
            [XmlAttribute("Caminho")]
            public string Path { get; set; }
             [XmlElement("Ficheiro")]
            public List<Automatismo.Ficheiro> Ficheiros { get; set; }
            [XmlAttribute("Nome")]
            public string Nome { get; set; }


            public class Ficheiro
            {
                [XmlAttribute("Nome")]
                public string Nome { get; set; }
                [XmlArray("Ligacoes")]
                public List<AutomatismosClass.Automatismo.Ficheiro.Consulta> Ligacoes { get; set; }
                [XmlAttribute("Configurações Horarios")]
                public string ConfigHorario_Range { get; set; }
                //[XmlArray]
                //public List<string> EmailsRE { get; set; }
                //[XmlArray]
                //public List<string> EmailsSup { get; set; }
                //[XmlArray]
                //public List<string> EmailsGC{ get; set; }

                [XmlRoot("Ligacao")]
                public class Consulta
                {
                   
                   // public AutomatismosClass.Automatismo.Ficheiro.Consulta Ligacao { get; set; }
                    [XmlText]
                    public string Nome { get; set; }
                    [XmlAttribute]
                    public string Ok { get; set; }
                    [XmlAttribute]
                    public string VOlBD_Export { get; set; }
                    [XmlAttribute]
                    public string Seq_Data_Report { get; set; }
                    [XmlAttribute]
                    public string Range { get; set; }
                    [XmlAttribute]
                    public string FO_Mod { get; set; }
                    [XmlAttribute]
                    public string FO_Trans { get; set; }
                    [XmlAttribute]
                    public string DA_Mod { get; set; }
                    [XmlIgnore]
                    public Image Image { get; set; }
                }
            }
        }
    }
}
