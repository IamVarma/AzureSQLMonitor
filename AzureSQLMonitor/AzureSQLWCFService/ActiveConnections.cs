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

            //if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
            //{
            //    string ConnectionResult = ChangeDatabasecontext(dbname);

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }


            //}

            var connectionString = "Server=tcp:" + ServerName + ",1433;Database=" + dbname + ";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";
            
            using(SqlConnection connection= new SqlConnection(connectionString))
            { 

                    string querytext = "select count(*) as ConnectionCount,getdate() as Time from sys.dm_exec_connections";
            try
            {
                connection.Open();

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

                return "Problem listing Database Connections::" + ex.Message;
            }

            finally
            {

                connection.Close();
            }
           



            }
            


        
        }


    }
}