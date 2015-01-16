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


        public string ConnectSQLAzure(string servername, string loginName, string password)
        {
            ConnectionBase connect = new ConnectionBase();

            var result=connect.initialConnection(servername, loginName, password);

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
            return UtilitiesInstance.GetSystemInfo();
        }

        public string GetTableSizeDetails(string database)
        {
            TableDetails tablesizes = new TableDetails();
            return tablesizes.getTableSizeDetails(database);

        }


        public string GetDatabaseSize(string dbname)
        {
            DatabaseSizeDetails databasesize = new DatabaseSizeDetails();
            return databasesize.GetDatabaseSize(dbname);
        }

        public string GetConnectionCount(string database)
        {
            ActiveConnections connections = new ActiveConnections();
            return connections.GetActiveConnections(database);
        }

        public string GetDBResourceUsage(string dbname)
        {
           ResourceUsageDetails resouceUsage = new ResourceUsageDetails();
            return resouceUsage.getResourceUsage(dbname);
        }

        public string GetDBConnectionDetails(string dbname)
        {
            DBConnectionDetails connections = new DBConnectionDetails();
            return connections.getDBConnectionDetails(dbname);
            
        }

        public string GetTopCpuConsumers(string dbname)
        {
            TopCPUConsumers consumers = new TopCPUConsumers();
            return consumers.getCpuConsumers(dbname);
        }
        
    }
}
