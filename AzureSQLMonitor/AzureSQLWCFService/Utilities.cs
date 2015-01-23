
using System.Data.SqlClient;
using Newtonsoft.Json;
using System;

namespace AzureSQLWCFService
{
    public class Utilities:ConnectionBase
    {
        public string GetSystemInfo()
        {
            string querytext = "select @@servername as servername,system_user as loginuser,@@version as versioninfo,getdate() as datetimeinfo";
            Sysinfoclass sysinfoobject = new Sysinfoclass();

            //if (DatabaseName != "master" || connection.State != System.Data.ConnectionState.Open)
            //{
            //    string ConnectionResult = ChangeDatabasecontext("master");

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }

            //}
            var connectionString = "Server=tcp:" + ServerName + ",1433;Database=master;User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";
           using(SqlConnection connection = new SqlConnection(connectionString))
           {
              

                try
                {
                    connection.Open();
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                   
                                SqlCommand sqlcmd = new SqlCommand(querytext, connection);
                                SqlDataReader dr = sqlcmd.ExecuteReader();
                                while (dr.Read())
                                {
                            
                                    sysinfoobject.ServerName = dr.GetValue(0).ToString();
                                    sysinfoobject.LoginName = dr.GetValue(1).ToString();
                                    sysinfoobject.VersionInfo = dr.GetValue(2).ToString();
                                    sysinfoobject.DatetimeInfo = dr.GetValue(3).ToString();
                            
                                }
                                dr.Close();
                    
                        }

                return JsonConvert.SerializeObject(sysinfoobject);
            
               }

                catch (Exception e)
                {
                    return "Problem fetching SystemInfo::" + e.Message;
                }
                finally
                {
                    connection.Close();
                }
           }
           
        }
    }
}