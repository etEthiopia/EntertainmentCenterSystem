using DagiCaliburn.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    class Utils
    {
        //Returns Size in String Format from Bytes
        public static string calculateSize(long size)
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

        //Returns a fullpath which "\" is replaced by "/"
        public static string BackToFront(string back)
        {
            string front = "";
            front = back.Replace('\\', '/');
            return front;
        }

        public static String[] videoTypes = { "WEBM", "MP4", "FLV", "MKV", "MOV", "MPG", "MP2", "MPEG", "MPE", "MPV", "OGG", "M4P", "M4V", "AVI", "WMV", "QT", "SWF", "AVCHD" };

        public static String[] audioTypes = { "PCM", "WAV", "AIFF", "MP3", "AAC", "OGG", "WMA", "FLAC", "ALAC" };

        //Returns true if the directory is a video
        public static bool IsVideo(String dir)
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

        //Returns true if the directory is an audio
        public static bool IsAudio(string dir)
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

        //Returns true if the directory is a folder
        public static bool IsFolder(String dir)
        {
            if (Directory.Exists(dir))
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
            else
            {
                return false;
            }
        }

        //Returns File Name from Front Path
        public static string GetName(string fullpath)
        {
            string[] sep = fullpath.Split('/');
            return sep.Last();
        }

        //Returns File Name from Front Path as a Front Dir
        public static string GetDir(string fullpath)
        {
            string dir = "";
            string[] sep = fullpath.Split('/');
            for (int i = 0; i < sep.Length - 1; i++)
            {
                dir += sep[i] + "/";
            }
            return dir;
        }

        //Returns Folder Dir from Front Path as a Front Dir
        public static string GetFolderDir(string fullpath)
        {
            string dir = "";
            string[] sep = fullpath.Split('/');
            for (int i = 0; i < sep.Length - 2; i++)
            {
                dir += sep[i] + "/";
            }
            return dir;
        }
        

        //Returns Drive Port from Front Path as a Front Dir
        public static string GetDrive(string fullpath)
        {
            string dir = "";
            string[] sep = fullpath.Split('/');
            if (sep.Length > 0)
            {
                   dir += sep[0] + "/";
                
            }
            return dir;
        }

        //Returns the Root Directory
        public static string GetRoot(string fullpath)
        {
            string dir = "";
            string[] sep = fullpath.Split('/');
            if (sep.Length > 1)
            {
                dir += sep[1] + "/";

            }
            return dir;
        }

        //Returns the Folder Name
        public static string GetFolderName(string fullpath)
        {
            string dir = "";
            string[] sep = fullpath.Split('/');
            if (sep.Length > 1)
            {
                dir += sep[sep.Length-2];

            }
            return dir;
        }

        //Returns TotalSize in Bytes
        public static long GetTotalSize(string dir)
        {
            long tz = (long)0;
            if (File.Exists(dir))
            {
                tz = new FileInfo(dir).Length;
            }
            return tz;
        }

        //Returns TotalSize of Folder in Bytes
        public static long CalculateFolderSize(String item)
        {
            long size = (long)0.0;
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
                        foreach (string file in Directory.GetFiles(item + "/"))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                size += finfo.Length;
                            }
                        }
                        foreach (string dir in Directory.GetDirectories(item))
                        {
                            size += CalculateFolderSize(dir + "/");
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

            return size;

        }

        //Returns SellModel from VideoModel
        public static SellModel videoTOSellModel(VideoModel v)
        {
            SellModel sm = new SellModel();
            sm.Name = v.Name;
            sm.Price = v.Price;
            sm.Initials = v.Type.Icon;
            sm.Type = v.Type;
            sm.RecieverPort = v.RecieverPort;
            sm.RecieverSerial = v.RecieverSerial;
            sm.Special = v.Special;
            sm.TotalSize = v.TotalSize;
            sm.Size = v.Size;
            sm.DateTime = v.DateTime;
            sm.Dir = v.Dir;
            sm.Folder = v.Folder;
            return sm;
        }

        //Returns FolderSellModel from VideoModel
        public static SellModel videoTOFolderModel(VideoModel v)
        {
            SellModel sm = new SellModel();
            sm.Name = Utils.GetFolderName(v.Dir);
            sm.Initials = "F";
            sm.Price = v.Price;
            sm.Type = v.Type;
            sm.RecieverPort = v.RecieverPort;
            sm.RecieverSerial = v.RecieverSerial;
            sm.Special = v.Special;
            sm.Size = v.Size;
            sm.DateTime = v.DateTime;
            sm.Dir = Utils.GetFolderDir(v.Dir);
            sm.Folder = v.Folder;
            return sm;
        }

        //Returns SellModel from AudioModel
        public static SellModel audioTOSellModel(AudioModel v)
        {
            SellModel sm = new SellModel();
            sm.Name = v.Name;
            sm.Price = v.Price;
            sm.Initials = v.Type.Icon;
            sm.Type = v.Type;
            sm.RecieverPort = v.RecieverPort;
            sm.RecieverSerial = v.RecieverSerial;
            sm.Special = v.Special;
            sm.TotalSize = v.TotalSize;
            sm.Size = v.Size;
            sm.DateTime = v.DateTime;
            sm.Dir = v.Dir;
            sm.Folder = v.Folder;
            return sm;
        }

        //Returns AudioModel from SellModel
        public static AudioModel selltoAudioModel(SellModel s)
        {

            AudioModel am = new AudioModel();

            am.Name = s.Name;
            am.Price = s.Price;
            am.Dir = s.Dir;
            am.Type = s.Type;
            am.Size = s.Size;
            am.DateTime = s.DateTime;
            am.Special = s.Special;


            return am;
        }

        //Returns SellModel from AudioModel
        public static SellModel otherTOSellModel(OthersModel v)
        {
            SellModel sm = new SellModel();
            sm.Name = v.Name;
            sm.Price = v.Price;
            sm.Initials = v.Type.Icon;
            sm.Type = v.Type;
            sm.RecieverPort = v.RecieverPort;
            sm.RecieverSerial = v.RecieverSerial;
            sm.Special = v.Special;
            sm.TotalSize = v.TotalSize;
            sm.Size = v.Size;
            sm.DateTime = v.DateTime;
            sm.Dir = v.Dir;
            sm.Folder = v.Folder;
            return sm;
        }

        //Returns Random number for folders
        public static int GenerateRandom()
        {
            int gr = 0;
            string nto = "";
            Random r = new Random();
            for(int jk = 0; jk < 8; jk++)
            {
                
                    gr = r.Next(2, 9);
                
                nto += gr; 
            }
            gr = int.Parse(nto);
            return gr;
        }

        //Adds 0 to first digit
        public static int AddOne(int gr)
        {
            List<char> k = new List<char>();
            string grst = gr.ToString();
            foreach(char c in grst)
            {
                k.Add(c);
            }
            if (!k[0].Equals('1'))
            {
                k.Insert(0, '1');

            }
            
            grst = "";
            foreach(char c in k)
            {
                grst += c;
            }
            int final = int.Parse(grst);
            return final;
        }

        //From String Size to Long
        public static long FromStringSizeToLong(string size)
        {
            long Long;
            long times = 0;
            try
            {
                string[] stringa = size.Split(' ');
                float tsize = float.Parse(stringa[0]);
                string measure = stringa[1];
                
                if (measure.Equals("GB"))
                {
                    times = 1073741824;
                }
                else if (measure.Equals("MB"))
                {
                    times = 1048576;
                }
                else if(measure.Equals("KB"))
                {
                    times = 1024;
                }
                times = (long)(times * tsize);
                
            }
            catch(Exception exp)
            {
                Console.WriteLine($"FromStringTOLong {exp.Message}");
            }

            return times;
        }

        //Work Out Folder Size from String
        public static string FolderStringSize(string sizze1, string sizze2)
        {
            long s1 = FromStringSizeToLong(sizze1);
            long s2 = FromStringSizeToLong(sizze2);
            return calculateSize(s1 + s2);
        }

    }


}
