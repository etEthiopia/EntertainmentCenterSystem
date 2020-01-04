using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class ConfigViewModel
    {
        public static string lightdark;
        public static string primary;
        public static string accent;

        public void green_green()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
                "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.green.xaml",
                "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.green.xaml");
            primary = "green";
            accent = "green";
            ConfigModel.updateConfig();
        }

        public void amber_deeporange()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
               "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.amber.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.deeporange.xaml");
            primary = "amber";
            accent = "deeporange";
            ConfigModel.updateConfig();
        }

        public void red_red()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
               "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.red.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.red.xaml");
            primary = "red";
            accent = "red";
            ConfigModel.updateConfig();
        }

        public void grey_teal()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
               "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.grey.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.teal.xaml");
            primary = "grey";
            accent = "teal";
            ConfigModel.updateConfig();
        }

        public void blue_cyan()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
               "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.blue.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.cyan.xaml");
            primary = "blue";
            accent = "cyan";
            ConfigModel.updateConfig();
        }

        public void brown_orange()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
               "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.brown.xaml",
               "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.orange.xaml");
            primary = "brown";
            accent = "orange";
            ConfigModel.updateConfig();
        }

        public void DarkMode()
        {
            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{lightdark}.xaml",
               "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
               $"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{primary}.xaml",
               $"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.{accent}.xaml");
            Console.WriteLine("ViewModel");
            ConfigModel.updateConfig();
        }
    }
}
