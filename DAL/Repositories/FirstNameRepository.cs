using System.Collections.Generic;
using Model;
using System.Linq;
namespace DAL.Repositories
{
    public class FirstNameRepository
    {
        private readonly AppDataContext _context;

        public FirstNameRepository(AppDataContext context)
        {
            _context = context;
        }


        public FirstName GetName(int firstNameId)
        {
            return _context.FirstNames.First(n => n.Id == firstNameId);
        }
        public FirstName GetName(string firstName)
        {
            return _context.FirstNames.First(n => n.Name == firstName);
        }

        public bool CheckIfExists(string firstName)
        {
            return _context.FirstNames.Any(n => n.Name == firstName);
        }

        public void Add(FirstName firstName)
        {
            _context.FirstNames.Add(firstName);
            _context.SaveChanges();
        }

        public IEnumerable<FirstName> GetAllNames()
        {
            return _context.FirstNames.Local;
        }
    }
}