using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class UnknownDirViewModel : Screen
    {
        public string DirectoryTitle { get; set; }

        public bool SpecialIsVisible { get; set; }

        public bool TypeIsVisible { get; set; }

        public bool SaveIsEnabled { get; set; }

        public UnknownDirViewModel(string dir)
        {
            DirectoryTitle = dir;
            SpecialIsVisible = false;
            TypeIsVisible = false;
            SaveIsEnabled = false;
        }
    }
}
