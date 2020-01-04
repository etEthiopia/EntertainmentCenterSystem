using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class SaleViewModel : Screen
    {
        private static BindableCollection<SaleModel> _sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
        private BindableCollection<SaleModel> sellsTemp = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
        private static BindableCollection<SaleModel> _fFiles;
        private static List<SaleModel> SelectedSells = new List<SaleModel>();

        public BindableCollection<SaleModel> FolderFiles
        {
            get
            {
                return _fFiles;
            }
            set
            {
                _fFiles = value;
                //OnPropertyChanged();
                this.NotifyOfPropertyChange(() => FolderFiles);
            }
        }
        public BindableCollection<SaleModel> Sells
        {
            get
            {
                return _sells;
            }
            set
            {
                _sells = value;
                //OnPropertyChanged();
                
                this.NotifyOfPropertyChange(() => Sells);
            }
        }
        public SaleModel SelectedSell { get; set; }

        BackgroundWorker worker = new BackgroundWorker();
        private bool _mainGridVsibility = true;
        private bool _blueIsVisible = false;
        private bool _greenIsVisible = false;
        private bool _fOPIV = false;

        public bool MainGridIsVisible {
            get { return _mainGridVsibility; }
            set { _mainGridVsibility = value;
                NotifyOfPropertyChange(() => MainGridIsVisible);
            }
        }
        public bool BlueIsVisible {
            get { return _blueIsVisible; }
            set
            {
                _blueIsVisible = value;
                NotifyOfPropertyChange(() => BlueIsVisible);
            }
        }
        public bool GreenIsVisible
        {
            get { return _greenIsVisible; }
            set
            {
                _greenIsVisible = value;
                NotifyOfPropertyChange(() => GreenIsVisible);
            }
        }
        public bool FileOnProgressIsVisible
        {
            get
            {
                return _fOPIV;
            }
            set
            {
                _fOPIV = value;
                NotifyOfPropertyChange(() => FileOnProgressIsVisible);
            }
        }

        private String _sInitials = "";
        private String _sName = "";
        private double _sPrice = 0;
        private String _sSize = "";
        private String _sDir = "";
        private DateTime _sDate;
        private String _sReciever = "";
        private int _sTS = 0;
        private int _sTTS = 0;

        public string _onProgress = "";

        public String OnProgress
        {
            get
            {
                return _onProgress;
            }
            set
            {
                _onProgress = value;
                NotifyOfPropertyChange(() => OnProgress);
            }

        }
 
        public String SelectedInitials {
            get
            {
                return _sInitials;
            }
            set
            {
                _sInitials = value;
                NotifyOfPropertyChange(() => SelectedInitials);
            }

        }

        public String SelectedName
        {
            get
            {
                return _sName;
            }
            set
            {
                _sName = value;
                NotifyOfPropertyChange(() => SelectedName);
            }

        }

        public double SelectedPrice
        {
            get
            {
                return _sPrice;
            }
            set
            {
                _sPrice = value;
                NotifyOfPropertyChange(() => SelectedPrice);
            }

        }

        public String SelectedSize
        {
            get
            {
                return _sSize;
            }
            set
            {
                _sSize = value;
                NotifyOfPropertyChange(() => SelectedSize);
            }

        }

        public String SelectedDir
        {
            get
            {
                return _sDir;
            }
            set
            {
                _sDir = value;
                NotifyOfPropertyChange(() => SelectedDir);
            }

        }

        public DateTime SelectedDateTime
        {
            get
            {
                return _sDate;
            }
            set
            {
                _sDate = value;
                NotifyOfPropertyChange(() => SelectedDateTime);
            }

        }

        public String SelectedReciever
        {
            get
            {
                return _sReciever;
            }
            set
            {
                _sReciever = value;
                NotifyOfPropertyChange(() => SelectedReciever);
            }

        }

        public int SelectedTodaySells
        {
            get
            {
                return _sTS;
            }
            set
            {
                _sTS = value;
                NotifyOfPropertyChange(() => SelectedTodaySells);
            }

        }

        public int SelectedTotalSells
        {
            get
            {
                return _sTTS;
            }
            set
            {
                _sTTS = value;
                NotifyOfPropertyChange(() => SelectedTotalSells);
            }

        }

        private List<ChartModel> _teList = SaleModel.calculateTodaysEarnings();

        public List<ChartModel> TodaysEarnings
        {
            get { return _teList; }
            set
            {
                _teList = value;
                NotifyOfPropertyChange(() => TodaysEarnings);
            }
        }

        private string _nte = "";

        public string NumberTodaysEarnings{
            get
            {
                return _nte;
            }
            set
            {
                _nte = value;
                NotifyOfPropertyChange(() => NumberTodaysEarnings);
            }
        }

       
       
        

        public SaleViewModel()
        {
            SetNTE();
        }


        public void Sell(string drive, string file, bool ff)
        {
            //Console.WriteLine($"File  {file}");
            
            if (IsFolder(drive + file))
            {
                for (int i = SaleModel.roots.Count - 1; i>=0; i--)
                {
                    if (SaleModel.roots[i].Name.Equals(file))
                    {
                        for (int j = SaleModel.albums.Count - 1; j >= 0; j--)
                        {
                            if (SaleModel.albums[j].Name.Equals(file))
                            {
                                Console.WriteLine($"Album found :   {SaleModel.albums[j].Name}");
                                SaleModel.albums[j].Reciever = drive.Replace('\\', '/').Substring(0, drive.Length - 1);
                                //Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());

                                //sellscomp.Add(s);
                                FileOnProgressIsVisible = false;
                                FileOnProgressIsVisible = true;


                                OnProgress = SaleModel.albums[j].Name + " ; " + SaleModel.albums[j].size;

                                sellsTemp.Insert(0, SaleModel.albums[j]);

                                Console.WriteLine($"Inserted {SaleModel.albums[j].Name} : {sellsTemp[0].TotalSize} : {drive} : {sellsTemp[0].Reciever}");

                                if (new DriveInfo(drive).DriveFormat.Equals("FAT32")){
                                    try
                                    {
                                        BackgroundWorker worker = new BackgroundWorker();
                                        worker.RunWorkerCompleted += WatchChanged_Completed;
                                        worker.WorkerReportsProgress = false;
                                        worker.DoWork += MusicAlbumFat32(j);
                                        worker.RunWorkerAsync();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"What the f {ex.Message}");
                                    }
                                }


                                else {
                                    FileSystemWatcher watcher = new FileSystemWatcher()
                                    {
                                        Path = drive,
                                        Filter = file
                                    };

                                    new Thread(() =>
                                    {

                                        Thread.BeginCriticalRegion();
                                        Console.WriteLine($"Watcher Enabled for {drive} {file}");
                                        watcher.Changed += new FileSystemEventHandler(WatchChanged);
                                        Thread.EndCriticalRegion();
                                        checkFile(SaleModel.albums[j], drive + file);
                                        watcher.EnableRaisingEvents = true;
                                    }).Start();
                                    break;
                                }
                            }
                        }
                        
                        
                    }
                }
            }
            else
            {
                
                if (IsVideo(drive + file))
                {
                    for (int i = SaleModel.videos.Count - 1; i >= 0; i--)
                    {
                        if (SaleModel.videos[i].Name.Equals(file))
                        {
                        //    SaleModel s = new SaleModel();
                        //    s.Name = file;
                        //    s.Dir = drive;
                        //    s.Status = 0;

                            SaleModel.videos[i].Reciever = drive.Replace('\\','/').Substring(0,drive.Length-1);
                            //Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());

                            //sellscomp.Add(s);
                            FileOnProgressIsVisible = false;
                            FileOnProgressIsVisible = true;
                            string sizz;
                            if (SaleModel.videos[i].TotalSize.ToString().Length > 9)
                            {
                                sizz = (SaleModel.videos[i].TotalSize / (1024 * 1024 * 1024)).ToString();
                                sizz += " GB";
                            }
                            else
                            {
                                sizz = (SaleModel.videos[i].TotalSize / (1024 * 1024)).ToString();
                                sizz += " MB";
                            }
                            
                            OnProgress = SaleModel.videos[i].Name + " ; " + sizz;
                            
                            sellsTemp.Insert(0,SaleModel.videos[i]);

                            Console.WriteLine($"Inserted {SaleModel.videos[i].Name} : {sellsTemp[0].TotalSize} : {drive} : {sellsTemp[0].Reciever}");
                            FileSystemWatcher watcher = new FileSystemWatcher()
                            {
                                Path = drive,
                                Filter = file
                            };
                            
                            new Thread(() =>
                            {
                                
                                Thread.BeginCriticalRegion();
                                Console.WriteLine($"Watcher Enabled for {drive} {file}");
                                watcher.Changed += new FileSystemEventHandler(WatchChanged);
                                Thread.EndCriticalRegion();
                            
                            watcher.EnableRaisingEvents = true;
                            }).Start();

                            //Console.WriteLine($"Video  {SaleModel.videos[i].Name} ");
                            //break;
                        }
                    }
                }
            }
        }

        

        private DoWorkEventHandler MusicAlbumFat32(int j)
        {
            SaleModel.albums[j].Reciever = sellsTemp[0].Reciever;
            int sl = 0;
            while (true)
            {
                sl++;
                if (checkFile(SaleModel.albums[j], SaleModel.albums[j].Reciever + SaleModel.albums[j].Name) == 1)
                {
                    String[] fullShit = SaleModel.albums[j].Dir.Split('\\');

                    string Dire = "";
                    for (int m = 0; m < fullShit.Length - m; m++)
                    {
                        Dire += fullShit[m] + "/";

                    }

                    SaleModel.albums[j].Dir = Dire;
                    if (SaleModel.albums[j].AddAsASale())
                    {
                        Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
                        TodaysEarnings = SaleModel.calculateTodaysEarnings();
                        SetNTE();
                        Console.WriteLine($"Successful { SaleModel.albums[j].Name} ");
                        FileOnProgressIsVisible = false;


                    }
                    else
                    {
                        Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
                        FileOnProgressIsVisible = false;
                        TodaysEarnings = SaleModel.calculateTodaysEarnings();
                        SetNTE();
                        Console.WriteLine($"Failed { SaleModel.albums[j].Name} ");

                    }

                    break;
                }
                Console.WriteLine($"Checking ---- {sl}");
                Thread.Sleep(2000);
            }
            throw new NotImplementedException();
        }


        private static bool IsVideo(String dir)
        {
            String[] fullname = dir.Split('.');
            String extension = fullname[fullname.Length - 1].ToUpper();
            if (SaleModel.videoTypes.Contains(extension))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string SetNTE()
        {
            double x = 0;
            foreach(ChartModel cm in TodaysEarnings)
            {
                x += cm.Percent;
            }

            NumberTodaysEarnings = x.ToString() + "  BIRR";
            return x.ToString() + "  BIRR";
        }

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


        private void WatchChanged(object source, FileSystemEventArgs e)
        {
               
            FileOnProgressIsVisible = true;
            Console.WriteLine($"WATCH ----- File:   {e.FullPath}   {e.ChangeType} ");

            try
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += WatchChanged_Completed;
                worker.WorkerReportsProgress = false;
                worker.DoWork += WatchChanged_DoWork(e);
                worker.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"What the f {ex.Message}");
            }

            //Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
        }

        private DoWorkEventHandler WatchChanged_DoWork(FileSystemEventArgs f)
        {
            
            String[] fullShit = f.FullPath.Split('\\');

            string Directory = "";
            for (int i = 0; i < fullShit.Length - 1; i++)
            {
                Directory += fullShit[i] + "/";

            }
            int k = 0;

            foreach (SaleModel sth in sellsTemp)
            {
                if (sth.Name.Equals(f.Name))
                {
                    sth.Reciever = Directory.Substring(0, Directory.Length - 1);

                    Console.WriteLine($"sth {sth.Name} {sth.Dir} {sth.TotalSize} {sth.Reciever} K: {k}");

                    k = sellsTemp.IndexOf(sth);
                    break;
                }
            }
            //Sells.Add

            Console.WriteLine($"Sells.Name {sellsTemp[k].Name} and e.Name:{f.Name},  Rec:{sellsTemp[k].Reciever} {sellsTemp[k].size} Sells.Status:{sellsTemp[k].Status}  K: {k}");
            SaleModel kk = sellsTemp[k];
            if (kk.Status == 0) // == 0
                                          //Console.WriteLine($"SELLS {Sells.Count}, SELLSCOMP {sellscomp.Count}");

            {
                if (kk.Name.Equals(f.Name))
                {
                    // && Sells[0].Reciever.Equals(Directory) 
                    //Console.WriteLine($"Index {i},  Length {Sells.Count}  {sellscomp.Count}");
                    //Console.WriteLine($"SELLS {Sells.Count}, SELLSCOMP {sellscomp.Count}");
                    //int j = i;
                    //if(sellscomp.Count == i)
                    //{
                    //    j--;
                    //}

                    kk.Status = checkFile(kk, f.FullPath);
                    string[] finalDirAdj = sellsTemp[0].Dir.Split('\\');
                    string dirAdj = "";
                    foreach (string path in finalDirAdj)
                    {
                        dirAdj += path + "\\\\";
                    }
                    kk.Dir = dirAdj.Substring(0, dirAdj.Length - 2);
                    Console.WriteLine($"About to check status of {kk.Name}, {kk.Status}  K: {k}");

                    if (kk.Status == 1)
                    {
                        //if (

                        //{



                        //new Thread(() =>
                        //{
                        //    Console.WriteLine($"Adddddd ");
                        //    Thread.BeginCriticalRegion();
                        //    Console.WriteLine($"BC ");
                        //    sellsTemp[k].AddAsASale();
                        //    Console.WriteLine($"EC");
                        //    Thread.EndCriticalRegion();
                        //}).Start();
                        int j = 0;
                        for (int i = SaleModel.videos.Count - 1; i >= 0; i--)
                        {
                            if (SaleModel.videos[i].Name.Equals(kk.Name))
                            {
                                kk.Dir = SaleModel.videos[i].Dir;

                                SaleModel.videos[i].Status = 1;
                                i = j;
                            }
                            break;
                        }
                        if (kk.AddAsASale())
                        {
                            Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
                            TodaysEarnings = SaleModel.calculateTodaysEarnings();
                            SetNTE();
                            Console.WriteLine($"Successful {sellsTemp[k].Name}  K: {k}");
                            FileOnProgressIsVisible = false;


                        }
                        else
                        {
                            Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
                            FileOnProgressIsVisible = false;
                            TodaysEarnings = SaleModel.calculateTodaysEarnings();
                            SetNTE();
                            Console.WriteLine($"Failed {sellsTemp[k].Name}  K: {k}");
                            MessageBox.Show($"Couldn't register sell of {sellsTemp[k].Name}");
                        }//
                    }


                }

            }
            throw new NotImplementedException();
        }

        private void WatchChanged_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        



        private int checkFile(SaleModel source, String target)
        {
            FileOnProgressIsVisible = true;
            Console.WriteLine($"Source {source.Name} Size = {source.TotalSize}");

            if (Directory.Exists(target) || File.Exists(target))
            {
                Console.WriteLine("Target Exists");
                //Console.WriteLine($"Target {new FileInfo(target).Name} Size = {new FileInfo(target).Length}, Source {source.Name} Size = {source.TotalSize}");

                if (IsFolder(target))
                {
                    if (SaleModel.CalculateFolderSize(target) < SaleModel.CalculateFolderSize(source.Dir + source.Name))
                    {
                        source.Status = 0;
                    }
                    else if (SaleModel.CalculateFolderSize(target) == SaleModel.CalculateFolderSize(source.Dir + source.Name))
                    {
                        source.Status = 1;
                    }
                    else
                    {
                        source.Status = -1;
                    }
                }
                else
                {

                    if (new FileInfo(target).Length < source.TotalSize)
                    {

                        source.Status = 0;
                    }
                    else if (new FileInfo(target).Length == source.TotalSize)
                    {
                        source.Status = 1;
                    }
                    else
                    {
                        source.Status = -1;
                    }
                }
            
            
            }
            else
            {
                Console.WriteLine($"Target :  {target} is not found");
            }

            //Console.WriteLine($" {source.Status} Compare Sizes Source {source.TotalSize}   Target {new FileInfo(target.Dir + target.Name).Length}");
            return source.Status;
        }

        public void AllSells()
        {
            Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
            
        }

        public void RecordBooks()
        {
            Sells = new BindableCollection<SaleModel>(SaleModel.getForeverPresentableSales());
            
        }

        public void SimpleSells()
        {
            
            Console.WriteLine($"Got the Click");
            Sells = new BindableCollection<SaleModel>(SaleModel.getTodaysPresentableSales());
            SetNTE();
        }

        public void CloseSelected()
        {
            BlueIsVisible = false;
            MainGridIsVisible = true;
            GreenIsVisible = false;
        }

        public void FCloseSelected()
        {
            BlueIsVisible = false;
            MainGridIsVisible = true;
            GreenIsVisible = false;
        }

        public void BackSelected()
        {
            if (SelectedSells.Count == 0)
            {
                GreenIsVisible = false;
                BlueIsVisible = false;
                MainGridIsVisible = true;
            }
            else
            {
                SelectedSell = SelectedSells.Last();
                SelectedInitials = SelectedSell.initial;
                SelectedReciever = SelectedSell.Reciever;
                SelectedDateTime = SelectedSell.DateTime;
                SelectedName = SelectedSell.Name;
                SelectedTodaySells = SaleModel.getTodaySalesByName(SelectedName);
                SelectedTotalSells = SaleModel.getTotalSalesByName(SelectedName);
                SelectedPrice = SelectedSell.TotalPrice;
                SelectedSize = SelectedSell.size;
                if (SelectedSell.initial.Equals("F"))
                {
                    SelectedDir = SelectedSell.Dir.Substring(0, SelectedSell.Dir.Length - 1);
                    FolderFiles = new BindableCollection<SaleModel>(SaleModel.getItems(SelectedSell));
                }
                SelectedSells.Clear();
                GreenIsVisible = true;
                BlueIsVisible = false;
                MainGridIsVisible = false;
            }
        }

        public void SellDClick()
        {
            if(SelectedSell.Name != null)
            {
                if (SelectedSell.initial == "F")
                {
                    SaleModel s = new SaleModel();
                    s = SelectedSell;
                    SelectedSells.Clear();
                    SaleViewModel.SelectedSells.Add(s);
                }
            }
            MainGridIsVisible = false;
            SelectedInitials = SelectedSell.initial;
            SelectedReciever = SelectedSell.Reciever;
            SelectedDateTime = SelectedSell.DateTime;
            SelectedName = SelectedSell.Name;
            SelectedTodaySells = SaleModel.getTodaySalesByName(SelectedName);
            SelectedTotalSells = SaleModel.getTotalSalesByName(SelectedName);
            SelectedPrice = SelectedSell.TotalPrice;
            SelectedSize = SelectedSell.size;
            if (SelectedSell.initial.Equals("F"))
            {
                GreenIsVisible = true;
                BlueIsVisible = false;
                SelectedDir = SelectedSell.Dir.Substring(0, SelectedSell.Dir.Length-1);
                FolderFiles = new BindableCollection<SaleModel>(SaleModel.getItems(SelectedSell));
            }
            else
            {
                GreenIsVisible = false;
                BlueIsVisible = true;
                SelectedDir = SelectedSell.Dir;
                
            }
            
            //MessageBox.Show($"{SelectedSell.DateTime}");

        }
    }
}
