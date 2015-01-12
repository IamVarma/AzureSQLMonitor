using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SqlMonitorService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SqlMonitorService.svc or SqlMonitorService.svc.cs at the Solution Explorer and start debugging.
    public class SqlMonitorService : ISqlMonitorService
    {


        public string ConnectSQLAzure(string servername, string login, string password)
        {
            ConnectionBase connect = new ConnectionBase();

            var result=connect.initialConnection(servername, login, password);

            return result;
        }


        public string GetDatabaseList()
        {

            ListDatabases databaselist = new ListDatabases();

            return databaselist.getDatabases();
        
        }

        public string getSystemInfo()
        {
            Utilities UtilitiesInstance = new Utilities();
            return UtilitiesInstance.getSystemInfo();
        }

        public string GetTableSizeDetails(string database)
        {
            TableDetails tablesizes = new TableDetails();
            return tablesizes.getTableSizeDetails(database);

        }

        public string GetConnectionCount(string database)
        {
            ActiveConnections connections = new ActiveConnections();
            return connections.GetActiveConnections(database);
        }



        
    }
}
