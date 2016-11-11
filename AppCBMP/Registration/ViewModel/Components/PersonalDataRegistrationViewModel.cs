using System;
using System.Collections.Generic;
using System.Linq;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using DAL;
using DAL.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;
namespace AppCBMP.Registration.ViewModel.Components
{
    public class PersonalDataRegistrationViewModel:ViewModelBase
    {
        private bool _getPerson;
        private ValidatablePerson _simplePerson;
        private Person _currentlyRegisteredPerson;
        private readonly UnitOfWork _unitOfWork;
        private readonly List<Person> _persons;
        public RelayCommand AddPersonCommand { get; set; }

        public PersonalDataRegistrationViewModel()
        {
            _unitOfWork = new UnitOfWork(new AppDataContext());
            FirstNames = _unitOfWork.FirstName.GetAllNames().ToList();
            _simplePerson = new ValidatablePerson();
            _currentlyRegisteredPerson = new Person();
            _getPerson = true;
            AddPersonCommand=new RelayCommand(AddPerson,CanAdd);
            _persons= new List<Person>(_unitOfWork.Person.GetAll());
        }

        public ValidatablePerson SimplePerson
        {
            get
            {
                if (string.IsNullOrEmpty(_simplePerson.Pesel)
                    || _getPerson == false
                    || _simplePerson.Pesel.Length != 11)
                    return _simplePerson;
                _getPerson = false;
                GetPerson(_simplePerson);
                return _simplePerson;
            }
            set
            {
                Set(ref _simplePerson, value);
                _getPerson = true;
            }
        }
        
        public List<FirstName> FirstNames { get; private set; }

        private Person CurrentlyRegisteredPerson
        {
            get { return _currentlyRegisteredPerson; }
            set { Set(ref _currentlyRegisteredPerson, value); }
        }

        private void GetPerson(ValidatablePerson person)
        {
            //if (_persons.Any(p => p.Pesel == person.Pesel))
              //  return _persons.First(p => p.Pesel == person.Pesel);
//            if (!_unitOfWork.Person.CheckIfExists(person.Pesel))
//                return;
//            CurrentlyRegisteredPerson = _unitOfWork.Person.GetPerson(person.Pesel);
//            ComplexPersonToSimplePerson(CurrentlyRegisteredPerson, SimplePerson);
        }

        private void ComplexPersonToSimplePerson(Person source, ValidatablePerson target)
        {
            target.Id = source.Id;
            target.Pesel = source.Pesel;
            target.FirstName = _unitOfWork.FirstName.GetName(source.FirstNameId).Name;
            target.LastName = source.LastName;
            target.BirthPlace = source.BirthPlace;
            target.PostCode = source.PostCode;
            target.City = source.City;
            target.Street = source.Street;
            target.HouseNumber = source.HouseNumber;
            target.Education = source.Education;
            target.Phone = source.PhoneNumber.ToString();
            AddPersonCommand.RaiseCanExecuteChanged();
        }

        private void SimplePersonToComplexPerson(ValidatablePerson source, Person target)
        {
            target.Id = source.Id;
            if (!_unitOfWork.FirstName.CheckIfExists(source.FirstName))
            {
                _unitOfWork.FirstName.Add(new FirstName() {Name = source.FirstName});
            }
            FirstName firstName = _unitOfWork.FirstName.GetName(source.FirstName);
            target.FirstNameId = firstName.Id;
            target.FirstName = firstName;
            target.Pesel = source.Pesel;
            target.LastName = source.LastName;
            target.BirthPlace = source.BirthPlace;
            target.PostCode = source.PostCode;
            target.City = source.City;
            target.Street = source.Street;
            target.HouseNumber = source.HouseNumber;
            target.Education = source.Education;
            target.PhoneNumber = Convert.ToInt64(source.Phone);
        }

        private void AddPerson()
        {
            if (string.IsNullOrEmpty(SimplePerson.Pesel)
                && string.IsNullOrEmpty(SimplePerson.FirstName)
                && string.IsNullOrEmpty(SimplePerson.LastName))
                return;
            SimplePersonToComplexPerson(_simplePerson, _currentlyRegisteredPerson);
            _currentlyRegisteredPerson = _unitOfWork.Person.AddOrUpdate(_currentlyRegisteredPerson);
            Messenger.Default.Send(_currentlyRegisteredPerson);
            Messenger.Default.Send(RegistrationNavigationEnum.CompanyAndReferralViewModel);
            _currentlyRegisteredPerson = new Person();
            _simplePerson = new ValidatablePerson();
        }

        private void ClearPersons()
        {
            SimplePerson=new ValidatablePerson();
            CurrentlyRegisteredPerson=new Person(); 
        }

        private bool CanAdd()
        {
            return !SimplePerson.HasErrors;
        }
    }
}