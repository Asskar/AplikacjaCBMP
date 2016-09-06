using AppCBMP.DAL;
using AppCBMP.DAL.Persistence;
using AppCBMP.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AppCBMP.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private RelayCommand _addPersonToDbCommand;
        private Registration _registration;

        public RegistrationViewModel()
        {
            Registration = new Registration(new UnitOfWork(new AppDataContext()));
            AddPersonToDbCommand = new RelayCommand(AddPersonToDb, IsValid);
        }

        public Registration Registration
        {
            get { return _registration; }
            set { Set(() => Registration, ref _registration, value); }
        }

        public RelayCommand AddPersonToDbCommand
        {
            get { return _addPersonToDbCommand; }
            set { Set(() => AddPersonToDbCommand, ref _addPersonToDbCommand, value); }
        }

        private void AddPersonToDb()
        {
            Registration.CompleteRegistration();
        }

        private bool IsValid()
        {
            return true;
        }
    }
}