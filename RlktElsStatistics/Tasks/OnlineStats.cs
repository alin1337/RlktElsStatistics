using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RlktElsStatistics
{
    class OnlineStats : RunTask
    {
        public OnlineStats(string name_, RunType type_)
        {
            name = name_;
            type = type_;
        }

        public override void Run()
        {
            SqlConnection sqlconn =  SqlLib.GetSqlConnection();
            if (sqlconn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand sqlCommand = new SqlCommand("_Stats_EveryMinute", sqlconn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                Dictionary<string, string> dictValues = new Dictionary<string, string>();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        for( int i = 0; i < rdr.FieldCount; i++)
                        {
                            string columnName = rdr.GetName(i);
                            string columnValue = rdr.GetValue(i).ToString();


                            dictValues.Add(columnName, columnValue);
                        }
                    }
                }

                InfluxDbLib.SendData(dictValues);

                Console.WriteLine( string.Format("[{0}] Just Ran.", name) );
            }
        }
    }
}
