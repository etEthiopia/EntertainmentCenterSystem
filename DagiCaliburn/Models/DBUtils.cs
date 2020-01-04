using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3307;
            string database = "fdb";
            string username = "root";
            string password = "root";

            return DBMySQLUtils.GetDBConnection(host, port,database,username,password);
        }

    }
}
