using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL.Repositories
{
    public class PsychologistRepository
    {
        private readonly AppDataContext _context;

        public PsychologistRepository(AppDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Psychologist> GetAll()
        {
            return _context.Psychologists;
        }

        public Psychologist GetPsychologist(int id)
        {
            return _context.Psychologists.First(p => p.Id == id);
        }
    }
}