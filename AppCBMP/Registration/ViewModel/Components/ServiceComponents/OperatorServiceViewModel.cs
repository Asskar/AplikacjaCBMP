using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using DAL;
using DAL.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;

namespace AppCBMP.Registration.ViewModel.Components.ServiceComponents
{
    public class OperatorServiceViewModel : ViewModelBase
    {

        private RelayCommand<string> _addNewPositionCommand;
        private RelayCommand<Position> _addPositionCommand;
        private RelayCommand<Position> _removePositionCommand;
        private RelayCommand<ServiceNavigationEnum> _navigateToServiceSelectorCommand;
        private RelayCommand<RegistrationNavigationEnum> _finishCommand;
        private Person _person;
        private Referral _referral;
        private Company _company;
        private Psychologist _psychologist;
        private Localization _localization;
        private PsychologicalServiceType _psychologicalServiceType;
        private PsychologicalService _psychologicalService;
        private readonly List<Position> _readOnlyPositions; 
        private ObservableCollection<Position> _positions;
        private ObservableCollection<Position> _addedPositions;
        private string _postitionsFilter;

        private readonly UnitOfWork _unitOfWork;



        public OperatorServiceViewModel()
        {
            Messenger.Default.Register<LocalizationPsychologistMessage>(this, HandleLozalizationPsychologistMessage);
            Messenger.Default.Register<CompanyReferralPersonMessage>(this, HandleCmpRefperMessage);
            _unitOfWork = new UnitOfWork(new AppDataContext());
            _readOnlyPositions = _unitOfWork.Position.GetAllPositions().
                ToList();
            _positions = new ObservableCollection<Position>(_readOnlyPositions);
            _addedPositions = new ObservableCollection<Position>();
            _addNewPositionCommand = new RelayCommand<string>(AddNewPosition);
            _addPositionCommand = new RelayCommand<Position>(AddPosition);
            _removePositionCommand = new RelayCommand<Position>(RemovePosition);
            _navigateToServiceSelectorCommand = new RelayCommand<ServiceNavigationEnum>(NavigateToServiceSelector);
            _finishCommand = new RelayCommand<RegistrationNavigationEnum>(Finish);
        }


        private void Finish(RegistrationNavigationEnum destination)
        {
            AddService();
            Messenger.Default.Send(destination);
        }

        private void NavigateToServiceSelector(ServiceNavigationEnum destination)
        {
            AddService();
            Messenger.Default.Send(destination);
        }

        private void AddService()
        {
            _psychologicalService = new PsychologicalService()
            {
                DateTimeOfService = DateTime.Now.Date,
                Person = _unitOfWork.Person.GetPerson(_person.Id),
                Company = _unitOfWork.Company.GetCompany(_company.Id),
                Referral =_unitOfWork.Referral.GetReferral(_referral.Id),
                Psychologist =_unitOfWork.Psychologist.GetPsychologist(_psychologist.Id),
                Localization = _unitOfWork.Localization.GetLocalization(_localization.Id),
                PsychologicalServiceType =_unitOfWork.PsychologicalServiceTypes.GetType(_psychologicalServiceType.Id),
                Number =_unitOfWork.PsychologicalService.GetLastNumber(_psychologist.Id,_localization.Id, _psychologicalServiceType.Id,DateTime.Now.Date)
            };
            _unitOfWork.PsychologicalService.Add(_psychologicalService);
            Messenger.Default.Send(new PersonPsychServiceMessage()
            {
                Person = _person,
                PsychologicalService = _psychologicalService
            });
        }

        public Person Person
        {
            get { return _person; }
            set { Set(ref _person, value); }
        }

        public string PostitionsFilter
        {
            get
            {
                return _postitionsFilter;
            }
            set
            {
                UpdatePositionsCollection(value);
                Set(ref _postitionsFilter, value);
            }
        }


        public RelayCommand<string> AddNewPositionCommand
        {
            get { return _addNewPositionCommand; }
            set { Set(ref _addNewPositionCommand, value); }
        }

        public RelayCommand<Position> AddPositionCommand
        {
            get { return _addPositionCommand; }
            set { Set(ref _addPositionCommand, value); }
        }

        public RelayCommand<Position> RemovePositionCommand
        {
            get { return _removePositionCommand; }
            set { Set(ref _removePositionCommand, value); }
        }

        public RelayCommand<ServiceNavigationEnum> NavigateToServiceSelectorCommand
        {
            get { return _navigateToServiceSelectorCommand; }
            set { Set(ref _navigateToServiceSelectorCommand, value); }
        }

        public RelayCommand<RegistrationNavigationEnum> FinishCommand
        {
            get { return _finishCommand; }
            set { Set(ref _finishCommand, value); }
        }

        public ObservableCollection<Position> Positions
        {
            get { return _positions; }
            set { Set(ref _positions, value); }
        }

        public ObservableCollection<Position> AddedPositions
        {
            get { return _addedPositions; }
            set { Set(ref _addedPositions, value); }
        }

        private void HandleCmpRefperMessage(CompanyReferralPersonMessage personMessage)
        {
            _referral = personMessage.Referral;
            _company = personMessage.Company;
            _psychologicalServiceType = personMessage.PsychologicalServiceType;
            _person = personMessage.Person;
        }

        private void HandleLozalizationPsychologistMessage(LocalizationPsychologistMessage obj)
        {
            _localization = obj.Localization;
            _psychologist = obj.Psychologist;
        }

        private void UpdatePositionsCollection(string positionFilter)
        {
            Positions = !string.IsNullOrEmpty(positionFilter)
                ? new ObservableCollection<Position>(_readOnlyPositions.Where(p => p.Name.ToLower().Contains(positionFilter.ToLower())))
                : new ObservableCollection<Position>(_readOnlyPositions);
            RaisePropertyChanged(() => Positions);
        }

        private void RemovePosition(Position position)
        {
            if (_addedPositions.Contains(position))
                _addedPositions.Remove(position);
        }

        private void AddPosition(Position position)
        {
            if(!_addedPositions.Contains(position))
                _addedPositions.Add(position);
        }

        private void AddNewPosition(string positionName)
        {
            Position position = new Position() {Name = positionName};
            if (!_unitOfWork.Position.CheckIfExists(positionName))
            {
                _unitOfWork.Position.Add(position);
            }
            position = _unitOfWork.Position.GetPosition(positionName);
            _addedPositions.Add(position);
        }
    }
}