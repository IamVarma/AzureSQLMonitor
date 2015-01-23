using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    public class ResourceUsageDetails:ConnectionBase
    {
        SqlCommand sqlcmd = null;
        ObservableCollection<DatabaseResources> ResourceDetails = new ObservableCollection<DatabaseResources>();
        private DatabaseResources _resourceDetails;


        public string getResourceUsage(string dbname)
        {

            //if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
            //{
            //    string ConnectionResult = ChangeDatabasecontext(dbname);

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }


            //}

            string querytext = "SELECT top(6) * FROM sys.dm_db_resource_stats ORDER BY end_time DESC;";
             var connectionString = "Server=tcp:" + ServerName + ",1433;Database=" + dbname + ";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";

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
                                 _resourceDetails = new DatabaseResources
                                 {
                                     EndTime = DateTime.Parse(dr["end_time"].ToString()),
                                     AverageCPU = dr["avg_cpu_percent"].ToString(),
                                     AverageMemoryUsage = dr["avg_memory_usage_percent"].ToString(),
                                     AverageDataIO = dr["avg_data_io_percent"].ToString(),
                                     AverageLogWrite = dr["avg_log_write_percent"].ToString()
                                 };

                                 ResourceDetails.Add(_resourceDetails);
                                 _resourceDetails = null;
                             }



                         }
                     }

                     return JsonConvert.SerializeObject(ResourceDetails);

                 }

                 catch (Exception ex)
                 {

                     return "Problem fetching ResourceUsageDetails::" + ex.Message;
                 }

                 finally
                 {

                     connection.Close();
                 }


             }
        }



    }
}