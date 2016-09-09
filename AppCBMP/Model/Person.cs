using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace AppCBMP.Model
{
    public class Person:ObservableObject
    {
        public int Id { get; set; }
        private string _pesel;
        private string _firstName;
        private string _lastName;
        private string _birthPlace;
        private string _postCode;
        private string _city;
        private string _street;
        private string _houseNumber;
        private int _apartmentNumber;
        private string _education;
        private long _phoneNumber;

        public string Pesel
        {
            get { return _pesel; }
            set { Set(()=>Pesel, ref _pesel, value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { Set(() => FirstName, ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { Set(() => LastName, ref _lastName, value); }
        }

        public string BirthPlace
        {
            get { return _birthPlace; }
            set { Set(() => BirthPlace, ref _birthPlace, value); }
        }

        public string PostCode
        {
            get { return _postCode; }
            set { Set(() => PostCode, ref _postCode, value); }
        }

        public string City
        {
            get { return _city; }
            set { Set(() => City, ref _city, value); }
        }

        public string Street
        {
            get { return _street; }
            set { Set(() => Street, ref _street, value); }
        }

        public string HouseNumber
        {
            get { return _houseNumber; }
            set { Set(() => HouseNumber, ref _houseNumber, value); }
        }

        public int ApartmentNumber
        {
            get { return _apartmentNumber; }
            set { Set(() => ApartmentNumber, ref _apartmentNumber, value); }
        }

        public string Education
        {
            get { return _education; }
            set { Set(() => Education, ref _education, value); }
        }

        public long PhoneNumber
        {
            get { return _phoneNumber; }
            set { Set(() => PhoneNumber, ref _phoneNumber, value); }
        }

        public ICollection<Company> Companies { get; set; }
        public ICollection<Referral> Refrrals { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}