using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppCBMP.DAL.Persistence;
using GalaSoft.MvvmLight;
using Model;

namespace AppCBMP.Model
{
    public class Registration : ObservableObject
    {
        private readonly UnitOfWork _unitOfWork;

        private string _companyTxtField;
        private string _peselTxtField;
        private string _positionTxtField;
        private string _referralTxtField;
        private Person _currentlyRegisteredPerson;
        private Company _currentlySelectedCompany;
        private Referral _currentlySelectedReferral;
        private PsychologicalService _psychologicalService;
        private List<Company> _companies;
        private List<Position> _positions;
        private List<Referral> _referrals;
        private ObservableCollection<Position> _currentlySelectedPositions;
        private ObservableCollection<Person> _persons;
        
        public Registration(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _currentlyRegisteredPerson = new Person();

            _persons = new ObservableCollection<Person>();
            _companies = new List<Company>(_unitOfWork.Company.GetAllCompanies());
            _positions = new List<Position>(_unitOfWork.Position.GetAllPositions());
            _referrals = new List<Referral>(_unitOfWork.Referral.GetAllReferrals());
            _currentlySelectedPositions = new ObservableCollection<Position>();
            _psychologicalService = new PsychologicalService();
        }

        public string PeselTxtField
        {
            get { return _peselTxtField; }
            set
            {
                Set(() => PeselTxtField, ref _peselTxtField, value);
                if (_peselTxtField.Length == 11
                    && _unitOfWork.Person.CheckIfExists(_peselTxtField))
                    _currentlyRegisteredPerson = _unitOfWork.Person.GetPerson(PeselTxtField);
                _currentlyRegisteredPerson.Pesel = PeselTxtField;
                RaisePropertyChanged(() => CurrentlyRegisteredPerson);
            }
        }

        public string CompanyTxtField
        {
            get { return _companyTxtField; }
            set
            {
                if (_companyTxtField != null
                    && (value.Length == _companyTxtField.Length + 1
                        || value.Length == _companyTxtField.Length - 1))
                    UpdateCompaniesCollection(value);
                Set(() => CompanyTxtField, ref _companyTxtField, value);
            }
        }

        public string ReferralTxtField
        {
            get { return _referralTxtField; }
            set
            {
                if (_referralTxtField != null
                    && (value.Length == _referralTxtField.Length + 1
                        || value.Length == _referralTxtField.Length - 1))
                    UpdateReferralsCollection(value);
                Set(() => ReferralTxtField, ref _referralTxtField, value);
            }
        }

        public string PositionTxtField
        {
            get { return _positionTxtField; }
            set
            {
                if (_positionTxtField != null
                    && (value.Length == _positionTxtField.Length + 1
                        || value.Length == _positionTxtField.Length - 1))
                    UpdatePositionsCollection(value);
                Set(() => PositionTxtField, ref _positionTxtField, value);
            }
        }

        public Person CurrentlyRegisteredPerson
        {
            get { return _currentlyRegisteredPerson; }
            set { Set(() => CurrentlyRegisteredPerson, ref _currentlyRegisteredPerson, value); }
        }

        public PsychologicalService PsychologicalService
        {
            get { return _psychologicalService; }
            set { Set(() => PsychologicalService, ref _psychologicalService, value); }
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

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { Set(() => Persons, ref _persons, value); }
        }

        public ObservableCollection<Position> CurrentlySelectedPositions
        {
            get { return _currentlySelectedPositions; }
            set { Set(() => CurrentlySelectedPositions, ref _currentlySelectedPositions, value); }
        }

       

        private void UpdatePositionsCollection(string value)
        {
            _positions = _unitOfWork.Position.GetFilterdPositions(value).
                ToList();
        }

        private void UpdateCompaniesCollection(string value)
        {
            _companies = _unitOfWork.Company.GetFilterdCompanies(value).
                ToList();
        }

        private void UpdateReferralsCollection(string value)
        {
            _referrals = _unitOfWork.Referral.GetFilterdReferrals(value).
                ToList();
        }

        private void ClearPersondata()
        {
            _currentlyRegisteredPerson = new Person();
            _currentlySelectedPositions = new ObservableCollection<Position>();
            _psychologicalService = new PsychologicalService();
            _companyTxtField = string.Empty;
            _positionTxtField = string.Empty;
            _referralTxtField = string.Empty;
            _peselTxtField = string.Empty;
            RaisePropertyChanged(() => CurrentlyRegisteredPerson);
            RaisePropertyChanged(() => CurrentlySelectedPositions);
            RaisePropertyChanged(() => PsychologicalService);
            RaisePropertyChanged(() => CompanyTxtField);
            RaisePropertyChanged(() => PositionTxtField);
            RaisePropertyChanged(() => ReferralTxtField);
            RaisePropertyChanged(() => PeselTxtField);
        }

        public void CompleteRegistration()
        {
            _currentlySelectedCompany = _unitOfWork.Company.SelectOrAdd(new Company {Name = _companyTxtField});
            _currentlySelectedReferral = _unitOfWork.Referral.SelectOrAdd(new Referral {Name = _referralTxtField});
            _currentlyRegisteredPerson = _unitOfWork.Person.SelectOrAdd(_currentlyRegisteredPerson);
            _unitOfWork.PsychologicalService.Add(_psychologicalService = new PsychologicalService
            {
                DateTimeOfService = DateTime.Now.Date,
                Person = _currentlyRegisteredPerson,
                PersonId = _currentlyRegisteredPerson.Id,
                Company = _currentlySelectedCompany,
                CompanyId = _currentlySelectedCompany.Id,
                Referral = _currentlySelectedReferral,
                ReferralId = _currentlySelectedReferral.Id,
                Positions = _currentlySelectedPositions
            });
            _unitOfWork.Complete();
            _persons.Add(_currentlyRegisteredPerson);

            ClearPersondata();
        }

        public void AddNewPositionToDb(string positionName)
        {
            var p = _unitOfWork.Position.SelectOrAdd(new Position() {Name = positionName});
            _currentlySelectedPositions.Add(p);
        }

        public void RemovePositionFromService(Position position)
        {
            _currentlySelectedPositions.Remove(position);
        }

        public void AddPositionToService(Position position)
        {
            _currentlySelectedPositions.Add(position);
        }
       
    }
}