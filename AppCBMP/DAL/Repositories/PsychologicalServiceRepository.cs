namespace AppCBMP.DAL.Repositories
{
    public class PsychologicalServiceRepository
    {
        private readonly AppDataContext _context;

        public PsychologicalServiceRepository(AppDataContext context)
        {
            _context = context;
        }
    }
}