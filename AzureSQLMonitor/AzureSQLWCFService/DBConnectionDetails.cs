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


        public string getDBConnectionDetails(string dbname)
        {
            if (connection.Database != "master" || connection.State == System.Data.ConnectionState.Closed)
                ChangeDatabasecontext("master");

            string querytext = "select top(5) end_time as Time, success_count as success, connection_failure_count as connectionfail, terminated_connection_count as terminate, throttled_connection_count as throttled  from sys.database_connection_stats where database_name='"+dbname+"' order by end_time desc ";
            try
            {
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
                                 Throttled= int.Parse(dr["throttled"].ToString())
                            };

                            ConnectionDetails.Add(_connectionDetails);
                            _connectionDetails = null;
                        }



                    }
                }

                return JsonConvert.SerializeObject(ConnectionDetails);

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