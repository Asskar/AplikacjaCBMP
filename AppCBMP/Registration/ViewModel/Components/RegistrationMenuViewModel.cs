using System.Collections.Generic;
using System.Linq;
using AppCBMP.DAL;
using AppCBMP.DAL.Persistence;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class RegistrationMenuViewModel:ViewModelBase
    {
        private readonly UnitOfWork _unitOfWork;
        private Psychologist _psychologist;
        private List<Psychologist> _psychologists;
        private RelayCommand<RegistrationNavigationEnum> _navigationCommand;
        //        private Localization _localization;
        //
        //        public Localization Localization
        //        {
        //            get { return _localization; }
        //            set { Set(ref _localization, value); }
        //        }
        //
        //        private List<Localization> _localizations;
        //
        //        public List<Localization> Localizations
        //        {
        //            get { return _localizations; }
        //            set { Set(ref _localizations, value); }
        //        }


        public RegistrationMenuViewModel()
        {
            _unitOfWork = new UnitOfWork(new AppDataContext());
            _psychologists = _unitOfWork.Psychologist.GetAll().ToList();
            _navigationCommand= new RelayCommand<RegistrationNavigationEnum>(Navigate);
        }

        private void Navigate(RegistrationNavigationEnum message)
        {
            if (Psychologist == null) //||localization==null
                return;
            Messenger.Default.Send(message);
        }

        public Psychologist Psychologist
        {
            get { return _psychologist; }
            set { Set(ref _psychologist, value); }
        }
        public List<Psychologist> Psychologists
        {
            get { return _psychologists; }
            set { Set(ref _psychologists, value); }
        }

       

        public RelayCommand<RegistrationNavigationEnum> NavigationCommand
        {
            get { return _navigationCommand; }
            set { Set(ref _navigationCommand, value); }
        }
    }
}