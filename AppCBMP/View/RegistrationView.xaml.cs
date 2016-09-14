using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using AppCBMP.Model;

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

        public bool IsOpen { get { return (bool)GetValue(IsOpenProperty); } set { SetValue(IsOpenProperty, value); } }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(RegistrationView), new PropertyMetadata(false));

        public void ToggleIsOpen()
        {
            IsOpen = !IsOpen;
        }
    }
}