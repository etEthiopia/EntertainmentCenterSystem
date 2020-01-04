﻿using DagiCaliburn.ViewModels;
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
    /// Interaction logic for TypesView.xaml
    /// </summary>
    public partial class TypesView : UserControl
    {
        public TypesView()
        {
            InitializeComponent();
        }

        private void EditType_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Iddd = button.Tag;
            TypesViewModel.EditType(int.Parse(Iddd.ToString()));
            //MessageBox.Show($"IDD {Iddd}");

        }
    }
}
