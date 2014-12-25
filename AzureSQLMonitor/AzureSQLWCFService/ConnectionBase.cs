using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;


namespace AzureSQLWCFService
{
    public class ConnectionBase
    {
        public static SqlConnection connection;
        public static string connectionString;

        public static string ServerName { get; set; }
        public static string DatabaseName { get; set; }
        public static string LoginName { get; set; }
        public static string Password { get; set; }

        public string initialConnection(string Servername, string Loginname, string password)
        {
            ServerName = Servername;
            DatabaseName = "master";
            LoginName = Loginname;
            Password = password;

            connectionString = "Server=tcp:" + ServerName + ",1433;Database=" + DatabaseName + ";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";


            return getConnection(connectionString);
           

        }

        public string ChangeDatabasecontext(string databasename)
        {

            DatabaseName = databasename;

            connectionString = "Server=tcp:" + ServerName + ",1433;Database=" + DatabaseName + ";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            return getConnection(connectionString);


        }




        string getConnection(string connstring)
        {

            try
            {

                connection = new SqlConnection(connstring);
                connection.Open();
                // connection.OpenWithRetry(retry);
                if (connection.State == System.Data.ConnectionState.Open)

                    return "Success";

                else

                    return "Invalid Server Name/Invalid Login Name";

            }
            catch
            {
                return "Server not valid/Login not valid";
            }

        }


       


    }
}