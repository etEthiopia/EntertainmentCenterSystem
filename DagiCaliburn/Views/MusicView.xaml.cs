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
    /// Interaction logic for MusicView.xaml
    /// </summary>
    public partial class MusicView : UserControl
    {
        public MusicView()
        {
            InitializeComponent();
        }

        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            if(this.Cursor != Cursors.Wait)
            {
                Mouse.OverrideCursor = Cursors.Hand;
            }
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Cursor != Cursors.Wait)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }
    }
}
