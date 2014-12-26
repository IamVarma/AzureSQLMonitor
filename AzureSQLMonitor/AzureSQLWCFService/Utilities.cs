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
        public string[] getSystemInfo()
        {
            string [] querytext = new string[4];
            string[] resultstext = new string[4];
            //   string QueryText = "select DB_NAME(sd.database_id) dbname,sd.state_desc statedesc, inr1.size SizeMB from sys.databases sd join (select database_id,sum((size*8)/1024) size from sys.master_files group by database_id) inr1 on inr1.database_id=sd.database_id";
            querytext[0] = "select @@servername as servername";
            querytext[1] = "select system_user as loginuser";
            querytext[2] = "select @@version as versioninfo";
            querytext[3] = "select getdate() as datetimeinfo";

           
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        SqlCommand sqlcmd = new SqlCommand(querytext[i], connection);
                        SqlDataReader dr = sqlcmd.ExecuteReader();
                        resultstext[i] = dr["servername"].ToString();
                    }
                }

                return resultstext;
            }
            catch (Exception ex)
            {
                return new string [] {"Something is wrong!!"};
            }
        }
    }
}