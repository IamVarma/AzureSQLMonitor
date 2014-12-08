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
            DatabaseListClass _dbase;
            string QueryText = "select * from sys.databases";
            SqlCommand sqlcmd;
           
            

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
                                _dbase = new DatabaseListClass { DatabaseName = dr["name"].ToString(), DatabaseState = dr["state_desc"].ToString() }; //, DatabaseSize = QueryDatabaseSize(dr["name"].ToString()) };
                                DBList.Add(_dbase);
                                _dbase = null;
                            }

                        }

                        
                    }

                    
                }

                return JsonConvert.SerializeObject(DBList);

            }
            catch (Exception ex)
            {
                return "There is an exception listing the databases in the instance";
            }
        }
    }
}