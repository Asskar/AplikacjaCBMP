using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL.Repositories
{
    public class PsychologicalServiceTypesRepository
    {
        private readonly AppDataContext _context;

        public PsychologicalServiceTypesRepository(AppDataContext context)
        {
            _context = context;
        }

        public IEnumerable<PsychologicalServiceType> GetAllTypes()
        {
            return _context.PsychologicalServiceTypes;
        }

        public PsychologicalServiceType GetType(int id)
        {
            return _context.PsychologicalServiceTypes.First(p => p.Id == id);
        }
    }
}