using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;

namespace DAL.Repositories
{
    public class PersonRepository
    {
        private readonly AppDataContext _context;

        public PersonRepository(AppDataContext context)
        {
            _context = context;
        }

        public bool CheckIfExists(string pesel)
        {
            return _context.Persons.Any(p => p.Pesel == pesel);
        }
        public bool CheckIfExists(int id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }
        public Person GetPerson(string pesel)
        {
            return _context.Persons.First(p => p.Pesel == pesel);
        }

        public Person AddOrUpdate(Person person)
        {
            if (CheckIfExists(person.Pesel))
                return GetPerson(person.Pesel);
            Add(person);
            return GetPerson(person.Pesel);
        }

        private void Add(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public Person GetPerson(int id)
        {
            return _context.Persons.First(p => p.Id == id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons;
        }
    }
}