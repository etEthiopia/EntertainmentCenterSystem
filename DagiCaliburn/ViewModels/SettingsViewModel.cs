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
        public static ProfileViewModel pvm;
        public static CreditsViewModel cdm;
        public static TypeShowViewModel tsm;
        

        public SettingsViewModel()
        {
            tvm = new TypeViewModel();
            tsvm = new TypesViewModel();
            cvm = new ConfigViewModel();
            pvm = new ProfileViewModel();
            cdm = new CreditsViewModel();
            tsm = new TypeShowViewModel();
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
            ActivateItem(cdm);
        }

        public void ProfileMenu()
        {
            ActivateItem(pvm);
        }

        public void AdType()
        {
            ActivateItem(tvm);
        }
        public void backToType()
        {
            ActivateItem(tsvm);
        }
        public void ShowType()
        {
            SettingsViewModel.tsm.TypeShow();
            ActivateItem(tsm);
        }


    }
}
