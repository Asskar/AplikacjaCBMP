
using System.Collections.Generic;
using System.Linq;
using Model;

namespace AppCBMP.DAL.Repositories
{
    public class PsychologicalServiceTypesRepository
    {
        private readonly AppDataContext _context;

        public PsychologicalServiceTypesRepository(AppDataContext context)
        {
            _context = context;
        }

        public IEnumerable<PsychologicalServiceType> GetTypes()
        {
            return _context.PsychologicalServiceTypes;
        }
    }
}