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
    class ProfileViewModel : Conductor<object>
    {
        private bool _editProfileIsVisible;
        private bool _profileCreatedIsVisible;
        private bool _profileIsVisible = true;
        private bool _profileNewIsVisible;
        private bool _errorIsVisible;
        private string _error = "";
        private string _name = "";
        private string _email = "";
        private string _password = "";
        private string _cpassword = "";
        private string _pname = "";
        private string _pemail = "";


        public static ProfileModel pm = new ProfileModel();

        public ProfileViewModel()
        {
            pm = pm.GetProfile();
            if (pm.EntName.Equals(""))
            {
                ProfileNewIsVisible = true;
                ProfileIsVisible = true;
                ProfileCreatedIsVisible = false;
                EditProfileIsVisible = false;
            }
            else
            {
                ProfileNewIsVisible = false;
                ProfileIsVisible = true;
                ProfileCreatedIsVisible = true;
                EditProfileIsVisible = false;
                PName = pm.EntName;
                PEmail = pm.Email;
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

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public string PEmail
        {
            get
            {
                return _pemail;
            }
            set
            {
                _pemail = value;
                NotifyOfPropertyChange(() => PEmail);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public string ConfirmPassword
                      
        {
            get
            {
                return _cpassword;
            }
            set
            {
                _cpassword = value;
                NotifyOfPropertyChange(() => ConfirmPassword);
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

        public bool EditProfileIsVisible
        {
            get
            {
                return _editProfileIsVisible;
            }
            set
            {
                _editProfileIsVisible = value;
                NotifyOfPropertyChange(() => EditProfileIsVisible);
            }
        }

        public bool ProfileCreatedIsVisible
        {
            get
            {
                return _profileCreatedIsVisible;
            }
            set
            {
                _profileCreatedIsVisible = value;
                NotifyOfPropertyChange(() => ProfileCreatedIsVisible);
            }
        }
        
        public bool ProfileNewIsVisible
        {
            get
            {
                return _profileNewIsVisible;
            }
            set
            {
                _profileNewIsVisible = value;
                NotifyOfPropertyChange(() => ProfileNewIsVisible);
            }
        }

        public bool ProfileIsVisible
        {
            get
            {
                return _profileIsVisible;
            }
            set
            {
                _profileIsVisible = value;
                NotifyOfPropertyChange(() => ProfileIsVisible);
            }
        }

        private void clearEntries()
        {
            Name = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
            Error = "";
            ErrorIsVisible = false;
        }

        public void Cancel()
        {
            clearEntries();
            EditProfileIsVisible = false;
            ProfileIsVisible = true;
        }

        public void Save()
        {
            if (Password.Length > 0 && Password == ConfirmPassword)
            {
                if (Password.Length > 6) {
                    if (Email.Length > 6 && Email.Contains('@') && Email.Contains('.'))
                    {
                        if (Name.Length > 1)
                        {
                            if(pm.SetProfile(Name, Password, Email))
                            {
                                clearEntries();
                                pm = pm.GetProfile();
                                PName = pm.EntName;
                                PEmail = pm.Email;
                                EditProfileIsVisible = false;
                                ProfileIsVisible = true;
                                ProfileCreatedIsVisible = true;
                                ProfileNewIsVisible = false;
                            }
                            else
                            {
                                ErrorIsVisible = true;
                                Error = "Error while Saving Profile";
                            }
                        }
                        else
                        {
                            ErrorIsVisible = true;
                            Error = "Name is not valid";
                        }
                    }
                    else
                    {
                        ErrorIsVisible = true;
                        Error = "Email is not valid";
                    }
                }
                else
                {
                    ErrorIsVisible = true;
                    Error = "Password Length must be 6 or above";
                }
            }
            else {
                ErrorIsVisible = true;
                Error = "Passwords doesnot match";
            }
        }

        public void CreateProfile()
        {
            ProfileIsVisible = false;
            EditProfileIsVisible = true;
        }

        public void EditProfile()
        {
            ProfileIsVisible = false;
            EditProfileIsVisible = true;
            Name = pm.EntName;
            Email = pm.Email;
            Password = pm.Password;
            ConfirmPassword = Password;
        }

    }
}
