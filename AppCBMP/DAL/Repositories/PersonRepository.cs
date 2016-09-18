using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AppCBMP.Model;
using Model;

namespace AppCBMP.DAL.Repositories
{
    public class PersonRepository
    {
        private readonly AppDataContext _context;

        public PersonRepository(AppDataContext context)
        {
            _context = context;
        }

        public Person GetPerson(string pesel)
        {
            return _context.Persons.Single(p => p.Pesel == pesel);
        }

        public Person GetPerson(int id)
        {
            return _context.Persons.Single(p => p.Id == id);
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);
        }

        public bool CheckIfExists(string pesel)
        {
            return _context.Persons.Any(p => p.Pesel == pesel);
        }
    }
}