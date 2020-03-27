using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class MusicViewModel : Screen
    {


        private static BindableCollection<SaleModel> _gbsells = new BindableCollection<SaleModel>();
        private BindableCollection<TypeModel> _ats = new BindableCollection<TypeModel>();
        private static BindableCollection<Drive> _ds = new BindableCollection<Drive>();
        Drive dd = new Drive();
        public static List<String> items = new List<string>();
        List<string> file_list = new List<string>();
        private double _nmoney = 0.0;
        private static string _totalSum = "0 MB";
        private string _currentAudio = "";
        private long sum = 0;
        private double pricePerGB = 0;
        private double price = 0;
        private static string _totalPrice = "0 BIRR";
        private int _ai = 0;
        private bool _registerIsVisible = false;
        private bool _drivesIsVisible = true;


        public bool RegisterIsVisible {
            get
            {
                return _registerIsVisible;
            }
            set
            {
                _registerIsVisible = value;
                NotifyOfPropertyChange(() => RegisterIsVisible);
            }
        }
        public bool DrivesIsVisible
        {
            get
            {
                return _drivesIsVisible;
            }
            set
            {
                _drivesIsVisible = value;
                NotifyOfPropertyChange(() => DrivesIsVisible);
            }
        }


        public BindableCollection<Drive> DrivesToCopyTo { get { return _ds; } set { _ds = value; NotifyOfPropertyChange(() => DrivesToCopyTo); } }

        public String TotalSum
        {
            get
            {
                return _totalSum;
            }
            set
            {
                _totalSum = calculateSize(sum);
                Console.WriteLine($"Total Sum {_totalSum}");
                NotifyOfPropertyChange(() => TotalSum);
            }
        }

        

        public double newmoney
        {
            get
            {
                return _nmoney;
            }
            set
            {
                _nmoney = value;
                NotifyOfPropertyChange(() => newmoney);
            }
        }

        public String TSum
        {
            get
            {
                return _totalSum;
            }
            set
            {
                _totalSum = calculateSize(sum);
                NotifyOfPropertyChange(() => TSum);
            }
        }

        public String TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = calculatePrice();
                Console.WriteLine($"Total SumP {_totalPrice}");
                NotifyOfPropertyChange(() => TotalPrice);
            }
        }

        public string CurrentAudio
        {
            get
            {
                return _currentAudio;
            }
            set
            {
                _currentAudio = value;
                // _currentAudio = AudioTypes[AudioIndex].Name; 
                NotifyOfPropertyChange(() => CurrentAudio);
            }
        }

        public int AudioIndex
        {
            get
            {
                return _ai;
            }
            set
            {
                _ai = value;
                NotifyOfPropertyChange(() => AudioIndex);
            }
        }

        public BindableCollection<TypeModel> AudioTypes
        {
            get
            {
                return _ats;
            }
            set
            {
                _ats = value;
                NotifyOfPropertyChange(() => AudioTypes);
                NotifyOfPropertyChange(() => CurrentAudio);
                NotifyOfPropertyChange(() => AudioIndex);
                NotifyOfPropertyChange(() => TotalSum);
                pricePerGB = TypeModel.GetGBPrice(AudioTypes[AudioIndex].Id);
                NotifyOfPropertyChange(() => TSum);
            }
        }



        public BindableCollection<SaleModel> GBMusicSells
        {
            get
            {
                return _gbsells;
            }
            set
            {
                _gbsells = value;
                _totalPrice = calculatePrice();
                _totalSum = calculateSize(sum);
                NotifyOfPropertyChange(() => TotalSum);
                NotifyOfPropertyChange(() => GBMusicSells);
            }

        }

        public MusicViewModel()
        {
            DrivesToCopyTo = new BindableCollection<Drive>(Drive.currentDrives());
            GBMusicSells = new BindableCollection<SaleModel>();
            TypeModel tt = new TypeModel();
            AudioTypes = new BindableCollection<TypeModel>(tt.getAudioTypes());
            CurrentAudio = AudioTypes[0].Name;
            pricePerGB = TypeModel.GetGBPrice(AudioTypes[0].Id);
        }

        public static void SetClip()
        {

            //Form1_Load();
        }

        public void Form1_Load()
        {
            file_list = new List<string>();
            foreach (string path in items)
            {
                Console.WriteLine($"fILE {path}");
                file_list.Add(path);
                SaleModel sm = new SaleModel(path);
                String[] fullname = path.Split('\\');
                string dr = "";
                for (int i = 0; i < fullname.Length - 1; i++)
                {
                    dr += fullname[i] + "/";
                }
                sm.Name = fullname.Last();

                dr = dr.Substring(0, dr.Length - 1);
                sm.Dir = dr + "/";
                GBMusicSells.Add(sm);

                sum += sm.TotalSize;
                Console.WriteLine($"SUM {sum}");
                TotalSum = calculateSize(sum);
                TotalPrice = calculatePrice();
            }
            //GBMusicSells = new BindableCollection<SaleModel>();
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.FileDrop, file_list.ToArray());
            //string[] file_names = (string[])Clipboard.GetData(DataFormats.FileDrop);

        }

        private string calculatePrice()
        {
            price = (double)((int)(sum / 1073741824)) * pricePerGB;
            if (price == 0.0)
            {
                price = pricePerGB;
            }

            return price + " BIRR";
        }

        private string calculatePrice(double nm)
        {
            

            return nm + " BIRR";
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
            Console.WriteLine($"TSum {trimmedSize}");

            return trimmedSize;
        }


        public ManagementObject[] getLogicalUsbDisks()
        {
            int i = 0;
            ManagementObject[] lista;
            Console.WriteLine("Fetching Logical Disks");

            try
            {
                ManagementObjectCollection drives = new ManagementObjectSearcher(
                    " SELECT Caption, DeviceID FROM Win32_DiskDrive WHERE InterfaceType='USB'"
                    ).Get();
                Console.WriteLine($"How many drives {drives.Count}");
                lista = new ManagementObject[drives.Count];
                foreach (ManagementObject drive in drives)
                {
                    foreach (ManagementObject partition in new ManagementObjectSearcher(
                        " ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"]
                        + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get()
                        )
                    {
                        foreach (ManagementObject disk in new ManagementObjectSearcher(
                             " ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"]
                        + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get()
                            )
                        {
                            Console.WriteLine($"Disk Caption {disk["CAPTION"].ToString()}");

                            lista[i] = disk;
                            i++;
                        }
                    }
                }
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in lista {e.Message}");

            }
            return null;
        }

        public void HardDClick(string pathh)
        {
            HomeViewModel.INGB = true;
            Console.WriteLine($"Pathh {pathh}");

            Drive d = new Drive();
                foreach(Drive dd in DrivesToCopyTo)
                {
                    if (pathh.Equals(dd.DName))
                    {
                        d = dd;
                        break;
                    }
                }
                if (d.DName != null)
                {
                dd = d;
                }
            DrivesIsVisible = false;
            RegisterIsVisible = true;

        }

        public void CloseGBSelected()
        {
            takeCareofShit();
            HomeViewModel.hvm.ActivateItem(HomeViewModel.svm);
        }

        public void NewBirr()
        {
            TotalPrice = calculatePrice(newmoney);
            price = newmoney;
            
        }

        private void takeCareofShit()
        {
            file_list.Clear();
            GBMusicSells.Clear();
            HomeViewModel.INGB = false;
            DrivesIsVisible = true;
            RegisterIsVisible = false;
            sum = 0;
            TotalPrice = "0 BIRR";
            price = 0;
            TotalSum = "0 MB";
        }

        public void Cancel()
        {
            takeCareofShit();

        }

        public void Register()
        {
            takeCareofShit();
            if (dd.DName != null && GBMusicSells.Count > 0)
            {
                SaleModel s = new SaleModel();
                s.Name = TotalSum + " of " + CurrentAudio;
                s.Dir = "/";
                s.Status = 1;
                s.TotalPrice = price;
                s.TotalSize = sum;
                s.Type = TypeModel.GetTypeToFile(AudioTypes[AudioIndex].Id);
                s.Reciever = dd.DName.Substring(0, dd.DName.Length - 1);
                if (s.AddAsASale())
                {
                    HomeViewModel.svm.TodaysEarnings = SaleModel.calculateTodaysEarnings();
                    HomeViewModel.svm.SetNTE();
                    HomeViewModel.svm.SimpleSells();
                    HomeViewModel.hvm.ActivateItem(HomeViewModel.svm);


                }
                else
                {
                    Console.WriteLine($"GB ERROR");
                }
            }
            

        }


    }
}
