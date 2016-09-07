using System.Collections.Generic;
using AppCBMP.DAL;
using AppCBMP.DAL.Persistence;
using AppCBMP.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AppCBMP.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        
        private RelayCommand<Position> _addPositionToListCommand;
        private RelayCommand _addPersonToDbCommand;
        private Registration _registration;
        private string _positionsAsString;

        public RegistrationViewModel()
        {
            Registration = new Registration(new UnitOfWork(new AppDataContext()));
            Registration.CurrentlyRegisteredPersonPositions=new List<Position>();
            AddPersonToDbCommand = new RelayCommand(AddPersonToDb, IsValid);
            AddPositionToListCommand = new RelayCommand<Position>(AddPersonToList);
            _positionsAsString = string.Empty;
        }

        public Registration Registration
        {
            get { return _registration; }
            set { Set(() => Registration, ref _registration, value); }
        }
        public string PositionsAsString
        {
            get { return _positionsAsString; }
            set { Set(() => PositionsAsString, ref _positionsAsString, value); }
        }

        public RelayCommand AddPersonToDbCommand
        {
            get { return _addPersonToDbCommand; }
            set { Set(() => AddPersonToDbCommand, ref _addPersonToDbCommand, value); }
        }
        public RelayCommand<Position> AddPositionToListCommand
        {
            get { return _addPositionToListCommand; }
            set { Set(() => AddPositionToListCommand, ref _addPositionToListCommand, value); }
        }

        
        private void AddPersonToDb()
        {
            Registration.CompleteRegistration();
            Registration.CurrentlyRegisteredPersonPositions = new List<Position>();
        }
        private void AddPersonToList(Position p)
        {
            Registration.CurrentlyRegisteredPersonPositions.Add(p);
                PositionsAsString += p.Name;
            AddPositionToListCommand = new RelayCommand<Position>(AddPersonToList);
        }

        private bool IsValid()
        {
            return true;
        }
    }
}