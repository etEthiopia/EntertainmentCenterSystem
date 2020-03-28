using Caliburn.Micro;
using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.Models
{
    class AudioModel
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
        
        public static List<String> AlbumNames = new List<string>();
        public static List<AudioModel> Albums = new List<AudioModel>();
        private static List<string> GBNames = new List<string>();

        MySqlConnection conn = DBUtils.GetDBConnection();

        //Get Selected Folders or Musics and Present them in the GB grid
        public static void ShowSelected(List<string> selected)
        {
            foreach(string sd in selected)
            {
                string sdd = Utils.BackToFront(sd);
                SellModel sml = new SellModel();
                sml.Name = Utils.GetName(sdd);
                if (!GBNames.Contains(sml.Name))
                {
                    sml.Dir = Utils.GetDir(sdd);
                    if (Utils.IsFolder(sdd))
                    {
                        sml.TotalSize = Utils.CalculateFolderSize(sdd);
                    }
                    else
                    {
                        sml.TotalSize = Utils.GetTotalSize(sdd);
                    }
                    sml.Size = Utils.calculateSize(sml.TotalSize);
                    StartViewModel.gbview.GBMusicSells.Insert(0, sml);
                    GBNames.Insert(0, sml.Name);
                }
                
            }
            StartViewModel.gbview.CalculateTotalSize();
            StartViewModel.gbview.CalculateMoney();
        }

        //Returns bool and out AudioModel
        public static bool AudioByReference(string item, TypeModel Typp, out AudioModel am)
        {
            am = new AudioModel();

            if (Typp.Id != 0)
            {
                am.Name = Utils.GetName(item);
                am.Dir = "UNKNOWN";
                am.Type = Typp;
                if (Utils.IsFolder(item))
                {
                    am.TotalSize = Utils.CalculateFolderSize(item);
                }
                else
                {
                    am.TotalSize = Utils.GetTotalSize(item);
                }
                am.Size = Utils.calculateSize(am.TotalSize);
                am.Price = TypeModel.GetAlbumPrice(Typp.Id);
                if(am.Size.Equals("0 KB"))
                {
                    am.Size = "";
                }
                return true;
            }
            return false;
        }

        //Returns AudioModel by reciveing path and TypeModel
        public static AudioModel FinalizeAudiosSale(string item, TypeModel tml)
        {
            AudioModel other = new AudioModel();
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
            other.Price = TypeModel.GetAlbumPrice(tml.Id);



            Console.WriteLine($"FINAL Audio TYPE IS --- {other.Type}");

            other.Size = Utils.calculateSize(other.TotalSize);
            return other;
        }

        //Add Audio Album as a Sale
        public bool AddAlbumAsSale()
        {
            string query = "";

            DateTime theDate = DateTime.Now;
            this.DateTime = theDate;
            String today = theDate.ToString("yyyy-MM-dd H:mm:ss");
            string checkdate = theDate.AddSeconds(-20).ToString("yyyy-MM-dd H:mm:ss");
            query = $"INSERT INTO audiosells" +
            $" (name, dir, type, price, size, datetime, reciever, recieverserial) values" +
            $" ('{Name}', '{Dir}' ,'{Type.Id}', '{Price}', '{Size}', '{today}', '{RecieverPort}',  '{RecieverSerial}' )";
            
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                string qy = $"SELECT id FROM audiosells WHERE datetime > '{checkdate}' and name = '{Name}' and reciever = '{RecieverPort}' order by id DESC";
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
                    StartViewModel.mainview.Sells.Insert(0, Utils.audioTOSellModel(this));
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
                Console.WriteLine($"Add Audio Sell Exception {Name} {e.Message}");
            }
            return false;
        }

        //Adds Dirs by Using itemtype id
        public static bool AddDirs(bool edit, int id, List<Dir> dirs)
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

        public static bool AlbumFolderByReference(string item, out AudioModel audio)
        {
            audio = new AudioModel();
            int type = 0;

            type = TypeModel.GetTypeIntReference(Utils.GetName(item));
            if (type != 0)
            {
                audio.Name = Utils.GetName(item);
                audio.Dir = "UNKNOWN";
                audio.Type = TypeModel.GetTypeToFile(type);
                audio.Price = TypeModel.GetAlbumPrice(type);
                audio.TotalSize = Utils.CalculateFolderSize(item);
                audio.Size = Utils.calculateSize(audio.TotalSize);
                return true;
            }
            return false;
        }

        //Adds the Type in itemTypes
        public static int AddAudioType(bool edit, string Namme,  string FileTyppe, string Referencce, string Initiaals, float PriceGB, float PriceALbum)
        {
            int id = 0;
            bool done = false;
            string query = "";
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd H:mm:ss");
            query = $"INSERT INTO fdb.itemtypes" +
            $" (name, price, filetype, reference, initials) values" +
            $" ('{Namme}', {0}, '{FileTyppe}', '{Referencce}', '{Initiaals}')";

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
                    try
                    {
                        query = $"INSERT INTO fdb.audiodetails" +
            $" (type, bysize, byalbum) values" +
            $" ({id}, {PriceGB}, {PriceALbum})";

                        MySqlConnection conn2 = DBUtils.GetDBConnection();
                        cmd = new MySqlCommand(query, conn2);


                        conn2.Open();
                        cmd.ExecuteNonQuery();
                        conn2.Close();
                        done = true;
                    }
                    catch(Exception expc)
                    {
                        Console.WriteLine("AudioDetial Error " + expc.Message);
                    }
                    if (done)
                    {
                        return id;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("GetType ID Execption "+e.Message);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"Add Item-type Exception {Namme} {e.Message}");
            }
            return 0;
        }



    }
}
