/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:AppCBMP.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using AppCBMP.Model;
using AppCBMP.Registration.ViewModel;
using AppCBMP.Registration.ViewModel.Components;
using AppCBMP.Registration.ViewModel.Components.ServiceComponents;
using AppCBMP.ViewModel.RegistrationViewModelFolder;

namespace AppCBMP.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RegistrationViewModel>();
            SimpleIoc.Default.Register<InvoiceViewModel>();
            SimpleIoc.Default.Register<PersonalDataRegistrationViewModel>();
            SimpleIoc.Default.Register<RegistrationWindowViewModel>();
            SimpleIoc.Default.Register<CompanyAndReferralViewModel>();
            SimpleIoc.Default.Register<SelectedServiceViewModel>();
            SimpleIoc.Default.Register<OperatorServiceViewModel>();
            SimpleIoc.Default.Register<ServiceSelectorViewModel>();
            SimpleIoc.Default.Register<RegistrationMenuViewModel>();
            SimpleIoc.Default.Register<PersonListViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public RegistrationViewModel Registration => ServiceLocator.Current.GetInstance<RegistrationViewModel>();

        public InvoiceViewModel Invoice => ServiceLocator.Current.GetInstance<InvoiceViewModel>();

        public RegistrationWindowViewModel RegistrationWindowViewModel
        {
            get { return ServiceLocator.Current.GetInstance<RegistrationWindowViewModel>(); }
        }

        public PersonalDataRegistrationViewModel PersonalDataRegistrationView
        {
            get { return ServiceLocator.Current.GetInstance<PersonalDataRegistrationViewModel>(); }
        }

        public CompanyAndReferralViewModel CompanyAndReferralViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CompanyAndReferralViewModel>(); }
        }
        public SelectedServiceViewModel SelectedServiceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SelectedServiceViewModel>(); }
        }
        public ServiceSelectorViewModel ServiceSelectorViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ServiceSelectorViewModel>(); }
        }
        public OperatorServiceViewModel OperatorServiceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<OperatorServiceViewModel>(); }
        }

        public RegistrationMenuViewModel RegistrationMenuViewModel
        {
            get { return ServiceLocator.Current.GetInstance<RegistrationMenuViewModel>(); }
        }
        public PersonListViewModel PersonListViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PersonListViewModel>(); }
        }
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}