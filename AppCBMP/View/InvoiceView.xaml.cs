using System.Windows;

namespace AppCBMP.View
{
    /// <summary>
    /// Description for InvoiceView.
    /// </summary>
    public partial class InvoiceView : Window
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceView class.
        /// </summary>
        public InvoiceView()
        {
            InitializeComponent();
        }
        public bool CompanyToggleOpen { get { return (bool)GetValue(CompanyToggleOpenProperty); } set { SetValue(CompanyToggleOpenProperty, value); } }

        public static readonly DependencyProperty CompanyToggleOpenProperty =
            DependencyProperty.Register("CompanyToggleOpen", typeof(bool), typeof(InvoiceView), new PropertyMetadata(false));

        public void CompanyToggleIsOpen()
        {
            CompanyToggleOpen = !CompanyToggleOpen;
        }

        public bool PersonToggleOpen { get { return (bool)GetValue(PersonToggleOpenProperty); } set { SetValue(PersonToggleOpenProperty, value); } }

        public static readonly DependencyProperty PersonToggleOpenProperty =
            DependencyProperty.Register("PersonToggleOpen", typeof(bool), typeof(InvoiceView), new PropertyMetadata(false));

        public void PersonToggleIsOpen()
        {
            PersonToggleOpen = !PersonToggleOpen;
        }
    }
}