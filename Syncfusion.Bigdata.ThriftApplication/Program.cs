using Syncfusion.ThriftHive.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Bigdata.ThriftApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string ThriftServerHostname = string.Empty;
            int ThriftServerPort = 10001;
            string[] ThriftServer = args[0].Split(':');
            ThriftServerHostname = ThriftServer[0];
            ThriftServerPort = Convert.ToInt32(ThriftServer[1]);
            //Checks wheather Spark-Thrift server is running or not.
            if (CheckConnectivityForProxyHost(ThriftServerHostname,ThriftServerPort))
            {
                ExecuteHqlQuery(ThriftServerHostname,ThriftServerPort, args[1]);
            }
            else
            {
                Console.WriteLine("Please ensure Spark Thrift Server is running in the Host " + ThriftServerHostname);
            }
            Console.WriteLine("Please any key to exit");
            Console.ReadKey();
        }

 
        /// <summary>
        /// Check wheather Spark-Thrift server running or not. 
        /// </summary>
        /// <param name="ippaddress"></param>
        /// <param name="port"></param>
        /// <returns>True if connection can be made, False if the host is unreachble</returns>
        public static bool CheckConnectivityForProxyHost(string ippaddress, int port)
        {
            var connection = new TcpClient();
            try
            {
                //Request the Remote server is UP and able to connect with the client
                var request = connection.BeginConnect(ippaddress, port, null, null);
                var isConnected = request.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(1000));
                if (!isConnected)
                {
                    return false;
                }
                connection.EndConnect(request);
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Thirft API to Execute the Query passed in argument.
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="query"></param>
        private static void ExecuteHqlQuery(string hostName,int port,string query)
        {
              //Initializing the Spark thrift server connection
            HqlConnection con = new HqlConnection(hostName, port, HiveServer.HiveServer2); 
            con.Open();
            //Creating Query to fetch all data set from the Database
            HqlCommand command = new HqlCommand(query, con);
            DateTime executionStartTime = DateTime.Now;
            //Execution query to fetch data from  Database 
            HqlDataReader reader = command.ExecuteReader();
            reader.FetchSize = int.MaxValue;
            DateTime executionEndTime = DateTime.Now;
            //Fetches the result from the reader and store it in a object 
            HiveResultSet result = reader.FetchResult();
            foreach (var row in result)
            {
                foreach (var data in row)
                {
                    Console.WriteLine(data);
                }
            }
            Console.WriteLine("Execution Start Time :"+ executionStartTime);
            Console.WriteLine("Execution End Time :" + executionEndTime);
            con.Close();
        }
    }
}
