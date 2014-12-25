using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
   public class TableDetails
    {
        string Table_Name;
        string Table_Size;

      
        public string TableName
        {
            get { return Table_Name; }
            set { Table_Name = value; }

        }

      
        public string TableSize
        {
            get { return Table_Size; }
            set
            {
                Table_Size = value;
            }

        }

    }
}
