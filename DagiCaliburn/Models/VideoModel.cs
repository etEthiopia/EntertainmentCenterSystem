using Caliburn.Micro;
using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.Models
{
    class VideoModel
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
        public bool Added;
        public int Folder { get; set; }
        public int Special { get; set; }
        public int VideoFolderSerial { set; get; }


        public static List<VideoModel> videos = new List<VideoModel>();
        public static Dictionary<int, List<VideoModel>> FolderWithVideos = new Dictionary<int, List<VideoModel>>();
        public static List<string> videoNames = new List<string>();  
        MySqlConnection conn = DBUtils.GetDBConnection();

        //Returns List of Presentable SellModels of simplified video sells
        public static SellModel getSimplifiedSells(int folder, string recieverserial)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT * FROM fdb.videosells WHERE videofolderserial like '%{folder}' and recieverserial = '{recieverserial}' order by id DESC";
            SellModel tm = new SellModel();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"getSimplesSales() - {query}");
                int F = 0;
                while (reader.Read())
                {
                    //Console.WriteLine($"SIMPLE "+ reader["id"].ToString() + reader["name"].ToString() + ";  " + reader["videofolderserial"].ToString());
                    if (F == 0)
                    {
                        tm.Name = Utils.GetFolderName(reader["dir"].ToString());
                        string d = reader["datetime"].ToString();
                        tm.DateTime = DateTime.Parse(d);
                        tm.Dir = Utils.GetFolderDir(reader["dir"].ToString());
                        tm.RecieverSerial = reader["recieverserial"].ToString();
                        tm.RecieverPort = reader["reciever"].ToString();
                        tm.Folder = int.Parse(reader["videofolderserial"].ToString());
                        tm.Initials = "F";
                        tm.Price = float.Parse(reader["price"].ToString());
                        tm.Size = reader["size"].ToString();
                        tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                        F++;
                    }
                    else
                    {
                        tm.Price += float.Parse(reader["price"].ToString());
                        tm.Size = Utils.FolderStringSize(tm.Size, reader["size"].ToString());
                    }
                    
                    
                   
                }
                Console.WriteLine($"SIMPLIFIED FOLDER, ID: {tm.ID},  NAME: {tm.Name},  DATETIME: {tm.DateTime}, " +
                    $"DIR: {tm.Dir},  RS: {tm.RecieverSerial},  RP:  {tm.RecieverPort},  ICON: {tm.Initials},  TYPE: {tm.Type.Name}");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Simpliefied Exception {e.Message}");
            }
            return tm;

        }

        //Returns List of All Videos Sold
        public static List<SellModel> getAllSells()
        {
            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            string query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%' order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                while (reader.Read())
                {
                    SellModel tm = new SellModel();
                    tm.Name = reader["name"].ToString();
                    tm.Dir = reader["dir"].ToString();
                    tm.Price = float.Parse(reader["price"].ToString());
                    tm.Size = reader["size"].ToString();
                    tm.RecieverSerial = reader["recieverserial"].ToString();
                    tm.RecieverPort = reader["reciever"].ToString();
                    string d = reader["datetime"].ToString();
                    tm.DateTime = DateTime.Parse(d);
                    if (int.Parse(reader["type"].ToString()) == 0)
                    {
                        tm.Initials = "F";
                        tm.Folder = int.Parse(reader["folderserial"].ToString());
                    }
                    else
                    {
                        tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                        tm.Initials = tm.Type.Icon;
                    }
                    todaysSales.Add(tm);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            return todaysSales;

        }

        //Adds the Type in itemTypes
        public static bool AddVideoType(bool edit, int id, string Namme, float Pricce, string FileTyppe, string Referencce, string Initiaals)
        {
            
            string query = "";
            MySqlConnection conn = DBUtils.GetDBConnection();

            query = $"INSERT INTO fdb.itemtypes" +
            $" (name, price, filetype, reference, initials) values" +
            $" ('{Namme}', {Pricce}, '{FileTyppe}', '{Referencce}', '{Initiaals}')";
            if (edit)
            {
                query = $"UPDATE fdb.itemtypes SET name='{Namme}', price = '{Pricce}', filetype = '{FileTyppe}', " +
                    $"reference = '{Referencce}', initials = '{Initiaals}' where id = {id}";
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
                Console.WriteLine($"Add Item-type Exception {Namme} {e.Message}");
            }
            return false;
        }

        //Adds Dirs by Using itemtype name
        

        public static List<SellModel> getEpisodes(SellModel folder)
        {
            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String dt = folder.DateTime.ToString("yyyy-MM-dd");
            string ff = folder.Folder.ToString();
            if (ff[0].Equals('1'))
            {
                ff = ff.Substring(1);
            }
            string query = $"SELECT * FROM fdb.videosells WHERE videofolderserial like '%{ff}' and datetime like '{dt}%' order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"getEpisodes() - {query}");

                while (reader.Read())
                {
                    SellModel tm = new SellModel();
                    tm.Name = reader["name"].ToString();
                    tm.Dir = reader["dir"].ToString();
                    tm.Price = float.Parse(reader["price"].ToString());
                    tm.Size = reader["size"].ToString();
                    tm.RecieverSerial = reader["recieverserial"].ToString();
                    tm.RecieverPort = reader["reciever"].ToString();
                    string d = reader["datetime"].ToString();
                    tm.DateTime = DateTime.Parse(d);
                    Console.WriteLine("1");
                    if (int.Parse(reader["type"].ToString()) == 0)
                    {
                        tm.Initials = "F";
                        tm.Folder = int.Parse(reader["folderserial"].ToString());
                        Console.WriteLine("2/");
                    }
                    else
                    {
                        tm.Folder = int.Parse(reader["video" +
                            "folderserial"].ToString());
                        tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                        tm.Initials = tm.Type.Icon;
                        Console.WriteLine("2//");
                    }
                    todaysSales.Add(tm);
                }
                Console.WriteLine("3");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            return todaysSales;

        }

        public static double CalculateFolder(String item, int folderNum)
        {
            SellModel.folders.Add(item + "/");
            double price = (double)0.0;

            try
            {
                if (!Directory.Exists(item))
                {
                    return price;
                }
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(item))
                        {
                            if (File.Exists(file))
                            {
                                if (Utils.IsVideo(file))
                                {
                                    VideoModel vm = FinalizeVideoSale(file);
                                    Console.WriteLine($"FinalizeVideoSale for {file}");
                                    vm.Folder = folderNum;
                                    Console.WriteLine($"FolderVideo {vm.Name}, {vm.Dir}, {vm.Folder}");

                                    FolderWithVideos[folderNum].Add(vm);

                                    //videos.Add(vm);
                                    videoNames.Insert(0,vm.Name);
                                    price += vm.Price;
                                }
                               
                            }
                        }
                        foreach (string dir in Directory.GetDirectories(item))
                        {

                            price += CalculateFolder(dir + "/", folderNum);
                        }

                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine("Unable to calculate folder size : {0}", e.Message);
                    }

                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Unable to calculate folder size : {0}", e.Message);
            }




            //Console.WriteLine("Folder bools");
            //foreach(Boolean fbool in foldersbool)
            //{
            //    Console.WriteLine($"     {fbool}");
            //}



            return price;

        }

        public static VideoModel FinalizeVideoSale(string item)
        {
            VideoModel video = new VideoModel();
            video.Name = Utils.GetName(item);
            video.Dir = Utils.GetDir(item);
            int type = 0;

            type = TypeModel.GetTypeIntReference(video.Name);
            Console.WriteLine($"ref TYPE IS --- {type}");
            if (type == 0)
            {
                type = TypeModel.GetTypeInt(item);
                video.Type = TypeModel.GetTypeToFile(type);
                video.Price = (float)video.Type.Price;
            }
            else 
            {
                video.Type = TypeModel.GetTypeToFile(type);
                video.Price = (float)video.Type.Price;
            }
            Console.WriteLine($"FINAL TYPE IS --- {video.Type}");
            video.TotalSize = Utils.GetTotalSize(item);
            video.Size = Utils.calculateSize(video.TotalSize);
            return video;
        }

        public static bool VideoByReference(string item, out VideoModel video)
        {
            video = new VideoModel();
            int type = 0;
            
            type = TypeModel.GetTypeIntReference(Utils.GetName(item));
            if (type != 0)
            {
                video.Name = Utils.GetName(item);
                video.Dir = "UNKNOWN";
                video.Type = TypeModel.GetTypeToFile(type);
                video.Price = (float)video.Type.Price;
                video.TotalSize = Utils.GetTotalSize(item);
                video.Size = Utils.calculateSize(video.TotalSize);
                return true;
            }
            return false;
        }

        //Add Video to Sell and Update every Progress on the Way
        public bool AddAsASale()
        {
            string query = "";
            

                DateTime theDate = DateTime.Now;
            this.DateTime = theDate;
            String today = theDate.ToString("yyyy-MM-dd HH:mm:ss");
                string checkdate = theDate.AddSeconds(-20).ToString("yyyy-MM-dd H:mm:ss");
            try
                {
                    string qy = $"SELECT id FROM fdb.videosells WHERE datetime >= '{checkdate}' and name = '{Name}' and reciever = '{RecieverPort}' order by id DESC";
                    int i = 0;
                
                    MySqlCommand cmmd = new MySqlCommand(qy, conn);


                    conn.Open();
                    MySqlDataReader reader = cmmd.ExecuteReader();
                    

                    //Console.WriteLine($"Checking-- {Name} {qy}");
                    
                    while (reader.Read())
                    {
                        //Console.WriteLine($"Reader for {Name}; {reader[0]}");
                        i++;
                    }
                    reader.Close();
                    conn.Close();

                if (Folder > 0)
                {
                    string qy2 = $"SELECT COUNT(id) FROM fdb.videosells WHERE videofolderserial like '%{Folder}' and reciever = '{RecieverPort}' order by id DESC";

                    MySqlCommand cmmmd = new MySqlCommand(qy2, conn);


                    conn.Open();
                    reader = cmmmd.ExecuteReader();


                    //Console.WriteLine($"Checking FOLDER-- {Name} {qy}");

                    while (reader.Read())
                    {
                        //Console.WriteLine($"CHECKFOLDER COUNT " +reader["COUNT(id)"].ToString());
                        if(int.Parse(reader["COUNT(id)"].ToString()) == 0)
                        {
                            Folder = Utils.AddOne(Folder);
                            
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                else
                {
                    Folder = 1;
                }
                //Console.WriteLine($"SALE I {Name}" + i);
                if (i== 0) {
                    query = $"INSERT INTO videosells" +
                $" (name, dir, type, price, size, datetime, reciever, recieverserial, videofolderserial) values" +
                $" ('{Name}', '{Dir}' ,'{Type.Id}', '{Price}', '{Size}', '{today}', '{RecieverPort}',  '{RecieverSerial}', " +
                $" '{Folder}')";

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.ExecuteNonQuery();
                    DateTime dd = new DateTime();
                    DateTime.TryParse(today, out dd);
                    //MessageBox.Show($"DATE :  {dd}");

                    this.DateTime = dd;
                    conn.Close();
                    if (Folder == 1)
                    {
                        StartViewModel.mainview.Sells.Insert(0, Utils.videoTOSellModel(this));
                        StartViewModel.mainview.Drives = new BindableCollection<Disk>(Disk.GetDrives());
                        bool update = true;
                        if (MainViewModel.TE > GoalModel.Goal)
                        {
                            update = false;
                        }
                        MainViewModel.TE += Price;
                        StartViewModel.mainview.SetTodaysEarnings();
                        if (update)
                        {
                            GoalModel.FixProgress();
                            StartViewModel.mainview.ProgressToGo = GoalModel.messagePerc;
                            StartViewModel.mainview.ProgressValue = GoalModel.percc + "%";
                            StartViewModel.mainview.TodaysProgress = GoalModel.percc;
                        }
                    }
                    else if (Folder.ToString()[0].Equals('1'))
                    {
                        StartViewModel.mainview.Sells.Insert(0, Utils.videoTOFolderModel(this));
                        StartViewModel.mainview.Drives = new BindableCollection<Disk>(Disk.GetDrives());
                        bool update = true;
                        if (MainViewModel.TE > GoalModel.Goal)
                        {
                            update = false;
                        }
                        MainViewModel.TE += Price;
                        StartViewModel.mainview.SetTodaysEarnings();
                        if (update)
                        {
                            GoalModel.FixProgress();
                            StartViewModel.mainview.ProgressToGo = GoalModel.messagePerc;
                            StartViewModel.mainview.ProgressValue = GoalModel.percc + "%";
                            StartViewModel.mainview.TodaysProgress = GoalModel.percc;
                        }
                    }
                    else if(Folder > 1)
                    {
                        int ff = Utils.AddOne(Folder);
                        //Console.WriteLine($"name:{Name}  ff: {ff}");
                        for(int cc = 0; cc < 10; cc++)
                        {
                            //Console.WriteLine($"StartViewModel.mainview.Sells[cc].Name: {StartViewModel.mainview.Sells[cc].Name}  StartViewModel.mainview.Sells[cc].Folder: {StartViewModel.mainview.Sells[cc].Folder}");

                            if (StartViewModel.mainview.Sells[cc].Folder == ff && StartViewModel.mainview.Sells[cc].RecieverSerial.Equals(RecieverSerial))
                            {
                                SellModel sf = StartViewModel.mainview.Sells[cc];
                                sf.Price += Price;
                                sf.Size = Utils.FolderStringSize(sf.Size, Size);
                                StartViewModel.mainview.Sells.RemoveAt(cc);
                                StartViewModel.mainview.Sells.Insert(cc,sf);
                                StartViewModel.mainview.Drives = new BindableCollection<Disk>(Disk.GetDrives());
                                bool update = true;
                                if (MainViewModel.TE > GoalModel.Goal)
                                {
                                    update = false;
                                }
                                MainViewModel.TE += Price;
                                StartViewModel.mainview.SetTodaysEarnings();
                                if (update)
                                {
                                    GoalModel.FixProgress();
                                    StartViewModel.mainview.ProgressToGo = GoalModel.messagePerc;
                                    StartViewModel.mainview.ProgressValue = GoalModel.percc + "%";
                                    StartViewModel.mainview.TodaysProgress = GoalModel.percc;
                                }
                                break;
                            }
                        }
                    }
                    //Console.WriteLine($"COMPLETED ADDING {this.Name}, {this.Type.Name}"); ;
                    return true;
                    }
                
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Add Sell Exception {Name} {e.Message}");
                }
            return false;
        }

    }
}
