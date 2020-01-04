using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class SellViewModel : Screen
    {
        public static SellModel newSell = new SellModel();

        private BindableCollection<string> _fileTypes = new BindableCollection<string>();
        private BindableCollection<TypeModel> _types = new BindableCollection<TypeModel>();
        private BindableCollection<string> _drivees = new BindableCollection<string>();


        private string _icon = "";
        private string _name = "";
        private string _fileType = "";
        private TypeModel _type = new TypeModel();
        private string _dir = "";
        private string _price = "";
        private string _sellViewMessage = "";
        private string _drrive = "";
        private string _size = "";

        public SellViewModel()
        {
            FileTypes.Add("Video");
            FileTypes.Add("Audio");
            FileTypes.Add("Others");

            foreach(Disk dk in StartViewModel.mainview.Drives)
            {
                Drivees.Add($"{dk.Port} -- {dk.Props}");
            }
        }

        public BindableCollection<string> FileTypes
        {
            get
            {
                return _fileTypes;
            }
            set
            {
                _fileTypes = value;
                NotifyOfPropertyChange(() => FileTypes);
            }
        }

        public BindableCollection<TypeModel> Types
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                NotifyOfPropertyChange(() => Types);
            }
        }

        public BindableCollection<string> Drivees
        {
            get
            {
                return _drivees;
            }
            set
            {
                _drivees = value;
                NotifyOfPropertyChange(() => Drivees);
            }
        }




        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
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
                return _fileType;
            }
            set
            {
                _fileType = value;
                NotifyOfPropertyChange(() => FileType
);
                SetupTypes();
            }
        }

        public string Drrive
        {
            get
            {
                return _drrive;
            }
            set
            {
                _drrive = value;
                NotifyOfPropertyChange(() => Drrive);
            }
        }

        public TypeModel Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                NotifyOfPropertyChange(() => Type);
                if (Type.Id > 0)
                {
                    Icon = Type.Icon;
                }
            }
        }

        public string Dir
        {
            get
            {
                return _dir;
            }
            set
            {
                _dir = value;
                NotifyOfPropertyChange(() => Dir);
            }
        }
        
        public string SellViewMessage
        {
            get
            {
                return _sellViewMessage;
            }
            set
            {
                _sellViewMessage = value;
                NotifyOfPropertyChange(() => SellViewMessage);
            }
        }

        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                NotifyOfPropertyChange(() => Size);
            }
        }

        public void CloseType()
        {
            StartViewModel.startview.ActivateItem(StartViewModel.mainview);
            SellViewMessage = "";
            Name = "";
            Icon = "";
            Type = new TypeModel();
            Type.Id = 0;
            FileType = "";
            Dir = "";
            Price = "";
            Types = new BindableCollection<TypeModel>();
            Drivees = new BindableCollection<string>();
        }

        public void SetupTypes()
        {
            Types = new BindableCollection<TypeModel>(TypeModel.GetTypeFromFileType(FileType));
            
        }

        public void Refresh()
        {
            Drivees = new BindableCollection<string>();
            foreach (Disk dk in StartViewModel.mainview.Drives)
            {
                Drivees.Add($"{dk.Port} -- {dk.Props}");
            }

        }

        public void Save()
        {

            if (Name.Length > 0)
            {
                if (Type.Id > 0)
                {
                    float pp = 0.0f;
                    if (float.TryParse(Price, out pp))
                    {
                        if (pp > 0.0f)
                        {
                            if (Type.DataType.Equals("Video"))
                            {
                                if (Drrive.Length > 0)
                                {
                                    VideoModel vmol = new VideoModel();
                                    vmol.Name = Name;
                                    vmol.Price = pp;
                                    vmol.Dir = Dir;
                                    vmol.Type = Type;
                                    vmol.RecieverPort = Drrive.Substring(0, 2);
                                    foreach (Disk d in StartViewModel.mainview.Drives)
                                    {
                                        if ((d.Port).Equals(vmol.RecieverPort))
                                        {
                                            vmol.RecieverSerial = d.serial;

                                            break;
                                        }

                                    }
                                    vmol.Size = Size;
                                    vmol.Folder = 1;
                                    if (vmol.AddAsASale())
                                    {
                                        SellViewMessage = $"'{vmol.Name}' successfully added";
                                    }
                                    else
                                    {
                                        SellViewMessage = $"'{vmol.Name}' not added";
                                    }
                                }
                                else
                                {
                                    SellViewMessage = "Drive is not in correct format";
                                }
                            }
                            else if (Type.DataType.Equals("Audio"))
                            {
                                if (Drrive.Length > 0)
                                {
                                    AudioModel vmol = new AudioModel();
                                    vmol.Name = Name;
                                    vmol.Price = pp;
                                    vmol.Dir = Dir;
                                    vmol.RecieverPort = Drrive.Substring(0, 2);
                                    foreach (Disk d in StartViewModel.mainview.Drives)
                                    {
                                        if ((d.Port).Equals(vmol.RecieverPort))
                                        {
                                            vmol.RecieverSerial = d.serial;

                                            break;
                                        }

                                    }
                                    vmol.Type = Type;
                                    vmol.Size = Size;
                                    vmol.Folder = 1;
                                    if (vmol.AddAlbumAsSale())
                                    {
                                        SellViewMessage = $"'{vmol.Name}' successfully added";
                                    }
                                    else
                                    {
                                        SellViewMessage = $"'{vmol.Name}' not added";
                                    }
                                }
                                else
                                {
                                    SellViewMessage = "Drive is not in correct format";
                                }
                            }
                            else if (Type.DataType.Equals("Others"))
                            {
                                if (Drrive.Length > 0)
                                {
                                    OthersModel vmol = new OthersModel();
                                    vmol.Name = Name;
                                    vmol.Price = pp;
                                    vmol.Type = Type;
                                    vmol.Dir = Dir;
                                    vmol.RecieverPort = Drrive.Substring(0, 2);
                                    foreach (Disk d in StartViewModel.mainview.Drives)
                                    {
                                        if ((d.Port).Equals(vmol.RecieverPort))
                                        {
                                            vmol.RecieverSerial = d.serial;

                                            break;
                                        }

                                    }
                                    vmol.Size = Size;
                                    vmol.Folder = 1;
                                    if (vmol.AddOtherAsSale())
                                    {
                                        SellViewMessage = $"'{vmol.Name}' successfully added";
                                    }
                                    else
                                    {
                                        SellViewMessage = $"'{vmol.Name}' not added";
                                    }
                                }
                                else
                                {
                                    SellViewMessage = "Drive is not in correct format";
                                }
                            }
                        }
                        else
                        {
                            SellViewMessage = "Price is not in correct format";
                        }
                    }
                }
            }
            else
            {
                Name = "Name is Mandatory";
            }
        }

    }
}
