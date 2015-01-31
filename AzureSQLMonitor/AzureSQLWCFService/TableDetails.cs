using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    public class TableDetails:ConnectionBase
    {

        SqlCommand sqlcmd = null;

        public string getTableSizeDetails(string dbname)
        {
            ObservableCollection<TableSizeDetails> DatabaseTableDetails = new ObservableCollection<TableSizeDetails>();
            TableSizeDetails _tabledetails;

            //if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
            //{
            //    string ConnectionResult = ChangeDatabasecontext(dbname);

            //    if (ConnectionResult != "Success")
            //    {
            //        return ConnectionResult;
            //    }


            //}


            string querytext = "SELECT TOP 5 sys.objects.name as TableName, SUM(reserved_page_count) * 8.0 / 1024 as Size FROM sys.dm_db_partition_stats, sys.objects WHERE sys.dm_db_partition_stats.object_id = sys.objects.object_id GROUP BY sys.objects.name ORDER BY Size Desc";
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
                                _tabledetails = new TableSizeDetails { TableName = dr["TableName"].ToString(), TableSize = dr["Size"].ToString() };
                                DatabaseTableDetails.Add(_tabledetails);
                                _tabledetails = null;


                            }


                        }
                    }

                    return JsonConvert.SerializeObject(DatabaseTableDetails);

                }

                catch (Exception ex)
                {

                    return "Problem listing TableSizes::" + ex.Message;
                }

                finally
                {

                    connection.Close();
                }
            }


        }


    }
}