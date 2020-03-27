using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class TypeShowViewModel : Screen
    {
        private bool _refv;
        private bool _vpricev;
        private bool _apricev;
        private bool _opricev;


        private string _id = "";
        private string _name = "";
        private string _ftype = "";
        private string _ref = "";
        private string _vprice = "";
        private BindableCollection<Dir> _directories = new BindableCollection<Dir>();







        public bool ReferenceIsVisible
        {
            get
            {
                return _refv;
            }
            set
            {
                _refv = value;
                NotifyOfPropertyChange(() => ReferenceIsVisible);
            }
        }

        public bool VideoPriceIsVisible
        {
            get
            {
                return _vpricev;
            }
            set
            {
                _vpricev = value;
                NotifyOfPropertyChange(() => VideoPriceIsVisible);
            }
        }

        public bool AudioPriceIsVisible
        {
            get
            {
                return _apricev;
            }
            set
            {
                _apricev = value;
                NotifyOfPropertyChange(() => AudioPriceIsVisible);
            }
        }

        public bool OthersPriceIsVisible
        {
            get
            {
                return _opricev;
            }
            set
            {
                _opricev = value;
                NotifyOfPropertyChange(() => OthersPriceIsVisible);
            }
        }

        public string Icon
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Icon);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string FileType
        {
            get
            {
                return _ftype;
            }
            set
            {
                _ftype = value;
                NotifyOfPropertyChange(() => FileType);
            }
        }

        public string Reference
        {
            get
            {
                return _ref;
            }
            set
            {
                _ref = value;
                NotifyOfPropertyChange(() => Reference);
            }
        }

        public string Price
        {
            get
            {
                return _vprice;
            }
            set
            {
                _vprice = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        public BindableCollection<Dir> Directories
        {
            get
            {
                return _directories;
            }
            set
            {
                _directories = value;
                NotifyOfPropertyChange(() => Directories);
            }
        }


        
        public void TypeShow()
        {
            Icon = SettingsViewModel.tvm.Initial;
            Name = SettingsViewModel.tvm.Name;
            FileType = SettingsViewModel.tvm.DataType;
            if (!SettingsViewModel.tvm.Reference.Equals(null))
            {
                if (SettingsViewModel.tvm.Reference.Length > 0)
                {
                    ReferenceIsVisible = true;
                    Reference = SettingsViewModel.tvm.Reference;
                }
            }
            switch (FileType)
            {
                case "Video":
                    VideoPriceIsVisible = true;
                    Price = SettingsViewModel.tvm.Price;
                    break;
            }

            
            foreach(Dir dir in SettingsViewModel.tvm.Dirs)
            {
                Directories = new BindableCollection<Dir>(SettingsViewModel.tvm.Dirs);
            }

        }


        public void CloseType()
        {
            SettingsViewModel.tvm.idd = 0;
            SettingsViewModel.tvm.Price = "";
            SettingsViewModel.tvm.Name = "";
            SettingsViewModel.tvm.EditIcon = "";
            SettingsViewModel.tvm.Reference = "";
            SettingsViewModel.tvm.CurrentType = "";
            SettingsViewModel.tvm.Dirs = new BindableCollection<Dir>();
            

            SettingsViewModel.settingsvm.backToType();
        }
    }
}
