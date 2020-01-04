using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class SettingsViewModel : Conductor<object>
    {
        public static SettingsViewModel settingsvm;
        public static TypesViewModel tsvm;
        public static TypeViewModel tvm;
        public static ConfigViewModel cvm;
        

        public SettingsViewModel()
        {
            tvm = new TypeViewModel();
            tsvm = new TypesViewModel();
            cvm = new ConfigViewModel();
            settingsvm = this;
            ActivateItem(tsvm);
        }

        public void TypesMenu()
        {
            ActivateItem(tsvm);
        }

        public void ConfigMenu()
        {
            ActivateItem(cvm);
        }

        public void CreditsMenu()
        {

        }

        public void ProfileMenu()
        {

        }

        public void AdType()
        {
            ActivateItem(tvm);
        }
        public void backToType()
        {
            ActivateItem(tsvm);
        }


    }
}
