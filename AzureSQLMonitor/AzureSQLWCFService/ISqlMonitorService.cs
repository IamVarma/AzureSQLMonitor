using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.ObjectModel;

namespace AzureSQLWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISqlMonitorService" in both code and config file together.
    [ServiceContract]
    public interface ISqlMonitorService
    {

       
        [OperationContract]
        string ConnectSQLAzure(string Servername, string LoginName, string Password);

        [OperationContract]
        string GetDatabaseList();


        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class DatabaseListClass
    {
        string database_name;
        string database_state;
        string database_size;

        [DataMember]
        public string DatabaseName
        {
            get { return database_name; }
            set { database_name = value; }

        }

        [DataMember]
        public string DatabaseState
        {
            get { return database_state; }
            set { database_state = value; }
        }

        [DataMember]
        public string DatabaseSize
        {
            get { return database_size; }
            set { database_size = value; }
        }
                     
    }


}
