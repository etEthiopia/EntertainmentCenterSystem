using DagiCaliburn.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.Models
{
    class Watcher
    {
        private Object watcherLock = new object();

        public void CreateWatch(string FullPath, string name)
        {
            //lock (watcherLock)
            //{
            string fullShit = Utils.BackToFront(FullPath);


            string Directory = "";
            Directory = Utils.GetDir(fullShit);

            string Name = name;
            int typee = TypeModel.GetTypeIntReference(Name);
            if (Utils.IsFolder(fullShit))
            {               
                if (typee == 0)
                {

                    if (AudioModel.AlbumNames.Contains(Name))
                    {
                        string drve = Utils.GetDrive(fullShit);
                        TraceAudioSt(drve, Name);
                    }
                    else if (OthersModel.OtherNames.Contains(Name))
                    {
                        string drve = Utils.GetDrive(fullShit);
                        TraceOtherSt(drve, Name);
                    }
                    else
                    {
                        int src = SellModel.roots.Count - 1;
                        if (src >= 0)
                        {
                            for (int k = src; k >= 0; k--)
                            {
                                if (k >= 0)
                                {
                                    SellModel FolderSL = SellModel.roots[k];
                                    string drve = Utils.GetDrive(fullShit);
                                    if (FolderSL.Name.Equals(Name))
                                    {
                                        try
                                        {
                                            Console.WriteLine($"Beforekkk: {FolderSL.Folder}, {k}");

                                            //Thread th = new Thread(
                                            //() => 
                                            TraceThatSt(drve, FolderSL);

                                            //th.Start();
                                        }
                                        catch (Exception etry)
                                        {
                                            Console.WriteLine($"WORKER.ERROR {etry.Message}");
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        //}
                    }
                }
                else
                {
                    TypeModel tyif = TypeModel.GetTypeToFile(typee);
                    if (tyif.DataType.Equals("Audio"))
                    {
                            AudioModel audioMayBe = new AudioModel();
                            if (AudioModel.AudioByReference(fullShit, tyif, out audioMayBe))
                            {
                                AudioModel.AlbumNames.Insert(0, audioMayBe.Name);
                                AudioModel.Albums.Add(audioMayBe);
                                string drve = Utils.GetDrive(fullShit);
                                TraceAudioSt(drve, Name);
                            }
                        
                    }
                    else if (tyif.DataType.Equals("Others"))
                        {
                            OthersModel otherMayBe = new OthersModel();
                            if (OthersModel.OtherByReference(fullShit, tyif, out otherMayBe))
                            {
                                OthersModel.OtherNames.Insert(0,otherMayBe.Name);
                                OthersModel.Others.Add(otherMayBe);
                                string drve = Utils.GetDrive(fullShit);
                                TraceOtherSt(drve, Name);
                            }
                        }
                        
                    
                    
                }

            }
            else
            {
                if (Utils.IsVideo(fullShit))
                {
                    Console.WriteLine($"Video False Sell - Full Path :  {Directory}, Name : {Name}");
                    FileSystemWatcher watcher = new FileSystemWatcher()
                    {
                        Path = Directory,
                        Filter = Name
                    };
                    watcher.EnableRaisingEvents = true;
                    watcher.Changed += new FileSystemEventHandler(StartViewModel.startview.WatchChanged);
                    //svm.Sell(Directory, e.Name, false);
                }
                else if (typee == 0) {
                    if (AudioModel.AlbumNames.Contains(Name))
                    {
                        Console.WriteLine($"Audio False Sell - Full Path :  {Directory}, Name : {Name}");
                        FileSystemWatcher watcher = new FileSystemWatcher()
                        {
                            Path = Utils.GetDir(fullShit),
                            Filter = Utils.GetName(fullShit)
                        };
                        watcher.EnableRaisingEvents = true;
                        watcher.Changed += new FileSystemEventHandler(StartViewModel.startview.WatchChanged);

                    }
                    else if (OthersModel.OtherNames.Contains(Name))
                    {
                        Console.WriteLine($"Other False Sell - Full Path :  {Directory}, Name : {Name}");
                        FileSystemWatcher watcher = new FileSystemWatcher()
                        {
                            Path = Utils.GetDir(fullShit),
                            Filter = Utils.GetName(fullShit)
                        };
                        watcher.EnableRaisingEvents = true;
                        watcher.Changed += new FileSystemEventHandler(StartViewModel.startview.WatchChanged);

                    }
                }
                else if (typee != 0)
                {
                    TypeModel tyif = TypeModel.GetTypeToFile(typee);
                    if (tyif.DataType.Equals("Others"))
                    {
                        OthersModel otherMayBe = new OthersModel();
                        if (OthersModel.OtherByReference(fullShit, tyif, out otherMayBe))
                        {
                            OthersModel.OtherNames.Insert(0, otherMayBe.Name);
                            OthersModel.Others.Add(otherMayBe);
                            Console.WriteLine($"Other False Sell - Full Path :  {Directory}, Name : {Name}");
                            FileSystemWatcher watcher = new FileSystemWatcher()
                            {
                                Path = Utils.GetDir(fullShit),
                                Filter = Utils.GetName(fullShit)
                            };
                            watcher.EnableRaisingEvents = true;
                            watcher.Changed += new FileSystemEventHandler(StartViewModel.startview.WatchChanged);

                        }
                    }
                    if (tyif.DataType.Equals("Audio"))
                    {
                        AudioModel audioMayBe = new AudioModel();
                        if (AudioModel.AudioByReference(fullShit, tyif, out audioMayBe))
                        {
                            AudioModel.AlbumNames.Insert(0, audioMayBe.Name);
                            AudioModel.Albums.Add(audioMayBe);

                            FileSystemWatcher watcher = new FileSystemWatcher()
                            {
                                Path = Utils.GetDir(fullShit),
                                Filter = Utils.GetName(fullShit)
                            };
                            watcher.EnableRaisingEvents = true;
                            watcher.Changed += new FileSystemEventHandler(StartViewModel.startview.WatchChanged);

                        }

                    }
                }

            }
            //Console.WriteLine($"SetupListener watchcreated in {FullPath} - {Name}");
            //}

        }

        private void TraceFolder(object sender, DoWorkEventArgs e)
        {
            int done = 0;
            //, string drrive, SellModel FsellModel
            object[] parameters = e.Argument as object[];
            string drrive = (string)parameters[0];
            SellModel FsellModel = (SellModel)parameters[1];
            try
            {
                while (true)
                {

                    foreach (VideoModel videoYay in VideoModel.FolderWithVideos[FsellModel.Folder])
                    {
                        if (File.Exists(drrive + FsellModel.Name + "/" + videoYay.Name))
                        {
                            long tz = new FileInfo(drrive + FsellModel.Name + "/" + videoYay.Name).Length;
                            if (videoYay.Added == false)
                            {
                                if (videoYay.TotalSize == tz)
                                {
                                    //MessageBox.Show($"COPY DONE {videoYay.Name}");
                                    videoYay.RecieverPort = drrive;
                                    foreach (Disk d in StartViewModel.mainview.Drives)
                                    {
                                        if (d.Port.Equals(videoYay.RecieverPort.Substring(0, 2)))
                                        {
                                            videoYay.RecieverSerial = d.serial;
                                            break;
                                        }
                                    }
                                    if (videoYay.AddAsASale())
                                    {
                                        videoYay.Added = true;
                                        done += 1;
                                        Console.WriteLine(Thread.CurrentThread.Name + " DONNNNNE " + done + " " + videoYay.Name + " IN " + videoYay.RecieverPort + " AT " + videoYay.DateTime);
                                    }

                                }
                            }
                            else
                            {
                                if (videoYay.TotalSize == tz)
                                {
                                    //MessageBox.Show($"COPY DONE {videoYay.Name}");
                                    if (!videoYay.RecieverPort.Equals(drrive))
                                    {
                                        Console.WriteLine($"It was already added to - {videoYay.RecieverPort}");
                                        videoYay.RecieverPort = drrive;
                                        foreach (Disk d in StartViewModel.mainview.Drives)
                                        {
                                            if (d.Port.Equals(videoYay.RecieverPort.Substring(0, 2)))
                                            {
                                                videoYay.RecieverSerial = d.serial;
                                                break;
                                            }
                                        }
                                        if (videoYay.AddAsASale())
                                        {
                                            videoYay.Added = true;
                                            done += 1;
                                            Console.WriteLine(Thread.CurrentThread.Name + " DONNNNNE " + done + " " + videoYay.Name + " IN " + videoYay.RecieverPort + " AT " + videoYay.DateTime);
                                        }
                                    }

                                }
                            }

                        }
                    }
                    if (done >= VideoModel.FolderWithVideos[FsellModel.Folder].Count)
                    {
                        e.Result = true;
                        break;
                    }
                    Thread.Sleep(2000);
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine($"TraceFolder -- {exx}");
            }
            //throw new NotImplementedException();
        }

        public void TraceOtherFolder(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            string drrive = (string)parameters[0];
            OthersModel ott = (OthersModel)parameters[1];
            try
            {
                while (true)
                {
                    if (ott.TotalSize <= Utils.CalculateFolderSize(drrive + ott.Name))
                    {
                        if (ott.Price > 0.0f)
                        {

                            ott.RecieverPort = drrive;
                            foreach (Disk d in StartViewModel.mainview.Drives)
                            {
                                if ((d.Port).Equals(ott.RecieverPort.Substring(0, 2)))
                                {
                                    ott.RecieverSerial = d.serial;

                                    Console.WriteLine($"{ott.Name}, other Added : {ott.AddOtherAsSale()}");

                                    break;
                                }
                            }

                        }
                        break;
                    }

                    Thread.Sleep(2000);
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine($"TraceOtherFolder -- {exx}");
            }
        }

        public void TraceAudioFolder(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            string drrive = (string)parameters[0];
            AudioModel att = (AudioModel)parameters[1];
            try
            {
                while (true)
                {
                    if (att.TotalSize <= Utils.CalculateFolderSize(drrive + att.Name))
                    {

                        att.RecieverPort = drrive;
                            foreach (Disk d in StartViewModel.mainview.Drives)
                            {
                                if ((d.Port).Equals(att.RecieverPort.Substring(0, 2)))
                                {
                                att.RecieverSerial = d.serial;

                                    Console.WriteLine($"{att.Name}, Audio Added : {att.AddAlbumAsSale()}");

                                    break;
                                }
                            }

                        
                        break;
                    }

                    Thread.Sleep(2000);
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine($"TraceOtherFolder -- {exx}");
            }
        }

        private void WatchChanged_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            Console.WriteLine($"***************Work Completed: {result}");
            //throw new NotImplementedException();
        }

        public void ChangeWatch(string FullPath, string name)
        {

            string fullShit = Utils.BackToFront(FullPath);


            string Directory = "";
            Directory = Utils.GetDir(fullShit);
            string drive = Utils.GetDrive(fullShit);
            string Name = name;


            if (Utils.IsVideo(fullShit))
            {
                VideoModel videoToSell = null;
                if (VideoModel.videoNames.Contains(Name))
                {
                    for (int i = VideoModel.videos.Count - 1; i >= 0; i--)
                    {
                        if (VideoModel.videos[i].Name.Equals(Name))
                        {
                            videoToSell = VideoModel.videos[i];
                            videoToSell.RecieverPort = drive;
                            foreach (Disk d in StartViewModel.mainview.Drives)
                            {
                                if ((d.Port).Equals(videoToSell.RecieverPort.Substring(0, 2)))
                                {
                                    videoToSell.RecieverSerial = d.serial;

                                    break;
                                }
                            }
                            Console.WriteLine($"Is {videoToSell.Name}, {videoToSell.RecieverPort} sold?  :  {videoToSell.AddAsASale()}");
                            break;

                        }
                    }
                }
                else if (VideoModel.VideoByReference(fullShit, out videoToSell))
                {
                    videoToSell.RecieverPort = drive;
                    foreach (Disk d in StartViewModel.mainview.Drives)
                    {
                        if (d.Port.Equals(videoToSell.RecieverPort.Substring(0, 2)))
                        {
                            videoToSell.RecieverSerial = d.serial;
                            break;
                        }
                    }
                    lock (watcherLock)
                    {
                        videoToSell.AddAsASale();

                    }
                }
            }
            //svm.Sell(Directory, e.Name, false);
            else if (OthersModel.OtherNames.Contains(name))
            {
                OthersModel ot = new OthersModel();
                for (int i = OthersModel.Others.Count - 1; i >= 0; i--)
                {
                    if (OthersModel.Others[i].Name.Equals(name))
                    {
                        ot = OthersModel.Others[i];
                        break;
                    }
                }
                if (ot.Price > 0.0f)
                {

                    ot.RecieverPort = drive;
                    foreach (Disk d in StartViewModel.mainview.Drives)
                    {
                        if ((d.Port).Equals(ot.RecieverPort.Substring(0, 2)))
                        {
                            ot.RecieverSerial = d.serial;
                            
                                Console.WriteLine($"{ot.Name}, other Added : {ot.AddOtherAsSale()}");
                            
                            break;
                        }
                    }

                }




                //Console.WriteLine($"SetupListener watchcreated in {FullPath} - {Name}");
            }

            else if (AudioModel.AlbumNames.Contains(name))
            {
                AudioModel al = new AudioModel();
                for (int i = AudioModel.Albums.Count - 1; i >= 0; i--)
                {
                    if (AudioModel.Albums[i].Name.Equals(name))
                    {
                        al = AudioModel.Albums[i];
                        
                        break;
                    }
                }
                
                    al.RecieverPort = drive;
                    foreach (Disk d in StartViewModel.mainview.Drives)
                    {
                        if ((d.Port).Equals(al.RecieverPort.Substring(0, 2)))
                        {
                            al.RecieverSerial = d.serial;

                            Console.WriteLine($"{al.Name}, Audio Added : {al.AddAlbumAsSale()}");

                            break;
                        }
                    }

                
            }
        }

        public void TraceThatSt(string drive, SellModel k2)
        {

            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WatchChanged_Completed);
            worker.WorkerReportsProgress = false;
            worker.DoWork += new DoWorkEventHandler(TraceFolder);
            Console.WriteLine("KKK " + k2.Folder + ", " + SellModel.roots.IndexOf(k2));
            object paramObj = drive;

            object paramObj2 = k2;
            object[] parameters = new object[] { paramObj, paramObj2 };

            worker.RunWorkerAsync(parameters);
        }

        public void TraceOtherSt(string drre, string oter)
        {

            OthersModel ot = new OthersModel();
            for (int i = OthersModel.Others.Count - 1; i >= 0; i--)
            {
                if (OthersModel.Others[i].Name.Equals(oter))
                {
                    ot = OthersModel.Others[i];
                    break;
                }
            }
            if (ot.Price > 0.0f)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WatchChanged_Completed);
                worker.WorkerReportsProgress = false;
                worker.DoWork += new DoWorkEventHandler(TraceOtherFolder);
                object paramObj = drre;
                object paramObj2 = ot;
                object[] parameters = new object[] { paramObj, paramObj2 };
                worker.RunWorkerAsync(parameters);
                
            }
        }

        public void TraceAudioSt(string drre, string oter)
        {

            AudioModel am = new AudioModel();
            for (int i = AudioModel.Albums.Count - 1; i >= 0; i--)
            {
                if (AudioModel.Albums[i].Name.Equals(oter))
                {
                    am = AudioModel.Albums[i];
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WatchChanged_Completed);
                    worker.WorkerReportsProgress = false;
                    worker.DoWork += new DoWorkEventHandler(TraceAudioFolder);
                    object paramObj = drre;
                    object paramObj2 = am;
                    object[] parameters = new object[] { paramObj, paramObj2 };
                    worker.RunWorkerAsync(parameters);
                    break;
                }
            }
            
                
            
        }

    }
}
