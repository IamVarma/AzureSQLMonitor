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

            if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
                ChangeDatabasecontext(dbname);

            string querytext = "SELECT TOP 10 sys.objects.name as TableName, SUM(reserved_page_count) * 8.0 / 1024 as Size FROM sys.dm_db_partition_stats, sys.objects WHERE sys.dm_db_partition_stats.object_id = sys.objects.object_id GROUP BY sys.objects.name ORDER BY Size Desc";

            try
            {
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

                return "There is a problem listing the Table details";
            }

            finally
            {

                connection.Close();
            }
           


        }


    }
}