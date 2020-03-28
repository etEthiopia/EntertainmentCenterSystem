using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class TypeModel
    {
        public List<String> directories;
        private string _name = "";
        public String Name { get { return _name; } set { _name = value; } }
        public double Price { set; get; }
        public string Refrence { set; get; }
        public int Id { set; get; }
        public String Icon { set; get; }
        public String DataType { set; get; }
        public object Directory { get; private set; }

        MySqlConnection conn = DBUtils.GetDBConnection();

        public TypeModel() { }

        private TypeModel(int id, String name, double price) {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Icon = "";
            
        }

        private TypeModel(int id, String name, double price, string dt)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.DataType = dt;
            this.Icon = "";
            String[] ic = name.Split(' ');
            for (int i = 0; i < ic.Length; i++)
            {
                this.Icon += ic[i][0];
            }
        }

        public static int GetTypeIntReference(string name)
        {
            List<string> similars = new List<string>();
            List<double> compared = new List<double>();
            List<int> types = new List<int>();
            string query = $"SELECT id,reference FROM itemtypes WHERE reference IS NOT NULL";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    string reff = reader["reference"].ToString();
                    //Console.WriteLine($"getType() {dir} , {Directory}");
                    // 
                    if (name.Contains(reff) && reff.Length > 0)
                    {
                        compared.Add(StringSimilarity.CalculateSimilarity(name, reff));
                        similars.Add(reff);
                        int tp = 0;
                        if (int.TryParse(reader["id"].ToString(), out tp))
                        {
                            Console.WriteLine($"NAME {name}; REF {reff}; TID {tp}");
                            types.Add(tp);
                        }
                        

                    }

                }


                if (compared.Count > 0)
                {
                    int indexMax = compared.IndexOf(compared.Max());
                    conn.Close();
                    return types[indexMax];
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTypeRef Execption");
            }

            return 0;
        }

        public static int GetTypeInt(string Dir)
        {
            //TypeModel tm = new TypeModel();
            List<string> similars = new List<string>();
            List<double> compared = new List<double>();
            List<int> types = new List<int>();
            string query = $"SELECT dir,type FROM dirs";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    string dir = reader["dir"].ToString();
                    //Console.WriteLine($"getType() {dir} , {Directory}");

                    if (Dir.Contains(dir))
                    {
                        compared.Add(StringSimilarity.CalculateSimilarity(Dir.ToUpper(), dir.ToUpper()));
                        similars.Add(dir);
                        int tp = 0;
                        if (int.TryParse(reader["type"].ToString(), out tp)) ;
                        types.Add(tp);
                        
                    }

                }
               

                if (compared.Count > 0)
                {
                    int indexMax = compared.IndexOf(compared.Max());
                    conn.Close();
                    
                    return types[indexMax];
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }

            return 0;
        }

        public static TypeModel GetTypeToFile(int id)
        {
            TypeModel tm = new TypeModel();
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT * FROM itemtypes WHERE id = {id}";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"GetTypeToFile.  Id: {id},  Connection and Reader are working");

                while (reader.Read())
                {
                    tm.Id = (int)reader["id"];
                    tm.Name = reader["name"].ToString();
                    tm.Price = Double.Parse(reader["price"].ToString());
                    tm.Icon = reader["initials"].ToString();
                    tm.DataType = reader["filetype"].ToString();
                    tm.Refrence = reader["reference"].ToString();
                    //Console.WriteLine($"GetTypeToFile.  Id: {id},  Name: {tm.Name}, Price: {tm.Price}");
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            return tm;

        }

        public string GetIconFromId(int id)
        {
            string name = "";
            string query = $"SELECT initials FROM itemtypes WHERE id = {id}";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    name = reader["initials"].ToString();
                    
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Name from Icon Execption " + e.Message);
            }
            return name;
        }

        public TypeModel GetType(int id)
        {
            TypeModel tm = new TypeModel();
            string query = $"SELECT * FROM itemtypes WHERE id = {id}";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tm.Id = (int)reader["id"];
                    tm.Name = reader["name"].ToString();
                    tm.Price = Double.Parse(reader["price"].ToString());
                    tm.Icon = reader["initials"].ToString();
                    tm.DataType = reader["filetype"].ToString();
                    Console.WriteLine($"DT {tm.DataType}");
                    String[] ic = tm.Name.Split(' ');
                    
                    for (int i = 0; i < ic.Length; i++)
                    {

                        tm.Icon += ic[i][0];
                    }

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption "+ e.Message);
            }
            return tm;

        }

        public static int GetTypeFromName(string name)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT id FROM itemTypes WHERE name = '{name}'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID FROM NAME : " + reader["id"].ToString());
                    return (int)reader["id"];
                    

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTypeFromName Execption: "+e.Message);
            }
            return 0;

        }

        public static float GetAlbumPrice(int te)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT byalbum FROM audiodetails WHERE type = {te}";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return (float)reader["byalbum"];


                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetAlbumPrice Execption");
            }
            return 0;

        }

        public static float GetGBPrice(int te)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT bysize FROM audiodetails WHERE type = {te}";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return (float)reader["bysize"];


                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetGBPrice Execption "+ e.Message);
            }
            return 0;

        }

        public static List<TypeModel> GetTypeFromFileType(string name)
        {
            List<TypeModel> ts = new List<TypeModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT * FROM itemTypes WHERE filetype = '{name}'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeModel tm = new TypeModel();
                    tm.Name = reader["name"].ToString();
                    tm.Id = int.Parse(reader["id"].ToString());
                    tm.Price = float.Parse(reader["price"].ToString());
                    tm.DataType = name;
                    tm.Icon = reader["initials"].ToString();
                    ts.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetTypeFromFileType Execption {name}, {e.Message}");
            }
            return ts;

        }

        public static List<TypeModel> GetAllTypes()
        {
            List<TypeModel> ts = new List<TypeModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT * FROM itemTypes";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeModel tm = new TypeModel();
                    tm.Name = reader["name"].ToString();
                    tm.Id = int.Parse(reader["id"].ToString());
                    tm.Price = float.Parse(reader["price"].ToString());
                    tm.DataType = reader["filetype"].ToString();
                    tm.Icon = reader["initials"].ToString();
                    tm.Refrence = reader["reference"].ToString();
                    ts.Add(tm);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetTypeFromFileType Execption , {e.Message}");
            }
            return ts;

        }

        public bool AddEditType(TypeModel t)
        {
            string query = "";
            if(t.Id == 0)
            {
                query = $"INSERT INTO itemTypes (name, default_price) values ('{t.Name}', '{t.Price.ToString()}')";
            }
            else
            {
                query = $"UPDATE itemTypes SET name = '{t.Name}', default_price = '{t.Price}' WHERE id = {t.Id}";
            }
            //try
            //{
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;

            //}
            //catch(Exception e)
            //{
              //  Console.WriteLine("AddEditType Exception");

            //}
            return false;
        }

        public List<TypeModel> getAudioTypes()
        {
            List<TypeModel> ts = getItemTypes();
            List<TypeModel> ats = new List<TypeModel>();

            foreach(TypeModel t in ts)
            {
                Console.WriteLine("Type Model ------ "+t.Name);
                if (t.DataType.Equals("Audio"))
                {
                    ats.Add(t);
                }
            }
            return ats;
        }

        public static List<TypeModel> getItemTypes()
        {
            List<TypeModel> types = new List<TypeModel>();

            string query = "SELECT * FROM filmdatabase.itemTypes";

            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
            
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = reader["name"].ToString();
                    string DataType = reader["file_type"].ToString();
                    double price = Double.Parse(reader["default_price"].ToString());

                    TypeModel typee = new TypeModel(id, name, price, DataType);

                    types.Add(typee);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("GetItemTypes Exception");
            }
            return types;
        }

        public static List<Dir> GetDirs(int idd)
        {
            //TypeModel tm = new TypeModel();
            List<Dir> dirs = new List<Dir>();
            string query = $"SELECT dir,type FROM dirs WHERE type = {idd}";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    string dir = reader["dir"].ToString();
                    dirs.Add(new Dir(dir));
                    

                }


                
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }

            return dirs;
        }

        public static bool CheckIcon(string icon)
        {
            bool checkI = false;
            List<Dir> dirs = new List<Dir>();
            string query = $"SELECT COUNT(initials) FROM itemtypes WHERE initials = '{icon}'";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int count = int.Parse(reader["COUNT(initials)"].ToString());
                    if (count == 0)
                    {
                        checkI = true;
                    }
                }



                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Check Icon Execption");
            }

            return checkI;
        }

        public static bool CheckName(string icon)
        {
            bool checkI = false;
            List<Dir> dirs = new List<Dir>();
            string query = $"SELECT COUNT(name) FROM itemtypes WHERE name = '{icon}'";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int count = int.Parse(reader["COUNT(name)"].ToString());
                    if (count == 0)
                    {
                        checkI = true;
                    }
                }



                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Check Name Execption");
            }

            return checkI;
        }

        public static bool CheckRef(string icon)
        {
            bool checkI = false;
            List<Dir> dirs = new List<Dir>();
            string query = $"SELECT COUNT(reference) FROM itemtypes WHERE reference = '{icon}'";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int count = int.Parse(reader["COUNT(reference)"].ToString());
                    if (count == 0)
                    {
                        checkI = true;
                    }
                }



                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Check Ref Execption");
            }

            return checkI;
        }

        public static List<string> CheckDirs(List<Dir> dirr)
        {
            //TypeModel tm = new TypeModel();
            List<string> dirs = new List<string>();
            List<string> fdirs = new List<string>();
            string query = $"SELECT dir FROM dirs";
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                        dirs.Add(reader["dir"].ToString());
                }
                foreach(Dir dip in dirr)
                {
                    if (dirs.Contains(Utils.BackToFront(dip.dir)))
                    {
                        fdirs.Add(dip.dir);
                    }
                }


                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Check Dirs Execption");
            }

            return fdirs;
        }

        public static List<OtherPrice> GetOthersPrice(int type)
        {
            List<OtherPrice> prices = new List<OtherPrice>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT gb,price FROM othersdetails WHERE type = {type} ORDER BY gb";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    prices.Add(new OtherPrice(reader["gb"].ToString(), reader["price"].ToString()));

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetOthersPrices Execption " + e.Message);
            }
            return prices;
        }

        public class OtherPrice
        {
            public string OtherGB { get; set; }
            public string OtherGBPrice { get; set; }
            public OtherPrice(string gb, string price)
            {
                OtherGB = gb;
                OtherGBPrice = price;
            }
        }
        
    }
}
