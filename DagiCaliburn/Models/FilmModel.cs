
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class FilmModel
    {
        private string _directory;
        private string _name;
        private double _price;
        private int _type;
        private int _id;


        MySqlConnection conn = DBUtils.GetDBConnection();


        public String Directory
        {
            get { return _directory; }

            set { _directory = value; }
        }

        public String Name
        {
            get { return _name; }

            set { _name = value; }
        }

        public double Price
        {
            get { return _price; }

            set { _price = value; }
        }

        public int Type
        {
            get { return _type; }

            set { _type = value; }
        }

        public int Id
        {
            get { return _id; }

            set { _id = value; }
        }

        public FilmModel()
        {

        }


        public FilmModel(String dir)
        {
           String[] fullShit  = dir.Split('\\');

            this.Name = fullShit[fullShit.Length - 1];
            this.Directory = "";
            for(int i=0; i < fullShit.Length - 1; i++)
            {
                this.Directory += fullShit[i]+"\\";
                
            }
        }

        public int getType()
        {
            //TypeModel tm = new TypeModel();
            List<string> similars = new List<string>();
            List<double> compared = new List<double>();
            List<int> types = new List<int>();
            string query = $"SELECT dir,type FROM directories";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    string dir = reader["dir"].ToString();
                    //Console.WriteLine($"getType() {dir} , {Directory}");

                    if (this.Directory.Contains(dir))
                    {
                        compared.Add(StringSimilarity.CalculateSimilarity(this.Directory.ToUpper(), dir.ToUpper()));
                        similars.Add(dir);
                        int tp = 0;
                        if (int.TryParse(reader["type"].ToString(), out tp));
                        types.Add(tp);
                        //this.Type = (int)reader["type"];
                        //return (int)reader["type"];
                    }

                }
                //for(int i = 0; i < similars.Count; i++)
                //{
                    //Console.WriteLine($"Directory: {similars[i]},  Compared: {compared[i]}");
                //}

                if(compared.Count > 0)
                {
                    int indexMax = compared.IndexOf(compared.Max());
                    conn.Close();
                    //Console.WriteLine($"Found!, MAX : {compared[indexMax]},  TYPE : {types[indexMax]}");
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

        public double getMusicAlbumPrice(int d)
        {
            //TypeModel tm = new TypeModel();
            double tp = 0.0;
            string query = $"SELECT byAlbum FROM audio WHERE type = {d}";
            try
            {
                Console.WriteLine($"getMusicAlbumPrice Q  {query}");
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    tp = double.Parse(reader["byAlbum"].ToString());
                    Console.WriteLine($"By Album price {tp}");
            
                }
                

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }

            return tp;
        }

    }
}
