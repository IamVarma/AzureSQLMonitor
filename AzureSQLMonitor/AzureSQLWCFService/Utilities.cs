
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    public class Utilities:ConnectionBase
    {
        public string GetSystemInfo()
        {
           // ObservableCollection<Sysinfoclass> Syslist = new ObservableCollection<Sysinfoclass>();
            string querytext = "select @@servername as servername,system_user as loginuser,@@version as versioninfo,getdate() as datetimeinfo";
            Sysinfoclass sysinfoobject = new Sysinfoclass();

            if (DatabaseName != "master" || connection.State != System.Data.ConnectionState.Open)
            {
                ChangeDatabasecontext("master");
            }

          //  string QueryText = "select DB_NAME(sd.database_id) dbname,sd.state_desc statedesc, inr1.size SizeMB from sys.databases sd join (select database_id,sum((size*8)/1024) size from sys.master_files group by database_id) inr1 on inr1.database_id=sd.database_id";
                     
            try
            {
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
                //sysinfoobject.ServerName = "varma";
                //return "hello " + sysinfoobject.ServerName;
            }
            catch 
            {
                return "Something is wrong!!";
            }
            finally
            {
                connection.Close();
            }
        }
    }
}