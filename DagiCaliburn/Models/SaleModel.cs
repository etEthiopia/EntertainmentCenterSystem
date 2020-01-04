using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class SaleModel
    {
        public FilmModel Film { get; set; }
        public TypeModel Type { get; set; }
        public String Name { get; set; }
        public String Dir { get; set; }
        public double TotalPrice { get; set; }
        public long TotalSize { get; set; }
        public string size { get; set; }
        public String Reciever { get; set; }
        public DateTime DateTime { get; set; }
        public double SpecialPrice { get; set; }
        public bool SpecialSale { get; set; }
        public int Status { get; set; }
        public double Percentage { get; set; }
        public string initial { get; set; }

        public static List<string> clipBoard = new List<string>();
        public static String[] videoTypes = { "WEBM", "MP4", "FLV", "MKV", "MOV", "MPG", "MP2", "MPEG", "MPE", "MPV", "OGG", "M4P", "M4V", "AVI", "WMV", "QT", "SWF", "AVCHD" };
        public static String[] audioTypes = { "PCM", "WAV", "AIFF", "MP3", "AAC", "OGG", "WMA", "FLAC", "ALAC"};
        private static List<String> folders = new List<string>();
        private static List<bool> foldersbool = new List<bool>();
        public static List<SaleModel> videos = new List<SaleModel>();
        public static List<SaleModel> roots = new List<SaleModel>();
        public static List<SaleModel> albums = new List<SaleModel>();
        public static Dictionary<int, string> albumKey = new Dictionary<int, string>();

        public static List<string> folderOfSale = new List<string>();
        public static List<SaleModel> videosInFolders = new List<SaleModel>();

        MySqlConnection conn = DBUtils.GetDBConnection();


        private bool IsFolder(String dir)
        {
            if (File.GetAttributes(dir).ToString().Contains("Directory"))
            {
               return true;
            }
            else
            {
                return false;
            }
        }

        public SaleModel(string dir)
        {
            String[] fullname = dir.Split('\\');
            string dr = "";
            for(int i =0; i <fullname.Length -2; i++)
            {
                dr += fullname[i] + "\\";
            }

            Name = fullname.Last();
            Dir = dr;
            if (IsFolder(dir))
            {
                TotalSize = CalculateFolderSize(dir);
                size = calculateSize(TotalSize);
            }
            else
            {
                TotalSize = new FileInfo(dir).Length;
                size = calculateSize(TotalSize);
            }
             

        }

        public SaleModel()
        {

        }



        private static bool IsVideo(String dir)
        {
            String[] fullname = dir.Split('.');
            String extension = fullname[fullname.Length - 1].ToUpper();
            if (videoTypes.Contains(extension))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool IsAudio(string dir)
        {
            String[] fullname = dir.Split('.');
            String extension = fullname[fullname.Length - 1].ToUpper();
            if (audioTypes.Contains(extension))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void SubDir()
        {
            try
            {
                int counter = 0;
                while (foldersbool.Contains(false))
                {

                    if (foldersbool[counter] == false)
                    {

                        string folder = folders[counter];
                        List<String> filePaths = Directory.GetFiles(@folder).ToList(); //+ 
                                                                                       //.Concat(Directory.GetDirectories(@folder).ToList());
                        foreach (var dirs in Directory.GetDirectories(@folder))
                        {
                            filePaths.Add(dirs);
                        }


                        foreach (string file in filePaths)
                        {
                            //Console.WriteLine("FilePath " + file);
                            try
                            {
                                if (IsFolder(file))
                                {
                                    if (!folders.Contains(file + "\\"))
                                    {
                                        folders.Add(file);
                                        foldersbool.Add(false);
                                    }
                                }
                                else
                                {
                                    if (IsVideo(file))
                                    {
                                        folderOfSale.Add(folders[counter]+"\\");
                                        videosInFolders.Add(FinalizeVideoSale(file));
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Item hidden");
                            }


                        }
                        //folders.Remove(folder);
                        foldersbool.Insert(folders.IndexOf(folder), true);
                        counter++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Shit Happend in folders array");
            }
            foreach (String folder in folders)
            {
                Console.WriteLine("Folder : " + folder + "Boolean : " + foldersbool[folders.IndexOf(folder)]);
            }
        }

        public void prepareSale(string item)
        {
            if (IsFolder(item))
            {
                if (!folders.Contains(item + "\\"))
                {
                    roots.Add(FinalizeFolderSale(item));
                    Console.WriteLine($"ROOT NAME: {roots.Last().Name}");
                    if (roots.Last().Type!= null)
                    {
                        if (roots.Last().Type.DataType.Equals("Audio"))
                        {
                            roots.Last().TotalPrice = roots.Last().Film.getMusicAlbumPrice(roots.Last().Type.Id);
                            Console.WriteLine($"Prepare Root {roots.Last().Name} Pirce :  {roots.Last().TotalPrice}  Type : {roots.Last().Type.Id}");

                            albums.Add(roots.Last());
                        }

                    }
                    foldersbool.Add(true);

                    //Console.WriteLine($" ROOT FOLDER {roots.Last().Dir+ roots.Last().Name}");
                    TotalPrice = CalculateFolder(item);
                    //Console.WriteLine($" TOTAL PRICE {TotalPrice}");
                }

            }
            else
            {
                if (IsVideo(item))
                {
                    
                    videos.Add(FinalizeVideoSale(item));
                }
            }
        }

        private SaleModel FinalizeFolderSale(string item)
        {
            SaleModel sm = new SaleModel();
            String[] fullShit = item.Split('\\');

            sm.Name = fullShit[fullShit.Length - 1];
            sm.Dir = "";
            for (int i = 0; i < fullShit.Length - 1; i++)
            {
                sm.Dir += fullShit[i] + "\\";

            }
            sm.TotalSize = CalculateFolderSize(item);
            if (sm.TotalSize.ToString().Length > 9)
            {
                sm.size = (TotalSize / (1024 * 1024 * 1024)).ToString();
                sm.size += " GB";
            }
            else
            {
                sm.size = (TotalSize / (1024 * 1024)).ToString();
                sm.size += " MB";
            }
            sm.Film = new FilmModel(item);
            int type = sm.Film.getType();
            if (type != 0)
            {
                sm.Type = TypeModel.GetTypeToFile(type);
                sm.initial = sm.Type.Icon;
                sm.TotalPrice = sm.Type.Price;
            }

            return sm;
        }

        public double CalculateFolder(String item)
        {
            folders.Add(item + "\\");
            foldersbool.Add(false);
            double price = (double)0.0;

            
            //Console.WriteLine($"FOLDER {item}");
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



                                if (IsVideo(file))
                                {
                                    
                                    folderOfSale.Add(item);

                                    SaleModel sm = new SaleModel();
                                    sm.Film = new FilmModel(file);
                                    int type = sm.Film.getType();
                                    if (type != 0)
                                    {
                                        sm.Type = TypeModel.GetTypeToFile(type);
                                        sm.initial = sm.Type.Icon;
                                        sm.TotalPrice =sm.Type.Price;
                                    }
                                    sm.TotalSize = new FileInfo(file).Length;
                                    if (sm.TotalSize.ToString().Length > 9)
                                    {
                                        sm.size = (TotalSize / (1024 * 1024 * 1024)).ToString();
                                        sm.size += " GB";
                                    }
                                    else
                                    {
                                        sm.size = (TotalSize / (1024 * 1024)).ToString();
                                        sm.size += " MB";
                                    }
                                    sm.Name = sm.Film.Name;
                                    sm.Dir = sm.Film.Directory;

                                    //Console.WriteLine($"VIDEO {sm.Name}");
                                    videosInFolders.Add(sm);
                                    videos.Add(sm);
                                    price += sm.Type.Price;
                                }
                                //FileInfo finfo = new FileInfo(file);
                                
                            }
                        }
                        foreach (string dir in Directory.GetDirectories(item))
                        {

                            price += CalculateFolder(dir + "\\");
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

        public static long CalculateFolderSize(String item)
        {
            long size = (long) 0.0;
            try
            {
                if (!Directory.Exists(item))
                {
                    return size;
                }
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(item + "\\"))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                size += finfo.Length;
                            }
                        }
                        foreach (string dir in Directory.GetDirectories(item))
                        {
                            size += CalculateFolderSize(dir + "\\");
                        }

                    }
                    catch(NotSupportedException e)
                    {
                        Console.WriteLine("Unable to calculate folder size : {0}", e.Message);
                    }

                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Unable to calculate folder size : {0}", e.Message);
            }

            return size;

        }

        private SaleModel FinalizeAlbumSale(string item)
        {
            return new SaleModel(); 
        }

        private SaleModel FinalizeVideoSale(string item)
        {
            SaleModel sm = new SaleModel();
            sm.Film = new FilmModel(item);
            int type = sm.Film.getType();
            if (type != 0)
            {
                sm.Type = TypeModel.GetTypeToFile(type);
                sm.TotalPrice = sm.Type.Price;
            }
            sm.TotalSize = new FileInfo(item).Length;
            sm.Name = sm.Film.Name;
            sm.Dir = sm.Film.Directory;
            return sm;
        }

        public  bool AddAsASale() {
            string query = "";
            string size = "";
            Console.WriteLine($"AddAsASale {Name}  : {Reciever} : {Status}");
            if (Status == 1)
            {
                if (TotalSize.ToString().Length > 9)
                {
                    size = (TotalSize / (1024 * 1024 * 1024)).ToString();
                    size += " GB";
                }
                else
                {
                    size = (TotalSize / (1024 * 1024)).ToString();
                    size += " MB";
                }

                DateTime theDate = DateTime.Now;
                String today = theDate.ToString("yyyy-MM-dd H:mm:ss");
                query = $"INSERT INTO sells" +
                $" (name, directory, price, size, type, date, reciever_name, status) values" +
                $" ('{Name}', '{Dir}' , '{TotalPrice}', '{size}', '{Type.Id}', '{today}', " +
                $" '{Reciever + '/'}', '{Status}')";

                //INSERT INTO `filmdatabase`.`sells` (`name`, `directory`, `price`, `size`, `type`, `date`, `reciever_name`) VALUES('Hey', 'He\\dfa\\', '1', '1', '1', '9/3/2019 11:04:40 AM', 'E\\');

                try
                {
                    Console.WriteLine($"Add Sale {query}");
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                   
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Add Sell Exception");

                }
            }
            return false;
        }

        void delete()
        {

        }

        void edit()
        {

        }

        public static List<SaleModel> getTodaysSales()
        {
            
            List<SaleModel> todaysSales = new List<SaleModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            string query = $"SELECT * FROM filmdatabase.sells WHERE date like '{today}%' order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                while (reader.Read())
                {
                    SaleModel tm = new SaleModel();
                    tm.Name = reader["name"].ToString();
                    
                    tm.Dir = reader["directory"].ToString();
                    tm.TotalPrice = Double.Parse(reader["price"].ToString());
                    tm.size = reader["size"].ToString();
                    //Console.WriteLine($"Sell  Name: {tm.Name}");
                    
                    //Console.WriteLine($"Sell  Name: {tm.Name}, Type Id: {int.Parse(reader["type"].ToString())}");
                    tm.Reciever = reader["reciever_name"].ToString();
                    string d = reader["date"].ToString();
                    tm.DateTime = DateTime.Parse(d);
                    tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                    tm.initial = tm.Type.Icon;
                    tm.Status = int.Parse(reader["status"].ToString());
                    int idd = int.Parse(reader["id"].ToString());
                    
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

        public static List<SaleModel> getTodaysPresentableSales()
        {

            List<SaleModel> todaysSales = new List<SaleModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            string query = $"SELECT * FROM filmdatabase.sells WHERE date like '{today}%' order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                SaleModel folder = null;
                bool onFolder = false;
                while (reader.Read())
                {
                    SaleModel tm = new SaleModel();
                    tm.Name = reader["name"].ToString();

                    tm.Dir = reader["directory"].ToString();
                    tm.TotalPrice = Double.Parse(reader["price"].ToString());
                    tm.size = reader["size"].ToString();
                    //Console.WriteLine($"Sell  Name: {tm.Name}");

                    //Console.WriteLine($"Sell  Name: {tm.Name}, Type Id: {int.Parse(reader["type"].ToString())}");
                    tm.Reciever = reader["reciever_name"].ToString();
                    string d = reader["date"].ToString();
                    tm.DateTime = DateTime.Parse(d);
                    tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                    tm.initial = tm.Type.Icon;
                    tm.Status = int.Parse(reader["status"].ToString());
                    int idd = int.Parse(reader["id"].ToString());
                    Console.WriteLine($"Item {tm.Name}, Reciever {tm.Reciever}, Length {tm.Reciever.Split('/').Length}");
                    if (tm.Reciever.Split('/').Length > 2)
                    {
                        onFolder = true;
                        if (folder == null)
                        {

                            folder = new SaleModel();
                            folder.Name = tm.Reciever.Split('/')[1];
                            folder.size = tm.size;
                            folder.TotalPrice = tm.TotalPrice;
                            string[] modDir = tm.Dir.Split('\\');
                            string dirr = "";
                            for (int i = 0; i < modDir.Length; i++)
                            {
                                if (!modDir[i].Equals(folder.Name))
                                {
                                    dirr += (modDir[i] + "\\");
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            folder.Reciever = tm.Reciever.Split('/')[0] + "/";

                            folder.Dir = dirr;
                            Console.WriteLine($"About to Add Folder: {folder.Name} , {folder.size}");
                            folder.Status = tm.Status;
                            folder.DateTime = DateTime.Parse(reader["date"].ToString());

                            folder.initial = "F";
                            todaysSales.Add(folder);
                            Console.WriteLine($"Added Folder: {folder.Name}");
                        }
                        else
                        {

                            if (folder.Name.Equals(tm.Reciever.Split('/')[1])) { 
                                int reals = folder.size.Length - 3;
                                double tsize = int.Parse(folder.size.Substring(0, reals));
                                Console.WriteLine($"Folder Size was {folder.size} : {tsize}");
                                int treals = tm.size.Length - 3;
                                double ttsize = int.Parse(tm.size.Substring(0, treals));
                                Console.WriteLine($"New Item Size is : {tsize}");

                                double Total = ttsize + tsize;
                                if (Total.ToString().Length > 3)
                                {
                                    folder.size = Total/1024 +" GB";
                                }
                                else
                                {
                                    folder.size = Total +" MB";
                                }
                                Console.WriteLine($"Folder SIZE IS {folder.size} : {Total} : Length {Total.ToString().Length}");

                                folder.TotalPrice += tm.TotalPrice;
                                Console.WriteLine($"Update Folder: {folder.Name}");
                                Console.WriteLine("Last Item: " + todaysSales.Last().Name);
                            }
                            else{
                                folder = new SaleModel();
                                folder.Name = tm.Reciever.Split('/')[1];

                                folder.size = tm.size;
                                folder.TotalPrice = tm.TotalPrice;
                                folder.Status = tm.Status;
                                folder.initial = "F";
                                folder.DateTime = DateTime.Parse(reader["date"].ToString());
                                string[] modDir = tm.Dir.Split('\\');
                                string dirr = "";
                                for (int i = 0; i < modDir.Length; i++)
                                {
                                    if (!modDir[i].Equals(folder.Name))
                                    {
                                        dirr += (modDir[i] + "\\");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                folder.Reciever = tm.Reciever.Split('/')[0] + "/";

                                folder.Dir = dirr;
                                Console.WriteLine($"About to Add Folder: {folder.Name}");
                                todaysSales.Add(folder);
                                Console.WriteLine($"Added Folder: {folder.Name}");
                            }
                        }
                    }
                    else
                    {
                        if (onFolder)
                        {
                            SaleModel directory = folder;
                            todaysSales.RemoveAt(todaysSales.Count - 1);
                            Console.WriteLine($"About to Add Directory: {directory.Name}");
                            onFolder = false;
                            todaysSales.Add(directory);
                            Console.WriteLine($"About to Add Video: {tm.Name}");
                            todaysSales.Add(tm);
                            folder = null;
                            
                        }
                        else
                        {
                            onFolder = false;
                            Console.WriteLine($"About to Add Video: {tm.Name}");
                            todaysSales.Add(tm);
                            
                        }
                        
                    }
                }
                conn.Close();
                //foreach (SaleModel sm in todaysSales)
                //{
                //    Console.WriteLine($"SaleModel {sm.Name}");
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            foreach (SaleModel sm in todaysSales)
            {
                Console.WriteLine($"Returning SaleModel {sm.Name}");
            }
            return todaysSales;

        }

        public static List<SaleModel> getForeverPresentableSales()
        {

            List<SaleModel> todaysSales = new List<SaleModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            
            string query = $"SELECT * FROM filmdatabase.sells order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                SaleModel folder = null;
                bool onFolder = false;
                while (reader.Read())
                {
                    SaleModel tm = new SaleModel();
                    tm.Name = reader["name"].ToString();

                    tm.Dir = reader["directory"].ToString();
                    tm.TotalPrice = Double.Parse(reader["price"].ToString());
                    tm.size = reader["size"].ToString();
                    //Console.WriteLine($"Sell  Name: {tm.Name}");

                    //Console.WriteLine($"Sell  Name: {tm.Name}, Type Id: {int.Parse(reader["type"].ToString())}");
                    tm.Reciever = reader["reciever_name"].ToString();
                    string d = reader["date"].ToString();
                    tm.DateTime = DateTime.Parse(d);
                    tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                    tm.initial = tm.Type.Icon;
                    tm.Status = int.Parse(reader["status"].ToString());
                    int idd = int.Parse(reader["id"].ToString());
                    Console.WriteLine($"Item {tm.Name}, Reciever {tm.Reciever}, Length {tm.Reciever.Split('/').Length}");
                    if (tm.Reciever.Split('/').Length > 2)
                    {
                        onFolder = true;
                        if (folder == null)
                        {

                            folder = new SaleModel();
                            folder.Name = tm.Reciever.Split('/')[1];
                            folder.size = tm.size;
                            folder.TotalPrice = tm.TotalPrice;
                            string[] modDir = tm.Dir.Split('\\');
                            string dirr = "";
                            for (int i = 0; i < modDir.Length; i++)
                            {
                                if (!modDir[i].Equals(folder.Name))
                                {
                                    dirr += (modDir[i] + "\\");
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            folder.Reciever = tm.Reciever.Split('/')[0] + "/";

                            folder.Dir = dirr;
                            Console.WriteLine($"About to Add Folder: {folder.Name} , {folder.size}");
                            folder.Status = tm.Status;
                            folder.DateTime = DateTime.Parse(reader["date"].ToString());

                            folder.initial = "F";
                            todaysSales.Add(folder);
                            Console.WriteLine($"Added Folder: {folder.Name}");
                        }
                        else
                        {

                            if (folder.Name.Equals(tm.Reciever.Split('/')[1]))
                            {
                                int reals = folder.size.Length - 3;
                                int tsize = int.Parse(folder.size.Substring(0, reals));
                                Console.WriteLine($"Folder Size was {folder.size} : {tsize}");
                                int treals = tm.size.Length - 3;
                                int ttsize = int.Parse(tm.size.Substring(0, treals));
                                Console.WriteLine($"New Item Size is : {tsize}");

                                int Total = ttsize + tsize;
                                if (Total.ToString().Length > 3)
                                {
                                    folder.size = Total + " GB";
                                }
                                else
                                {
                                    folder.size = Total + " MB";
                                }
                                Console.WriteLine($"Folder SIZE IS {folder.size} : {Total} : Length {Total.ToString().Length}");

                                folder.TotalPrice += tm.TotalPrice;
                                Console.WriteLine($"Update Folder: {folder.Name}");
                                Console.WriteLine("Last Item: " + todaysSales.Last().Name);
                            }
                            else
                            {
                                folder = new SaleModel();
                                folder.Name = tm.Reciever.Split('/')[1];

                                folder.size = tm.size;
                                folder.TotalPrice = tm.TotalPrice;
                                folder.Status = tm.Status;
                                folder.initial = "F";
                                folder.DateTime = DateTime.Parse(reader["date"].ToString());
                                string[] modDir = tm.Dir.Split('\\');
                                string dirr = "";
                                for (int i = 0; i < modDir.Length; i++)
                                {
                                    if (!modDir[i].Equals(folder.Name))
                                    {
                                        dirr += (modDir[i] + "\\");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                folder.Reciever = tm.Reciever.Split('/')[0] + "/";

                                folder.Dir = dirr;
                                Console.WriteLine($"About to Add Folder: {folder.Name}");
                                todaysSales.Add(folder);
                                Console.WriteLine($"Added Folder: {folder.Name}");
                            }
                        }
                    }
                    else
                    {
                        if (onFolder)
                        {
                            SaleModel directory = folder;
                            todaysSales.RemoveAt(todaysSales.Count - 1);
                            Console.WriteLine($"About to Add Directory: {directory.Name}");
                            onFolder = false;
                            todaysSales.Add(directory);
                            Console.WriteLine($"About to Add Video: {tm.Name}");
                            todaysSales.Add(tm);
                            folder = null;

                        }
                        else
                        {
                            onFolder = false;
                            Console.WriteLine($"About to Add Video: {tm.Name}");
                            todaysSales.Add(tm);

                        }

                    }
                }
                conn.Close();
                //foreach (SaleModel sm in todaysSales)
                //{
                //    Console.WriteLine($"SaleModel {sm.Name}");
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            foreach (SaleModel sm in todaysSales)
            {
                Console.WriteLine($"Returning SaleModel {sm.Name}");
            }
            return todaysSales;

        }

        public static int getTodaySalesByName(string name)
        {
            int count = 0;
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            string query = $"SELECT * FROM filmdatabase.sells WHERE date like '{today}%' and name = '{name}' order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");
               
                while (reader.Read())
                {
                    count++;
                    
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            return count;
        }

        public static int getTotalSalesByName(string name)
        {
            int count = 0;
            MySqlConnection conn = DBUtils.GetDBConnection();
            string query = $"SELECT * FROM filmdatabase.sells WHERE name = '{name}' order by id DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                while (reader.Read())
                {
                    count++;

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetType Execption");
            }
            return count;
        }

        public static List<SaleModel> getItems(SaleModel fr)
        {
            int id = 0;
            string r = "";
            List<SaleModel> todaysSales = new List<SaleModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = fr.DateTime;
            String date = theDate.ToString("yyyy-MM-dd hh-mm-ss");
            Console.WriteLine($"Date: {date}");
            string query = $"SELECT id,reciever_name FROM filmdatabase.sells WHERE date = '{date}'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                while (reader.Read())
                {
                    id = int.Parse(reader["id"].ToString());
                    r = reader["reciever_name"].ToString();
                }
                conn.Close();
                Console.WriteLine($"Id worked {id}");
                date =theDate.ToString("yyyy-MM-dd");
                query = $"SELECT * FROM filmdatabase.sells WHERE directory like '%{fr.Name}%' and date like '{date}%' and id <= {id} and reciever_name = '{r}' order by id DESC";
                MySqlConnection connn = DBUtils.GetDBConnection();
                MySqlCommand cmmd = new MySqlCommand(query, connn);

                connn.Open();
                MySqlDataReader readr = cmmd.ExecuteReader();
                Console.WriteLine($"Reader Worked :  {readr.VisibleFieldCount}");

                bool element = false;
                int predid = 0;
                
                while (readr.Read())
                {
                    Console.WriteLine($"reading {readr["id"].ToString()}");

                    SaleModel tm = new SaleModel();
                    if(id == int.Parse(readr["id"].ToString()))
                    {
                        element = true;
                        predid = id;
                    }
                    if (element)
                    {
                        if((predid - int.Parse(readr["id"].ToString())) <= 1)
                        {
                            tm.Name = readr["name"].ToString();

                            tm.Dir = readr["directory"].ToString();
                            tm.TotalPrice = Double.Parse(readr["price"].ToString());
                            tm.size = readr["size"].ToString();
                            //Console.WriteLine($"Sell  Name: {tm.Name}");

                            //Console.WriteLine($"Sell  Name: {tm.Name}, Type Id: {int.Parse(reader["type"].ToString())}");
                            tm.Reciever = readr["reciever_name"].ToString();
                            string d = readr["date"].ToString();
                            tm.DateTime = DateTime.Parse(d);
                            Console.WriteLine($"Tm Date {tm.DateTime}");
                            tm.Type = TypeModel.GetTypeToFile(int.Parse(readr["type"].ToString()));
                            tm.initial = tm.Type.Icon;
                            tm.Status = int.Parse(readr["status"].ToString());
                            predid = int.Parse(readr["id"].ToString());

                            todaysSales.Add(tm);
                        }
                        else
                        {
                            element = false;
                            break;
                        }
                    }
                    
                }
                connn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return todaysSales;

        }

        public static List<ChartModel> calculateTodaysEarnings()
        {
            List<ChartModel> barChart = new List<ChartModel>();
            List<SaleModel> todaysSells = SaleModel.getTodaysSales();
            Dictionary<string, double> typeMoney = new Dictionary<string, double>();

            foreach(SaleModel sale in todaysSells)
            {
                if (typeMoney.ContainsKey(sale.Type.Name))
                {
                    typeMoney[sale.Type.Name] += sale.TotalPrice; 
                }
                else
                {
                    typeMoney.Add(sale.Type.Name, sale.TotalPrice);
                }
            }
            foreach (KeyValuePair<string, double> item in typeMoney)
            {
                ChartModel cm = new ChartModel(item.Key, item.Value);

                barChart.Add(cm);

            }
            return barChart;

        }


        
        private string calculateSize(long size)
        {
            string trimmedSize = "";
            if (size > 1000000000)
            {
                float ss = (float)Math.Round((double)size / 1073741824, 2);

                trimmedSize = ss‬ + " GB";
            }
            else if (size > 1000000)
            {
                float ss = (float)Math.Round((double)size / 1048576, 0);
                trimmedSize = ss‬ + " MB";
            }
            else
            {
                float ss = (float)Math.Round((double)size / 1024, 0);
                trimmedSize = ss + " KB";
            }
            return trimmedSize;
        }


    }
}
