using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
    public class DBConnectionClass
    {
       
        public DateTime Time { get; set; }

      

        public int Success { get; set; }


       

        public int LoginFailures { get; set; }

       

        public int Terminated { get; set; }

       
        public int Throttled { get; set; }

    }
}
