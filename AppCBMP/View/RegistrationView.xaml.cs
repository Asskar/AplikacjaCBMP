using System.Windows;

namespace AppCBMP.View
{
    /// <summary>
    /// Description for RegistrationView.
    /// </summary>
    public partial class RegistrationView : Window
    {
        /// <summary>
        /// Initializes a new instance of the RegistrationView class.
        /// </summary>
        public RegistrationView()
        {
            InitializeComponent();
            
        }
        
        public bool LocationToggleOpen { get { return (bool)GetValue(LocationToggleOpenProperty); } set { SetValue(LocationToggleOpenProperty, value); } }

        public static readonly DependencyProperty LocationToggleOpenProperty =
            DependencyProperty.Register("LocationToggleOpen", typeof(bool), typeof(RegistrationView), new PropertyMetadata(false));

        public void LocationToggleIsOpen()
        {
            LocationToggleOpen = !LocationToggleOpen;
        }

        public bool MiscellaneousToggleOpen { get { return (bool)GetValue(MiscellaneousToggleOpenProperty); } set { SetValue(MiscellaneousToggleOpenProperty, value); } }

        public static readonly DependencyProperty MiscellaneousToggleOpenProperty =
            DependencyProperty.Register("MiscellaneousToggleOpen", typeof(bool), typeof(RegistrationView), new PropertyMetadata(false));

        public void MiscellaneousToggleIsOpen()
        {
            MiscellaneousToggleOpen = !MiscellaneousToggleOpen;
        }
    }
}