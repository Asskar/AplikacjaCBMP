using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;

namespace AppCBMP.DAL.Repositories
{
    public class ServiceRepository
    {
        private readonly AppDataContext _context;

        public ServiceRepository(AppDataContext context)
        {
            _context = context;
        }

        public void Add(Service service)
        {
            _context.Services.Add(service);
        }

        public IEnumerable<PsychologicalExaminationType> GetAllExaminationTypes()
        {
            return _context.PsychologicalExaminationTypes;
        }
    }
}