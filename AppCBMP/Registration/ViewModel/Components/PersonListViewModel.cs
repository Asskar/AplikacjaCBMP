using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class PersonListViewModel:ViewModelBase
    {
        private List<Person> _persons;

        public List<Person> Persons
        {
            get { return _persons; }
            set { Set(ref _persons, value); }
        }

        public PersonListViewModel()
        {
            _persons= new List<Person>();
        }
    }
}