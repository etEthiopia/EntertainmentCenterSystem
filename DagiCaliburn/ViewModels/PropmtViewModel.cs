using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class PropmtViewModel : Screen
    {
        private string _pname = "";
        private string _ppassword = "";
        private string _error = "";
        private bool _errorIsVisible;
        private int count = 3;


        public string PName
        {
            get
            {
                return _pname;
            }
            set
            {
                _pname = value;
                NotifyOfPropertyChange(() => PName);
            }
        }

        public string PPassword
        {
            get
            {
                return _ppassword;
            }
            set
            {
                _ppassword = value;
                NotifyOfPropertyChange(() => PPassword);
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                NotifyOfPropertyChange(() => Error);
            }
        }

        public bool ErrorIsVisible
        {
            get
            {
                return _errorIsVisible;
            }
            set
            {
                _errorIsVisible = value;
                NotifyOfPropertyChange(() => ErrorIsVisible);
            }
        }

        public string Pwd = "";


        public void SignIn()
        {
            if (Pwd.Equals(PPassword))
            {
                StartViewModel.startview.OkaySignIsVisible = true;
                StartViewModel.startview.HomeBtn();
            }
            else
            {
                count--;
                ErrorIsVisible = true;
                if (count == 0)
                {
                    Error = $"Wrong Entry, DONE";
                    
                }
                else
                {
                    Error = $"Wrong Entry, Try Again. {count} chances left";
                }
               
                

                
            }
        }

    }

}
