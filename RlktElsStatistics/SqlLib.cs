using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RlktElsStatistics
{
    class SqlLib
    {
        //
        string DB_HOST = "10.0.0.3";
        string DB_USER = "elapi_prod_stats";
        string DB_PASS = "どいてください";
        string DB_NAME = "LogWarehouse";

        public string connection_string = @"Server={0};Database={1};User Id={2};Password={3};";
        public static SqlConnection connection = null;
        public SqlLib()
        {
            connect();
        }

        public void connect()
        {
            connection = new SqlConnection( string.Format(connection_string, DB_HOST, DB_NAME, DB_USER, DB_PASS) );
            connection.Open();
        }

        public void disconnect()
        {
            connection.Close();
        }

        public static SqlConnection GetSqlConnection()
        {
            return connection;
        }
    }
}
