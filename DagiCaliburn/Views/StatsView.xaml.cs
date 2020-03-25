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
    /// Interaction logic for StatsView.xaml
    /// </summary>
    public partial class StatsView : UserControl
    {
        public static int i = 1;
        public StatsView()
        {
            InitializeComponent();
            if(i == 1)
            {
                DailyBtn.IsChecked = true;
            }
            else if(i == 2)
            {
                WeeklyBtn.IsChecked = true;
            }
            else if(i == 3)
            {
                MonthlyBtn.IsChecked = true;
            }
            //else
            //{
            //    CusomterBtn.IsChecked = true;
            //}
        }

        private void DailyBtn_Checked(object sender, RoutedEventArgs e)
        {
            i = 1;
        }

        private void WeeklyBtn_Checked(object sender, RoutedEventArgs e)
        {
            i = 2;
        }

        private void MonthlyBtn_Checked(object sender, RoutedEventArgs e)
        {
            i = 3;
        }

        //private void CusomterBtn_Checked(object sender, RoutedEventArgs e)
        //{
        //    i = 4;
        //}
    }
}
