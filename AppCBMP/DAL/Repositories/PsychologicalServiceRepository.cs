using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace AppCBMP.DAL.Repositories
{
    public class PsychologicalServiceRepository
    {
        private readonly AppDataContext _context;

        public PsychologicalServiceRepository(AppDataContext context)
        {
            _context = context;
        }

        public void Add(PsychologicalService psychologicalService)
        {
            _context.PsychologicalServices.Add(psychologicalService);
        }

        public int GetLastNumber(int psychologistId, int typeId, DateTime date)
        {
            //List<PsychologicalService> list = _context.PsychologicalServices.
            //    Where(s => s.PsychologistId == psychologistId).
            //    Where(s => s.PsychologicalServiceTypeId == typeId).
            //    Where(s => s.DateTimeOfService.Year == date.Year).
            //    Where(s => s.DateTimeOfService.Month == date.Month).
            //    ToList();
            //if (list.Count == 0)
            //    return 1;
            //return list.Last().
            //    Number + 1;
            return 1;
        }
    }
}