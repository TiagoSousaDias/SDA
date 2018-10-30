using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace TBMails
{
    public class Database
    {
        private DataType _Type;
        public Database(DataType type)
        {
            _Type = type;

        }
        public enum DataType
        {
            SQL = 0,
            ACCESS = 1,
            EXCEL = 2
        }
    }
}
