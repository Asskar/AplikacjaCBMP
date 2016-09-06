using System.Collections.Generic;

namespace AppCBMP.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Person> Persons { get; set; }

    }
}