using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AzureSQLWCFService
{
    public class DatabaseSizeDetails:ConnectionBase
    {
        SqlCommand sqlcmd = null;
        DatabaseSizeClass _databaseSize = new DatabaseSizeClass();
        ObservableCollection<DatabaseSizeClass> DatabaseSize = new ObservableCollection<DatabaseSizeClass>();

        public string GetDatabaseSize(string dbname)
        {
            //if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
            //{
            //    string ConnectionResult = ChangeDatabasecontext(dbname);

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }


            //}

            string querytext = "select cast(DATABASEPROPERTYEX  ( '"+dbname+"','MaxSizeInBytes' ) as bigint)/1048576000 as MaxSize, SUM(reserved_page_count)*8.0/1024 as UsedSize FROM sys.dm_db_partition_stats;";
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

                            dr.Read();
                            string dbsizemax = float.Parse(dr["MaxSize"].ToString()).ToString("F2");
                            string dbsizeused = float.Parse(dr["UsedSize"].ToString()).ToString("F2");
                      
                            //dbsize = float.Parse(dr["UsedSize"].ToString());
                            //decimal dbsizeused = Math.Round((Decimal)dbsize, 2, MidpointRounding.AwayFromZero);
                            //_databaseSize = new DatabaseSizeClass { Name = "TotalSize (" + dr["MaxSize"].ToString() + " MB)", Size = float.Parse(dbsizemax.ToString()) };
                            //DatabaseSize.Add(_databaseSize);
                            //_databaseSize = new DatabaseSizeClass { Name = "UsedSize (" + dr["UsedSize"].ToString() + " MB)", Size = float.Parse(dbsizeused.ToString()) };
                            //DatabaseSize.Add(_databaseSize);

                            _databaseSize = new DatabaseSizeClass { Name = "TotalSize (" + dr["MaxSize"].ToString() + " MB)", Size = float.Parse(dbsizemax) };
                            DatabaseSize.Add(_databaseSize);
                            _databaseSize = new DatabaseSizeClass { Name = "UsedSize (" + dr["UsedSize"].ToString() + " MB)", Size = float.Parse(dbsizeused) };
                            DatabaseSize.Add(_databaseSize);

                        }
                    }

                    return JsonConvert.SerializeObject(DatabaseSize);

                }

                catch (Exception ex)
                {

                    return "Problem fetching DatabaseDetails::" + ex.Message;
                }

                finally
                {

                    connection.Close();
                }
            }
        }

    }
}