using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    public class DBConnectionDetails:ConnectionBase
    {
        SqlCommand sqlcmd = null;

        ObservableCollection<DBConnectionClass> ConnectionDetails = new ObservableCollection<DBConnectionClass>();
        DBConnectionClass _connectionDetails;

        ObservableCollection<DBConnectionEventsClass> ConnectionEvents = new ObservableCollection<DBConnectionEventsClass>();
        DBConnectionEventsClass _connectionEvents;


        public string getDBConnectionDetails(string dbname)
        {
            string[] FinalOutPut = new string[2];

            //if (DatabaseName != "master" || connection.State != System.Data.ConnectionState.Open)
            //{
            //    string ConnectionResult = ChangeDatabasecontext("master");

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }

            //}

            string querytext = "select top(5) end_time as Time, success_count as success, connection_failure_count as connectionfail, terminated_connection_count as terminate, throttled_connection_count as throttled  from sys.database_connection_stats where database_name='"+dbname+"' order by end_time desc ";
            var connectionString = "Server=tcp:" + ServerName + ",1433;Database=master;User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


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
                                _connectionDetails = new DBConnectionClass
                                {
                                    Time = DateTime.Parse(dr["Time"].ToString()),
                                    Success = int.Parse(dr["success"].ToString()),
                                    LoginFailures = int.Parse(dr["connectionfail"].ToString()),
                                    Terminated = int.Parse(dr["terminate"].ToString()),
                                    Throttled = int.Parse(dr["throttled"].ToString())
                                };

                                ConnectionDetails.Add(_connectionDetails);
                                _connectionDetails = null;
                            }



                        }
                    }

                    FinalOutPut[0] = JsonConvert.SerializeObject(ConnectionDetails);

                    FinalOutPut[1] = getConnectionEvents(dbname);


                    return JsonConvert.SerializeObject(FinalOutPut);

                }




                catch (Exception ex)
                {

                    return "Problem listing Connection Count::" + ex.Message;
                }

                finally
                {

                    connection.Close();
                }
            }

        }

        string getConnectionEvents(string dbname)
        {

            string querytext = "select top (10) event_type,  event_subtype_desc, count(1) as occurences from sys.event_log where database_name = '"+dbname+"' and event_subtype_desc <> 'connection_successful' and end_time > (getdate() -7) group by database_name , event_type, event_subtype , event_subtype_desc order by occurences desc";

            try
            {

                if (connection.State == System.Data.ConnectionState.Open)
                {

                    sqlcmd = new SqlCommand(querytext, connection);

                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            _connectionEvents = new DBConnectionEventsClass
                            {
                                EventType = dr["event_type"].ToString(),
                                EventDesc = dr["event_subtype_desc"].ToString(),
                                EventCount = int.Parse(dr["occurences"].ToString())
                            };

                            ConnectionEvents.Add(_connectionEvents);
                            _connectionEvents = null;
                        }

                    }


                }
                return JsonConvert.SerializeObject(ConnectionEvents);
            }

                       

            catch(Exception e)
            {
                return "Problem listing ConnectionEvents::"+ e.Message;
            }

           

        }



    }
}