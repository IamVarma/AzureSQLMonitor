using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
   public class ConnectionsCount
    {
        int connection_count;
        DateTime time;
        public int Connections
        {
            get { return connection_count; ; }
            set
            {
                connection_count = value;
            }

        }

        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
            }
        }

    }
}
