using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace DagiCaliburn.Models
{
    class DBMySQLUtils
    {
        public static MySqlConnection
                 GetDBConnection(string host, int port, string database, string username, string password)

        {
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password" + password + ";";

            String connectionString = "SERVER=" + host + ";" + "DATABASE=" +
            database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";" + "PORT=3307;";
            //Debug.WriteLine($"Connectionnnnnnnnnnnnnnnnnnnnnnnn ---- {connectionString}");
            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
    }
}
