using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class ProfileModel
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        public string EntName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }


        public bool SetProfile(string name, string password, string email)
        {
            
                string query = "";
                if (ProfileViewModel.pm.EntName.Equals(""))
                {
                    query = $"INSERT INTO profile (EntName, email, password) values ('{name}', '{email}', '{password}')";
                }
                else
                {
                    query = $"UPDATE profile SET EntName = '{name}', email = '{email}', password = '{password}' WHERE id = {1}";
                }
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
                Console.WriteLine("Add/Update Profile Exception: "+e.Message);

            }
            return false;
            
        }

        public ProfileModel GetProfile()
        {
            //TypeModel tm = new TypeModel();
            ProfileModel tp = new ProfileModel();
            tp.EntName = "";
            tp.Password = "";
            tp.Picture = "";
            string query = $"SELECT * FROM profile";
            try
            {
                Console.WriteLine($"Profile: {query}");
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    tp.EntName = reader["EntName"].ToString();
                    tp.Email = reader["email"].ToString();
                    tp.Password = reader["password"].ToString();
                    tp.Picture = reader["picture"].ToString();

                }


                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Profile Execption: "+e.Message);
            }

            return tp;
        }
    }
}
