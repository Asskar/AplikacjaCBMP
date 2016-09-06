using System;
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
        private ObservableCollection<Person> _persons;
        private ObservableCollection<Company> _companies;
        private ObservableCollection<Referral> _referrals;
        private ObservableCollection<Position> _positions;

        public Registration(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _currentlyRegisteredPerson = new Person
            {
                Companies = new ObservableCollection<Company>(),
                Refrrals = new ObservableCollection<Referral>(),
                Services = new ObservableCollection<Service>()
            };
            Persons = new ObservableCollection<Person>();
            Companies = new ObservableCollection<Company>(_unitOfWork.Company.GetAllCompanies());
            Positions = new ObservableCollection<Position>(_unitOfWork.Position.GetAllPositions());
        }


        public Person CurrentlyRegisteredPerson
        {
            get { return _currentlyRegisteredPerson; }
            set { Set(() => CurrentlyRegisteredPerson, ref _currentlyRegisteredPerson, value); }
        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { Set(() => Persons, ref _persons, value); }
        }
        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set { Set(() => Companies, ref _companies, value); }
        }
        public ObservableCollection<Referral> Referrals
        {
            get { return _referrals; }
            set { Set(() => Referrals, ref _referrals, value); }
        }
        public ObservableCollection<Position> Positions
        {
            get { return _positions; }
            set { Set(() => Positions, ref _positions, value); }
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
                if (PeselTxtField.Length == 11 && _unitOfWork.Person.CheckIfPersonExists(_peselTxtField))
                    CurrentlyRegisteredPerson = _unitOfWork.Person.GetPerson(PeselTxtField);
                CurrentlyRegisteredPerson.Pesel = PeselTxtField;

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
                        Companies =
                            new ObservableCollection<Company>(_unitOfWork.Company.GetFilterdCompanies(value));
                    else if (value == string.Empty)
                        Companies =
                            new ObservableCollection<Company>(_unitOfWork.Company.GetAllCompanies());
                }
                else
                    Companies = new ObservableCollection<Company>(_unitOfWork.Company.GetAllCompanies());
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
                        Referrals =
                            new ObservableCollection<Referral>(_unitOfWork.Referral.GetFilterdReferrals(value));
                    else if (value == string.Empty)
                        Referrals =
                            new ObservableCollection<Referral>(_unitOfWork.Referral.GetAllReferrals());
                }
                else
                    Referrals = new ObservableCollection<Referral>(_unitOfWork.Referral.GetAllReferrals());
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
                            new ObservableCollection<Position>(_unitOfWork.Position.GetFilterdPositions(value));
                    else if (value == string.Empty)
                        Positions =
                            new ObservableCollection<Position>(_unitOfWork.Position.GetAllPositions());
                }
                else
                    Positions = new ObservableCollection<Position>(_unitOfWork.Position.GetAllPositions());
                Set(() => PositionTxtField, ref _positionTxtField, value);
            }
        }

        private void AddOrSelectPerson()
        {
            CurrentlyRegisteredPerson = _unitOfWork.Person.Add(CurrentlyRegisteredPerson);
            Persons.Add(CurrentlyRegisteredPerson);
            _unitOfWork.Complete();
        }

        private void AddNewCompanyForPerson()
        {
            CurrentlyRegisteredPerson.Companies =
                _unitOfWork.Person.GetPersonCompanies(CurrentlyRegisteredPerson);
            CurrentlyRegisteredPerson.Companies.Add(
                _unitOfWork.Company.AddCompany(new Company() { Name = CompanyTxtField }));
        }

        private void AddNewReferralForPerson()
        {
            CurrentlyRegisteredPerson.Refrrals =
               _unitOfWork.Person.GetPersonReferrals(CurrentlyRegisteredPerson);
            CurrentlyRegisteredPerson.Refrrals.Add(
                _unitOfWork.Referral.AddReferral(new Referral() { Name = ReferralTxtField }));
        }

        private void AddNewServiceForPerson()
        {
            _currentlyRegisteredPerson.Services =
                _unitOfWork.Person.GetPersonServices(_currentlyRegisteredPerson);
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

            CurrentlyRegisteredPerson = new Person();

        }
    }
}