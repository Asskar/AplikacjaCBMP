using System.Linq;
using AppCBMP.Model;

namespace AppCBMP.DAL.Repositories
{
    public class PsychologicalExaminationRepository
    {
        private readonly AppDataContext _context;

        public PsychologicalExaminationRepository(AppDataContext context)
        {
            _context = context;
        }

        public PsychologicalExaminationType GetExaminationType(string typeOfExamination)
        {


            if (_context.PsychologicalExaminationTypes.Any(r => r.Name == typeOfExamination))
                return _context.PsychologicalExaminationTypes.Single(r => r.Name == typeOfExamination);
            _context.PsychologicalExaminationTypes.Add(new PsychologicalExaminationType() { Name = typeOfExamination });
            return new PsychologicalExaminationType() { Name = typeOfExamination };
        }
    }
}