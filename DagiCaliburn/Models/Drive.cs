using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DagiCaliburn.Models
{
    class Drive
    {
        private string _availableSpace;
        private string _dataFormat;
        private string _driveType;
        private string _status;
        private string _name;
        private string _rootDirectory;
        private string _totalSize;

        public string AvailableSpace {
            get {
                return _availableSpace;
            }
        }

        public long TotalFreeSpace;
        

        public string FreeSpace { get; set; }

        public string TotalSpace { get; set; }

        public string Icon { get; set; }

        public string DName { get; set; }

        public string TotalSize
        {
            get
            {
                return _totalSize;
            }
        }

        public string DataFormat {
            get
            {
                return _dataFormat;
            }
                
        }

        public string Type
        {
            get
            {
                return _driveType;
            }

        }

        public string Ready
        {
            get
            {
                return _status;
            }

        }

        public string Name
        {
            get
            {
                return _name;
            }

        }

        public string RootDirectory
        {
            get
            {
                return _rootDirectory;
            }

        }


        public static List<Drive> GetDrives()
        {
            List<Drive> drives = new List<Drive>();
            
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {

                Drive drive = new Drive();
                

                drive._availableSpace = drive.calculateSize(d.AvailableFreeSpace);
                drive._dataFormat = d.DriveFormat;
                drive._driveType = d.DriveType.ToString();
                drive.TotalFreeSpace = d.TotalFreeSpace;
                drive._name = d.Name;
                drive._rootDirectory = d.RootDirectory.ToString();
                if (d.IsReady)
                {
                    drive._status = "Yes";
                }
                else
                {
                    drive._status = "No";
                }
                drive._totalSize = drive.calculateSize(d.TotalSize);
                if (d.IsReady)
                {
                    //Console.WriteLine($"Device {drive.Name}");
                    drives.Add(drive);
                    //Console.WriteLine($"Device {drive.Name} {drives[drives.Count - 1].Name}");
                }
                else
                {
                    continue;
                }
               

            }
            
            return drives;
        }

        public static List<Drive> currentDrives()
        {
            List<Drive> drives = new List<Drive>();

            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                if (d.Name.Equals("C:\\"))
                {
                    continue;
                }

                Drive drive = new Drive();

                drive.DName = d.Name;
                drive.TotalFreeSpace = d.TotalFreeSpace;
                drive.FreeSpace = drive.calculateSize(d.AvailableFreeSpace);
                drive.TotalSpace = drive.calculateSize(d.TotalSize);
                drive._availableSpace = drive.calculateSize(d.AvailableFreeSpace);
                if(d.DriveType == DriveType.Removable)
                {
                    drive.Icon = "ZipDisk";
                }
                else if(d.DriveType == DriveType.Fixed)
                {
                    drive.Icon = "Harddisk";
                }
                else
                {
                    drive.Icon = "AccountQuestionMark";
                }
                drive._dataFormat = d.DriveFormat;
                drive._driveType = d.DriveType.ToString();
                drive._name = d.Name;
                drive._rootDirectory = d.RootDirectory.ToString();
                if (d.IsReady)
                {
                    drive._status = "Yes";
                }
                else
                {
                    drive._status = "No";
                    continue;
                }
                drive._totalSize = drive.calculateSize(d.TotalSize);
                if (d.IsReady)
                {
                    //Console.WriteLine($"Device {drive.Name}");
                    drives.Add(drive);
                    //Console.WriteLine($"Device {drive.Name} {drives[drives.Count - 1].Name}");
                }
                else
                {
                    continue;
                }


            }

            return drives;
        }


        private string calculateSize(long size)
        {
            string trimmedSize = "";
            if(size > 1000000000)
            {
                float ss = (float)Math.Round((double) size / 1073741824, 2);
                
                trimmedSize = ss‬ + " GB";
            }
            else if(size > 1000000)
            {
                float ss = (float)Math.Round((double)size / 1048576, 0);
                trimmedSize = ss‬ + " MB";
            }
            else{
                float ss = (float)Math.Round((double)size / 1024, 0);
                trimmedSize = ss + " KB";
            }
            return trimmedSize;
        }
    }
}
