using System;

namespace AppCBMP.Model
{
    public class Service
    {
        public int Id { get; set; }
        public DateTime DateTimeOfService { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}