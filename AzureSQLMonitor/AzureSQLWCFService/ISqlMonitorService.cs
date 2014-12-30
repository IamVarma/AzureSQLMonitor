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

        [OperationContract]
        string getSystemInfo();

        [OperationContract]
        string GetTableSizeDetails(string dbname);

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

    //Class to define the Table size details in a list

    [DataContract]
    public class TableSizeDetails
    {
        string Table_Name;
        string Table_Size;

        [DataMember]
        public string TableName
        {
            get { return Table_Name; }
            set { Table_Name = value; }

        }

        [DataMember]
        public string TableSize
        {
            get { return Table_Size; }
            set
            {
                Table_Size = value;
            }

        }


    }

    [DataContract]
    public class Sysinfoclass
    {
        string server_name;
        string login_name;
        string version_info;
        string datetime_info;

        [DataMember]
        public string ServerName
        {
            get { return server_name; }
            set { server_name = value; }

        }

        [DataMember]
        public string LoginName
        {
            get { return login_name; }
            set
            {
                login_name = value;
            }

        }
        [DataMember]
        public string VersionInfo
        {
            get { return version_info; }
            set
            {
                version_info = value;
            }

        }
        [DataMember]
        public string DatetimeInfo
        {
            get { return datetime_info; }
            set
            {
                datetime_info = value;
            }

        }

    }


}
