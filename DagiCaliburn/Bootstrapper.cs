using Caliburn.Micro;
using DagiCaliburn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            
            Initialize();
        }

        void InstallMeOnStartUp()
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            }
            catch(Exception e) {
                MessageBox.Show("Couldnt make startup");
            }
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            InstallMeOnStartUp();
            
            DisplayRootViewFor<StartViewModel>();
        }
        
    }
}
