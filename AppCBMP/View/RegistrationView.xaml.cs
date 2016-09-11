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
            List<Position> listapozycji = new List<Position>()
            {
                new Position() {Name = "pozycja bardzo długa"},
                new Position() {Name = "pozycja bardzo długa"},
                new Position() {Name = "pozycja bardzo długa"}
            };
        }
    }
}