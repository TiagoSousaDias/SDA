using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;

namespace TBMails
{


    public partial class Settings
    {
        [XmlElement("Automatismos_Folder")]
        public string MainFolder { get; set; }
        [XmlElement("Automatismos_Subfolders")]
        public List<SubFolder> SubFolders { get; set; }
        [XmlElement("Download_Folder")]
        public string DownloadFolder { get; set; }
        [XmlElement("Portal_CGI_Links")]
        public List<Links> DownloadLinks { get; set; }
        public class Links
        {
            [XmlAttribute]
            public string Link { get; set; }
            [XmlAttribute]
            public string Active { get; set; }
        }
    }

}


