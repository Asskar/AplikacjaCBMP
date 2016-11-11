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
    public class RegistrationMenuViewModel:ViewModelBase
    {
        
        private RelayCommand<RegistrationNavigationEnum> _navigationCommand;
        

        public RegistrationMenuViewModel()
        {
            _navigationCommand = new RelayCommand<RegistrationNavigationEnum>(Navigate);
        }

        private void Navigate(RegistrationNavigationEnum destination)
        {
            Messenger.Default.Send(destination);

        }

        public RelayCommand<RegistrationNavigationEnum> NavigationCommand
        {
            get { return _navigationCommand; }
            set { Set(ref _navigationCommand, value); }
        }
    }
}