using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class SpecialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime ExpireDate { get; set; }
        public string AcDate { get; set; }

        MySqlConnection conn = DBUtils.GetDBConnection();

        //Add Special 
        public bool AddSpecial()
        {
            string query = "";
            bool update = false;
            if (!AcDate.Equals("NULL"))
            {
                query = $"INSERT INTO specials" +
                $" (name, price, expiredate) values" +
                $" ('{Name}', {Price} ,'{AcDate}') " +
                $"ON DUPLICATE KEY UPDATE price={Price}, expiredate='{AcDate}'";
            }
            else
            {
                query = $"INSERT INTO specials" +
                $" (name, price, expiredate) values" +
                $" ('{Name}', {Price}, NULL) " +
                $"ON DUPLICATE KEY UPDATE price={Price}, expiredate= NULL";
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                string qy = $"SELECT id FROM specials WHERE name = '{Name}'";
                int i = 0;

                MySqlCommand cmmd = new MySqlCommand(qy, conn);


                conn.Open();
                MySqlDataReader reader = cmmd.ExecuteReader();


                Console.WriteLine($"Checking-- {Name}; {qy}; {query}");

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    //query = $"UPDATE specials SET price = {Price}, expiredate = '{AcDate}' WHERE id =  {id} AND name = '{Name}'";
                    update = true;
                    break;
                }
                reader.Close();
                conn.Close();

                Console.WriteLine($"SALE I {Name} QY {query} UPDATE {update}");
                //if (update)
                //{
                //    conn.Open();
                //    MySqlDataReader myReader;
                //    myReader = cmd.ExecuteReader();
                //    while (myReader.Read())
                //    {
                //    }
                //    conn.Close();
                //    return true;
                //}
                //else
                //{
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    return true;

                //}
            }
            catch (Exception e)
            {
                Console.WriteLine($"Add Special Exception {Name} {e.Message}");
            }
            return false;
        }

        //Delete Special
        public bool Delete()
        {
            string query = "";
            query = $"DELETE FROM specials WHERE id = {this.Id}";
                

            try
            {
                

                MySqlCommand cmmd = new MySqlCommand(query, conn);


                conn.Open();
                MySqlDataReader reader = cmmd.ExecuteReader();


               
                while (reader.Read())
                {
                    
                }
                reader.Close();
                conn.Close();

                return true;

                //}
            }
            catch (Exception e)
            {
                Console.WriteLine($"Delete Special Exception {Name} {e.Message}");
            }
            return false;
        }

        //Gets All Specials
        public static List<SpecialModel> GetAllSpecials()
        {
            List<SpecialModel> ts = new List<SpecialModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT * FROM specials ORDER BY id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SpecialModel tm = new SpecialModel();
                    tm.Name = reader["name"].ToString();
                    tm.Id = int.Parse(reader["id"].ToString());
                    tm.Price = float.Parse(reader["price"].ToString());
                    string d = reader["expiredate"].ToString();
                    if (!(d.Equals("") || d.Equals("NULL")))
                        {
                        tm.ExpireDate = DateTime.Parse(d);
                    }
                    tm.AcDate = d;
                    ts.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Specials Execption , {e.Message}");
            }
            return ts;

        }
    }
}
