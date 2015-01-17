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

            if (ServerName != null && DatabaseName != null && LoginName != null && Password != null)
            {
                connectionString = "Server=tcp:" + ServerName + ",1433;Database=" + DatabaseName + ";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";
                return getConnection(connectionString);
            }

            else
            {
                return "Connection is Closed, please login again";
            }
           

        }

        public string ChangeDatabasecontext(string databasename)
        {

            DatabaseName = databasename;

            if (ServerName != null && DatabaseName != null && LoginName != null && Password != null)
            {
                connectionString = "Server=tcp:" + ServerName + ",1433;Database=" + DatabaseName + ";User ID=" + LoginName + ";Password=" + Password + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=10;Application Name=AzureMonitor;";
                return getConnection(connectionString);
            }

            else
            {
                return "Connection is Closed, please login again";
            }

        }




        string getConnection(string connstring)
        {

            try
            {
             
                connection = new SqlConnection(connstring);
                connection.Open();
              

                return "Success";
            }
            catch(Exception e)
            {
                return "Problem opening a connection::"+e.Message;
            }

        }


       


    }
}