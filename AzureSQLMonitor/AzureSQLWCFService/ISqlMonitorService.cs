
using System.Runtime.Serialization;
using System.ServiceModel;
using System;

namespace AzureSQLWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISqlMonitorService" in both code and config file together.
    [ServiceContract]
    public interface ISqlMonitorService
    {

       
        [OperationContract]
        string ConnectSQLAzure(string servername, string loginName, string password);

        [OperationContract]
        string GetDatabaseList();

        [OperationContract]
        string getSystemInfo();

        [OperationContract]
        string GetDatabaseSize(string dbname);

        [OperationContract]
        string GetConnectionCount(string dbname);

        [OperationContract]
        string GetTableSizeDetails(string dbname);
        
        [OperationContract]
        string GetDBResourceUsage(string dbname);

        [OperationContract]
        string GetDBConnectionDetails(string dbname);


        [OperationContract]
        string GetTopCpuConsumers(string dbname);

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
        public string DatabaseSlo
        { get; set; }


        [DataMember]

        public string AccessDesc
        { get; set; }


        [DataMember]

        public string IsEncrypt
        { get; set; }

        [DataMember]

        public string IsReadOnly
        { get; set; }

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

    [DataContract]
    class ConnectionsCount
    {
        int connection_count;
        DateTime time;

        [DataMember]
        public int Connections
        {
            get { return connection_count; ; }
            set
            {
                connection_count = value;
            }

        }

        [DataMember]
        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
            }
        }

    }

    [DataContract]
    class DatabaseResources
    {
        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public string AverageCPU { get; set; }
        [DataMember]
        public string AverageMemoryUsage { get; set; }
        [DataMember]
        public string AverageDataIO { get; set; }

        [DataMember]
        public string AverageLogWrite { get; set; }
    }

    [DataContract]
    class DatabaseSizeClass
    {
        [DataMember]
        public string Name { get; set; }

        

        [DataMember]
        public float Size { get; set; }

    }


    [DataContract]
    class DBConnectionClass
    {

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]

        public int Success { get; set; }

        
        [DataMember]

        public int LoginFailures { get; set; }

        [DataMember]

        public int Terminated { get; set; }

        [DataMember]
        public int Throttled { get; set; }

    }

    [DataContract]
    class DBConnectionEventsClass
    {
        [DataMember]
        public string EventType { get; set; }
        
        [DataMember]
        public string EventDesc { get; set; }

        [DataMember]
        public int EventCount { get; set; }

    }


    [DataContract]
    class TopCPUConsumersClass
    {
        [DataMember]
        public string SQLText { get; set; }

        [DataMember]
        public int ExecutionCount { get; set; }

        [DataMember]

        public int TotalCPUMs { get; set; }

        [DataMember]
        public int TotalElapsedMs{get; set;}

        [DataMember]

        public float CPUPercentage { get; set; }

        [DataMember]
        public int AverageCPUMs { get; set; }
    }


    



    


}
