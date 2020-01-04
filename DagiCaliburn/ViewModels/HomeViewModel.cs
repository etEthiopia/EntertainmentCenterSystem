using Caliburn.Micro;
using DagiCaliburn.Models;
using DagiCaliburn.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace DagiCaliburn.ViewModels
{
    class HomeViewModel : Conductor<object>
    {

        

        private double _screenHeight;
        private double _screenWidth;
        public static int news = 0;
        public static List<String> prevClipbrds = new List<string>();
        SharpClipboard clipboard = new SharpClipboard();
        IWindowManager manager = new WindowManager();
        public static bool INGB = false;
        public static SaleViewModel  svm = new SaleViewModel();
        public static MusicViewModel mv = new MusicViewModel();
        public static HomeViewModel hvm;
        //System.Windows.SystemParameters.

        public HomeViewModel() {
            ScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            ScreenWidth = (System.Windows.SystemParameters.PrimaryScreenWidth) - 600;
            clipboard.ClipboardChanged += ClipboardChanged;
            hvm = this;
            try
            {
                FileSystemWatcher watcher = new FileSystemWatcher()
                {
                    Path = "E:\\",
                    Filter = null
                };
                watcher.Created += new FileSystemEventHandler(WatchCreated);
                watcher.EnableRaisingEvents = true;

                FileSystemWatcher watcher2 = new FileSystemWatcher()
                {
                    Path = "D:\\",
                    Filter = null
                };
                watcher2.Created += new FileSystemEventHandler(WatchCreated);
                watcher2.EnableRaisingEvents = true;

            }
            catch (Exception e)
            {
                Console.WriteLine("No Drives");
            }

            ActivateItem(svm);
           
            
            
        }

        
            
        public void WatchCreated(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine($"WATCCCCCCCCHHHHHHHHHH ----- File:   {e.FullPath}   {e.ChangeType}");
            //
            if (!INGB)
            {
                String[] fullShit = e.FullPath.Split('\\');

                string Directory = "";
                for (int i = 0; i < fullShit.Length - 1; i++)
                {
                    Directory += fullShit[i] + "\\";

                }
                if (IsFolder(e.FullPath))
                {
                    FileSystemWatcher fwatcher = new FileSystemWatcher()
                    {
                        Path = e.FullPath + "\\",
                        Filter = null
                    };
                    fwatcher.Created += new FileSystemEventHandler(WatchCreated);
                    fwatcher.EnableRaisingEvents = true;
                    Console.WriteLine($"True Sell - Full Path :  {e.FullPath + '\\'},  Name :  {e.Name}");
                    svm.Sell(Directory, e.Name, true);
                }
                else
                {

                    if (IsVideo(e.FullPath))
                    {
                        Console.WriteLine($"False Sell - Full Path :  {Directory},  Name :  {e.Name}");
                        svm.Sell(Directory, e.Name, false);
                    }
                    if (IsAudio(e.FullPath))
                    {
                        Console.WriteLine($"False Sell - Full Path :  {Directory},  Name :  {e.Name}");
                        svm.Sell(Directory, e.Name, false);
                    }
                   

                }

            }
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

        public double ScreenHeight {

            get {
                return _screenHeight;
            }

            set {
                _screenHeight = value;
            }
        }

        public double ScreenWidth
        {

            get
            {
                return _screenWidth;
            }

            set
            {
                _screenWidth = value;
            }
        }

        private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {

            //GetListOfSelected();
            // Is the content copied of file type?
            if (!INGB)
            {
                if (e.ContentType == SharpClipboard.ContentTypes.Files)
                {

                    String cps = e.Content.ToString();

                    int kk = clipboard.ClipboardFiles.ToArray().Length - news;


                    //Console.WriteLine($" Clipboard Size = {clipboard.ClipboardFiles.ToArray().Length}, News ={news}, K ={kk}");


                    //Console.WriteLine($"Folder : {clipboard.ClipboardFiles.ToArray().Last()}  size is {SaleModel.CalculateFolderSize(clipboard.ClipboardFiles.ToArray().Last())}");
                    SaleModel smodel = new SaleModel();
                    //smodel.prepareSale(clipboard.ClipboardFiles.ToArray().Last());

                    for (int i = clipboard.ClipboardFiles.ToArray().Length - 1; i >= news; i--)
                    {
                        // Console.WriteLine($"MUSSSSSSSSSSSSSSSSTTTTTTTTTTTTT     {clipboard.ClipboardFiles.ToArray().Length} {i}");
                        if (i < 0)
                        {
                            news = 0;
                            break;
                        }
                        Console.WriteLine($"PrepareSale i = {i}, clipboard item = {clipboard.ClipboardFiles.ToArray()[i]}");
                        smodel.prepareSale(clipboard.ClipboardFiles.ToArray()[i]);
                    }

                    news = clipboard.ClipboardFiles.ToArray().Length;



                    //manager.ShowWindow(new CopyViewModel(toSend.ToArray()), null, null);
                    //Debug.WriteLine(clipboard.ClipboardFile);
                }

                // If the cut/copied content is complex, use 'Other'.
                else if (e.ContentType == SharpClipboard.ContentTypes.Other)
                {
                    // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
                }
            }
        }

        public void MusicShit()
        {
            ActivateItem(mv);
        }

        public void HomeShit()
        {
            ActivateItem(svm);
        }

        public void ConfigureSettings()
        {
            ActivateItem(new SettingsViewModel());
            
            //manager.ShowWindow(new SettingsViewModel());
        }

        private static bool IsAudio(string dir)
        {
            String[] fullname = dir.Split('.');
            String extension = fullname[fullname.Length - 1].ToUpper();
            if (SaleModel.audioTypes.Contains(extension))
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
