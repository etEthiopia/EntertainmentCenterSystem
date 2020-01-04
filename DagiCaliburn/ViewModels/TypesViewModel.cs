using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class TypesViewModel : Conductor<Object>
    {
        public static TypeModel tmlClicked = new TypeModel();
        bool _typeIsVisible = true;
        bool _editTypeIsVisible = false;
        private BindableCollection<TypeModel> _types;
        
        public BindableCollection<TypeModel> Typpes
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                NotifyOfPropertyChange(() => Typpes);
            }
        }
        string[] Names = {
                "Series",
                "Movie",
                "Korean Series",
                "Kana Films",
                "International Music",
                "Amharic Movies",
                "Amharic Songs"
                };
        string[] Initials = { "S", "M", "KS", "KF", "IM", "AM", "AS"};

        public TypesViewModel()
        {
           Typpes = new BindableCollection<TypeModel>(TypeModel.GetAllTypes());

        }

        public bool TypesIsVisible { get {
                return _typeIsVisible;
            } set {
                _typeIsVisible = value;
                
                NotifyOfPropertyChange(() => TypesIsVisible);
            } }

        public bool EditTypeIsVisible {
            get
            {
                return _editTypeIsVisible;
            }
            set
            {
                _editTypeIsVisible = value;

                NotifyOfPropertyChange(() => EditTypeIsVisible);
            }
        }

        public static void EditType(int id) {
            
            tmlClicked = TypeModel.GetTypeToFile(id);
            SettingsViewModel.tvm.idd = TypesViewModel.tmlClicked.Id;
            SettingsViewModel.tvm.Price = TypesViewModel.tmlClicked.Price.ToString();
            SettingsViewModel.tvm.Name = TypesViewModel.tmlClicked.Name;
            SettingsViewModel.tvm.Reference = TypesViewModel.tmlClicked.Refrence;
            SettingsViewModel.tvm.EditIcon = TypesViewModel.tmlClicked.Icon;
            SettingsViewModel.tvm.Initial = TypesViewModel.tmlClicked.Icon;
            SettingsViewModel.tvm.Dirs = new BindableCollection<Dir>(TypeModel.GetDirs(TypesViewModel.tmlClicked.Id));
            SettingsViewModel.tvm.TypesIsVisible = false;
            SettingsViewModel.settingsvm.AdType();

        }

        public void AddType()
        {
            //tmlClicked = new TypeModel();
            SettingsViewModel.settingsvm.AdType();
            
        }

        

    }

    

    class Typpe
    {
        private string _name;
        private string _initial;
        private string _price;
        private string _filetype;

        public Typpe(string name,string init, string price, string ftype)
        {
            Name = name;
            Initial = init;
            Price = price;
            FileType = ftype;
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

            }
        }
        public string Initial
        {
            get
            {
                return _initial;
            }
            set
            {
                _initial = value;
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
            }
        }
        public string FileType
        {
            get
            {
                return _filetype;
            }
            set
            {
                _filetype = value;
            }
        }
    }
}
