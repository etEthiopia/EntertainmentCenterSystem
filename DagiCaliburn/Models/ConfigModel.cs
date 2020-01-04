using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class ConfigModel
    {
        public static object ConfigViewMode { get; private set; }

        //Sets the static variable TE in MainViewModel to TOdaysEarnings
        public static void getConfig()
        {

            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            
            string query = $" SELECT * FROM fdb.config";


            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                

                while (reader.Read())
                {
                    ConfigViewModel.lightdark =  reader["lightdark"].ToString();
                    ConfigViewModel.primary = reader["primarry"].ToString();
                    ConfigViewModel.accent = reader["accent"].ToString();
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Config Error {e.Message}");
            }


        }

        public static void updateConfig()
        {

            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();

            string query = $" UPDATE fdb.config SET lightdark = '{ConfigViewModel.lightdark}', primarry = '{ConfigViewModel.primary}', accent = '{ConfigViewModel.accent}' WHERE id = 1";


            try
            {
                Console.WriteLine($"UPDATE CONFIG {query}");
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                

                while (reader.Read())
                {
                   
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Config Error {e.Message}");
            }


        }
    }
}
