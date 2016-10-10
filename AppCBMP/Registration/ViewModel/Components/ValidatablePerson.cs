using AppCBMP.Helpers.ClassBases;
using System.ComponentModel.DataAnnotations;
using AppCBMP.Helpers;
using AppCBMP.Helpers.ValidationAttributes;
using System;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class ValidatablePerson : ValidatableBindableBase
    {
        private int _id;
        private string _pesel;
        private string _firstName;
        private string _lastName;
        private string _birthPlace;
        private string _postCode;
        private string _city;
        private string _street;
        private string _houseNumber;
        private string _education;
        private string _phone;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        [PeselValidation]
        public string Pesel
        {
            get { return _pesel; }
            set { SetProperty(ref _pesel, value); }
        }

        [Required(ErrorMessage = @"Imie jest wymagane.")]
        [PermittedSpecialCharsAndLetters("- ", ErrorMessage = @"Imie zawiera niedozwolone znaki.")]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        [Required(ErrorMessage = @"Nazwisko jest wymagane.")]
        [PermittedSpecialCharsAndLetters("- ", ErrorMessage = @"Nazwisko zawiera niedozwolone znaki.")]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        [PermittedSpecialCharsAndLetters("- ", ErrorMessage = @"Miejsce urodzenia zawiera niedozwolone znaki.")]
        public string BirthPlace
        {
            get { return _birthPlace; }
            set { SetProperty(ref _birthPlace, value); }
        }

        [PostCode(ErrorMessage = @"Błędny kod pocztowy.")]
        public string PostCode
        {
            get { return _postCode; }
            set { SetProperty(ref _postCode, value); }
        }

        [PermittedSpecialCharsAndLetters("- ", ErrorMessage = @"Miasto zawiera niedozwolone znaki.")]
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        [PermittedSpecialCharsAndLetters("- '", ErrorMessage = @"Ulica urodzenia zawiera niedozwolone znaki.")]
        public string Street
        {
            get { return _street; }
            set { SetProperty(ref _street, value); }
        }

        public string HouseNumber
        {
            get { return _houseNumber; }
            set { SetProperty(ref _houseNumber, value); }
        }

        public string Education
        {
            get { return _education; }
            set { SetProperty(ref _education, value); }
        }

        [PhoneValidation(ErrorMessage = @"Zły format numeru telefonu.")]
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        
    }
}