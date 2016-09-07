using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppCBMP.DAL.Persistence;
using GalaSoft.MvvmLight;

namespace AppCBMP.Model
{
    public class Registration : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;
        private string _companyTxtField;
        private string _referralTxtField;
        private string _positionTxtField;
        private string _peselTxtField;
        private string _typeOfExamination;
        private Person _currentlyRegisteredPerson;
        private List<Person> _persons;
        private List<Company> _companies;
        private List<Referral> _referrals;
        private List<Position> _positions;
        private List<Position> _currentlyRegisteredPersonPositions;

        public Registration(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _currentlyRegisteredPerson = new Person
            {
                Companies = new List<Company>(),
                Refrrals = new List<Referral>(),
                Services = new List<Service>()
            };
            _persons = new List<Person>();
            _companies = new List<Company>(_unitOfWork.Company.GetAllCompanies());
            _positions = new List<Position>(_unitOfWork.Position.GetAllPositions());
        }


        public Person CurrentlyRegisteredPerson
        {
            get { return _currentlyRegisteredPerson; }
            set { Set(() => CurrentlyRegisteredPerson, ref _currentlyRegisteredPerson, value); }
        }

        public List<Person> Persons
        {
            get { return _persons; }
            set { Set(() => Persons, ref _persons, value); }
        }
        public List<Company> Companies
        {
            get { return _companies; }
            set { Set(() => Companies, ref _companies, value); }
        }
        public List<Referral> Referrals
        {
            get { return _referrals; }
            set { Set(() => Referrals, ref _referrals, value); }
        }
        public List<Position> Positions
        {
            get { return _positions; }
            set { Set(() => Positions, ref _positions, value); }
        }
        public List<Position> CurrentlyRegisteredPersonPositions
        {
            get { return _currentlyRegisteredPersonPositions; }
            set { Set(() => CurrentlyRegisteredPersonPositions, ref _currentlyRegisteredPersonPositions, value); }
        }


        public string TypeOfExamination
        {
            get { return _typeOfExamination; }
            set { Set(() => TypeOfExamination, ref _typeOfExamination, value); }
        }
        public string PeselTxtField
        {
            get { return _peselTxtField; }
            set
            {
                Set(() => PeselTxtField, ref _peselTxtField, value);
                if (_peselTxtField.Length == 11 && _unitOfWork.Person.CheckIfPersonExists(_peselTxtField))
                    _currentlyRegisteredPerson = _unitOfWork.Person.GetPerson(PeselTxtField);
                _currentlyRegisteredPerson.Pesel = PeselTxtField;

            }
        }

        public string CompanyTxtField
        {
            get { return _companyTxtField; }
            set
            {
                if (_companyTxtField != null)
                {
                    if (value.Length == _companyTxtField.Length + 1
                        || value.Length == _companyTxtField.Length - 1)
                        _companies =
                            _unitOfWork.Company.GetFilterdCompanies(value).ToList();
                    else if (value == string.Empty)
                        _companies =
                            _unitOfWork.Company.GetAllCompanies().
                                ToList();
                }
                else
                    _companies = _unitOfWork.Company.GetAllCompanies().ToList();
                Set(() => CompanyTxtField, ref _companyTxtField, value);
            }
        }

        public string ReferralTxtField
        {
            get { return _referralTxtField; }
            set
            {
                if (_referralTxtField != null)
                {
                    if (value.Length == _referralTxtField.Length + 1
                        || value.Length == _referralTxtField.Length - 1)
                        _referrals =
                           _unitOfWork.Referral.GetFilterdReferrals(value).ToList();
                    else if (value == string.Empty)
                        _referrals =
                           _unitOfWork.Referral.GetAllReferrals().ToList();
                }
                else
                    _referrals = _unitOfWork.Referral.GetAllReferrals().ToList();
                Set(() => ReferralTxtField, ref _referralTxtField, value);
            }
        }
        public string PositionTxtField
        {
            get { return _positionTxtField; }
            set
            {
                if (_positionTxtField != null)
                {
                    if (value.Length == _positionTxtField.Length + 1
                        || value.Length == _positionTxtField.Length - 1)
                        Positions =
                            _unitOfWork.Position.GetFilterdPositions(value).ToList();
                    else if (value == string.Empty)
                        Positions =
                            _unitOfWork.Position.GetAllPositions().ToList();
                }
                else
                    _unitOfWork.Position.GetAllPositions().ToList();
                Set(() => PositionTxtField, ref _positionTxtField, value);
            }
        }

        private void AddOrSelectPerson()
        {
            _currentlyRegisteredPerson = _unitOfWork.Person.Add(CurrentlyRegisteredPerson);
            _persons.Add(CurrentlyRegisteredPerson);
            _unitOfWork.Complete();
        }

        private void AddNewCompanyForPerson()
        {
            CurrentlyRegisteredPerson.Companies =
                _unitOfWork.Person.GetPersonCompanies(CurrentlyRegisteredPerson).ToList();
            CurrentlyRegisteredPerson.Companies.Add(
                _unitOfWork.Company.AddCompany(new Company() { Name = CompanyTxtField }));
        }

        private void AddNewReferralForPerson()
        {
            CurrentlyRegisteredPerson.Refrrals =
               _unitOfWork.Person.GetPersonReferrals(CurrentlyRegisteredPerson).ToList();
            CurrentlyRegisteredPerson.Refrrals.Add(
                _unitOfWork.Referral.AddReferral(new Referral() { Name = ReferralTxtField }));
        }

        private void AddNewServiceForPerson()
        {
            _currentlyRegisteredPerson.Services =
                _unitOfWork.Person.GetPersonServices(_currentlyRegisteredPerson).ToList();
            Company company = _currentlyRegisteredPerson.Companies.First(c => c.Name == CompanyTxtField);
            Referral referral = _currentlyRegisteredPerson.Refrrals.First(r => r.Name == ReferralTxtField);
            PsychologicalExaminationType pEType =
                _unitOfWork.PsychologicalExamination.GetExaminationType(TypeOfExamination);
            PsychologicalExamination pe = new PsychologicalExamination()
            {
                DateTimeOfService = DateTime.Today.Date,
                Person = _currentlyRegisteredPerson,
                PersonId = _currentlyRegisteredPerson.Id,
                Company = company,
                CompanyId = company.Id,
                Referral = referral,
                ReferralId = referral.Id,
                Positions = Positions,
                Type = pEType,
                TypeId = pEType.Id
            };
            _currentlyRegisteredPerson.Services.Add(pe);
        }
        public void CompleteRegistration()
        {
            AddOrSelectPerson();

            AddNewCompanyForPerson();

            AddNewReferralForPerson();

            AddNewServiceForPerson();

            _unitOfWork.Complete();

            _currentlyRegisteredPerson = new Person
            {
                Companies = new List<Company>(),
                Refrrals = new List<Referral>(),
                Services = new List<Service>()
            };

        }
    }
}