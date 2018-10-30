using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;

namespace TBMails
{
    [XmlRoot("TBS")]
    public partial class TBSettings
    {
        [XmlElement("TBS_Folder")]
        public List<TB_MainFolder> MainFolder { get; set; }
        
        public class TB_MainFolder
        {
            public string Nome { get; set; }
            public string Path { get; set; }
            public List<TB_SubFolder> Folders { get; set; }

            public class TB_SubFolder
            {
                public string Nome { get; set; }
                public string Path { get; set; }
                public List<TB_SubFolder> Folders { get; set; }
                public List<TB_File> Ficheiros { get; set; }
            }
            public class TB_File
            {
                public string Nome { get; set; }
                public string Path { get; set; }
                public bool Ativo { get; set; }
                public List<string> Emails { get; set; }
                public List<string> EmailsCC { get; set; }
                public List<string> EmailsBCC { get; set; }
            }
        }
    }
}
