using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
   public class ConnectionsCount
    {
        string connection_count;
        string time;
        public string Connections
        {
            get { return connection_count; ; }
            set
            {
                connection_count = value;
            }

        }

        public string Time
        {
            get { return time; }
            set
            {
                time = value;
            }
        }

    }
}
