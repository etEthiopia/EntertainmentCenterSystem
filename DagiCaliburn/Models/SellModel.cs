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
    class SellModel
    {
        public string Name { get; set; }
        public string Dir { get; set; }
        public TypeModel Type { get; set; }
        public string Initials { get; set; }
        public float Price { get; set; }
        public string Size { get; set; }
        public DateTime DateTime { get; set; }
        public string RecieverPort { get; set; }
        public string RecieverSerial { get; set; }
        public int Folder { get; set; }
        public int Special { get; set; }
        public int Item { get; set; }
        public int ID { get; set; }
        public long TotalSize { get; set; }


        public static List<String> folders = new List<string>();
        public static List<SellModel> roots = new List<SellModel>();
        public static List<SellModel> albums = new List<SellModel>();
        MySqlConnection conn = DBUtils.GetDBConnection();


        //All Sells by the given params
        public static List<SellModel> allSells()
        {
            return VideoModel.getAllSells();
        }

        //Sells by the given params
        public static List<SellModel> getSells(string view,string category, int t, string date, int paged)
        {

            Console.WriteLine($"GETSELLS VIEW: {view}, CATEGORY: {category}, T:{t}, DATE:{date}");
            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            paged = paged * 6;
            string query = "";

            if (!date.Equals(""))
            {
                today = date;
            }

            //if (view.Equals("Listed"))

            if (t != 0)
            {
                if (category.Equals("") || category.Equals("All"))
                {
                    if (view.Equals("Listed"))
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%' UNION ALL SELECT * FROM fdb.audiosells WHERE datetime like '{today}%' UNION ALL SELECT * FROM fdb.othersells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";

                    }
                    else
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%' and videofolderserial like '1%' UNION ALL SELECT * FROM fdb.audiosells WHERE datetime like '{today}%' UNION ALL SELECT * FROM fdb.othersells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";
                    }
                }
                else if (category.Equals("Video"))
                {
                    if (view.Equals("Listed"))
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%' and type = {t} order by datetime DESC LIMIT {paged}";

                    }
                    else
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%' and videofolderserial like '1%'  and type = {t} order by datetime DESC LIMIT {paged}";

                    }
                }
                else if (category.Equals("Audio"))
                {
                    query = $"SELECT * FROM fdb.audiosells WHERE datetime like '{today}%'  and type = {t} order by datetime DESC LIMIT {paged}";
                }
                else if (category.Equals("Others"))
                {
                    query = $"SELECT * FROM fdb.othersells WHERE datetime like '{today}%'  and type = {t} order by datetime DESC LIMIT {paged}";
                }
            }
            else
            {
                if (category.Equals("") || category.Equals("All"))
                {
                    if (view.Equals("Listed"))
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%'  UNION ALL SELECT * FROM fdb.audiosells WHERE datetime like '{today}%' UNION ALL SELECT * FROM fdb.othersells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";
                    }
                    else
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%'  and videofolderserial like '1%'  UNION ALL SELECT * FROM fdb.audiosells WHERE datetime like '{today}%' UNION ALL SELECT * FROM fdb.othersells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";

                    }
                }
                else if (category.Equals("Video"))
                {
                    if (view.Equals("Listed"))
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";
                    }
                    else
                    {
                        query = $"SELECT * FROM fdb.videosells WHERE datetime like '{today}%'  and videofolderserial like '1%' order by datetime DESC LIMIT {paged}";
                    }
                }
                else if (category.Equals("Audio"))
                {
                    query = $"SELECT * FROM fdb.audiosells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";
                }
                else if (category.Equals("Others"))
                {
                    query = $"SELECT * FROM fdb.othersells WHERE datetime like '{today}%' order by datetime DESC LIMIT {paged}";
                }
            }

            Console.WriteLine($"FINAL QUERY : {query}");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                //Console.WriteLine($"getTodaysSales() - {query}");

                while (reader.Read())
                {
                        SellModel tm = new SellModel();
                    if (int.Parse(reader["videofolderserial"].ToString()) == 1 || view.Equals("Listed"))
                    {
                        tm.Name = reader["name"].ToString();
                        tm.ID = int.Parse(reader["id"].ToString());
                        //tm.Dir = reader["dir"].ToString();
                        tm.Price = float.Parse(reader["price"].ToString());
                        tm.Size = reader["size"].ToString();
                        tm.RecieverSerial = reader["recieverserial"].ToString();
                        tm.RecieverPort = reader["reciever"].ToString();
                        tm.Folder = int.Parse(reader["videofolderserial"].ToString());
                        //string d = reader["datetime"].ToString();
                        //tm.DateTime = DateTime.Parse(d);
                        if (int.Parse(reader["type"].ToString()) == 0)
                        {
                            tm.Initials = "F";
                            
                            //tm.Folder = int.Parse(reader["folderserial"].ToString());
                        }
                        else
                        {
                            tm.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                            tm.Initials = tm.Type.Icon;
                        }
                    }
                    else if(int.Parse(reader["videofolderserial"].ToString()) > 1 && reader["videofolderserial"].ToString()[0].Equals('1'))
                    {
                        Console.WriteLine($"PRE-SIMPLIFED---- " + int.Parse(reader["videofolderserial"].ToString()));
                        tm = VideoModel.getSimplifiedSells(int.Parse(reader["videofolderserial"].ToString().Substring(1)), reader["recieverserial"].ToString());
                    }
                    if (tm.Name.Count()> 0)
                    {
                        todaysSales.Add(tm);
                    }
                    
                    
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetSells Execption {e.Message}");
            }

            return todaysSales;



            //return VideoModel.getSells(view, category, type, date);
        }


        //Sets the static variable TE in MainViewModel to TOdaysEarnings
        public static void getTodaysEarnings()
        {

            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            DateTime theDate = DateTime.Now;
            String today = theDate.ToString("yyyy-MM-dd");
            
            
            string query = $" SELECT SUM(price) FROM fdb.videosells WHERE datetime like '{today}%' UNION ALL SELECT SUM(price) FROM fdb.audiosells WHERE datetime like '{today}%' UNION ALL SELECT SUM(price) FROM fdb.othersells WHERE datetime like '{today}%' ";

            
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"getEpisodes() - {query}");

                MainViewModel.TE = 0.0f;

                while (reader.Read())
                {
                    if (reader["SUM(price)"].ToString().Length > 0)
                    {
                        MainViewModel.TE += float.Parse(reader["SUM(price)"].ToString());
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Todays Earnings Execption {e.Message}");
            }
            

        }

        //Prepare Sale
        public void PrepareSale(string fulldir)
        {
            string fullpath = Utils.BackToFront(fulldir);
            Console.WriteLine($"Prepared Sale {fullpath}");
            if (Utils.IsVideo(fullpath))
            {
                VideoModel vml = VideoModel.FinalizeVideoSale(fullpath);
                if (vml.Price > 0)
                {
                    VideoModel.videos.Add(vml);
                    VideoModel.videoNames.Insert(0,vml.Name);
                   // Console.WriteLine($"Copyyyyy {VideoModel.videos.Count}");
                }
                
            }
            else if (Utils.IsFolder(fullpath))
            {
                if (!folders.Contains(fullpath + "/"))
                {
                    
                    roots.Add(FinalizeFolderSale(fullpath));
                    int rf = TypeModel.GetTypeIntReference(roots.Last().Name);
                    if (rf != 0)
                    {
                        roots.Last().Type = TypeModel.GetTypeToFile(rf);
                        if (TypeModel.GetTypeToFile(rf).DataType.Equals("Audio"))
                        {
                           Console.WriteLine($"Audio Got By Reference: {roots.Last().Name}, {roots.Last().Type.Name}");
                           roots.Last().Price = TypeModel.GetAlbumPrice(rf);
                           AudioModel.AlbumNames.Insert(0,roots.Last().Name);
                           AudioModel am = Utils.selltoAudioModel(roots.Last());
                           AudioModel.Albums.Add(am);
                           Console.WriteLine($"{roots.Last().Name} in AlbumNames");  
                        }
                        else if (TypeModel.GetTypeToFile(rf).DataType.Equals("Others"))
                        {
                            if (!OthersModel.OtherNames.Contains(Utils.GetName(fullpath)))
                            {
                                OthersModel oml = OthersModel.FinalizeOthersSale(fullpath);
                                OthersModel.Others.Add(oml);
                                OthersModel.OtherNames.Insert(0,oml.Name);
                            }
                            
                            
                        }
                        else if (roots.Last().Type.DataType.Equals("Video"))
                        {
                            VideoModel.FolderWithVideos.Add(roots.Last().Folder, new List<VideoModel>());
                            roots.Last().Price = (float)VideoModel.CalculateFolder(fullpath + "/", roots.Last().Folder);

                        }
                    }
                    else
                    {
                        if (roots.Last().Type != null)
                        {
                            if (roots.Last().Type.DataType.Equals("Audio"))
                            {
                                Console.WriteLine($"Audio Got By Dir : {roots.Last().Name}, {roots.Last().Type.Name}");
                                roots.Last().Price = TypeModel.GetAlbumPrice(roots.Last().Type.Id);
                                AudioModel.AlbumNames.Insert(0,roots.Last().Name);
                                Console.WriteLine($"{roots.Last().Name} in AlbumNames");
                                AudioModel am = Utils.selltoAudioModel(roots.Last());
                                AudioModel.Albums.Add(am);
                                //roots.Last().Price = roots.Last().Film.getMusicAlbumPrice(roots.Last().Type.Id);
                                //Console.WriteLine($"Prepare Root {roots.Last().Name} Pirce :  {roots.Last().TotalPrice}  Type : {roots.Last().Type.Id}");
                                //albums.Add(roots.Last());
                            }
                            else if (roots.Last().Type.DataType.Equals("Others"))
                            {
                                if (!OthersModel.OtherNames.Contains(Utils.GetName(fullpath)))
                                {
                                    OthersModel oml = OthersModel.FinalizeOthersSale(fullpath);
                                    OthersModel.Others.Add(oml);
                                    OthersModel.OtherNames.Insert(0,oml.Name);
                                }
                            }
                            else if (roots.Last().Type.DataType.Equals("Video"))
                            {
                                VideoModel.FolderWithVideos.Add(roots.Last().Folder, new List<VideoModel>());
                                roots.Last().Price = (float)VideoModel.CalculateFolder(fullpath + "/", roots.Last().Folder);

                            }
                        }
                    }
                    
                }
            }
            else
            {
                int rf = TypeModel.GetTypeIntReference(Utils.GetName(fullpath));
                if (rf != 0)
                {
                    TypeModel tmMaybe = TypeModel.GetTypeToFile(rf);
                    if (tmMaybe.DataType.Equals("Others"))
                    {
                        if (!OthersModel.OtherNames.Contains(Utils.GetName(fullpath)))
                        {
                            OthersModel oml = OthersModel.FinalizeOthersSale(fullpath, tmMaybe);
                            OthersModel.Others.Add(oml);
                            OthersModel.OtherNames.Insert(0,oml.Name);
                        }
                    }
                    else if (tmMaybe.DataType.Equals("Audio"))
                    {
                        if (!AudioModel.AlbumNames.Contains(Utils.GetName(fullpath)))
                        {
                            AudioModel am = AudioModel.FinalizeAudiosSale(fullpath, tmMaybe);
                            AudioModel.AlbumNames.Insert(0, am.Name);
                            AudioModel.Albums.Add(am);
                            //SellModel.albums(am);
                            
                        }
                    }


                }
                else
                {
                    int typeId = TypeModel.GetTypeInt(Utils.GetDir(fullpath));
                        if (typeId != 0) {
                        TypeModel tmMaybe = TypeModel.GetTypeToFile(typeId);
                        if (tmMaybe.DataType.Equals("Others"))
                        {
                            if (!OthersModel.OtherNames.Contains(Utils.GetName(fullpath)))
                            {
                                OthersModel oml = OthersModel.FinalizeOthersSale(fullpath, tmMaybe);
                                OthersModel.Others.Add(oml);
                                OthersModel.OtherNames.Insert(0,oml.Name);
                            }
                        }
                        else if (tmMaybe.DataType.Equals("Audio"))
                        {
                            AudioModel am = AudioModel.FinalizeAudiosSale(fullpath,tmMaybe);
                            
                            AudioModel.Albums.Add(am);
                            AudioModel.AlbumNames.Insert(0, am.Name);

                            //roots.Last().Price = roots.Last().Film.getMusicAlbumPrice(roots.Last().Type.Id);
                            //Console.WriteLine($"Prepare Root {roots.Last().Name} Pirce :  {roots.Last().TotalPrice}  Type : {roots.Last().Type.Id}");
                            //albums.Add(roots.Last());
                        }


                    }
                }
            }
   
        }

        //Final Prepare Folder
        public SellModel FinalizeFolderSale(string item)
        {
            SellModel sm = new SellModel();
            sm.Name = Utils.GetName(item);
            sm.Dir = Utils.GetDir(item);

            sm.TotalSize = Utils.CalculateFolderSize(item);

            sm.Size = Utils.calculateSize(sm.TotalSize);

            int type = TypeModel.GetTypeInt(item);
            Console.WriteLine($"FOLDER TYPE : {type}");


            if (type != 0)
            {
                sm.Type = TypeModel.GetTypeToFile(type);
                Console.WriteLine($"{sm.Name}: type is {sm.Type.Name}, {sm.Type.DataType}");
                if (sm.Type.DataType.Equals("Video"))
                {
                    sm.Price = (float)sm.Type.Price;
                    sm.Initials = sm.Type.Icon;

                    sm.Folder = Utils.GenerateRandom();
                }
                else if (sm.Type.DataType.Equals("Audio"))
                {
                    //MessageBox.Show($"{Name} is Audio");
                }
            }

            
                return sm;
        }

        //Get full detialed Sell 
        public SellModel getSell()
        {
            SellModel smk = new SellModel();
            TypeModel tml = new TypeModel();
            tml = this.Type;
            string query = "";
            Console.WriteLine($"GETSELL QUERY: {query} {ID}, {tml.DataType}");
            if (tml.DataType.Equals("Video"))
            {
                query = $"SELECT * FROM fdb.videosells WHERE id = {ID}";
                
            }
            else if (tml.DataType.Equals("Audio"))
            {
                query = $"SELECT * FROM fdb.audiosells WHERE id = {ID}";
            }
            else if (tml.DataType.Equals("Others"))
            {
                query = $"SELECT * FROM fdb.othersells WHERE id = {ID}";
            }
            Console.WriteLine($"GETSELL QUERY: {query}");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    smk.Name = reader["name"].ToString();
                    smk.ID = int.Parse(reader["id"].ToString());
                    smk.Dir = reader["dir"].ToString();
                    smk.Price = float.Parse(reader["price"].ToString());
                    smk.Size = reader["size"].ToString();
                    smk.RecieverSerial = reader["recieverserial"].ToString();
                    smk.RecieverPort = reader["reciever"].ToString();
                    smk.Folder = int.Parse(reader["videofolderserial"].ToString());
                    string d = reader["datetime"].ToString();
                    smk.DateTime = DateTime.Parse(d);
                    if (int.Parse(reader["type"].ToString()) == 0)
                    {
                        smk.Initials = "F";
                        //tm.Folder = int.Parse(reader["folderserial"].ToString());
                    }
                    else
                    {
                        smk.Type = TypeModel.GetTypeToFile(int.Parse(reader["type"].ToString()));
                        smk.Initials = smk.Type.Icon;
                    }

                    
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetSell Exception, {e.Message}");
            }

            return smk;
        }

        //Get full detialed DATE of Sell 
        public String getSellDateFromName()
        {
            string date = "";
            string query = "";
            try
            {
                if (Type.DataType.Equals("Video"))
            {
                query = $"SELECT datetime FROM fdb.videosells WHERE name = '{Name}' and dir = '{Dir}' order by ID DESC";

            }
                else if (Type.DataType.Equals("Audio"))
            {
                query = $"SELECT datetime FROM fdb.audiosells WHERE name = '{Name}' and dir = '{Dir}' order by ID DESC";
            }
                else if (Type.DataType.Equals("Others"))
            {
                query = $"SELECT datetime FROM fdb.othersells WHERE name = '{Name}' and dir = '{Dir}' order by ID DESC";
            }
                else
                {
                    query = $"SELECT datetime FROM fdb.videosells WHERE name = '{Name}' and dir = '{Dir}' order by ID DESC";

                }
                Console.WriteLine($"GETSELL QUERY: {query}");
            
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    date = reader["datetime"].ToString();
                    


                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetDateSell from name  Exception, {e.Message}");
            }

            return date;
        }

        public String getSellDateFromFolder()
        {
            string date = "";
            string query = "";
            string dir = Dir + Name + "/";
            try
            {
                if (Type.DataType.Equals("Video"))
                {
                    query = $"SELECT datetime FROM fdb.videosells WHERE dir = '{dir}' order by ID DESC";

                }
                
                Console.WriteLine($"GETSELL FOLDER DATE QUERY: {query}");

                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    date = reader["datetime"].ToString();



                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetDateSell from name  Exception, {e.Message}");
            }

            return date;
        }

        //Delete Sell
        public bool Delete()
        {
            string query = "";
            Console.WriteLine($"FOLDER {Folder}");
            string k = DateTime.ToString("yyyy-MM-dd hh: mm:ss");
            DateTime = DateTime.Parse(k);
            if (Type.DataType.Equals("Video"))
            {
                query = $"DELETE FROM videosells WHERE name = '{Name}'  AND recieverserial = '{RecieverSerial}' AND videofolderserial = {Folder}";
            }
            else if (Type.DataType.Equals("Audio"))
            {
                query = $"DELETE FROM audiosells WHERE name = '{Name}' AND recieverserial = '{RecieverSerial}' AND videofolderserial = {Folder}";
            }
            else if (Type.DataType.Equals("Others"))
            {
                query = $"DELETE FROM othersells WHERE name = '{Name}' AND recieverserial = '{RecieverSerial}' AND videofolderserial = {Folder}";
            }


            try
            {


                MySqlCommand cmmd = new MySqlCommand(query, conn);
                Console.WriteLine($"Delete Sell: {query}");

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
                Console.WriteLine($"Delete Sell Exception {Name} {e.Message}");
            }
            return false;
        }

        //Delete Special
        public bool DeleteFolder()
        {
            string query = "";
            long I1 = (long)0.0;
            long I = (long)0.0;
            Console.WriteLine($"FOLDER {Folder}");
            if (Folder.ToString()[0].Equals('1'))
            {
                I1 = (long)Folder;
                if (Folder > 1)
                {
                    I = long.Parse(Folder.ToString().Substring(1));
                }
                else
                {
                    I = I1;
                }
            }
            else
            {
                I = (long)Folder;
                I1 = long.Parse('1' + Folder.ToString());
            }

            query = $"DELETE FROM videosells WHERE videofolderserial = {I} or videofolderserial = {I1}  AND recieverserial = '{RecieverSerial}' ";
            


            try
            {


                MySqlCommand cmmd = new MySqlCommand(query, conn);

                Console.WriteLine($"Delete FSell: {query}");
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
                Console.WriteLine($"Delete Sell Exception {Name} {e.Message}");
            }
            return false;
        }

        public static bool AddDirs(bool edit, int type, string Namme, List<Dir> dirs)
        {
            List<SellModel> todaysSales = new List<SellModel>();
            MySqlConnection conn = DBUtils.GetDBConnection();
            int id = 0;
            foreach (Dir d in dirs)
            {
                Console.WriteLine($"dir recived : " + d.dir);
                Console.WriteLine($"INSERT IGNORE INTO fdb.dirs (dir,type) values('{Utils.BackToFront(d.dir)}',{type})");
            }
            string query = $"SELECT id FROM fdb.itemtypes WHERE name = '{Namme}'";
            if (!edit)
            {


                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine($"Get ID from Name() - {query}");

                    while (reader.Read())
                    {
                        id = int.Parse(reader["id"].ToString());
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("GetType ID Execption");
                }
            }
            else
            {
                id = type;
            }


            if (id > 0)
            {
                if (edit)
                {
                    deleteDirs(id);
                }
                try
                {
                    foreach (Dir dik in dirs)
                    {
                        if (dik.dir.Length > 0)
                        {
                            query = $"INSERT IGNORE INTO fdb.dirs (dir,type) values('{Utils.BackToFront(dik.dir)}',{id})";
                            MySqlConnection conn2 = DBUtils.GetDBConnection();
                            Console.WriteLine($"INSERT {dik}, {query}");
                            conn2.Open();
                            MySqlCommand cmd = new MySqlCommand(query, conn2);

                            cmd.ExecuteNonQuery();

                            conn2.Close();
                        }

                    }
                    return true;
                }

                catch (Exception e)
                {
                    Console.WriteLine("Add Dir Execption " + e.Message);
                }
            }
            return false;

        }
        // Delete othersdetails
        public static bool deleteDirs(int type)
        {
            string query = $"DELETE FROM dirs WHERE type = {type}";
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {


                MySqlCommand cmmd = new MySqlCommand(query, conn);
                Console.WriteLine($"Delete Dir: {query}");

                conn.Open();
                MySqlDataReader reader = cmmd.ExecuteReader();

                while (reader.Read())
                {

                }
                reader.Close();
                conn.Close();

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Delete Dir Exception {e.Message}");
            }
            return false;
        }



    }
}
