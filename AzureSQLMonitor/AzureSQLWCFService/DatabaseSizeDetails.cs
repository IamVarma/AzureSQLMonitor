using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AzureSQLWCFService
{
    public class DatabaseSizeDetails:ConnectionBase
    {
        SqlCommand sqlcmd = null;
        DatabaseSizeClass _databaseSize = new DatabaseSizeClass();
        ObservableCollection<DatabaseSizeClass> DatabaseSize = new ObservableCollection<DatabaseSizeClass>();

        public string GetDatabaseSize(string dbname)
        {
            if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
                ChangeDatabasecontext(dbname);

            string querytext = "select cast(DATABASEPROPERTYEX  ( '"+dbname+"','MaxSizeInBytes' ) as bigint)/1048576000 as MaxSize, SUM(reserved_page_count)*8.0/1048576 as UsedSize FROM sys.dm_db_partition_stats;";
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {

                    sqlcmd = new SqlCommand(querytext, connection);

                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {

                        dr.Read();
                        _databaseSize = new DatabaseSizeClass { Name = "MaxSize", Size = float.Parse(dr["MaxSize"].ToString()) };
                        DatabaseSize.Add(_databaseSize);
                        _databaseSize = new DatabaseSizeClass { Name = "UsedSize", Size = float.Parse(dr["UsedSize"].ToString()) };
                        DatabaseSize.Add(_databaseSize);
                        
                    }
                }

                return JsonConvert.SerializeObject(DatabaseSize);

            }

            catch (Exception ex)
            {

                return "There is some problem listing the Connection count";
            }

            finally
            {

                connection.Close();
            }
           
        }

    }
}