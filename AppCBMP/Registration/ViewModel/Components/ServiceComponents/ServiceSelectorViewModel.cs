using System;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AppCBMP.Registration.ViewModel.Components.ServiceComponents
{
    public class ServiceSelectorViewModel : ViewModelBase
    {
        private RelayCommand<ServiceNavigationEnum> _navigationCommand;

        public ServiceSelectorViewModel()
        {
            _navigationCommand=new RelayCommand<ServiceNavigationEnum>(OnNavigation);
        }

        public RelayCommand<ServiceNavigationEnum> NavigationCommand
        {
            get { return _navigationCommand; }
            set { Set(ref _navigationCommand, value); }
        }


        private void OnNavigation(ServiceNavigationEnum navigationEnum)
        {
           Messenger.Default.Send(navigationEnum);
        }
    }
}