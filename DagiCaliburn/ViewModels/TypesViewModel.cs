﻿using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static DagiCaliburn.Models.TypeModel;

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
            SettingsViewModel.tvm.DataType = TypesViewModel.tmlClicked.DataType;
            SettingsViewModel.tvm.CurrentType = SettingsViewModel.tvm.DataType;
            SettingsViewModel.tvm.ChangeType();
            if (SettingsViewModel.tvm.DataType == "Audio")
            {
                SettingsViewModel.tvm.AudioAlbumPrice = TypeModel.GetAlbumPrice(SettingsViewModel.tvm.idd).ToString();
                SettingsViewModel.tvm.AudioPricePerGB = TypeModel.GetGBPrice(SettingsViewModel.tvm.idd).ToString();
            }
            else if (SettingsViewModel.tvm.DataType == "Others")
            {
                String[] OBs = { SettingsViewModel.tvm.OGB1, SettingsViewModel.tvm.OGB2, SettingsViewModel.tvm.OGB3,
                SettingsViewModel.tvm.OGB4,SettingsViewModel.tvm.OGB5};

                String[] OPs = { SettingsViewModel.tvm.OBIRR1, SettingsViewModel.tvm.OBIRR2, SettingsViewModel.tvm.OBIRR3,
                SettingsViewModel.tvm.OBIRR4, SettingsViewModel.tvm.OBIRR5};

                List<OtherPrice> obps = TypeModel.GetOthersPrice(SettingsViewModel.tvm.idd);
                for (int i = 0; i < obps.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            SettingsViewModel.tvm.OGB1 = obps[i].OtherGB;
                            SettingsViewModel.tvm.OBIRR1 = obps[i].OtherGBPrice;
                            break;
                        case 1:
                            SettingsViewModel.tvm.OGB2 = obps[i].OtherGB;
                            SettingsViewModel.tvm.OBIRR2 = obps[i].OtherGBPrice;
                            break;
                        case 2:
                            SettingsViewModel.tvm.OGB3 = obps[i].OtherGB;
                            SettingsViewModel.tvm.OBIRR3 = obps[i].OtherGBPrice;
                            break;
                        case 3:
                            SettingsViewModel.tvm.OGB4 = obps[i].OtherGB;
                            SettingsViewModel.tvm.OBIRR4 = obps[i].OtherGBPrice;
                            break;
                        case 4:
                            SettingsViewModel.tvm.OGB5 = obps[i].OtherGB;
                            SettingsViewModel.tvm.OBIRR5 = obps[i].OtherGBPrice;
                            break;
                    }
                }
            }
            SettingsViewModel.tvm.TypesIsVisible = false;
            SettingsViewModel.tvm.Edit = true;
            SettingsViewModel.settingsvm.AdType();

        }

        public void TypeSelected(string id)
        {
            //MessageBox.Show($"{id} Type");
            int idd = 0;
            int.TryParse(id, out idd);
            if (idd != 0)
            {
                tmlClicked = TypeModel.GetTypeToFile(idd);
                SettingsViewModel.tvm.idd = TypesViewModel.tmlClicked.Id;
                SettingsViewModel.tvm.Price = TypesViewModel.tmlClicked.Price.ToString();
                SettingsViewModel.tvm.Name = TypesViewModel.tmlClicked.Name;
                SettingsViewModel.tvm.Reference = TypesViewModel.tmlClicked.Refrence;
                SettingsViewModel.tvm.DataType = TypesViewModel.tmlClicked.DataType;
                SettingsViewModel.tvm.Initial = TypesViewModel.tmlClicked.Icon;
                if (SettingsViewModel.tvm.DataType == "Audio")
                {
                    SettingsViewModel.tvm.AudioAlbumPrice = TypeModel.GetAlbumPrice(idd).ToString();
                    SettingsViewModel.tvm.AudioPricePerGB = TypeModel.GetGBPrice(idd).ToString();
                }
                
                
                SettingsViewModel.tvm.Dirs = new BindableCollection<Dir>(TypeModel.GetDirs(TypesViewModel.tmlClicked.Id));
                SettingsViewModel.tvm.TypesIsVisible = false;
                SettingsViewModel.settingsvm.ShowType();

            }

        }

        public void AddType()
        {
            //tmlClicked = new TypeModel();
            SettingsViewModel.tvm.Edit = false;
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
