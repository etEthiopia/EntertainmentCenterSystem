using DagiCaliburn.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.Models
{
    class Disk
    {
        private string _cost = "";
        private string _item = "";
        private string _name = "";
        private string _port = "";
        private string _props = "";
        public string serial = "";
        public bool removed = false;
        public DateTime Time;


        public float Money = 0;
        public int ProdItems = 0;

        public string Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }

        public string Items
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;

            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

            }
        }

        public string Props
        {
            get
            {
                return _props;
            }
            set
            {
                _props = value;
            }
        }

        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }



        static ManagementEventWatcher w = null;

        public static void AddRemoveUSBHandler()
        {
            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceDeletionEvent";
                q.WithinInterval = new TimeSpan(0, 0, 1);
                q.Condition = "TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += USBRemoved;
                w.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (w != null)
                {
                    w.Stop();
                }
            }
        }

        public static void AddInsertUSBHandler()
        {
            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;

            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceCreationEvent";
                q.WithinInterval = new TimeSpan(0, 0, 1);
                q.Condition = "TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += USBInserted;
                w.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (w != null)
                {
                    w.Stop();
                }
            }
        }


        static void USBInserted(object sender, EventArgs e)
        {
            Console.WriteLine($"A USB device is inserted");
            //getDetails();
            List <string> old = new List<string>();
            foreach (Disk dk in StartViewModel.mainview.Drives.ToList())
            {
                Console.WriteLine($"SERIAL {dk.serial}");
                old.Add(dk.serial);
            }
            StartViewModel.mainview.Drives = new Caliburn.Micro.BindableCollection<Disk>(GetDrives());
            if (old.Count < StartViewModel.mainview.Drives.Count)
            {
                
                foreach (Disk d in StartViewModel.mainview.Drives)
                {
                    if (!old.Contains(d.serial))
                    {

                        d.Time = DateTime.Now;
                        Console.WriteLine($"FROM NEW, time: {d.Time}, port: {d.Port}, serial: {d.serial}");

                    }
                }
                
            }
            StartViewModel.startview.setupListeners();
        }

        static void USBRemoved(object sender, EventArgs e)
        {
            Console.WriteLine("A USB device is removed");
            List<Disk> Rdrives = GetDrives();
            //if (StartViewModel.mainview.Drives.Count > 0)
            //{
            //    for (int i = 0; i < StartViewModel.mainview.Drives.Count; i++)
            //    {
            //        if (!StartViewModel.mainview.Drives[i].serial.Equals(Rdrives[i].serial))
            //        {
            //            StartViewModel.mainview.Drives[i].removed = true;
            //            //Rdrives.Insert(i, StartViewModel.mainview.Drives[i]);
            //        }

            //    }
            //}
            StartViewModel.mainview.Drives = new Caliburn.Micro.BindableCollection<Disk>(GetDrives());
            List<string> ports = new List<string>();
            foreach(Disk d in StartViewModel.mainview.Drives)
            {
                ports.Add(d.Port);
            }
            string portt = "";
            foreach(string port in StartViewModel.watchers.Keys)
            {
                if (!ports.Contains(port))
                {
                    portt = port;
                }
            }
            if (StartViewModel.watchers.Keys.Contains(portt))
            {
                StartViewModel.watchers.Remove(portt);
            }
            //StartViewModel.startview.setupListeners();
        }



        public static List<Disk> GetDrives()
        {
            List<Disk> disks = new List<Disk>();
            var driveQuery = new ManagementObjectSearcher("select * from Win32_DiskDrive");
            List<string> serials = new List<string>();
            try
            {
                foreach (Disk dk in StartViewModel.mainview.Drives)
                {
                    serials.Add(dk.serial);
                }
            }
            catch(Exception ee)
            {
                Console.WriteLine($"Extracting plugged serials failed {ee.Message}");
            }
            try
            {
                foreach (ManagementObject d in driveQuery.Get())
                {
                    var deviceId = d.Properties["DeviceId"].Value;
                    var partitionQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_DiskDriveToDiskPartition", d.Path.RelativePath);
                    var partitionQuery = new ManagementObjectSearcher(partitionQueryText);
                    foreach (ManagementObject p in partitionQuery.Get())
                    {
                        var logicalDriveQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_LogicalDiskToPartition", p.Path.RelativePath);
                        var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
                        foreach (ManagementObject ld in logicalDriveQuery.Get())
                        {
                            //Console.WriteLine("Logical drive");
                            //Console.WriteLine(ld);

                            var physicalName = Convert.ToString(d.Properties["Name"].Value); // \\.\PHYSICALDRIVE2
                            var diskName = Convert.ToString(d.Properties["Caption"].Value); // WDC WD5001AALS-xxxxxx
                            var diskModel = Convert.ToString(d.Properties["Model"].Value); // WDC WD5001AALS-xxxxxx
                            var diskInterface = Convert.ToString(d.Properties["InterfaceType"].Value); // IDE
                            var capabilities = (UInt16[])d.Properties["Capabilities"].Value; // 3,4 - random access, supports writing
                            var mediaLoaded = Convert.ToBoolean(d.Properties["MediaLoaded"].Value); // bool
                            var mediaType = Convert.ToString(d.Properties["MediaType"].Value); // Fixed hard disk media
                            var mediaSignature = Convert.ToUInt32(d.Properties["Signature"].Value); // int32
                            var mediaStatus = Convert.ToString(d.Properties["Status"].Value); // OK

                            var driveName = Convert.ToString(ld.Properties["Name"].Value); // C:
                            var driveId = Convert.ToString(ld.Properties["DeviceId"].Value); // C:
                            var driveCompressed = Convert.ToBoolean(ld.Properties["Compressed"].Value);
                            var driveType = Convert.ToUInt32(ld.Properties["DriveType"].Value); // C: - 3
                            var fileSystem = Convert.ToString(ld.Properties["FileSystem"].Value); // NTFS
                            var freeSpace = Convert.ToUInt64(ld.Properties["FreeSpace"].Value); // in bytes
                            var totalSpace = Convert.ToUInt64(ld.Properties["Size"].Value); // in bytes
                            var driveMediaType = Convert.ToUInt32(ld.Properties["MediaType"].Value); // c: 12
                            var volumeName = Convert.ToString(ld.Properties["VolumeName"].Value); // System
                            var volumeSerial = Convert.ToString(ld.Properties["VolumeSerialNumber"].Value); // 12345678 //

                            //Console.WriteLine("PhysicalName: {0}", physicalName);
                            //Console.WriteLine("DiskName: {0}", diskName);
                            //Console.WriteLine("DiskModel: {0}", diskModel);
                            //Console.WriteLine("DiskInterface: {0}", diskInterface);
                            //Console.WriteLine("Capabilities: {0}", capabilities);
                            //Console.WriteLine("MediaLoaded: {0}", mediaLoaded);
                            //Console.WriteLine("MediaType: {0}", mediaType);
                            //Console.WriteLine("MediaSignature: {0}", mediaSignature);
                            //Console.WriteLine("MediaStatus: {0}", mediaStatus);

                            //Console.WriteLine("DriveName: {0}", driveName);
                            //Console.WriteLine("DriveId: {0}", driveId);
                            //Console.WriteLine("DriveCompressed: {0}", driveCompressed);
                            //Console.WriteLine("DriveType: {0}", driveType);
                            //Console.WriteLine("FileSystem: {0}", fileSystem);
                            //Console.WriteLine("FreeSpace: {0}", freeSpace);
                            //Console.WriteLine("TotalSpace: {0}", totalSpace);
                            //Console.WriteLine("DriveMediaType: {0}", driveMediaType);
                            //Console.WriteLine("VolumeName: {0}", volumeName);
                            //Console.WriteLine("VolumeSerial: {0}", volumeSerial);

                            //Console.WriteLine(new string('-', 79));
                            Disk disk = new Disk();
                            if (mediaType.Equals("Fixed hard disk media"))
                            {
                                continue;

                            }
                            else
                            {
                                disk.Name = volumeName;
                                disk.Port = driveId;
                                disk.serial = volumeSerial;
                                if (serials.Contains(disk.serial))
                                {

                                    disk.Time = StartViewModel.mainview.Drives[serials.IndexOf(disk.serial)].Time;
                                    disk.GetCostAndItem();

                                }
                                else
                                {
                                    disk.Time = DateTime.Now;
                                    disk.Cost = "0 BIRR";
                                    disk.Items = "0 ITEMS";

                                }
                                disk.Props = $"{Utils.calculateSize((long)freeSpace)} FREE / {Utils.calculateSize((long)totalSpace)}";
                                disks.Add(disk);

                            }
                        }
                    }
                }
            }
            catch(Exception en)
            {
                Console.WriteLine($"Disk Failure, {en.Message}");
            }
            return disks;
        }

        public void GetCostAndItem()
        {
            float cost = 0;
            int items = 0;
            String today = Time.ToString("yyyy-MM-dd HH:mm:ss");

            MySqlConnection conn = DBUtils.GetDBConnection();
            Console.WriteLine($"Port {Port}, Time {Time}");
            string query = $"SELECT * FROM fdb.videosells WHERE datetime > '{today}' UNION ALL SELECT * FROM fdb.audiosells WHERE datetime > '{today}' UNION ALL SELECT * FROM fdb.othersells WHERE datetime > '{today}' order by datetime DESC";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["recieverserial"].ToString().Equals(serial))
                    {
                        cost += float.Parse(reader["price"].ToString());
                        items++;
                    }
                }
                
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Cost and Items Execption. {e.Message}");
            }
            finally
            {
                Cost = cost + " BIRR";
                Money = cost;
                ProdItems = items;
                Items = items + " Items";
            }
        }

        public List<SellModel> GetPurchased()
        {
            String today = Time.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlConnection conn = DBUtils.GetDBConnection();
            List<SellModel> Purchased = new List<SellModel>();
            string query = $"SELECT name,price,size,datetime FROM fdb.videosells WHERE datetime > '{today}' AND recieverserial = '{serial}' UNION ALL SELECT name,price,size,datetime FROM fdb.audiosells WHERE datetime > '{today}' AND recieverserial = '{serial}' UNION ALL SELECT name,price,size,datetime FROM fdb.othersells WHERE datetime > '{today}' AND recieverserial = '{serial}' order by datetime DESC";
            Console.WriteLine($"QUERY 12345 {query}");

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                       SellModel smol = new SellModel();
                        smol.Name = reader["name"].ToString();
                        smol.Price = float.Parse(reader["price"].ToString());
                        smol.Size = reader["size"].ToString();
                    Purchased.Add(smol);
                    
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Purchased Execption. {e.Message}");
            }
            return Purchased;
        }


    }
}
