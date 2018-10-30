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
        [XmlElement("Portal_S3_Links")]
        public List<Clientes> ClientesList{ get; set; }
        [XmlElement("Portal_S3_UploadFile")]
        public List<UploadFile> UploadFiles { get; set; }
        public class Clientes
        {
            [XmlAttribute]
            public string Cliente { get; set; }
            [XmlAttribute]
            public string Active { get; set; }
        }
        public class UploadFile
        {
            [XmlAttribute]
            public string Documento { get; set; }
            [XmlAttribute]
            public string Refresh { get; set; }
            [XmlAttribute]
            public string Active { get; set; }
        }
    }

}


