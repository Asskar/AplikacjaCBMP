using System.Collections.Generic;

namespace Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Pesel { get; set; }
        public int FirstNameId { get; set; }
        public FirstName FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthPlace { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Education { get; set; }
        public long? PhoneNumber { get; set; }

        public ICollection<PsychologicalService> PsychologicalServices { get; set; }
    }
}