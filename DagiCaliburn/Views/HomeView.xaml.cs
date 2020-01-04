using DagiCaliburn.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace DagiCaliburn.Views
{
    public partial class HomeView : Window
    {
        public Rect workingArea;
        public HomeView()
        {
            InitializeComponent();
            this.Top = 0.0;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WindowCaptionHeight;
            workingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = workingArea.Right - this.Width;
            this.Top = workingArea.Bottom - this.Height;
            
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

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_CAPITAL); //CTRL + CAPS_LOCK
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
                            if (vkey == VK_CAPITAL)
                            {
                                var w = this;
                                w.Show();
                                w.Activate();
                                w.Focus();
                                HomeViewModel.INGB = true;
                                HomeViewModel.hvm.ActivateItem(HomeViewModel.mv);
                                MusicViewModel.items = GetListOfSelected();
                                HomeViewModel.mv.Form1_Load();
                                
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


        public List<string> GetListOfSelected()
        {
            string filename;
            List<string> selected = new List<string>();
            var shell = new Shell32.Shell();

            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();


            foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindowsClass())
            {
                filename = Path.GetFileNameWithoutExtension(window.FullName).ToLower();
                if (filename.ToLowerInvariant() == "explorer")
                {
                    Shell32.FolderItems items = ((Shell32.IShellFolderViewDual2)window.Document).SelectedItems();
                    
                    foreach (Shell32.FolderItem item in items)
                    {
                        Console.WriteLine("Itme " + item.Path +" "+ item.Size);
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

            return selected;
        }

    }
}
