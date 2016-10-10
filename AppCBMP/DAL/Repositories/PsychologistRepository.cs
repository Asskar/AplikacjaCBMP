using System.Collections;
using System.Collections.Generic;
using Model;

namespace AppCBMP.DAL.Repositories
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
    }
}