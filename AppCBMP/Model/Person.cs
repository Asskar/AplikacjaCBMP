using System.Collections.Generic;

namespace AppCBMP.Model
{
    public class Person
    {
        public int Id { get; set; }

        public string Pesel { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthPlace { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public int ApartmentNumber { get; set; }

        public string Education { get; set; }

        public long PhoneNumber { get; set; }

        public ICollection<Company> Companies { get; set; }
        public ICollection<Referral> Refrrals { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}