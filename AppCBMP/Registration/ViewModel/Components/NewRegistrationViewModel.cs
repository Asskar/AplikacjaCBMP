using System.Collections.Generic;
using System.Linq;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using DAL;
using DAL.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class NewRegistrationViewModel:ViewModelBase
    {
        private readonly UnitOfWork _unitOfWork;
        private Psychologist _psychologist;
        private Localization _localization;
        private RelayCommand<RegistrationNavigationEnum> _navigationCommand;
        public NewRegistrationViewModel()
        {
            _unitOfWork = new UnitOfWork(new AppDataContext());
            Psychologists = _unitOfWork.Psychologist.GetAll().
                ToList();
            Localizations = _unitOfWork.Localization.GetAllLocalizations().ToList();
            _navigationCommand = new RelayCommand<RegistrationNavigationEnum>(Navigate);
        }

        public RelayCommand<RegistrationNavigationEnum> NavigationCommand
        {
            get { return _navigationCommand; }
            set { Set(ref _navigationCommand, value); }
        }
        public Psychologist Psychologist
        {
            get { return _psychologist; }
            set { Set(ref _psychologist, value); }
        }
        public Localization Localization
        {
            get { return _localization; }
            set { Set(ref _localization, value); }
        }
        public List<Psychologist> Psychologists { get; set; }
        public List<Localization> Localizations { get; set; }

        private void Navigate(RegistrationNavigationEnum destination)
        {
            if (Localization == null)
                return;
            if (Psychologist == null)
                return;
            Messenger.Default.Send(destination);
            Messenger.Default.Send(new LocalizationPsychologistMessage()
            {
                Localization = Localization,
                Psychologist = Psychologist
            });
        }
    }
}