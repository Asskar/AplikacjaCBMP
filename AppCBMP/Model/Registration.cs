using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        private PsychologicalExaminationType _examinationType;
        private Person _currentlyRegisteredPerson;
        private Company _currentlySelectedCompany;
        private Referral _currentlySelectedReferral;
        private Service _service;
        private ObservableCollection<Person> _persons;
        private List<Company> _companies;
        private List<Referral> _referrals;
        private List<Position> _positions;
        private List<PsychologicalExaminationType> _examinationTypes;
        private ObservableCollection<Position> _currentlyRegisteredPersonPositions;

        public Registration(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _currentlyRegisteredPerson = new Person
            {
                Companies = new List<Company>(),
                Refrrals = new List<Referral>(),
                Services = new List<Service>()
            };
            _persons = new ObservableCollection<Person>();
            _companies = new List<Company>(_unitOfWork.Company.GetAllCompanies());
            _positions = new List<Position>(_unitOfWork.Position.GetAllPositions());
            _examinationTypes= new List<PsychologicalExaminationType>(_unitOfWork.Service.GetAllExaminationTypes());
            _currentlyRegisteredPersonPositions= new ObservableCollection<Position>();
        }



        public Person CurrentlyRegisteredPerson
        {
            get { return _currentlyRegisteredPerson; }
            set { Set(() => CurrentlyRegisteredPerson, ref _currentlyRegisteredPerson, value); }
        }
        public Company CurrentlySelectedCompany
        {
            get { return _currentlySelectedCompany; }
            set { Set(() => CurrentlySelectedCompany, ref _currentlySelectedCompany, value); }
        }
        public Referral CurrentlySelectedReferral
        {
            get { return _currentlySelectedReferral; }
            set { Set(() => CurrentlySelectedReferral, ref _currentlySelectedReferral, value); }
        }
        public Service Service
        {
            get { return _service; }
            set { Set(() => Service, ref _service, value); }
        }
        public ObservableCollection<Person> Persons
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
        public List<PsychologicalExaminationType> ExaminationTypes
        {
            get { return _examinationTypes; }
            set { Set(() => ExaminationTypes, ref _examinationTypes, value); }
        }
        public ObservableCollection<Position> CurrentlyRegisteredPersonPositions
        {
            get { return _currentlyRegisteredPersonPositions; }
            set { Set(() => CurrentlyRegisteredPersonPositions, ref _currentlyRegisteredPersonPositions, value); }
        }


        public PsychologicalExaminationType ExaminationType
        {
            get { return _examinationType; }
            set { Set(() => ExaminationType, ref _examinationType, value); }
        }
        public string PeselTxtField
        {
            get { return _peselTxtField; }
            set
            {
                Set(() => PeselTxtField, ref _peselTxtField, value);
                if (_peselTxtField.Length == 11 && _unitOfWork.Person.CheckIfExists(_peselTxtField))
                    _currentlyRegisteredPerson = _unitOfWork.Person.GetPerson(PeselTxtField);
                _currentlyRegisteredPerson.Pesel = PeselTxtField;
                RaisePropertyChanged(()=>CurrentlyRegisteredPerson);
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
                    _positions=_unitOfWork.Position.GetAllPositions().ToList();
                Set(() => PositionTxtField, ref _positionTxtField, value);
            }
        }
        private void AddOrSelectPerson()
        {
            if(!_unitOfWork.Person.CheckIfExists(_currentlyRegisteredPerson.Pesel))
            {
                _unitOfWork.Person.Add(_currentlyRegisteredPerson);
            }
            else
            {
                _currentlyRegisteredPerson = _unitOfWork.Person.GetPerson(_currentlyRegisteredPerson.Pesel);
            }
            _currentlyRegisteredPerson.Companies.Add(_currentlySelectedCompany);
            _currentlyRegisteredPerson.Refrrals.Add(_currentlySelectedReferral);
            _currentlyRegisteredPerson.Services.Add(_service);
        }

        private void AddOrSelectCompany()
        {
            if (!_unitOfWork.Company.CheckIfExists(_companyTxtField))
            {
                _unitOfWork.Company.Add(_currentlySelectedCompany);
            }
            else
            {
                _currentlySelectedCompany = _unitOfWork.Company.GetCompany(_companyTxtField);
            }
        }

        private void AddOrSelectReferral()
        {
            if (!_unitOfWork.Referral.CheckIfExists(_companyTxtField))
            {
                _unitOfWork.Referral.Add(_currentlySelectedReferral);
            }
            else
            {
                _currentlySelectedReferral = _unitOfWork.Referral.GetReferral(_referralTxtField);
            }
        }

        private void AddService()
        {
            _service = new PsychologicalExamination()
            {
                DateTimeOfService = DateTime.Now.Date,
                Person = _currentlyRegisteredPerson,
                Company = _currentlySelectedCompany,
                Referral = _currentlySelectedReferral,
                Type = _examinationType,
                Positions = Positions
            };
        }
        private void ClearPersondata()
        {
            _currentlyRegisteredPerson = new Person
            {
                Companies = new List<Company>(),
                Refrrals = new List<Referral>(),
                Services = new List<Service>()
            };
            _currentlyRegisteredPersonPositions = new ObservableCollection<Position>();
            CompanyTxtField = string.Empty;
            PositionTxtField = string.Empty;
            ReferralTxtField = string.Empty;
            PeselTxtField = string.Empty;
            ExaminationType = new PsychologicalExaminationType();
            RaisePropertyChanged(() => CurrentlyRegisteredPerson);
            RaisePropertyChanged(() => CurrentlyRegisteredPersonPositions);
            RaisePropertyChanged(() => CompanyTxtField);
            RaisePropertyChanged(() => PositionTxtField);
            RaisePropertyChanged(() => ReferralTxtField);
            RaisePropertyChanged(() => PeselTxtField);
            RaisePropertyChanged(() => ExaminationType);
        }
        public void CompleteRegistration()
        {
            _currentlySelectedCompany = new Company() {Name = _companyTxtField};
            _currentlySelectedReferral = new Referral() {Name = _referralTxtField};

            
            AddOrSelectCompany();
            AddOrSelectReferral();
            AddService();

            AddOrSelectPerson();

            _unitOfWork.Complete();

            ClearPersondata();
        }



        public void RemovePositionFromService(Position position)
        {
            _currentlyRegisteredPersonPositions.Remove(position);
        }

        public void AddNewPositionToDb(string positionName)
        {
            Position p = new Position() {Name = positionName};
            if (!_unitOfWork.Position.CheckIfExists(positionName))
            {
                _unitOfWork.Position.Add(p);
                
            }
            else
            {
                p = _unitOfWork.Position.GetPosition(positionName);
            }
            _currentlyRegisteredPersonPositions.Add(p);
            _unitOfWork.Complete();
        }
    }
}