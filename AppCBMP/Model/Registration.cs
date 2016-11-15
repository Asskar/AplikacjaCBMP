using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DAL.Persistence;
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
        private Psychologist _currentPsychologist;
        private PsychologicalServiceType _currentlySelectedType;
        private PsychologicalService _psychologicalService;
        private List<Company> _companies;
        private List<Position> _positions;
        private List<Referral> _referrals;
        private List<PsychologicalServiceType> _psychologicalServiceTypes;
        private List<Psychologist> _psychologists; 
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
            _psychologicalServiceTypes= new List<PsychologicalServiceType>(_unitOfWork.PsychologicalServiceTypes.GetAllTypes());
            _psychologists= new List<Psychologist>(_unitOfWork.Psychologist.GetAll());
            //_firstNames= new List<FirstName>(_unitOfWork.FirstName.GetAllNames());
        }

        public string PeselTxtField
        {
            get { return _peselTxtField; }
            set
            {
                Set(() => PeselTxtField, ref _peselTxtField, value);
                //if (_peselTxtField.Length == 11
                //    && _unitOfWork.Persons.CheckIfExists(_peselTxtField))
               //     _currentlyRegisteredPerson = _unitOfWork.Persons.GetPerson(PeselTxtField);
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
                        || value.Length == _positionTxtField.Length - 1
                        || value == string.Empty))
                    UpdatePositionsCollection(value);
                Set(() => PositionTxtField, ref _positionTxtField, value);
            }
        }

        public Person CurrentlyRegisteredPerson
        {
            get { return _currentlyRegisteredPerson; }
            set { Set(() => CurrentlyRegisteredPerson, ref _currentlyRegisteredPerson, value); }
        }
        public Psychologist CurrentPsychologist
        {
            get { return _currentPsychologist; }
            set { Set(() => CurrentPsychologist, ref _currentPsychologist, value); }
        }
        public PsychologicalServiceType CurrentlySelectedType
        {
            get { return _currentlySelectedType;}
            set { Set(() => CurrentlySelectedType, ref _currentlySelectedType, value); }
        }
        public PsychologicalService PsychologicalService
        {
            get { return _psychologicalService; }
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
        public List<PsychologicalServiceType> PsychologicalServiceTypes
        {
            get { return _psychologicalServiceTypes; }
            set { Set(() => PsychologicalServiceTypes, ref _psychologicalServiceTypes, value); }
        }

        public List<Psychologist> Psychologists
        {
            get { return _psychologists; }
            set { Set(() => Psychologists, ref _psychologists, value); }
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
            RaisePropertyChanged(()=>Positions);
        }

        private void UpdateCompaniesCollection(string value)
        {
            _companies = _unitOfWork.Company.GetFilterdCompanies(value).
                ToList();
            RaisePropertyChanged(() => Companies);
        }

        private void UpdateReferralsCollection(string value)
        {
            _referrals = _unitOfWork.Referral.GetFilterdReferrals(value).
                ToList();
            RaisePropertyChanged(() => Referrals);
        }

        private void ClearPersondata()
        {
            _currentlyRegisteredPerson = new Person();
            _currentlySelectedPositions = new ObservableCollection<Position>();
            _psychologicalService = new PsychologicalService();
            _companies = _unitOfWork.Company.GetAllCompanies().
                ToList();
            _referrals = _unitOfWork.Referral.GetAllReferrals().
                ToList();
            _positions = _unitOfWork.Position.GetAllPositions().
                ToList();
            _companyTxtField = string.Empty;
            _positionTxtField = string.Empty;
            _referralTxtField = string.Empty;
            _peselTxtField = string.Empty;
            RaisePropertyChanged(()=>Companies);
            RaisePropertyChanged(()=>Referrals);
            RaisePropertyChanged(()=>Positions);
            RaisePropertyChanged(() => CurrentlyRegisteredPerson);
            RaisePropertyChanged(() => CurrentlySelectedPositions);
            RaisePropertyChanged(() => PsychologicalService);
            RaisePropertyChanged(() => CompanyTxtField);
            RaisePropertyChanged(() => PositionTxtField);
            RaisePropertyChanged(() => ReferralTxtField);
            RaisePropertyChanged(() => PeselTxtField);
        }

        public void SetPerson()
        {
            
        }

        public void CompleteRegistration()
        {
            _currentlySelectedCompany = _unitOfWork.Company.SelectOrAdd(new Company {Name = _companyTxtField});
            _currentlySelectedReferral = _unitOfWork.Referral.SelectOrAdd(new Referral {Name = _referralTxtField});
            //_currentlyRegisteredPerson = _unitOfWork.Persons.SelectOrAdd(_currentlyRegisteredPerson);
            _psychologicalService = new PsychologicalService
            {
                DateTimeOfService = DateTime.Now.Date,
                Person = _currentlyRegisteredPerson,
                PersonId = _currentlyRegisteredPerson.Id,
                Company = _currentlySelectedCompany,
                CompanyId = _currentlySelectedCompany.Id,
                Referral = _currentlySelectedReferral,
                ReferralId = _currentlySelectedReferral.Id,
                Positions = _currentlySelectedPositions,
                PsychologicalServiceType = _currentlySelectedType,
                PsychologicalServiceTypeId = _currentlySelectedType.Id,
                //Psychologist = _currentPsychologist,
                //PsychologistId = _currentPsychologist.Id,
//                Number = _unitOfWork.PsychologicalService.GetLastNumber(_currentPsychologist.Id, CurrentlySelectedType.Id, DateTime.Now.Date)
            };


            //todo
            //Kroki dla ustawienia numeru
            //sprawdz psychologa
            //sprawdz rejestr
            //sprawdz miesiąc i rok
            //sprawdz ostaniu numer dla tego miesiąca roku
            //dodaj do numeru jeden
            //zapisz numer jako psycholog/rejestr/numer/miesiac/rok/lokalizacja
            
            _unitOfWork.PsychologicalService.Add(_psychologicalService);
            _unitOfWork.Complete();
            _persons.Add(_currentlyRegisteredPerson);

            ClearPersondata();
        }

        public void AddNewPositionToDb(string positionName)
        {
            Position p = _unitOfWork.Position.SelectOrAdd(new Position() {Name = positionName});
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