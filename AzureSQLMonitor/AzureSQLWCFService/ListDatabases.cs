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
    public class ListDatabases:ConnectionBase
    {

       

        public string getDatabases()
        {

            ObservableCollection<DatabaseListClass> DBList = new ObservableCollection<DatabaseListClass>();
            //DatabaseListClass[] _dbase = new DatabaseListClass [] {};
            List<DatabaseListClass> _dbase = new List<DatabaseListClass>();
            string[] x = new string[3];
          //  int dbaseindex = 0;
           //string QueryText = "select DB_NAME(sd.database_id) dbname,sd.state_desc statedesc, inr1.size SizeMB from sys.databases sd join (select database_id,sum((size*8)/1024) size from sys.master_files group by database_id) inr1 on inr1.database_id=sd.database_id";
            string QueryText = "select name,state_desc from sys.databases";
            string queryfordatabasesize = "SELECT SUM(reserved_page_count)*8.0/1024 as dbasesize FROM sys.dm_db_partition_stats";
            SqlCommand sqlcmd;

            if (DatabaseName != "master" || connection.State != System.Data.ConnectionState.Open)
            {
                ChangeDatabasecontext("master");
            }

            

            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {

                    sqlcmd = new SqlCommand(QueryText, connection);

                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            string db = dr["name"].ToString();
                            if (db != "master")
                            {
                                DatabaseListClass dbaseelement = new DatabaseListClass();
                                dbaseelement.DatabaseName = dr["name"].ToString();
                                dbaseelement.DatabaseState = dr["state_desc"].ToString();
                                _dbase.Add(dbaseelement);
                            }
                            
                        }
                        dr.Close();                   
                    }
                    //connection.Close();
                    int whileindex=0;
                    while (whileindex < _dbase.Count)
                    {
                        ChangeDatabasecontext(_dbase[whileindex].DatabaseName);
                        sqlcmd = new SqlCommand(queryfordatabasesize, connection);
                        using (SqlDataReader dr = sqlcmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                _dbase[whileindex].DatabaseSize = dr["dbasesize"].ToString()+" (MB)";
                                DBList.Add(_dbase[whileindex]);
                            }

                        }
                    whileindex += 1;
                    
                    }
                    
                }

                return JsonConvert.SerializeObject(DBList);
            }
            catch (Exception ex)
            {
                return "There is an exception listing the databases in the instance: "+ex;
            }

            
            
            
            finally
            {
                connection.Close();
            }

        }
    }
}