﻿using System;
using System.Data.Entity.Infrastructure;
using AppCBMP.Registration.ViewModel.Components;
using AppCBMP.Registration.ViewModel.Components.NavigationEnums;
using AppCBMP.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace AppCBMP.Registration.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class RegistrationWindowViewModel : ViewModelBase
    {

        private ViewModelBase _currentViewModel;

        private readonly PersonalDataRegistrationViewModel _personalDataRegistrationViewModel;     
        private readonly CompanyAndReferralViewModel _companyAndReferralViewModel;
        private readonly SelectedServiceViewModel _selectedServiceViewModel;
        private readonly RegistrationMenuViewModel _registrationMenuViewModel;
       

        public RegistrationWindowViewModel()
        {
            Messenger.Default.Register<RegistrationNavigationEnum>(this,OnNavigation);
            _registrationMenuViewModel = ServiceLocator.Current.GetInstance<RegistrationMenuViewModel>();
            _personalDataRegistrationViewModel = ServiceLocator.Current.GetInstance<PersonalDataRegistrationViewModel>();
            _companyAndReferralViewModel = ServiceLocator.Current.GetInstance<CompanyAndReferralViewModel>();
            _selectedServiceViewModel = ServiceLocator.Current.GetInstance<SelectedServiceViewModel>();

            _currentViewModel = _registrationMenuViewModel;


        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }


        private void OnNavigation(RegistrationNavigationEnum direction)
        {
            switch (direction)
            {
                case RegistrationNavigationEnum.RegistrationMenuViewModel:
                    CurrentViewModel = _registrationMenuViewModel;
                    break;
                case RegistrationNavigationEnum.PersonalDataRegistrationViewModel:
                    CurrentViewModel = _personalDataRegistrationViewModel;
                    break;
                case RegistrationNavigationEnum.CompanyAndReferralViewModel:
                    CurrentViewModel = _companyAndReferralViewModel;
                    break;
                case RegistrationNavigationEnum.SelectedServiceViewModel:
                    CurrentViewModel = _selectedServiceViewModel;
                    break;
                default:
                    break;
            }
            RaisePropertyChanged(() => CurrentViewModel);
        }
        
    }
}