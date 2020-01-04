using DagiCaliburn.ViewModels;
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
    /// Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView : UserControl
    {
        public ConfigView()
        {
            InitializeComponent();
            if (ConfigViewModel.lightdark.Equals("Dark"))
            {
                DarkMode.IsChecked = true;
            }
        }

        private void LeftWindow_Click(object sender, RoutedEventArgs e)
        {
            LeftWindow.IsChecked = true;
            RightWindow.IsChecked = false;
            StartView.stv.Left = 0.0;
        }

        private void RightWindow_Click(object sender, RoutedEventArgs e)
        {
            LeftWindow.IsChecked = false;
            RightWindow.IsChecked = true;
            StartView.stv.Left = StartView.stv.workingArea.Right - StartView.stv.Width;
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            
                ConfigViewModel.lightdark = "Dark";
            
                
            
            Console.WriteLine("Cs");
        }

        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfigViewModel.lightdark = "Light";
        }
    }
}
