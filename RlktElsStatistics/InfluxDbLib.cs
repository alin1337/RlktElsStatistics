using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RlktElsStatistics
{
    static class InfluxDbLib
    {
        public static string INFLUX_DB_HOST = "10.2.4.4:8086";
        public static string INFLUX_DB_NAME = "elstats";

        static public string Serialize(Dictionary<string, string> dict)
        {
            StringBuilder builder = new StringBuilder();
            foreach(KeyValuePair<string, string> pair in dict)
            {
                builder.Append( string.Format("{0},flavor=prod value={1}", pair.Key, pair.Value));
                
                if(dict.Values.Last() != pair.Value)
                    builder.Append('\n');
            }

            return builder.ToString();
        }

        static public void SendData(string data)
        {
            try
            {
                WebClient client = new WebClient();
                var reponse = client.UploadString(String.Format("http://{0}/write?db={1}", INFLUX_DB_HOST, INFLUX_DB_NAME), data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void SendData(Dictionary<string, string> dict)
        {
            SendData(Serialize(dict));
        }
    }
}
