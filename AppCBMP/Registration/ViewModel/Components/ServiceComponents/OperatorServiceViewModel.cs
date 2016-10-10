using System;
using System.Collections.Generic;
using System.Linq;
using AppCBMP.DAL;
using AppCBMP.DAL.Persistence;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
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
        private PsychologicalServiceType _psychologicalServiceType;
        private PsychologicalService _psychologicalService;
        private readonly List<Position> _readOnlyPositions; 
        private List<Position> _positions;
        private List<Position> _addedPositions;
        private string _postitionsFilter;

        private readonly UnitOfWork _unitOfWork;


        
        public OperatorServiceViewModel()
        {
            Messenger.Default.Register<CompanyReferralPersonMessage>(this,HandleCmpRefPerMessage);
            Messenger.Default.Register<Psychologist>(this, HandlePsychologistMessage);
            _unitOfWork=new UnitOfWork(new AppDataContext());
            _readOnlyPositions = _unitOfWork.Position.GetAllPositions().
                ToList();
            _positions = _readOnlyPositions;
            _addNewPositionCommand= new RelayCommand<string>(AddNewPosition);
            _addPositionCommand=  new RelayCommand<Position>(AddPosition);
            _removePositionCommand= new RelayCommand<Position>(RemovePosition);
            _navigateToServiceSelectorCommand=new RelayCommand<ServiceNavigationEnum>(NavigateToServiceSelector);
            _finishCommand=new RelayCommand<RegistrationNavigationEnum>(Finish);
        }

        private void HandlePsychologistMessage(Psychologist message)
        {
            _psychologist = message;
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
            _psychologicalService = new PsychologicalService
            {
                DateTimeOfService = DateTime.Now.Date,
                Person = _person,
                PersonId = _person.Id,
                Company = _company,
                CompanyId = _company.Id,
                Referral = _referral,
                ReferralId = _referral.Id,
                Positions = _addedPositions,
                PsychologicalServiceType = _psychologicalServiceType,
                PsychologicalServiceTypeId = _psychologicalServiceType.Id,
                Psychologist = _psychologist,
                PsychologistId = _psychologist.Id,
                Number = _unitOfWork.PsychologicalService.GetLastNumber(_psychologist.Id, _psychologicalServiceType.Id, DateTime.Now.Date)
            };
            _unitOfWork.PsychologicalService.Add(_psychologicalService);
            _unitOfWork.Complete();
            
        }

        public Person Person
        {
            get { return _person; }
            set { Set(ref _person, value); }
        }

        public string PostitionsFilter
        {
            get { return _postitionsFilter; }
            set
            {
                if (_postitionsFilter != null
                    && (value.Length == _postitionsFilter.Length + 1 || value.Length == _postitionsFilter.Length - 1 || value == string.Empty))
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

        public List<Position> Positions
        {
            get { return _positions; }
            set { Set(ref _positions, value); }
        }

        public List<Position> AddedPositions
        {
            get { return _addedPositions; }
            set { Set(ref _addedPositions, value); }
        }

        private void HandleCmpRefPerMessage(CompanyReferralPersonMessage message)
        {
            Person = message.Person;
            _referral = message.Referral;
            _company = message.Company;
            _psychologicalServiceType = message.PsychologicalServiceType;
        }

        private void UpdatePositionsCollection(string positionFilter)
        {
            _positions = _readOnlyPositions.Where(p => p.Name == positionFilter).
                ToList();
            RaisePropertyChanged(() => Positions);
        }

        private void RemovePosition(Position position)
        {
            _addedPositions.Remove(position);
        }

        private void AddPosition(Position position)
        {
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