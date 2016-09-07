using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;

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

        public Person Add(Person person)
        {
            if (_context.Persons.Any(p => p.Pesel == person.Pesel))
                return _context.Persons.Single(p => p.Pesel == person.Pesel);
            _context.Persons.Add(person);
            return person;
        }

        public bool CheckIfPersonExists(string pesel)
        {
            return _context.Persons.Any(p => p.Pesel == pesel);
        }

        public bool CheckIfPersonExists(int id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }

        public IEnumerable<Company> GetPersonCompanies(Person person)
        {
            return CheckIfPersonExists(person.Id)
                ? _context.Persons.First(p => p.Id == person.Id).
                    Companies
                : new List<Company>();
        }

        public IEnumerable<Referral> GetPersonReferrals(Person person)
        {
            return CheckIfPersonExists(person.Id)
               ? _context.Persons.First(p => p.Id == person.Id).
                   Refrrals
               : new List<Referral>();
        }

        public IEnumerable<Service> GetPersonServices(Person person)
        {
            return CheckIfPersonExists(person.Id)
               ? _context.Persons.First(p => p.Id == person.Id).
                   Services
               : new List<Service>();
        }
    }
}