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
    class SpecialsViewModel : Screen
    {
        private string _spPrice;
        private string _spName;
        private string _spExp;
        private bool _specialIsVisible = true;
        private bool _successIsVisible = false;
        private bool _existingExpiryIsVisible = false;
        private string _specialTitle = "New Special";
        private bool _deleteIsVisible = false;
        private bool _dPromptIsVisible = false;
        private SpecialModel _selectedSpecial = new SpecialModel();
        

        private BindableCollection<SpecialModel> _specials = new BindableCollection<SpecialModel>();

        private BindableCollection<string> _expiredates = new BindableCollection<string>();
        
        public SpecialsViewModel()
        {
            _expiredates.Add("1 Hour");
            _expiredates.Add("6 Hours");
            _expiredates.Add("End of the Day");
            _expiredates.Add("No Exipry Date");
            Specials = new BindableCollection<SpecialModel>(SpecialModel.GetAllSpecials());
        }

        public BindableCollection<string> ExpireDates
        {
            get
            {
                return _expiredates;
            }
            
        }

        public BindableCollection<SpecialModel> Specials
        {
            get
            {
                return _specials;
            }
            set
            {
                _specials = value;
                NotifyOfPropertyChange(() => Specials);
            }

        }

        public string SpPrice
        {
            get
            {
                return _spPrice;
            }
            set
            {
                _spPrice = value;
                NotifyOfPropertyChange(() => SpPrice);
            }
        }

        public string SpName
        {
            get
            {
                return _spName;
            }
            set
            {
                _spName = value;
                NotifyOfPropertyChange(() => SpName);
            }
        }

        public string SpExp
        {
            get
            {
                return _spExp;
            }
            set
            {
                _spExp = value;
                NotifyOfPropertyChange(() => SpExp);
            }
        }

        public string SpecialTitle
        {
            get
            {
                return _specialTitle;
            }
            set
            {
                _specialTitle = value;
                NotifyOfPropertyChange(() => SpecialTitle);
            }
        }

        public string _specialMessage = "";

        public string SpecialMessage
        {
            get
            {
                return _specialMessage;
            }
            set
            {
                _specialMessage = value;
                NotifyOfPropertyChange(() => SpecialMessage);
            }
        }

        public bool SpecialIsVisible
        {
            get
            {
                return _specialIsVisible;
            }
            set
            {
                _specialIsVisible = value;
                NotifyOfPropertyChange(() => SpecialIsVisible);
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

        public SpecialModel SelectedSpecial
        {
            get
            {
                return _selectedSpecial;
            }
            set
            {
                _selectedSpecial = value;
                NotifyOfPropertyChange(() => SelectedSpecial);
            }
        }

        public bool DeleteIsVisible
        {
            get
            {
                return _deleteIsVisible;
            }
            set
            {
                _deleteIsVisible = value;
                NotifyOfPropertyChange(() => DeleteIsVisible);
            }
        }

        public bool DPromptIsVisible
        {
            get
            {
                return _dPromptIsVisible;
            }
            set
            {
                _dPromptIsVisible = value;
                NotifyOfPropertyChange(() => DPromptIsVisible);
            }
        }

        public bool ExisitingExpiryIsVisible
        {
            get
            {
                return _existingExpiryIsVisible;
            }
            set
            {
                _existingExpiryIsVisible = value;
                NotifyOfPropertyChange(() => ExisitingExpiryIsVisible);
            }
        }

        private string ActualDate = "";
        int tp = 0;

        public void Save()
        {
            try
            {
                if (int.TryParse(SpPrice, out tp))
                {
                    if (tp != 0)
                    {
                        if(SpName.Count()> 0)
                        {
                            if(SpExp.Count() > 0)
                            {
                                DateTime dtt = DateTime.Now;
                                if(SpExp.Equals("1 Hour"))
                                {
                                     ActualDate = dtt.AddHours(1).ToString("yyyy-MM-dd H:mm:ss");
                                }
                                else if (SpExp.Equals("6 Hours"))
                                {
                                    ActualDate = dtt.AddHours(6).ToString("yyyy-MM-dd H:mm:ss");
                                }
                                else if (SpExp.Equals("End of the Day"))
                                {
                                    ActualDate = dtt.AddDays(1).ToString("yyyy-MM-dd ");
                                    ActualDate += "23:59:59";
                                }
                                else
                                {
                                    ActualDate = "NULL";
                                }
                                Console.WriteLine($"ActualDate: {SpExp}");
                                SpecialModel sm = new SpecialModel();
                                sm.Name = SpName;
                                sm.Price = tp;
                                sm.AcDate = ActualDate;
                                if (sm.AddSpecial())
                                {
                                    Specials = new BindableCollection<SpecialModel>(SpecialModel.GetAllSpecials());
                                    SpecialMessage = $"{sm.Name}\n ADDED successfully";
                                }
                                else
                                {
                                    SpecialMessage = $"{sm.Name}\n not ADDED, Try Again !";
                                }
                                SuccessIsVisible = true;
                                SpecialIsVisible = false;
                                Cancel();
                            }
                            else
                            {
                                if(SelectedSpecial.Id > 0)
                                {
                                    SpecialModel sm = new SpecialModel();
                                    sm.Name = SpName;
                                    sm.Price = tp;
                                    sm.AcDate = SelectedSpecial.AcDate;
                                    if (sm.AddSpecial())
                                    {
                                        Specials = new BindableCollection<SpecialModel>(SpecialModel.GetAllSpecials());
                                        SpecialMessage = $"{sm.Name}\n ADDED successfully";
                                    }
                                    else
                                    {
                                        SpecialMessage = $"{sm.Name}\n not ADDED, Try Again !";
                                    }
                                    SuccessIsVisible = true;
                                    SpecialIsVisible = false;
                                    SelectedSpecial = new SpecialModel();
                                    Cancel();
                                }
                            }
                            
                        }
                        else
                        {
                            SpName = "Name can not be Null";
                        }
                    }
                    else
                    {
                        SpPrice = $"Error";
                    }
                }
                else
                {
                    SpPrice = $"Error";
                }
            }
            catch (Exception convertEd)
            {
                SpPrice = $"Amount Only";
                Console.WriteLine($"Convert GBMoney {convertEd}");
            }
        }

        public void SellDClick()
        {
            SpName = SelectedSpecial.Name;
            SpPrice = SelectedSpecial.Price.ToString();
            SpExp = SelectedSpecial.AcDate;
            ExisitingExpiryIsVisible = true;
            DeleteIsVisible = true;
            SpecialTitle = "Edit Special";

        }

        public void Cancel()
        {
            SpName = "";
            SpPrice = "";
            SpExp = "";
            ExisitingExpiryIsVisible = false;
            DeleteIsVisible = false;
            SpecialTitle = "New Special";
            SelectedSpecial = new SpecialModel();
        }

        public void Delete()
        {
            DPromptIsVisible = true;
            SpecialIsVisible = false;
            SpecialMessage = $"Are you Sure to Delete\n '{SelectedSpecial.Name}'";
        }

        public void CancelDelete()
        {
            DPromptIsVisible = false;
            SpecialIsVisible = true;
            SpecialMessage = $"";

        }

        public void OkDelete()
        {
            DPromptIsVisible = false;
            SpecialIsVisible = true;
            SpecialMessage = $"";
            if (SelectedSpecial.Id > 0)
            {
                if (SelectedSpecial.Delete())
                {
                    SuccessIsVisible = true;
                    SpecialIsVisible = false;
                    SpecialMessage = "Successfully Deleted";
                }
                else
                {
                    SuccessIsVisible = true;
                    SpecialIsVisible = false;
                    SpecialMessage = "Delete not Successful";
                }
            }
            Cancel();
            Specials = new BindableCollection<SpecialModel>(SpecialModel.GetAllSpecials());
        }

        public void CloseSuccess()
        {
            SuccessIsVisible = false;
            SpecialIsVisible = true;
        }
    }
}
