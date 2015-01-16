using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
   public class TopCPUConsumersClass
    {

      
        public string SQLText { get; set; }

      
        public int ExecutionCount { get; set; }


        public int TotalCPUMs { get; set; }

      
        public int TotalElapsedMs{get; set;}

      

        public float CPUPercentage { get; set; }

      
        public int AverageCPUMs { get; set; }

    }
}
