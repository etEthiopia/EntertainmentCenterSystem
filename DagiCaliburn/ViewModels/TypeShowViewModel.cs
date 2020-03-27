using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static DagiCaliburn.Models.TypeModel;

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
        private string _aprice = "";
        private string _abprice = "";
        private BindableCollection<Dir> _directories = new BindableCollection<Dir>(); 
        private BindableCollection<OtherPrice> _othersPrices = new BindableCollection<OtherPrice>();

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

        public BindableCollection<OtherPrice> OthersPrices
        {
            get
            {
                return _othersPrices;
            }
            set
            {
                _othersPrices = value;
                NotifyOfPropertyChange(() => OthersPrices);
            }
        }

        public string GBPrice
        {
            get
            {
                return _aprice;
            }
            set
            {
                _aprice = value;
                NotifyOfPropertyChange(() => GBPrice);
            }
        }

        public string AlbumPrice
        {
            get
            {
                return _abprice;
            }
            set
            {
                _abprice = value;
                NotifyOfPropertyChange(() => AlbumPrice);
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
                case "Audio":
                    AudioPriceIsVisible = true;
                    GBPrice = SettingsViewModel.tvm.AudioPricePerGB;
                    AlbumPrice = SettingsViewModel.tvm.AudioAlbumPrice;
                    break;
                case "Others":
                    OthersPriceIsVisible = true;
                    OthersPrices = new BindableCollection<OtherPrice>(TypeModel.GetOthersPrice(SettingsViewModel.tvm.idd));
                    break;
            }

            
            foreach(Dir dir in SettingsViewModel.tvm.Dirs)
            {
                Directories = new BindableCollection<Dir>(SettingsViewModel.tvm.Dirs);
            }

        }


        public void CloseType()
        {
            SettingsViewModel.tvm = new TypeViewModel();
            
            Icon = "";
            Name = "";
            Price = "";
            FileType = "";
            Reference = "";
            GBPrice = "";
            AlbumPrice = "";
            Directories = null;
            ReferenceIsVisible = false;
            VideoPriceIsVisible = false;
            OthersPriceIsVisible = false;
            AudioPriceIsVisible = false;


            SettingsViewModel.settingsvm.backToType();
        }
    }
}
