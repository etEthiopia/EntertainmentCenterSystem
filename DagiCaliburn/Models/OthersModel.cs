using Caliburn.Micro;
using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class OthersModel
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Dir { get; set; }
        public TypeModel Type { get; set; }
        public float Price { get; set; }
        public string Size { get; set; }
        public long TotalSize { get; set; }
        public DateTime DateTime { get; set; }
        public string RecieverPort { get; set; }
        public string RecieverSerial { get; set; }
        public int Folder { get; set; }
        public int Special { get; set; }


        public static List<OthersModel> Others = new List<OthersModel>();
        public static List<string> OtherNames = new List<string>();

        MySqlConnection conn = DBUtils.GetDBConnection();

        //Adds the Type in itemTypes
        public static int AddOthersType(string Namme, string FileTyppe, string Referencce, string Initiaals, float Pricce, int[] gbs, float[] gbprices)
        {
            int id = 0;
            bool done = false;
            string query = "";
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd H:mm:ss");
            query = $"INSERT INTO fdb.itemtypes" +
            $" (name, price, filetype, reference, initials) values" +
            $" ('{Namme}', {Pricce}, '{FileTyppe}', '{Referencce}', '{Initiaals}')";

            try
            {

                MySqlCommand cmd = new MySqlCommand(query, conn);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                query = $"SELECT id FROM fdb.itemtypes WHERE name = '{Namme}'";
                try
                {
                    MySqlConnection conn1 = DBUtils.GetDBConnection();

                    cmd = new MySqlCommand(query, conn1);

                    conn1.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine($"Get ID from Name() - {query}");

                    while (reader.Read())
                    {
                        id = int.Parse(reader["id"].ToString());
                    }
                    conn1.Close();
                    if (id > 0)
                    {
                        for (int kount = 0; kount < 5; kount++)
                        {
                            if (gbs[kount] > 0)
                            {
                                try
                                {
                                    query = $"INSERT INTO fdb.othersdetails" +
                        $" (gb, price, type) values" +
                        $" ({gbs[kount]}, {gbprices[kount]}, {id})";

                                    MySqlConnection conn2 = DBUtils.GetDBConnection();
                                    cmd = new MySqlCommand(query, conn2);


                                    conn2.Open();
                                    cmd.ExecuteNonQuery();
                                    conn2.Close();
                                    done = true;
                                }
                                catch (Exception expc)
                                {
                                    Console.WriteLine("OtherDetial Error " + expc.Message);
                                }
                            }
                        }
                        if (done)
                        {
                            return id;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("GetType ID Execption " + e.Message);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"Add Item-type Exception {Namme} {e.Message}");
            }
            return 0;
        }

        //Adds Dirs by Using itemtype id
        public static bool AddDirs(int id, List<Dir> dirs)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();

            if (id > 0)
            {
                foreach (Dir dik in dirs)
                {
                    string query = $"INSERT INTO fdb.dirs (dir,type) values('{Utils.BackToFront(dik.dir)}',{id})";


                    try
                    {
                        MySqlConnection conn2 = DBUtils.GetDBConnection();
                        Console.WriteLine($"INSERT {dik}, {query}");
                        conn2.Open();
                        MySqlCommand cmd = new MySqlCommand(query, conn2);

                        cmd.ExecuteNonQuery();

                        conn2.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Add Dir Execption " + e.Message);
                    }
                }
            }
            return false;

        }

        //Returns OthersModel by reciveing path
        public static OthersModel FinalizeOthersSale(string item)
        {
            OthersModel other = new OthersModel();
            other.Name = Utils.GetName(item);
            other.Dir = Utils.GetDir(item);
            int type = 0;

            type = TypeModel.GetTypeIntReference(other.Name);
            if (Utils.IsFolder(item))
                {
                    other.TotalSize = Utils.CalculateFolderSize(item);
                }
                else
                {
                    other.TotalSize = Utils.GetTotalSize(item);
                }
            
            
            Console.WriteLine($"ref TYPE IS --- {type}");
            if (type == 0)
            {
                type = TypeModel.GetTypeInt(item);
                other.Type = TypeModel.GetTypeToFile(type);
                float pp = GetPrice(type, other.TotalSize);
                if (pp == 0.0f)
                {
                    other.Price = (float)other.Type.Price;
                }
                else
                {
                    other.Price = pp;
                }
                
            }
            else
            {
                other.Type = TypeModel.GetTypeToFile(type);
                float pp = GetPrice(type, other.TotalSize);
                if (pp == 0.0f)
                {
                    other.Price = (float)other.Type.Price;
                }
                else
                {
                    other.Price = pp;
                }
                
            }
            Console.WriteLine($"FINAL O TYPE IS --- {other.Type}");
            
            other.Size = Utils.calculateSize(other.TotalSize);
            return other;
        }

        //Returns OthersModel by reciveing path and TypeModel
        public static OthersModel FinalizeOthersSale(string item, TypeModel tml)
        {
            OthersModel other = new OthersModel();
            other.Name = Utils.GetName(item);
            other.Dir = Utils.GetDir(item);
            
            if (Utils.IsFolder(item))
            {
                other.TotalSize = Utils.CalculateFolderSize(item);
            }
            else
            {
                other.TotalSize = Utils.GetTotalSize(item);
            }


            
            other.Type = tml;
                float pp = GetPrice(tml.Id, other.TotalSize);
                if (pp == 0.0f)
                {
                    other.Price = (float)other.Type.Price;
                }
                else
                {
                    other.Price = pp;
                }

            
            
            Console.WriteLine($"FINAL O TYPE IS --- {other.Type}");

            other.Size = Utils.calculateSize(other.TotalSize);
            return other;
        }

        //Returns the Price considering size
        public static float GetPrice(int te , long size)
        {
            float price = 0.0f;
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT gb,price,type FROM othersdetails WHERE type = {te}";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //
                    long compareIt = long.Parse(reader["gb"].ToString()) * (long)1073741824;
                    if(size >= compareIt)
                    {
                        price = (float)reader["price"];
                        conn.Close();
                        return price;
                    }

                    


                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetOthersPrice Execption "+ e.Message);
            }
            return price;

        }

        //Returns bool and out OtherModel
        public static bool OtherByReference(string item, TypeModel Typp, out OthersModel other)
        {
            other = new OthersModel();
            
            if (Typp.Id != 0)
            {
                other.Name = Utils.GetName(item);
                other.Dir = "UNKNOWN";
                other.Type = Typp;
                if (Utils.IsFolder(item))
                {
                    other.TotalSize = Utils.CalculateFolderSize(item);
                }
                else
                {
                    other.TotalSize = Utils.GetTotalSize(item);
                }
                other.Size = Utils.calculateSize(other.TotalSize);
                float pp = GetPrice(Typp.Id, other.TotalSize);
                if (pp == 0.0f)
                {
                    other.Price = (float)other.Type.Price;
                }
                else
                {
                    other.Price = pp;
                }
                return true;
            }
            return false;
        }

        //Add OtherModel as a Sale
        public bool AddOtherAsSale()
        {
            string query = "";

            DateTime theDate = DateTime.Now;
            this.DateTime = theDate;
            String today = theDate.ToString("yyyy-MM-dd H:mm:ss");
            string checkdate = theDate.AddSeconds(-20).ToString("yyyy-MM-dd H:mm:ss");
            query = $"INSERT INTO othersells" +
            $" (name, dir, type, price, size, datetime, reciever, recieverserial) values" +
            $" ('{Name}', '{Dir}' ,'{Type.Id}', '{Price}', '{Size}', '{today}', '{RecieverPort}',  '{RecieverSerial}' )";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                string qy = $"SELECT id FROM othersells WHERE datetime > '{checkdate}' and name = '{Name}' and reciever = '{RecieverPort}' order by id DESC";
                int i = 0;

                MySqlCommand cmmd = new MySqlCommand(qy, conn);


                conn.Open();
                MySqlDataReader reader = cmmd.ExecuteReader();


                Console.WriteLine($"Checking-- {Name}; {qy}; {query}");

                while (reader.Read())
                {
                    Console.WriteLine($"Reader for {Name}; {reader[0]}");
                    i++;
                }
                reader.Close();
                conn.Close();



                Console.WriteLine($"SALE I {Name}" + i);
                if (i == 0)
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    StartViewModel.mainview.Drives = new BindableCollection<Disk>(Disk.GetDrives());
                    StartViewModel.mainview.Sells.Insert(0, Utils.otherTOSellModel(this));
                    MainViewModel.TE += Price;
                    StartViewModel.mainview.SetTodaysEarnings();
                    GoalModel.FixProgress();
                    StartViewModel.mainview.ProgressToGo = GoalModel.messagePerc;
                    StartViewModel.mainview.ProgressValue = GoalModel.percc + "%";
                    StartViewModel.mainview.TodaysProgress = GoalModel.percc;
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Add Other Sell Exception {Name} {e.Message}");
            }
            return false;
        }

    }
}
