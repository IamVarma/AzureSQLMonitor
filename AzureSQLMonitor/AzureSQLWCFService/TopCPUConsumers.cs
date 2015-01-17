using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AzureSQLWCFService
{
    public class TopCPUConsumers:ConnectionBase
    {
        SqlCommand sqlcmd = null;
        ObservableCollection<TopCPUConsumersClass> TopQueries = new ObservableCollection<TopCPUConsumersClass>();
        private TopCPUConsumersClass _TopQueries;


        public string getCpuConsumers(string dbname)
        {
            if (connection.Database != dbname || connection.State == System.Data.ConnectionState.Closed)
            {
                string ConnectionResult = ChangeDatabasecontext(dbname);

                if (ConnectionResult != "Success")
                {
                    return ConnectionResult;
                }


            }

            string querytext = "SELECT TOP 10 sample_statement_text AS SQLText," +
                               "execution_count As ExecutionCount,total_cpu_time_ms As TotalCPU_ms,total_elapsed_time_ms As TotalElapsed_ms,cast(round((cast(total_cpu_time_ms as decimal(20,2))/ ( cast ((CASE total_elapsed_time_ms when 0 then 1 else total_elapsed_time_ms end) as decimal(20,2))))*100, 2,0) as decimal(20,2)) As PercentageCPU,total_elapsed_time_ms/execution_count As AverageCPUTimePerStatement_ms FROM" +
                               "(SELECT query_hash, query_plan_hash,COUNT (*) AS cached_plan_object_count,MAX (plan_handle) AS sample_plan_handle,SUM (execution_count) AS execution_count,SUM (total_worker_time)/1000 AS total_cpu_time_ms,SUM (total_elapsed_time)/1000 AS total_elapsed_time_ms,SUM (total_logical_reads) AS total_logical_reads,SUM (total_logical_writes) AS total_logical_writes,SUM (total_physical_reads) AS total_physical_reads FROM sys.dm_exec_query_stats GROUP BY query_hash, query_plan_hash) AS plan_hash_stats " +
                               "CROSS APPLY (SELECT TOP 1 qs.sql_handle AS sample_sql_handle,qs.statement_start_offset AS sample_statement_start_offset,qs.statement_end_offset AS sample_statement_end_offset,SUBSTRING (sql.[text],(qs.statement_start_offset/2) + 1,((CASE qs.statement_end_offset WHEN -1 THEN DATALENGTH(sql.[text]) WHEN 0 THEN DATALENGTH(sql.[text]) ELSE qs.statement_end_offset END - qs.statement_start_offset)/2) + 1) AS sample_statement_text FROM sys.dm_exec_sql_text(plan_hash_stats.sample_plan_handle) AS sql INNER JOIN sys.dm_exec_query_stats AS qs ON qs.plan_handle = plan_hash_stats.sample_plan_handle) AS sample_query_text ORDER BY total_cpu_time_ms DESC;";
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {

                    sqlcmd = new SqlCommand(querytext, connection);

                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            _TopQueries = new TopCPUConsumersClass
                            {
                                SQLText = dr["SQLText"].ToString(),
                                ExecutionCount = int.Parse(dr["ExecutionCount"].ToString()),
                                TotalCPUMs = int.Parse(dr["TotalCPU_ms"].ToString()),
                                TotalElapsedMs = int.Parse(dr["TotalElapsed_ms"].ToString()),
                                CPUPercentage = float.Parse(dr["PercentageCPU"].ToString()),
                                AverageCPUMs = int.Parse((dr["AverageCPUTimePerStatement_ms"].ToString()))

                            };

                            TopQueries.Add(_TopQueries);
                            _TopQueries = null;
                        }



                    }
                }

                return JsonConvert.SerializeObject(TopQueries);

            }

            catch (Exception ex)
            {

                return "Problem listing CPUQueries::"+ex.Message;
            }

            finally
            {

                connection.Close();
            }
           
        }


    }
}