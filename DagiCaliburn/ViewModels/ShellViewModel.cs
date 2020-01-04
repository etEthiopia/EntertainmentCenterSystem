using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            Persons.Add(new PersonModel {FirstName = "Abiy", LastName= "Ahmed" });
            Persons.Add(new PersonModel { FirstName = "Meles", LastName = "Zenawi" });
            Persons.Add(new PersonModel { FirstName = "Hailemariam", LastName = "Desalegn" });
            
        }

        private string _firstName = "Dagmawi";
        private string _lastName = "Negussu";
        private PersonModel _selectedPerson;
        private BindableCollection<PersonModel> _persons =
        new BindableCollection<PersonModel>();


        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string FullName {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public PersonModel SelectedPerson {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }

        public BindableCollection<PersonModel> Persons {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
            }
        }

        public bool CanClearText(string firstName, string lastName) {
            if(String.IsNullOrWhiteSpace(firstName) && String.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ClearText(string firstName, string lastName)
        {
            FirstName = "";
            LastName = "";
        }
    }
}
