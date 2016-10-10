using System;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using AppCBMP.Registration.ViewModel.Components.ServiceComponents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class SelectedServiceViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
       


        private readonly ServiceSelectorViewModel _serviceSelectorViewModel;
        private readonly OperatorServiceViewModel _operatorServiceViewModel;

        public SelectedServiceViewModel()
        {
            Messenger.Default.Register<ServiceNavigationEnum>(this, Navigation);
            _serviceSelectorViewModel = ServiceLocator.Current.GetInstance<ServiceSelectorViewModel>();
            _operatorServiceViewModel = ServiceLocator.Current.GetInstance<OperatorServiceViewModel>();
            _currentViewModel = _serviceSelectorViewModel;
        }




        private void Navigation(ServiceNavigationEnum navigation)
        {
            switch (navigation)
            {
                case ServiceNavigationEnum.ServiceSelectorViewModel:
                    CurrentViewModel = _serviceSelectorViewModel;
                    break;
                case ServiceNavigationEnum.OperatorViewModel:
                    CurrentViewModel = _operatorServiceViewModel;
                    break;
                case ServiceNavigationEnum.DriverViewModel:
                    break;
                case ServiceNavigationEnum.SpecialViewModel:
                    break;
                default:
                    break;
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }


    }
}