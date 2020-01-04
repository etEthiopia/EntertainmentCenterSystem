using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DagiCaliburn.Views
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public static int i = 1;
        public SettingsView()
        {
            InitializeComponent();
            if (i == 1)
            {
                TypesMenu.IsChecked = true;
            }
            else if (i == 2)
            {
                ConfigMenu.IsChecked = true;
            }
            else if (i == 3)
            {
                CreditsMenu.IsChecked = true;
            }
            else
            {
                ProfileMenu.IsChecked = true;
            }
        }

        private void AddType(object sender, RoutedEventArgs e)
        {
            i = 1;
        }

        private void TypesMenu_Checked(object sender, RoutedEventArgs e)
        {
            i = 1;
        }

        private void ConfigMenu_Checked(object sender, RoutedEventArgs e)
        {
            i = 2;
        }

        private void CreditsMenu_Checked(object sender, RoutedEventArgs e)
        {
            i = 3;
        }

        private void ProfileMenu_Checked(object sender, RoutedEventArgs e)
        {
            i = 4;
        }
    }
}
