using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class GBViewModel : Screen
    {
        //private BindableCollection<Disk> _drives = new BindableCollection<Disk>();
        private TypeModel _currentAudio;
        private BindableCollection<SellModel> _gBMusicSells = new BindableCollection<SellModel>();
        private string _totalSum = "0 MB";
        private string _gBmoney = "0 BIRR";
        public static List<string> GBSells = new List<string>();
        private BindableCollection<TypeModel> _audioTypes 
        = new BindableCollection<TypeModel>(TypeModel.GetTypeFromFileType("Audio"));
        

        private long totalSize = 0;
        private float totalPrice = 0.0f;

        public BindableCollection<Disk> Drives
        {
            get
            {
                return StartViewModel.mainview.Drives;
            }
            set
            {
                StartViewModel.mainview.Drives = value;
                NotifyOfPropertyChange(() => Drives);
            }
        }
        public BindableCollection<TypeModel> AudioTypes
        {
            get
            {
                return _audioTypes;
            }
            set
            {
                _audioTypes = value;
                NotifyOfPropertyChange(() => AudioTypes);
            }
        }
        public string TotalSum
        {
            get
            {
                return _totalSum;
            }
            set
            {
                _totalSum = value;
                NotifyOfPropertyChange(() => TotalSum);
            }
        }
        public string GBmoney
        {
            get
            {
                return _gBmoney;
            }
            set
            {
                _gBmoney = value;
                NotifyOfPropertyChange(() => GBmoney);
            }
        }
        public TypeModel CurrentAudio {
            get
            {
                return _currentAudio;
            }
            set
            {
                _currentAudio = value;
                NotifyOfPropertyChange(() => CurrentAudio);
                CalculateMoney();
                
            }
        }
        public BindableCollection<SellModel> GBMusicSells
        {
            get
            {
                return _gBMusicSells;
            }
            set
            {
                _gBMusicSells = value;
                CalculateTotalSize();
                CalculateMoney();
                NotifyOfPropertyChange(() => TotalSum);
                NotifyOfPropertyChange(() => GBmoney);
                NotifyOfPropertyChange(() => GBMusicSells);
            }
        }
        
        public GBViewModel()
        {
            CurrentAudio = AudioTypes[0];
            Drives = StartViewModel.mainview.Drives;
        }

        public void GBClick(string port)
        {
            StringCollection addToCB = new StringCollection();
            foreach(SellModel sml in GBMusicSells)
            {
                string fullpath = "";
                fullpath += sml.Dir;
                fullpath += sml.Name;
                addToCB.Add(fullpath);
                Console.WriteLine($"Fullpath: {fullpath}");
                GBSells.Add(sml.Name);
            }
            StartViewModel.mainview.ClearGBIsVisible = true;
            StartViewModel.mainview.ClearGBName = "Clear GB Counter";
            try
            {
                string[] money = GBmoney.Split(' ');
                int tp = 0;
                if (int.TryParse(money[0], out tp))
                {
                    if (tp != 0)
                    {
                        totalPrice = tp;
                    }
                    else
                    {
                        TotalSum = $"Enter again";
                    }
                }
                else
                {
                    TotalSum = $"Enter again";
                }
            }
            catch (Exception convertEd)
            {
                TotalSum = $"Enter again";
                Console.WriteLine($"Convert GBMoney {convertEd}");
            }
            try
            {
                StartViewModel.INGB = true;
                Clipboard.SetFileDropList(addToCB);
                
                AudioModel amsize = new AudioModel();
                amsize.Name = TotalSum + " of " + CurrentAudio.Name;
                amsize.Price = totalPrice;
                //amsize.Dir = "UNKNOWN";
                amsize.Dir = $"{GBMusicSells[0].Dir} AND OTHERS...";
                amsize.Type = CurrentAudio;
                amsize.Size = TotalSum;
                
                amsize.RecieverPort = port + "/";
                Console.WriteLine($"PORT {amsize.RecieverPort}");
                foreach (Disk d in StartViewModel.mainview.Drives)
                {
                    if (d.Port.Equals(amsize.RecieverPort.Substring(0, 2)))
                    {
                        amsize.RecieverSerial = d.serial;
                        break;
                    }
                }
                amsize.AddAlbumAsSale();
                
                StartViewModel.INGB = false;
                Process.Start(@"" + port + "\\");
                Drives = StartViewModel.mainview.Drives;

            }
            catch (Exception e)
            {
                StartViewModel.INGB = false;
                Console.WriteLine($"clipboard changed {e.Message}");
                MessageBox.Show($"error please try again !");
                StartViewModel.INGB = false;
            }
            ////clipboard.setdata(dataformats.filedrop, (object))
            StartViewModel.INGB = false;
        }

        public void CloseGBSelected()
        {
            GBMusicSells = new BindableCollection<SellModel>();
            totalPrice = 0.0f;
            totalSize = 0;
            TotalSum = "0 MB";
            GBmoney = "0 BIRR";
            StartViewModel.startview.ActivateItem(StartViewModel.mainview);
        }

        public void CalculateTotalSize()
        {
            totalSize = 0;
            foreach(SellModel gbs in GBMusicSells)
            {
                totalSize += gbs.TotalSize;
            }
            TotalSum = Utils.calculateSize(totalSize);
            Console.WriteLine($"SIZE {TotalSum}, {totalSize}");
        }

        public void CalculateMoney()
        {
            int k = 0;
            totalPrice = 0;
            if(totalSize < 1073741824)
            {
                k = 1;
            }
            else
            {
                k = (int) Math.Ceiling((double)(totalSize / 1073741824));
            }
            totalPrice = k * TypeModel.GetGBPrice(CurrentAudio.Id);
            GBmoney = totalPrice + " BIRR";
            Console.WriteLine($"MONEY {GBmoney}, {k}");
        }
    }
}
