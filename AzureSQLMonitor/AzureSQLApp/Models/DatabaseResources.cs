using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
   public  class DatabaseResources
    {

       public DateTime EndTime { get; set; }

      
        public string AverageCPU { get; set; }
      
        public string AverageMemoryUsage { get; set; }
      
        public string AverageDataIO { get; set; }

      
        public string AverageLogWrite { get; set; }
    }
}
