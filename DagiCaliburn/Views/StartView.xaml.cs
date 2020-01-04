using DagiCaliburn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using DagiCaliburn.Models;

namespace DagiCaliburn.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public static StartView stv;
        public Rect workingArea;
        public StartView()
        {
            
            ConfigModel.getConfig();

            if((ConfigViewModel.lightdark.Length > 0 && ConfigViewModel.primary.Length > 0 && ConfigViewModel.accent.Length > 0))
            {

            }
            else
            {
                ConfigViewModel.lightdark = "Light";
                ConfigViewModel.primary = "indigo";
                ConfigViewModel.accent = "cyan";

            }

            App.app.ChangeTheme($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{ConfigViewModel.lightdark}.xaml",
                "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                $"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{ConfigViewModel.primary}.xaml",
                $"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.{ConfigViewModel.accent}.xaml");



            InitializeComponent();
            var hotKey = new HotKey(Key.Q, KeyModifier.Shift, OnHotKeyHandler);
            Console.WriteLine($"HOTKEYyyyy: {hotKey.Id}, {hotKey.Key}, {hotKey.KeyModifiers}");
            Disk.AddInsertUSBHandler();
            Disk.AddRemoveUSBHandler();
            stv = this;
            this.Top = 0.0;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WindowCaptionHeight;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth /3;

            workingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = workingArea.Right - this.Width;
            this.Top = workingArea.Bottom - this.Height;
        }

        private void OnHotKeyHandler(HotKey hotKey)
        {
            
            var w = this;
            w.Show();
            w.Activate();
            w.Focus();
            //HomeViewModel.INGB = true;
            StartViewModel.startview.ActivateItem(StartViewModel.gbview);

            AudioModel.ShowSelected(GetListOfSelected());
        }

        private void figureHW()
        {
            int x = 40;
            HomeBtn.Height = x;
            HomeBtn.Width = x;
            SpecialBtn.Height = x;
            SpecialBtn.Width = x;
            ChartsBtn.Height = x;
            ChartsBtn.Width = x;
            SettingsBtn.Height = x;
            SettingsBtn.Width = x;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            figureHW();
            HomeBtn.Height = 50;
            HomeBtn.Width = 50;
            
        }

        private void SpecialBtn_Click(object sender, RoutedEventArgs e)
        {
            figureHW();
            SpecialBtn.Height = 50;
            SpecialBtn.Width = 50;
        }

        private void ChartsBtn_Click(object sender, RoutedEventArgs e)
        {
            figureHW();
            ChartsBtn.Height = 50;
            ChartsBtn.Width = 50;
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            figureHW();
            SettingsBtn.Height = 50;
            SettingsBtn.Width = 50;
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;
        

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        private const uint VK_CAPITAL = 0x14; //CAPS LOCK:      


        private IntPtr _windowHandle;
        private HwndSource _source;


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);


            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            
            try
            {
                RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_CAPITAL); //CTRL + CAPS_LOCK
                //MessageBox.Show($"Hot Key Ctrl + Caps  {_windowHandle},{HOTKEY_ID}, {MOD_CONTROL}, {VK_CAPITAL}");
            }
            catch (Exception en)
            {
                Console.WriteLine($"Hot Key Ctrl + Caps, {en.Message}");
            }
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            
            switch (msg)
            {

                case WM_HOTKEY:
                    
                    switch (wParam.ToInt32())
                    {

                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            
                            //MOD_ALT
                            if (vkey == VK_CAPITAL)
                            {
                                //MessageBox.Show($"HELLO {hotKey}");
                                SellModel smk = new SellModel();
                                foreach (string prerp in GetListOfSelected())
                                {
                                    smk.PrepareSale(prerp);
                                }


                            }

                            handled = true;
                            break;
                            
                        


                    }
                    break;
            }
            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
            base.OnClosed(e);
        }

        public  List<string> GetListOfSelected()
        {

            string filename;
            List<string> selected = new List<string>();
            try
            {
                var shell = new Shell32.Shell();

                SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();


                foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindowsClass())
                {
                    filename = System.IO.Path.GetFileNameWithoutExtension(window.FullName).ToLower();
                    if (filename.ToLowerInvariant() == "explorer")
                    {
                        Shell32.FolderItems items = ((Shell32.IShellFolderViewDual2)window.Document).SelectedItems();

                        foreach (Shell32.FolderItem item in items)
                        {
                            //MessageBox.Show(item.Path.ToString());
                            String[] fullname = item.Path.Split('\\');
                            string dr = "";
                            for (int i = 0; i < fullname.Length - 1; i++)
                            {
                                dr += fullname[i] + "/";
                            }
                            dr = dr.Substring(0, dr.Length - 1);

                            selected.Add(item.Path);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"GETTING SELECTED ITEMS ERROR {e.Message}");
            }
            return selected;
        }
    }
}
