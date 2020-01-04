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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        
        public MainView()
        {
            InitializeComponent();
            //MessageBox.Show($"FIRSTPAGE {gridfirstpage.IsSelected}, PREVIOUSTPAGE {gridbackpage.IsSelected},NEXTPAGE {gridnextpage.IsSelected}");
        }

        private void gridbackpage_Selected(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"FIRSTPAGE {gridfirstpage.IsSelected}, PREVIOUSTPAGE {gridbackpage.IsSelected},NEXTPAGE {gridnextpage.IsSelected}");
            StartViewModel.mainview.backpage();
        }

        private void gridfirstpage_Selected(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"FIRSTPAGE {gridfirstpage.IsSelected}, PREVIOUSTPAGE {gridbackpage.IsSelected},NEXTPAGE {gridnextpage.IsSelected}");

            StartViewModel.mainview.firstpage();
        }

        private void gridnextpage_Selected(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"FIRSTPAGE {gridfirstpage.IsSelected}, PREVIOUSTPAGE {gridbackpage.IsSelected},NEXTPAGE {gridnextpage.IsSelected}");


            StartViewModel.mainview.nextpage();
        }

        
    }
}
