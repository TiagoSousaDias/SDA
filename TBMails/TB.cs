using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBMails
{
    class TB
    {
        public List<TBInfo> TBS { get; set; }
        public class TBInfo
        {
            public string Nome { get; set; }
            public string Caminho { get; set; }
            public class Connections
            {
                public string Nome { get; set; }
                public string Refreshed { get; set; }
                
            }
        }
    }
}