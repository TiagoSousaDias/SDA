using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
namespace TBMails
{
    
   public partial class  SubFolder
    {
        public string Path { get; set; }
        public List<Ficheiro>Files { get; set; }
    }
}
