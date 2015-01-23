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
            DatabaseListClass dbaseelement;
          //  int dbaseindex = 0;
           //string QueryText = "select DB_NAME(sd.database_id) dbname,sd.state_desc statedesc, inr1.size SizeMB from sys.databases sd join (select database_id,sum((size*8)/1024) size from sys.master_files group by database_id) inr1 on inr1.database_id=sd.database_id";
            string QueryText = "SELECT database_name=database_name.name,database_name.state_desc as currentstate,current_slo=current_slo.name,database_name.user_access_desc as access,(CASE database_name.is_encrypted WHEN 0 THEN 'FALSE' WHEN 1 THEN 'TRUE' END) as isencrypt,(CASE database_name.is_read_only WHEN 0 THEN 'FALSE' WHEN 1 THEN 'TRUE' END) as isread FROM slo_database_objectives AS database_slo INNER JOIN slo_service_objectives AS current_slo ON database_slo.current_objective_id = current_slo.objective_id INNER JOIN slo_service_objectives AS target_slo ON database_slo.configured_objective_id = target_slo.objective_id INNER JOIN sys.databases AS database_name  ON database_slo.database_id = database_name.database_id;";
            string queryfordatabasesize = "SELECT SUM(reserved_page_count)*8.0/1024 as dbasesize FROM sys.dm_db_partition_stats";
            SqlCommand sqlcmd;

            //if (DatabaseName != "master" || connection.State != System.Data.ConnectionState.Open)
            //{
            //    string ConnectionResult=ChangeDatabasecontext("master");

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }

            //}
         var connectionString = "Server=tcp:" + ServerName + ",1433;Database=master;User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";
            
            using(SqlConnection connection= new SqlConnection(connectionString))
            {
               try
               {

                   connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {

                    sqlcmd = new SqlCommand(QueryText, connection);

                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {
                            string db = dr["database_name"].ToString();
                            if (db != "master")
                            {
                                dbaseelement = new DatabaseListClass
                                {
                                    DatabaseName = dr["database_name"].ToString(),
                                    DatabaseState = dr["currentstate"].ToString(),
                                    DatabaseSlo = dr["current_slo"].ToString(),
                                    AccessDesc = dr["access"].ToString(),
                                    IsEncrypt = dr["isencrypt"].ToString(),
                                    IsReadOnly = dr["isread"].ToString()
                                };
                                _dbase.Add(dbaseelement);
                                dbaseelement = null;
                            }
                        }
                        dr.Close();                   
                    }

                }
              }
                
                        catch (Exception ex)
                    {
                        return "Problem listing databases::"+ex.Message;
                    }


            
            
                    finally
                    {
                        connection.Close();
                    }
            }       //connection.Close();
                   
            
            
                    int whileindex=0;
                    while (whileindex < _dbase.Count)
                    {
                        var connectionstring1 = "Server=tcp:" + ServerName + ",1433;Database="+_dbase[whileindex].DatabaseName+";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";
                        
                        using(SqlConnection connection1 = new SqlConnection(connectionstring1))
                        {
                            try{

                                connection1.Open();

                                sqlcmd = new SqlCommand(queryfordatabasesize, connection1);
                                using (SqlDataReader dr = sqlcmd.ExecuteReader())
                                {
                                    while (dr.Read())
                                    {
                                        _dbase[whileindex].DatabaseSize = dr["dbasesize"].ToString();//+" (MB)";
                                        DBList.Add(_dbase[whileindex]);
                                    }

                                }
                              whileindex += 1;
                            }
                        
                       catch (Exception ex)
                                {
                                    return "Problem listing databases::"+ex.Message;
                                }


            
            
                                finally
                                {
                                    connection.Close();
                                }

                        }
                    }
                    return JsonConvert.SerializeObject(DBList);
                }

                
            }
            
        }
