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

        public void AddOrUpdate(Person person)
        {
            _context.Entry(person).State = person.Id == 0 ?
                        EntityState.Added :
                        EntityState.Modified;
            _context.SaveChanges();
        }

        public Person GetPerson(int id)
        {
            return _context.Persons.First(p => p.Id == id);
        }
    }
}