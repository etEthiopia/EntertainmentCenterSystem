using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WK.Libraries.SharpClipboardNS;

namespace DagiCaliburn.ViewModels
{
    class CopyViewModel : Screen
    {
        IWindowManager manager = new WindowManager();
        public BindableCollection<Drive> Drives { get; set; }
        public string[] CurrentC;
        private double _copyPercentage;
        private string[] clip;
        private List<String> folders = new List<string>();
        private List<bool> foldersbool = new List<bool>();
        private String[] videoTypes = { "WEBM", "MP4", "FLV","MKV", "MOV", "MPG", "MP2", "MPEG", "MPE", "MPV", "OGG",  "M4P", "M4V", "AVI", "WMV",  "QT","SWF", "AVCHD" };
        private List<string> types = new List<string>();
        private List<string> unknownDIrs = new List<string>();
        public int SelectedIndex { get; set; }
        private BindableCollection<FilmModel> _clipp;
        private double max;
        private double val;

        public string TotalSize { get; set; }

        public int TotalItems { get; set; }

        public double TotalPrice { get; set; }

        public double CopyProgress {
            get {
                return _copyPercentage;
            }
            set {
                _copyPercentage = value;
                
                NotifyOfPropertyChange(() => CopyProgress);
            }
        }

        public BindableCollection<FilmModel> Clipp {
            get
            {
                return _clipp;
            }
            set
            {
                _clipp = value;
                //NotifyOfPropertyChange(() => Clipp);
            }
        }


