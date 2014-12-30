using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    public class Utilities:ConnectionBase
    {
        public string getSystemInfo()
        {
            ObservableCollection<Sysinfoclass> Syslist = new ObservableCollection<Sysinfoclass>();
            string querytext = "select @@servername as servername,system_user as loginuser,@@version as versioninfo,getdate() as datetimeinfo";
            
            //   string QueryText = "select DB_NAME(sd.database_id) dbname,sd.state_desc statedesc, inr1.size SizeMB from sys.databases sd join (select database_id,sum((size*8)/1024) size from sys.master_files group by database_id) inr1 on inr1.database_id=sd.database_id";
                     
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                   
                        SqlCommand sqlcmd = new SqlCommand(querytext, connection);
                        SqlDataReader dr = sqlcmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Sysinfoclass sysinfoobject = new Sysinfoclass();
                            sysinfoobject.ServerName = dr.GetValue(0).ToString();
                            sysinfoobject.LoginName = dr.GetValue(1).ToString();
                            sysinfoobject.VersionInfo = dr.GetValue(2).ToString();
                            sysinfoobject.DatetimeInfo = dr.GetValue(3).ToString();
                            Syslist.Add(sysinfoobject);
                        }
                        dr.Close();
                    
                }

               // return JsonConvert.SerializeObject(Syslist);
                return "hello";
            }
            catch (Exception ex)
            {
                return "Something is wrong!!";
            }
        }
    }
}