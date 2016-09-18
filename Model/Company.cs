using System.Collections.Generic;

namespace Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Service> Services { get; set; }

    }
}