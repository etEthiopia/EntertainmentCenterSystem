using DagiCaliburn.ViewModels;
using DagiCaliburn.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.Models
{
    class GoalModel
    {
        public static float yesterdy = 0.0f;
        public static float Goal = 0.0f;
        public static float percc = 0.0f;
        public static string messagePerc = "";
        //Get Yesterdays Earnings
        public static float yesterdaysEarning()
        {
            
            MySqlConnection conn = DBUtils.GetDBConnection();
            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);
            string ydy = yesterday.ToString("yyyy-MM-dd");
            string query = $" SELECT SUM(price) FROM fdb.videosells WHERE datetime like '{ydy}%' UNION ALL SELECT SUM(price) FROM fdb.audiosells WHERE datetime like '{ydy}%' UNION ALL SELECT SUM(price) FROM fdb.othersells WHERE datetime like '{ydy}%' ";


            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"getEpisodes() - {query}");

                

                while (reader.Read())
                {
                if (reader["SUM(price)"].ToString().Length > 0)
                {
                    yesterdy += float.Parse(reader["SUM(price)"].ToString());
                }
                }
                conn.Close();
        }
            catch (Exception e)
            {
                Console.WriteLine($"Get Yesterdays Earnings Execption {e.Message}");
            }
            Console.WriteLine($"YESTERDAY {yesterdy}");
            return yesterdy;

        }

        //Progress Bar and String
        public static void FixProgress()
        {
            if (Goal > 0.0) {

                float perc = (100 * MainViewModel.TE) / Goal;
                if (((100 * MainViewModel.TE) / Goal) > 100)
                {
                    percc = 100;
                    messagePerc = "Goal Exceeded Congra !";
                    //StartViewModel.mainview.ProgressValue = "100%";
                    Console.WriteLine($"1 PERC {perc},ProgressToGo {Goal - MainViewModel.TE} ");
                }
                else if (((100 * MainViewModel.TE) / Goal) < 25)
                {
                    //StartViewModel.mainview.ProgressValue = perc + "%";
                    percc = perc;
                    messagePerc = Goal - MainViewModel.TE + " BIRR to go !";
                    Console.WriteLine($"2 PERC {perc},ProgressToGo {Goal - MainViewModel.TE} ");
                }
                else
                {
                    //StartViewModel.mainview.ProgressValue = perc + "%";
                    percc = perc;
                    messagePerc = "Only "+ (Goal - MainViewModel.TE) + " BIRR to go !";
                    Console.WriteLine($"3 PERC {perc},ProgressToGo {Goal - MainViewModel.TE} ");
                }
            }
            else
            {
                GetGoal();
                float perc = (100 * MainViewModel.TE) / Goal;
                if (((100 * MainViewModel.TE) / Goal) > 100)
                {
                    percc = 100;
                    messagePerc = "Goal Exceeded Congra !";
                    //StartViewModel.mainview.ProgressValue = "100%";
                    Console.WriteLine($"4 PERC {perc},ProgressToGo {Goal - MainViewModel.TE} ");
                }
                else if (((100 * MainViewModel.TE) / Goal) < 25)
                {
                    percc = perc;
                    messagePerc = Goal - MainViewModel.TE + " BIRR  to go !";
                    //StartViewModel.mainview.ProgressValue = perc + "%";
                    Console.WriteLine($"5 PERC {perc},ProgressToGo {Goal - MainViewModel.TE} ");
                }
                else
                {
                    percc = perc;
                    //StartViewModel.mainview.ProgressValue = perc + "%";
                    messagePerc = "Only " + (Goal - MainViewModel.TE) + " BIRR to go !";
                    Console.WriteLine($"6 PERC {perc},ProgressToGo {Goal - MainViewModel.TE} ");
                }
            }
            
        }
        
        //Get Goal
        public static void GetGoal()
        {
            
                MySqlConnection conn = DBUtils.GetDBConnection();
                var today = DateTime.Now;
                string tod = today.ToString("yyyy-MM-dd");
                string query = $" SELECT goal FROM fdb.dailygoal WHERE date like '{tod}%' ";


                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine($"CheckGoal() - {query}");



                    while (reader.Read())
                    {
                        if (reader["goal"].ToString().Length > 0)
                        {
                        Goal = float.Parse(reader["goal"].ToString());
                        break;
                        }
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Get Check Goal Execption {e.Message}");
                }
                
            
        }

        //Check if the goal is set for the day
        public static bool CheckGoal()
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            var today = DateTime.Now;
            string tod = today.ToString("yyyy-MM-dd");
            string query = $" SELECT COUNT(id) FROM fdb.dailygoal WHERE date like '{tod}%' ";


            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"CheckGoal() - {query}");



                while (reader.Read())
                {
                    if (reader["COUNT(id)"].ToString().Length > 0)
                    {
                        if (int.Parse(reader["COUNT(id)"].ToString()) == 1)
                        {
                            return true;
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Check Goal Execption {e.Message}");
            }
            return false;
        }

        //Set goal for the day
        public static bool SetGoal(float goal)
        {
            string query = "";
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            query = $"INSERT INTO dailygoal" +
            $" (date, goal) values" +
            $" ('{today}', {goal} )";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    
                    return true;
                

            }
            catch (Exception e)
            {
                Console.WriteLine($"Set Goal Exception {e.Message}");
            }
            return false;
        }

        
    }
}
