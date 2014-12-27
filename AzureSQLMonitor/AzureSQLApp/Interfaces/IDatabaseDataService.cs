using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AzureSQLApp.Models;


namespace AzureSQLApp.Interfaces
{
    interface IDatabaseDataService
    {
        Task<ObservableCollection<Database>> GetDatabases(String servername, String login, String Password);
        
        
    }
}
