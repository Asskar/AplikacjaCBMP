using System;
using System.Collections.Generic;
using System.Linq;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using AppCBMP.Registration.ViewModel.Components.ServiceComponents;
using DAL;
using DAL.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class CompanyAndReferralViewModel:ViewModelBase
    {
        private readonly UnitOfWork _unitOfWork;
        private string _companyFilter;
        private string _referralFilter;
        private Person _person;
        private Company _currentlySelectedCompany;
        private Referral _currentlySelectedReferral;
        private PsychologicalServiceType _currentlySelectedPsychologicalServiceType;
        private List<Company> _companies;
        private List<Referral> _referrals;
        private List<PsychologicalServiceType> _psychologicalServiceTypes;

        private RelayCommand<RegistrationNavigationEnum> _navigationCommand;

        public CompanyAndReferralViewModel()
        {
            Messenger.Default.Register<Person>(this,HandleMessage);
            _unitOfWork= new UnitOfWork(new AppDataContext());
            _companies = new List<Company>(_unitOfWork.Company.GetAllCompanies().
                ToList());
            _referrals = new List<Referral>(_unitOfWork.Referral.GetAllReferrals().
                ToList());
            _psychologicalServiceTypes =
                new List<PsychologicalServiceType>(_unitOfWork.PsychologicalServiceTypes.GetAllTypes().
                    ToList());
            _navigationCommand = new RelayCommand<RegistrationNavigationEnum>(Navigation);
            _currentlySelectedReferral=new Referral();
            _currentlySelectedCompany= new Company();
        }

        public Person Person
        {
            get { return _person; }
            set { Set(ref _person, value); }
        }

        public RelayCommand<RegistrationNavigationEnum> NavigationCommand
        {
            get { return _navigationCommand; }
            set { Set(ref _navigationCommand, value); }
        }

        public Company CurrentlySelectedCompany
        {
            get { return _currentlySelectedCompany; }
            set { Set(ref _currentlySelectedCompany, value); }
        }

        public Referral CurrentlySelectedReferral
        {
            get { return _currentlySelectedReferral; }
            set { Set(ref _currentlySelectedReferral, value); }
        }

        public PsychologicalServiceType CurrentlySelectedPsychologicalServiceType
        {
            get { return _currentlySelectedPsychologicalServiceType; }
            set { Set(ref _currentlySelectedPsychologicalServiceType, value); }
        }

        public List<PsychologicalServiceType> PsychologicalServiceTypes
        {
            get { return _psychologicalServiceTypes; }
            set { Set(ref _psychologicalServiceTypes, value); }
        }

        public List<Company> Companies
        {
            get { return _companies; }
            set { Set(ref _companies, value); }
        }

        public List<Referral> Referrals
        {
            get { return _referrals; }
            set { Set(ref _referrals, value); }
        }

        public string CompanyFilter
        {
            get { return _companyFilter; }
            set
            {
                if (_companyFilter != null
                    && (value.Length == _companyFilter.Length + 1 || value.Length == _companyFilter.Length - 1))
                    UpdateCompaniesCollection(value);
                Set(ref _companyFilter, value);
            }
        }

        public string ReferralFilter
        {
            get { return _referralFilter; }
            set
            {
                if (_referralFilter != null
                    && (value.Length == _referralFilter.Length + 1 || value.Length == _referralFilter.Length - 1))
                    UpdateReferralsCollection(value);
                Set(ref _referralFilter, value);
            }
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

        private void AddCompanyReferral()
        {
            if(!string.IsNullOrEmpty(CompanyFilter))
            {
                if (!_unitOfWork.Company.CheckIfExists(CompanyFilter))
                {
                    _unitOfWork.Company.Add(CompanyFilter);
                }
                CurrentlySelectedCompany = _unitOfWork.Company.GetCompany(CompanyFilter);
            }
            if (!string.IsNullOrEmpty(ReferralFilter))
            {
                if (!_unitOfWork.Referral.CheckIfExists(ReferralFilter))
                {
                    _unitOfWork.Referral.Add(ReferralFilter);
                }
                CurrentlySelectedReferral = _unitOfWork.Referral.GetReferral(ReferralFilter);
            }
            Messenger.Default.Send(new CompanyReferralPersonMessage()
            {
                Company = CurrentlySelectedCompany,
                Referral = CurrentlySelectedReferral,
                Person = Person,
                PsychologicalServiceType = CurrentlySelectedPsychologicalServiceType
            });
        }
        private void Navigation(RegistrationNavigationEnum direction)
        {
            if (direction == RegistrationNavigationEnum.SelectedServiceViewModel)
            {
                AddCompanyReferral();
            }
            Messenger.Default.Send(direction);
        }

        private void HandleMessage(Person person)
        {
            Person = person;
        }
    }
}