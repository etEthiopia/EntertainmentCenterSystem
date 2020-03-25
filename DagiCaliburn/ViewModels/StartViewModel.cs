using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;

namespace DagiCaliburn.ViewModels
{
    class StartViewModel : Conductor<object>
    {
        public static MainViewModel mainview;
        public static PropmtViewModel promptView;
        public static StartViewModel startview;
        public static SettingsViewModel settingsview;
        public static GBViewModel gbview;
        public static SellViewModel sellview;
        public static StatsViewModel statsView;
        public static SpecialsViewModel specialview;
        public static bool INGB = false;
        public static int news = 0;
        public static bool okSign = false;
        public static Dictionary<string, FileSystemWatcher> watchers;
        SharpClipboard clipboard = new SharpClipboard();
        Watcher watcher;

        public StartViewModel()
        {
            startview = this;
            mainview =  new MainViewModel();
            statsView = new StatsViewModel();
            promptView = new PropmtViewModel();
            gbview = new GBViewModel();
            sellview = new SellViewModel();
            settingsview = new SettingsViewModel();
            specialview = new SpecialsViewModel();
            try
            {
                clipboard.ClipboardChanged += ClipboardChanged;
            }
            catch(Exception svme)
            {
                Console.WriteLine($"STARTVIEWMODEL CLIPBOARD CHANGED: {svme.Message}");
            }
            watchers = new Dictionary<string, FileSystemWatcher>();
            setupListeners();
            watcher = new Watcher();
            ProfileModel p = new ProfileModel();
            p = p.GetProfile();
            if (p.EntName != "" && p.Password != "")
            {
                promptView.PName = p.EntName;
                promptView.Pwd = p.Password;
                ActivateItem(promptView);
                
            }
            else
            {
                ActivateItem(mainview);
            }
            
        }


        

        public void HomeBtn()
        {
            if (okSign)
            {
                ActivateItem(mainview);
            }
        }

        public void SettingsBtn()
        {
            if (okSign)
            {
                ActivateItem(settingsview);
            }
        }

        public void SpecialBtn()
        {
            if (okSign)
            {
                ActivateItem(specialview);
            }
        }

        public void ChartsBtn()
        {
            if (okSign)
            {
                ActivateItem(statsView);
            }
        }

        private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {
            try {
                if (!INGB)
                {
                    Console.WriteLine($"GB CLIPBOARD CHANGED {e.Content.ToString()}, {INGB}");
                    if (e.ContentType == SharpClipboard.ContentTypes.Files)
                    {
                       
                        String cps = e.Content.ToString();

                        int kk = clipboard.ClipboardFiles.ToArray().Length - news;
                        SellModel smodel = new SellModel();
                        // Console.WriteLine($"NEws before : {news}");
                        for (int i = clipboard.ClipboardFiles.ToArray().Length - 1; i >= news; i--)
                        {
                            if (i < 0)
                            {
                                news = 0;
                                break;
                            }
                            Console.WriteLine($"PrepareSale i = {i} {news}, clipboard item = {clipboard.ClipboardFiles.ToArray()[i]}");
                            smodel.PrepareSale(clipboard.ClipboardFiles.ToArray()[i]);
                        }

                        news = clipboard.ClipboardFiles.ToArray().Length;
                        //Console.WriteLine($"NEws after : {news}");

                    }

                    else if (e.ContentType == SharpClipboard.ContentTypes.Other)
                    {

                    }
                }
            }
            catch(Exception k)
            {
                Console.WriteLine($"Clipboard ERROR {k.Message}");
            }
        }

        public void setupListeners()
        {
            try
            {
                foreach(string port in watchers.Keys)
                {
                    Console.WriteLine($"PORT : {port}");
                }
                foreach(Disk d in mainview.Drives)
                {
                    Console.WriteLine($"SetupListener watchcreated about to in {d.Port}");
                    
                        watchers.Add(d.Port, new FileSystemWatcher()
                        {
                            Path = d.Port + "\\",
                            Filter = null,
                            EnableRaisingEvents = true
                        }
                        );

                        watchers[d.Port].Created += WatchCreated;
                        Console.WriteLine($"SetupListener watchcreated in {d.Port}");
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"No Drives {e.Message}");
            }
        }

        public void WatchCreated(object source, FileSystemEventArgs e)
        {
            if (!INGB)
            {
                if (!GBViewModel.GBSells.Contains(e.Name))
                {

                    Console.WriteLine($"About to create watcher for {e.FullPath}");
                    //Thread th = new Thread(
                    //  () => 
                    watcher.CreateWatch(e.FullPath, e.Name);
                    //th.Start();

                    Console.WriteLine($"Succesfully Watcher for {e.FullPath} Created");
                }
                else
                {
                    MessageBox.Show("Contains");
                }
                }
        }

        public void WatchChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"About to change watcher for {e.FullPath}");

            Thread th = new Thread(
                    () => watcher.ChangeWatch(e.FullPath, e.Name));
            th.Start();
            
            
            Console.WriteLine($"Succesfully Watcher for {e.FullPath} Changed");
        }

    }
}
