using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class TypeViewModel : Screen
    {
        private bool _typesIsVisible = true;
        private bool _successIsVisible;
        public bool _errorC;
        private bool _tForm = true;
        private String _name = "";
        private String _editIcon = "";
        private String _price = "";
        private string _reference = "";
        private string _currentType = "";
        private string _audioAlbumPrice = "";
        private string _audioPricePerGB = "";
        private bool _audioIsVisible;
        private bool _othersIsVisible;
        private bool _priceIsVisible;
        private bool _othersPriceIsVisible;
        private string _ogb1;
        private string _ogb2;
        private string _ogb3;
        private string _ogb4;
        private string _ogb5;
        private string _obirr1;
        private string _obirr2;
        private string _obirr3;
        private string _obirr4;
        private string _obirr5;
        private string _iconC = "";
        private string _nameC = "";
        private string _priceC = "";
        private string _refC = "";
        private string _typeC = "";
        private BindableCollection<Dir> _dirs = new BindableCollection<Dir>();
        private BindableCollection<Dir> _dirsC = new BindableCollection<Dir>();
        private BindableCollection<String> _types = new BindableCollection<string>();
        private int[] OGBS = new int[5];
        private float[] OBIRRS = new float[5];

        public BindableCollection<string> FileTypes
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                NotifyOfPropertyChange(() => FileTypes);
            }
        }

        public BindableCollection<Dir> Dirs
        {
            get
            {
                return _dirs;
            }
            set
            {
                _dirs = value;
                NotifyOfPropertyChange(() => Dirs);
            }
        }

        public BindableCollection<Dir> DirsC
        {
            get
            {
                return _dirsC;
            }
            set
            {
                _dirsC = value;
                NotifyOfPropertyChange(() => DirsC);
            }
        }

        public bool TypesIsVisible
        {
            get
            {
                return _typesIsVisible;
            }
            set
            {
                _typesIsVisible = value;
                NotifyOfPropertyChange(() => TypesIsVisible);
            }
        }

        public bool AudioIsVisible
        {
            get
            {
                return _audioIsVisible;
            }
            set
            {
                _audioIsVisible = value;
                NotifyOfPropertyChange(() => AudioIsVisible);
            }
        }

        public bool OthersIsVisible
        {
            get
            {
                return _othersIsVisible;
            }
            set
            {
                _othersIsVisible = value;
                NotifyOfPropertyChange(() => OthersIsVisible);
            }
        }

        public bool TFormIsVisible
        {
            get
            {
                return _tForm;
            }
            set
            {
                _tForm = value;
                NotifyOfPropertyChange(() => TFormIsVisible);
            }
        }

        public bool ERRORIsVisible
        {
            get
            {
                return _errorC;
            }
            set
            {
                _errorC = value;
                NotifyOfPropertyChange(() => ERRORIsVisible);
            }
        }

        public bool PriceIsVisible
        {
            get
            {
                return _priceIsVisible;
            }
            set
            {
                _priceIsVisible = value;
                NotifyOfPropertyChange(() => PriceIsVisible);
            }
        }

        public bool OthersPriceIsVisible
        {
            get
            {
                return _othersPriceIsVisible;
            }
            set
            {
                _othersPriceIsVisible = value;
                NotifyOfPropertyChange(() => OthersPriceIsVisible);
            }
        }

        public bool SuccessIsVisible
        {
            get
            {
                return _successIsVisible;
            }
            set
            {
                _successIsVisible = value;
                NotifyOfPropertyChange(() => SuccessIsVisible);
            }
        }

        public string IconC
        {
            get
            {
                return _iconC;
            }
            set
            {
                _iconC = value;
                NotifyOfPropertyChange(() => IconC);
            }
        }

        public string NameC
        {
            get
            {
                return _nameC;
            }
            set
            {
                _nameC = value;
                NotifyOfPropertyChange(() => NameC);
            }
        }

        public string PriceC
        {
            get
            {
                return _priceC;
            }
            set
            {
                _priceC = value;
                NotifyOfPropertyChange(() => PriceC);
            }
        }

        public string DataType { get; set; }

        public string RefC
        {
            get
            {
                return _refC;
            }
            set
            {
                _refC = value;
                NotifyOfPropertyChange(() => RefC);
            }
        }

        public string TypeC
        {
            get
            {
                return _typeC;
            }
            set
            {
                _typeC = value;
                NotifyOfPropertyChange(() => TypeC);
            }
        }

        public String Price
        {   get
            {
                return _price;
            }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        public String EditIcon
        {
            get
            {
                return _editIcon;
            }
            set
            {
                _editIcon = value;

                NotifyOfPropertyChange(() => EditIcon);
                NotifyOfPropertyChange(() => Initial);
            }
        }

        public string AudioAlbumPrice
        {
            get
            {
                return _audioAlbumPrice;
            }
            set
            {
                _audioAlbumPrice = value;
                NotifyOfPropertyChange(() => AudioAlbumPrice);
            }
        }

        public string AudioPricePerGB
        {
            get
            {
                return _audioPricePerGB;
            }
            set
            {
                _audioPricePerGB = value;
                NotifyOfPropertyChange(() => AudioPricePerGB);
            }
        }

        public String Initial
        {
            get
            {
                return _editIcon;
            }
            set
            {
                
                    _editIcon = value.ToUpper();
                
                NotifyOfPropertyChange(() => Initial);
                NotifyOfPropertyChange(() => EditIcon);
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                TraceName();
                /*
                if (_name.Length > 0)
                {
                    string inn = "";
                    EditIcon = "";

                    String[] ic = Name.Split(' ');
                    for (int i = 0; i < ic.Length; i++)
                    {
                        EditIcon += ic[i][0];
                    }

                }
                */
                NotifyOfPropertyChange(() => EditIcon);
            }
        }

        public string Reference
        {
            get
            {
                return _reference;
            }
            set
            {
                _reference = value;
                NotifyOfPropertyChange(() => Reference);
            }
        }

        public string CurrentType
        {
            get
            {
                return _currentType;
            }
            set
            {
                _currentType = value;
                ChangeType();
                NotifyOfPropertyChange(() => CurrentType);

            }
        }

        public string OGB1
        {
            get
            {
                return _ogb1;
            }
            set
            {
                _ogb1 = value;
                NotifyOfPropertyChange(() => OGB1);
                int gbb = 0;
                if(value.Length > 0)
                {
                    if(!int.TryParse(value, out gbb))
                    {
                        gbb = -1;
                    }
                    
                }
                
                OGBS[0] = gbb;
            }
        }

        public string OGB2
        {
            get
            {
                return _ogb2;
            }
            set
            {
                _ogb2 = value;
                NotifyOfPropertyChange(() => OGB2);
                int gbb = 0;
                if (value.Length > 0)
                {
                    if (!int.TryParse(value, out gbb))
                    {
                        gbb = -1;
                    }

                }

                OGBS[1] = gbb;
            }
        }

        public string OGB3
        {
            get
            {
                return _ogb3;
            }
            set
            {
                _ogb3 = value;
                NotifyOfPropertyChange(() => OGB3);
                int gbb = 0;
                if (value.Length > 0)
                {
                    if (!int.TryParse(value, out gbb))
                    {
                        gbb = -1;
                    }

                }

                OGBS[2] = gbb;
            }
        }

        public string OGB4
        {
            get
            {
                return _ogb4;
            }
            set
            {
                _ogb4 = value;
                NotifyOfPropertyChange(() => OGB4);
                int gbb = 0;
                if (value.Length > 0)
                {
                    if (!int.TryParse(value, out gbb))
                    {
                        gbb = -1;
                    }

                }

                OGBS[3] = gbb;
            }
        }

        public string OGB5
        {
            get
            {
                return _ogb5;
            }
            set
            {
                _ogb5 = value;
                NotifyOfPropertyChange(() => OGB5);
                int gbb = 0;
                if (value.Length > 0)
                {
                    if (!int.TryParse(value, out gbb))
                    {
                        gbb = -1;
                    }

                }

                OGBS[4] = gbb;
            }
        }

        public string OBIRR1
        {
            get
            {
                return _obirr1;
            }
            set
            {
                _obirr1 = value;
                NotifyOfPropertyChange(() => OBIRR1);
                float gbb = 0.0f;
                if (value.Length > 0)
                {
                    if (!float.TryParse(value, out gbb))
                    {
                        gbb = -1f;
                    }

                }

                OBIRRS[0] = gbb;
            }
        }

        public string OBIRR2
        {
            get
            {
                return _obirr2;
            }
            set
            {
                _obirr2 = value;
                NotifyOfPropertyChange(() => OBIRR2);
                float gbb = 0.0f;
                if (value.Length > 0)
                {
                    if (!float.TryParse(value, out gbb))
                    {
                        gbb = -1f;
                    }

                }

                OBIRRS[1] = gbb;
            }
        }

        public string OBIRR3
        {
            get
            {
                return _obirr3;
            }
            set
            {
                _obirr3 = value;
                NotifyOfPropertyChange(() => OBIRR3);
                float gbb = 0.0f;
                if (value.Length > 0)
                {
                    if (!float.TryParse(value, out gbb))
                    {
                        gbb = -1f;
                    }

                }

                OBIRRS[2] = gbb;
            }
        }

        public string OBIRR4
        {
            get
            {
                return _obirr4;
            }
            set
            {
                _obirr4 = value;
                NotifyOfPropertyChange(() => OBIRR4);
                float gbb = 0.0f;
                if (value.Length > 0)
                {
                    if (!float.TryParse(value, out gbb))
                    {
                        gbb = -1f;
                    }

                }

                OBIRRS[3] = gbb;
            }
        }

        public string OBIRR5
        {
            get
            {
                return _obirr5;
            }
            set
            {
                _obirr5 = value;
                NotifyOfPropertyChange(() => OBIRR5);
                float gbb = 0.0f;
                if (value.Length > 0)
                {
                    if (!float.TryParse(value, out gbb))
                    {
                        gbb = -1f;
                    }

                }

                OBIRRS[4] = gbb;
            }
        }

        public int idd = 0;

        private TypeModel tm = new TypeModel();

        public TypeViewModel()
        {

            FileTypes.Add("Video");
            FileTypes.Add("Audio");
            FileTypes.Add("Others");
        }

        public TypeViewModel(int id)
        {

            tm = tm.GetType(id);
            Price = tm.Price.ToString();
            EditIcon = tm.Icon;
            Name = tm.Name;
            idd = id;
            
        }

        public void ChangeType()
        {
            if (CurrentType.Equals("Video"))
            {
                PriceIsVisible = true;
                AudioIsVisible = false;
                OthersIsVisible = false;
                OthersPriceIsVisible = false;
            }
            else if (CurrentType.Equals("Audio"))
            {
                PriceIsVisible = false;
                AudioIsVisible = true;
                OthersIsVisible = false;
                OthersPriceIsVisible = false;
            }
            else if (CurrentType.Equals("Others"))
            {
                PriceIsVisible = false;
                AudioIsVisible = false;
                OthersIsVisible = true;
                OthersPriceIsVisible = true;
            }
        }

        public void TraceName()
        {
            string[] names = Name.Split(' ');
            Initial = "";
            EditIcon = "";
            int leng = names.Length;
            if (names.Length <= 1)
            {
                leng = names.Length;
            }
            else
            {
                leng = 2;
            }
            for (int k = 0; k < leng; k++)
            {
                try
                {
                    
                    string kk = names[k][0].ToString();
                    if (kk.Length > 0)
                    {
                        
                        Initial += kk.ToUpper();
                        
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Trace Nma " + e.Message);
                }
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
            TFormIsVisible = true;
            ERRORIsVisible = false;
            SuccessIsVisible = false;
            if (OthersIsVisible)
            {
                OBIRR1 = ""; OBIRR2 = ""; OBIRR3 = ""; OBIRR4 = ""; OBIRR5 = "";
                OGB1 = ""; OGB2 = ""; OGB3 = ""; OGB4 = ""; OGB5 = "";
            }
            OthersIsVisible = false;
            PriceIsVisible = false;
            OthersPriceIsVisible = false;
            if (AudioIsVisible)
            {
                AudioPricePerGB = ""; AudioAlbumPrice = "";
            }
            AudioIsVisible = false;
            TypesIsVisible = true;

            SettingsViewModel.settingsvm.backToType();
        }

        public void OKC()
        {
            ERRORIsVisible = false;
            TFormIsVisible = true;
              IconC = "";
              NameC = "";
              PriceC = "";
              RefC = "";
              TypeC = "";
            DirsC = new BindableCollection<Dir>();
    }

        public void Save()
        {
            bool eligdable = true;
            bool checkIcon = TypeModel.CheckIcon(EditIcon);
            if (!checkIcon)
            {
                IconC = $"Icon '{Initial}' is already registered !";
                eligdable = false;
            }
            bool checkName = TypeModel.CheckName(Name);
            if (!checkName)
            {
                NameC = $"Name '{Name}' is already registered !";
                eligdable = false;
            }
            if(Name.Length <= 0)
            {
                NameC = $"Name can't be Blank !";
                eligdable = false;
            }
            
            if(CurrentType.Length == 0)
            {
                TypeC = $"File Type is important !";
                eligdable = false;
                
            }
            else
            {
                if (CurrentType.Equals("Video"))
                {
                    float actualPrice = 0.0f;
                    bool checkPrice = float.TryParse(Price, out actualPrice);
                    if (!checkPrice)
                    {
                        PriceC = $"Price '{Price}' is not in accepted format !";
                        eligdable = false;
                    }
                }
                else if (CurrentType.Equals("Audio"))
                {
                    float actualGBPrice = 0.0f;
                    bool checkPrice = float.TryParse(AudioPricePerGB, out actualGBPrice);
                    if (!checkPrice)
                    {
                        PriceC = $"Enter a proper Price of 'Audio per GB' !";
                        eligdable = false;
                    }

                    float actualAlbumPrice = 0.0f;
                    bool checkAlbumPrice = float.TryParse(AudioAlbumPrice, out actualAlbumPrice);
                    if (!checkPrice)
                    {
                        PriceC += $"\nEnter a proper Price of 'Album Price' !";
                        eligdable = false;
                    }
                }
                else if (CurrentType.Equals("Others"))
                {
                    float actualPrice = 0.0f;
                    bool checkPrice = float.TryParse(Price, out actualPrice);
                    if (!checkPrice)
                    {
                        PriceC = $"Default Price '{Price}' is not in accepted format !";
                        eligdable = false;
                    }
                    else
                    {
                        for(int i = 0; i < 5; i++)
                        {
                            if (OGBS[i] == -1)
                            {
                                PriceC = $"Error in Price by GB at Entry {i+1} !";
                                eligdable = false;
                                break;
                            }
                            else if(OGBS[i] != 0)
                            {
                                if (OBIRRS[i] == -1f)
                                {
                                    PriceC = $"Error in Price by GB at Entry {i + 1} !";
                                    eligdable = false;
                                    break;
                                }
                                else  if(OBIRRS[i] != 0.0f)
                                {
                                    continue;
                                }
                                else
                                {
                                    PriceC = $"EError in Price by GB at Entry {i + 1} !";
                                    eligdable = false;
                                    break;
                                }
                            }
                            
                            
                        }
                    }
                }
            }

            bool checkRef = TypeModel.CheckRef(Reference);
            if (!checkRef)
            {
                RefC = $"Reference '{Reference}' is already registered !";
                eligdable = false;
            }

            foreach (string dirk in TypeModel.CheckDirs(Dirs.ToList()))
            {
                Console.WriteLine($"MATCH DIR FOUND: {dirk}");
                DirsC.Add(new Dir("The "+dirk+" is already registered !"));
                eligdable = false;
            }

            if(Reference.Length == 0 && Dirs.Count() == 0)
            {
                RefC = $"Both Reference and Directories can't be Blank !";
                eligdable = false;
            }

            if (!eligdable)
            {
                ERRORIsVisible = true;
                TFormIsVisible = false;
            }
            else
            {
                if (CurrentType.Equals("Video"))
                {
                    if(VideoModel.AddVideoType(Name, float.Parse(Price), "Video", Reference, Initial))
                    {
                        if (Dirs.Count() > 0)
                        {
                            if (VideoModel.AddDirs(Name, Dirs.ToList()))
                            {
                                SuccessIsVisible = true;
                                TFormIsVisible = false;
                                ERRORIsVisible = false;
                            }
                        }
                        else
                        {
                            SuccessIsVisible = true;
                            TFormIsVisible = false;
                            ERRORIsVisible = false;
                        }
                    }
                }
                else if (CurrentType.Equals("Audio"))
                {
                    if (Dirs.Count() > 0)
                    {
                        if (AudioModel.AddDirs(AudioModel.AddAudioType(Name, "Audio", Reference,
                            Initial, float.Parse(AudioPricePerGB), float.Parse(AudioAlbumPrice)), Dirs.ToList()))
                        {

                            SuccessIsVisible = true;
                            TFormIsVisible = false;
                            ERRORIsVisible = false;
                        }
                    }
                    else
                    {
                        if(AudioModel.AddAudioType(Name, "Audio", Reference,
                            Initial, float.Parse(AudioPricePerGB), float.Parse(AudioAlbumPrice)) > 1)
                        {
                            SuccessIsVisible = true;
                            TFormIsVisible = false;
                            ERRORIsVisible = false;
                        }
                    }
                }
                else if (CurrentType.Equals("Others"))
                {
                    if (Dirs.Count() > 0)
                    {
                        if (OthersModel.AddDirs(OthersModel.AddOthersType(Name, "Others", Reference,
                            Initial, float.Parse(Price), OGBS, OBIRRS), Dirs.ToList()))
                        {

                            SuccessIsVisible = true;
                            TFormIsVisible = false;
                            ERRORIsVisible = false;
                        }
                    }
                    else
                    {
                        if ((OthersModel.AddOthersType(Name, "Others", Reference,
                            Initial, float.Parse(Price), OGBS, OBIRRS)) > 1)
                        {
                            SuccessIsVisible = true;
                            TFormIsVisible = false;
                            ERRORIsVisible = false;
                        }
                    }
                    //MessageBox.Show($"1| {OGBS[0]}: {OBIRRS[0]} \n" +
                    //    $"2| {OGBS[1]}: {OBIRRS[1]} \n" +
                    //    $"3| {OGBS[2]}: {OBIRRS[2]} \n" +
                    //    $"4| {OGBS[3]}: {OBIRRS[3]} \n" +
                    //    $"5| {OGBS[4]}: {OBIRRS[4]} \n");
                }
            }

        }

        public void SuccessOk()
        {
            SettingsViewModel.tsvm = new TypesViewModel();
            CloseType();
        }
    }

    public class Dir
    {
        public Dir(string dir)
        {
            this.dir = dir;
        }
        public Dir()
        {

        }
        private string _dir = "";
        public string dir
        {
            get
            {
                return _dir;
            }
            set
            {
                _dir = value;
            }
        }

    }
}
