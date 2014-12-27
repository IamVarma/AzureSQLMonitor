using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AzureSQLApp.Models;
using AzureSQLApp.Interfaces;

namespace AzureSQLApp.Services
{
    /* public class DatabaseDataService : IDatabaseDataService
     {
         DatabaseService.SqlMonitorServiceClient SqlMonitorServiceClient = null;
         public async Task<ObservableCollection<Database>> GetDatabases()
         {
             SqlMonitorServiceClient = new DatabaseService.SqlMonitorServiceClient();

             var databases = new ObservableCollection<Database>();
             var databaselist = await SqlMonitorServiceClient.GetDatabaseListAsync();

             foreach (Database _database in databases)
             {
                 var database = new Database()
                 {
                     /*DBName = databaselist.databasename,
                     Status = databaselist.,
                     Size = databaselist.
                 };
                 databases.Add(database);
             }
             return databases;
         }

                  

            
            
     }*/
}
