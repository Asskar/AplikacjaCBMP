using System.Collections.Generic;

namespace Model
{
    public class Localization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<PsychologicalService> PsychologicalServices { get; set; }  
    }
}