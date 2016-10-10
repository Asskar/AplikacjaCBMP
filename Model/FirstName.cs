using System.Collections.Generic;

namespace Model
{
    public class FirstName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Person> Persons { get; set; } 
    }
}