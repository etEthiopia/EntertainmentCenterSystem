﻿using MySql.Data.MySqlClient;
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

        // Daily Stats === START

        // Get Daily Sells Pie-Chart
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

        // Get Daily Sells interms of 2 hours
        public Dictionary<int, double> getMainDailySellByHours()
        {
            Dictionary<int, double> dailysells = new Dictionary<int, double>();

            DateTime theDate = DateTime.Now;
            
            

            
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                string query = "";
                for (int h = 10; h <= 22; h = h+2)
                {
                    String today = theDate.ToString("yyyy-MM-dd");
                    string dt = today;
                    if (h == 10)
                    {
                        today += " 09:59:59";
                         query = $"SELECT sum(price) FROM fdb.audiosells where datetime like '{dt}%' AND datetime < '{today}' UNION ALL " +
                $"SELECT sum(price) FROM fdb.videosells where datetime like '{dt}%' AND datetime < '{today}' UNION ALL " +
                $"SELECT sum(price) FROM fdb.othersells where datetime like '{dt}%' AND datetime < '{today}';";

                    }
                    else
                    {
                        today = theDate.ToString("yyyy-MM-dd");
                        string up = "";
                        string down = "";
                        if((h-1).ToString().Length < 2)
                        {
                            up = today + $" 0{h - 1}:59:59";
                        }
                        else
                        {
                            up = today + $" {h - 1}:59:59";
                        }
                        if ((h - 2).ToString().Length < 2)
                        {
                            down = today + $" 0{h - 2}:00:00";
                        }
                        else
                        {
                            down = today + $" {h - 2}:00:00";
                        }
                        query = $"SELECT sum(price) FROM fdb.audiosells where datetime like '{today}%' AND datetime < '{up}' AND datetime > '{down}' UNION ALL " +
                    $"SELECT sum(price) FROM fdb.videosells where datetime like '{today}%' AND datetime < '{up}' AND datetime > '{down}' UNION ALL " +
                    $"SELECT sum(price) FROM fdb.othersells where datetime like '{today}%' AND datetime < '{up}' AND datetime > '{down}';";

                    }
                    Console.WriteLine($"GET DAILY SELL QUERY {h}: {query}");

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        double price = 0.0;
                        double.TryParse(reader["sum(price)"].ToString(), out price);
                        Console.WriteLine($"{h}, price= {price}");
                        if (dailysells.ContainsKey(h))
                        {
                            dailysells[h] += price;
                        }
                        else
                        {
                            dailysells[h] = price;
                        }
                        


                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Daily Hours Sell Exception, {e.Message}");
            }

            return dailysells;
        }

        // Get number of total counts of items
        public int getMainDailySellItems()
        {
            int dailysells = 0;

            DateTime theDate = DateTime.Now;




            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                string query = "";
                
                        string today = theDate.ToString("yyyy-MM-dd");
                        
                        query = $"SELECT count(id) FROM fdb.audiosells where datetime like '{today}%' UNION ALL " +
                    $"SELECT count(id) FROM fdb.videosells where datetime like '{today}%' UNION ALL " +
                    $"SELECT count(id) FROM fdb.othersells where datetime like '{today}%';";

                    
                    Console.WriteLine($"GET DAILY SELL QUERY ITEM : {query}");

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int items = 0;
                        int.TryParse(reader["count(id)"].ToString(), out items);
                    dailysells += items;



                    }
                    conn.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Daily Hours Sell Exception, {e.Message}");
            }

            return dailysells;
        }

        // Get Top three sells of the day
        public List<TopSellModel> getMainTopDailySell()
        {
            List<TopSellModel> dailytsells = new List<TopSellModel>();

            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            
            string query = $"SELECT name, sum(price) AS sm,type,count(name) AS cn FROM fdb.audiosells where datetime like '{today}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) AS cn FROM fdb.videosells where datetime like '{today}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) AS cn FROM fdb.othersells where datetime like '{today}%' GROUP BY name order by cn DESC LIMIT 3;";

           

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
                    tm.Count = int.Parse(reader["cn"].ToString()) + " SOLD";
                     tm.TotalPrice = float.Parse(reader["sm"].ToString()) +" BIRR";
                    
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

        // Daily Stats === END



        // Weekly Stats === START

        public List<TopSellModel> getMainTopWeeklySell()
        {
            List<TopSellModel> weeklytsells = new List<TopSellModel>();

            DateTime theDate = DateTime.Now.AddDays(1);
            DateTime theWDate = DateTime.Now.AddDays(-7);
            String today = theDate.ToString("yyyy-MM-dd");
            string week = theWDate.ToString("yyyy-MM-dd");

            string query = $"SELECT name, sum(price) AS sm,type,count(name) AS cn FROM fdb.audiosells where datetime < '{today}%' AND datetime > '{week}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) AS cn FROM fdb.videosells where datetime < '{today}%' AND datetime > '{week}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) AS cn FROM fdb.othersells where datetime < '{today}%' AND datetime > '{week}%' GROUP BY name order by cn DESC LIMIT 3;";



            Console.WriteLine($"GET TOP WWEKLY SELL QUERY: {query}");
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
                    tm.Count = int.Parse(reader["cn"].ToString()) + " SOLD";
                    tm.TotalPrice = float.Parse(reader["sm"].ToString()) + " BIRR";

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
                    weeklytsells.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Weekly Sell Exception, {e.Message}");
            }

            return weeklytsells;
        }

        // Get Weekly Sells interms of DAYS
        public Dictionary<int, double> getMainWeeklySellByDays()
        {
            Dictionary<int, double> dailysells = new Dictionary<int, double>();



            DateTime theDate = DateTime.Now;
            int mond = 0;
            switch (theDate.DayOfWeek.ToString())
            {
                case "Monday":
                    mond = 0;
                    break;
                case "Tuesday":
                    mond = 1;
                    break;
                case "Wednesday":
                    mond = 2;
                    break;
                case "Thursday":
                    mond = 3;
                    break;
                case "Friday":
                    mond = 4;
                    break;
                case "Saturday":
                    mond = 5;
                    break;
                case "Sunday":
                    mond = 6;
                    break;

            }
            String today = theDate.AddDays((-1 * (mond +  1))).ToString("yyyy-MM-dd");
            string week = theDate.AddDays((7 - mond)).ToString("yyyy-MM-dd");

            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                string query = "";

                today += " 23:59:59";
                query = $"SELECT sum(price),datetime FROM fdb.audiosells where datetime > '{today}%' AND datetime < '{week}%' GROUP BY datetime UNION ALL " +
               $"SELECT sum(price),datetime FROM fdb.videosells where datetime > '{today}%' AND datetime < '{week}%' GROUP BY datetime UNION ALL " +
               $"SELECT sum(price),datetime FROM fdb.othersells where datetime > '{today}%' AND datetime < '{week}%' GROUP BY datetime;";

                    
                    Console.WriteLine($"GET WEEKLY PER DAY SELL QUERY : {query}");

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    
                
                while (reader.Read())
                    {
                        double price = 0.0;
                    string date = reader["datetime"].ToString();
                        double.TryParse(reader["sum(price)"].ToString(), out price);

                    int key = 0;
                    switch (DateTime.Parse(date).DayOfWeek.ToString())
                    {
                        case "Monday":
                            key = 1;
                            break;
                        case "Tuesday":
                            key = 2;
                            break;
                        case "Wednesday":
                            key = 3;
                            break;
                        case "Thursday":
                            key = 4;
                            break;
                        case "Friday":
                            key = 5;
                            break;
                        case "Saturday":
                            key = 6;
                            break;
                        case "Sunday":
                            key = 7;
                            break;

                    }
                    Console.WriteLine(key + $", price= {price}");
                        
                        if (dailysells.ContainsKey(key))
                        {
                            dailysells[key] += price;
                        }
                        else
                        {
                            dailysells[key] = price;
                        }



                    }
                    conn.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Weekly Per Day Sell Exception, {e.Message}");
            }

            return dailysells;
        }

        // Weekly Stats === END



        // Monthly Stats === START

        // Get Monthly Sells Pie-Chart
        public Dictionary<string, double> getMainMonthlySell()
        {
            Dictionary<string, double> dailysells = new Dictionary<string, double>();

            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM");

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

        // Get Top three sells of the month
        public List<TopSellModel> getMainTopMonthlySell()
        {
            List<TopSellModel> monthlytsells = new List<TopSellModel>();

            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM");

            string query = $"SELECT name, sum(price) AS sm,type,count(name) AS cn FROM fdb.audiosells where datetime like '{today}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) AS cn FROM fdb.videosells where datetime like '{today}%' GROUP BY name UNION ALL " +
                $"SELECT name,sum(price) AS sm,type,count(name) AS cn FROM fdb.othersells where datetime like '{today}%' GROUP BY name order by cn DESC LIMIT 3;";



            Console.WriteLine($"GET TOP MONTHLY SELL QUERY: {query}");
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
                    tm.Count = int.Parse(reader["cn"].ToString()) + " SOLD";
                    tm.TotalPrice = float.Parse(reader["sm"].ToString()) + " BIRR";

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
                    monthlytsells.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get monthly Sell Exception, {e.Message}");
            }

            return monthlytsells;
        }

        // Monthly Stats === END


    }
}
