using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.Models
{
    class StatisticsModel
    {

       
        TypeModel tm = new TypeModel();

        public Dictionary<string, double> getMainDailySell()
        {
            Dictionary<string, double> dailysells = new Dictionary<string, double>();
            
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");

            string query = $"SELECT sum(price),type FROM fdb.audiosells where datetime like '{today}%' GROUP BY type UNION ALL " +
                $"SELECT sum(price),type FROM fdb.videosells where datetime like '{today}%' GROUP BY type UNION ALL " +
                $"SELECT sum(price),type FROM fdb.othersells where datetime like '{today}%' GROUP BY type;";

            Console.WriteLine($"GET DAILY SELL QUERY: {query}");
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($" TYPE:  " + int.Parse(reader["type"].ToString()));
                    string ic = tm.GetIconFromId(int.Parse(reader["type"].ToString()));
                    double price = double.Parse(reader["sum(price)"].ToString());
                    if (dailysells.ContainsKey(ic))
                    {
                        dailysells[ic] += price;
                    }
                    else
                    {
                        dailysells[ic] = price;
                    }


                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Daily Sell Exception, {e.Message}");
            }

            return dailysells;
        }

        public List<TopSellModel> getMainTopDailySell()
        {
            List<TopSellModel> dailytsells = new List<TopSellModel>();

            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");

            string query = $"SELECT name, sum(price) AS sm,type,count(name) FROM fdb.audiosells where datetime like '{today}%' GROUP BY type UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) FROM fdb.videosells where datetime like '{today}%' GROUP BY type UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) FROM fdb.othersells where datetime like '{today}%' GROUP BY type order by sm DESC LIMIT 3;";

           

            Console.WriteLine($"GET TOP DAILY SELL QUERY: {query}");
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);


                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();



                while (reader.Read())
                {
                    TopSellModel tm = new TopSellModel();

                    tm.Name = reader["name"].ToString();
                    tm.Count = int.Parse(reader["count(name)"].ToString());
                     tm.TotalPrice = float.Parse(reader["sm"].ToString());
                    
                    if (int.Parse(reader["type"].ToString()) == 0)
                    {
                        tm.Initials = "F";

                        //tm.Folder = int.Parse(reader["folderserial"].ToString());
                    }
                    else
                    {
                        TypeModel mk = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                        tm.Initials = mk.Icon;
                    }
                    Console.WriteLine($"NAME: {tm.Name}, COUNT: {tm.Count}, TOTALPRICE: {tm.TotalPrice}, INITIALS: {tm.Initials}");
                    dailytsells.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Daily Sell Exception, {e.Message}");
            }

            return dailytsells;
        }

        public static List<SellModel> GetAllSpecials()
        {
            List<SellModel> ts = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            string query = $"SELECT name,sum(price),count(id),type FROM fdb.audiosells where datetime like '{today}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price),count(id),type FROM fdb.videosells where datetime like '{today}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price),count(id),type FROM fdb.othersells where datetime like '{today}%' GROUP BY name order by 2 DESC LIMIT 3;";
            try
            {
                Console.WriteLine($"DAta query {query}");
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SellModel tm = new SellModel();
                    tm.Name = reader["name"].ToString();
                    tm.Type = TypeModel.GetTypeToFile((int)reader["type"]);
                    tm.Price = float.Parse(reader["sum(price)"].ToString());
                    tm.Initials = tm.Type.Icon;
                    tm.Item = int.Parse(reader["count(id)"].ToString());
                    //MessageBox.Show($"{tm.Name}, {tm.Price}, {tm.Item}");
                    ts.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Top Sells Execption , {e.Message}");
            }
            return ts;

        }
    }
}
