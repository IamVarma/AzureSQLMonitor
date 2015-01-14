using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace AzureSQLWCFService
{
    public class ActiveConnections:ConnectionBase
    {
        SqlCommand sqlcmd = null;
        ObservableCollection<ConnectionsCount> connectionCount = new ObservableCollection<ConnectionsCount>();
        ConnectionsCount _connectionCount;
        

        public string GetActiveConnections(string dbname)
        {

            if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
                 ChangeDatabasecontext(dbname);

            string querytext = "select count(*) as ConnectionCount,getdate() as Time from sys.dm_exec_connections";
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {  

                    sqlcmd = new SqlCommand(querytext, connection);

                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            _connectionCount = new ConnectionsCount { Connections = int.Parse(dr["ConnectionCount"].ToString()), Time = DateTime.Parse(dr["Time"].ToString()) };

                        }

                     //   connectionCount.Add(_connectionCount);
                        
                    }
                }

                return JsonConvert.SerializeObject(_connectionCount);

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