        private bool IsFolder(String dir) {
            Console.WriteLine($"Dirrrrrrrrrrr  {dir}");
            //Console.WriteLine("File Attributes "+File.GetAttributes(dir).ToString());
            if (File.GetAttributes(dir).ToString().Contains("Directory")) {
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsVideo(String dir)
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

        private void figureThingsOut(string[] clip)
        {
            foreach (String cp in clip)
            {
                //Console.WriteLine($"Folder ? {IsFolder(cp)}");
                if (IsFolder(cp))
                {
                    if (!folders.Contains(cp + "\\"))
                    {

                        
                        folders.Add(cp + "\\");
                        foldersbool.Add(false);
                    }

                }
                else
                {
                    if (IsVideo(cp))
                    {
                        Clipp.Add(new FilmModel(cp));
                        Console.WriteLine("File " + cp);
                    }
                }
                //Clipp.Add(new FilmModel(cp));
            }
            try{
                int counter = 0;
                while (foldersbool.Contains(false))
                {
                    
                    if (foldersbool[counter] == false)
                    {

                        string folder = folders[counter];
                        List<String> filePaths = Directory.GetFiles(@folder).ToList(); //+ 
                                                                                       //.Concat(Directory.GetDirectories(@folder).ToList());
                        foreach (var dirs in Directory.GetDirectories(@folder))
                        {
                            Console.WriteLine("DDIIRR " + dirs.ToString());
                            filePaths.Add(dirs);
                        }


                        Console.WriteLine("FilePaths Size " + filePaths.Count + "of " + folder);
                        foreach (string file in filePaths)
                        {
                            Console.WriteLine("FilePath " + file);
                            try
                            {
                                if (IsFolder(file))
                                {
                                    if (!folders.Contains(file + "\\"))
                                    {
                                        folders.Add(file + "\\");
                                        foldersbool.Add(false);
                                    }
                                }
                                else
                                {
                                    if (IsVideo(file))
                                    {
                                        
                                        Clipp.Add(new FilmModel(file));
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Item hidden");
                            }


                        }
                        //folders.Remove(folder);
                        foldersbool.Insert(folders.IndexOf(folder), true);
                        counter++;
                    }
                }
            }
            catch(Exception){
                Console.WriteLine("Shit Happend in folders array");
            }
            foreach(String folder in folders)
            {
                Console.WriteLine("Folder : " + folder + "Boolean : "+foldersbool[folders.IndexOf(folder)]);
            }
        }

        public CopyViewModel(string[] clip)
        {
            this.clip = clip;
            Setup();
            
        }

        private void Setup()
        {
            TotalPrice = 0;
            this.CurrentC = clip;
            Clipp = new BindableCollection<FilmModel>();
            figureThingsOut(clip);
            TotalSize = "";
            foreach (var file in Clipp)
            {
                if (TotalSize.Length == 0)
                {
                    TotalSize = new FileInfo(file.Directory + file.Name).Length.ToString();
                }
                else
                {
                    TotalSize = (long.Parse(TotalSize) + new FileInfo(file.Directory + file.Name).Length).ToString();
                }

                int ft = file.getType();
                Console.WriteLine($" File returnted type = {ft}");
                if (ft != 0)
                {
                    file.Type = ft;
                    types.Add(TypeModel.GetTypeToFile(file.Type).Name);
                    Console.WriteLine($" File type name = {types.Last()},  Price = {file.Price}");
                    TotalPrice += TypeModel.GetTypeToFile(file.Type).Price;
                }
                else
                {
                    unknownDIrs.Add(file.Directory);
                    
                }


                // TotalSize += new FileInfo(file.Directory+file.Name).Length;
                Console.WriteLine($"Size {TotalSize}");
                Console.WriteLine($"FROM ALL FILES {file.Name}");
            }
            if (unknownDIrs.Count > 0)
            {
                manager.ShowWindow(new UnknownDirViewModel(unknownDIrs[0]), null, null);
            }
            TotalItems = Clipp.Count();

            //foreach( var vp in CurrentC)
            //{
            //    Clipp += vp.ToString() + "\n";

            //}



            Drives = new BindableCollection<Drive>(Drive.GetDrives());
        }

        public CopyViewModel()
        {

            Drives = new BindableCollection<Drive>(Drive.GetDrives());
        }


        public void Send()
        {
            //foreach (string file in CurrentC)
            //{
            //    string fileName1 = System.IO.Path.GetFileName(file);

            //    //File.Copy(file, dirCopyTo + "\\" + fileName1, true);
            //    //td
            //    //MessageBox.Show("Selected Index  "+ SelectedIndex);

            //    Console.WriteLine($"To copy ||| {file} , {Drives[SelectedIndex].Name + fileName1}");
            //    try
            //    {
            //        File.Copy(file, Drives[SelectedIndex].Name + fileName1, true);
            //    }
            //    catch(Exception e)
            //    {
            //        MessageBox.Show("Could not copy");
            //    }
            //}
            //Copy();
            
            var task = CoppyAsync();
            var result = task.AsResult();
            
           
        }






        private async Task CoppyAsync() {
            var dictionary = new Dictionary<string, string>();

            


            foreach (string file in CurrentC)
            {
                string fileName1 = System.IO.Path.GetFileName(file);
                Console.WriteLine($"Copy Ordered File :  {file},  FileName1 : {fileName1}");
                try
                {

                    dictionary.Add(file, Drives[SelectedIndex].Name + fileName1);
                    HomeViewModel.prevClipbrds.Remove(file);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not copy");
                }

            }


            

            max = 10000.0;
            val = 0;
            await CopyFiles(dictionary, prog => val = prog);
        }

        private void Copy()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CopyProgress =(Double) e.ProgressPercentage;

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int i = 0;
                foreach (string file in CurrentC)
                {
                    string fileName1 = System.IO.Path.GetFileName(file);

                    //File.Copy(file, dirCopyTo + "\\" + fileName1, true);
                    //td
                    //MessageBox.Show("Selected Index  "+ SelectedIndex);

                    Console.WriteLine($"To copy ||| {file} , {Drives[SelectedIndex].Name + fileName1}");
                    try
                    {
                        File.Copy(file, Drives[SelectedIndex].Name + fileName1,true);
                        i++;
                    worker.ReportProgress((i * 100) / CurrentC.Length);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not copy");
                    }
                
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            CopyProgress = 0;
        }


         async Task CopyFiles(Dictionary<string, string> files, Action<double> progressCallback)
        {
            long total_size = files.Keys.Select(x => new FileInfo(x).Length).Sum();

            long total_read = 0;
                                   
            double progress_size = 10000.0;

            Console.WriteLine($"Total Size {total_size}");

            foreach (var item in files)
            {
                long total_read_for_file = 0;

                var from = item.Key;
                var to = item.Value;

                using (var outStream = new FileStream(to, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (var inStream = new FileStream(from, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await CopyStream(inStream, outStream, x =>
                        {
                            total_read_for_file = x;
                            progressCallback(((total_read + total_read_for_file) / (double)total_size) * progress_size);
                            //Console.WriteLine($"Total Read for File {item.Key} is {total_read_for_file}");
                           // Console.WriteLine($"Progress Callback is {((total_read + total_read_for_file) / (double)total_size) * progress_size}");
                            CopyProgress = ((total_read + total_read_for_file) / (100 * (double)total_size) * progress_size);
                            //Console.WriteLine($"CopyProgress is {CopyProgress}");
                        });
                    }
                }

                total_read += total_read_for_file;
            }
        }


        public static async Task CopyStream(Stream from, Stream to, Action<long> progress)
        {
            int buffer_size = 10240;

            byte[] buffer = new byte[buffer_size];

            long total_read = 0;

            while (total_read < from.Length)
            {
                int read = await from.ReadAsync(buffer, 0, buffer_size);

                await to.WriteAsync(buffer, 0, read);

                total_read += read;

                progress(total_read);
            }
        }
    }

    //public static class Copier
    //{
    //    public static async Task CopyFiles(Dictionary<string, string> files, Action<double> progressCallback)
    //    {
    //        long total_size = files.Keys.Select(x => new FileInfo(x).Length).Sum();

    //        long total_read = 0;

    //        double progress_size = 1000000.0;

    //        Console.WriteLine($"Total Size {total_size}");
            
    //        foreach (var item in files)
    //        {
    //            long total_read_for_file = 0;

    //            var from = item.Key;
    //            var to = item.Value;

    //            using (var outStream = new FileStream(to, FileMode.Create, FileAccess.Write, FileShare.Read))
    //            {
    //                using (var inStream = new FileStream(from, FileMode.Open, FileAccess.Read, FileShare.Read))
    //                {
    //                    await CopyStream(inStream, outStream, x =>
    //                    {
    //                        total_read_for_file = x;
    //                        progressCallback(((total_read + total_read_for_file) / (double)total_size) * progress_size);
    //                        Console.WriteLine($"Total Read for File {item.Key} is {total_read_for_file}");
    //                        Console.WriteLine($"Progress Callback is {((total_read + total_read_for_file) / 1000 * (double)total_size) * progress_size}");
    //                       // cvm.CopyProgress = ((total_read + total_read_for_file) / 1000 * (double)total_size) * progress_size;
    //                    });
    //                }
    //            }

    //            total_read += total_read_for_file;
    //        }
    //    }


    //    public static async Task CopyStream(Stream from, Stream to, Action<long> progress)
    //    {
    //        int buffer_size = 10240;

    //        byte[] buffer = new byte[buffer_size];

    //        long total_read = 0;

    //        while (total_read < from.Length)
    //        {
    //            int read = await from.ReadAsync(buffer, 0, buffer_size);

    //            await to.WriteAsync(buffer, 0, read);

    //            total_read += read;

    //            progress(total_read);
    //        }
    //    }

    //}

    

    

}
