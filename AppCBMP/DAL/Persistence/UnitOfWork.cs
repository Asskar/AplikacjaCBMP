using AppCBMP.DAL.Repositories;

namespace AppCBMP.DAL.Persistence
{
    public class UnitOfWork
    {
        private readonly AppDataContext _context;

        public UnitOfWork(AppDataContext context)
        {
            _context = context;
            Company = new CompanyRepository(context);
            Referral = new ReferralRepository(context);
            Position = new PositionRepository(context);
            Person = new PersonRepository(context);
            PsychologicalService = new PsychologicalServiceRepository(context);
            PsychologicalServiceTypes = new PsychologicalServiceTypesRepository(context);
        }

        public CompanyRepository Company { get; set; }
        public ReferralRepository Referral { get; set; }
        public PositionRepository Position { get; set; }
        public PersonRepository Person { get; set; }
        public PsychologicalServiceRepository PsychologicalService { get; set; }
        public PsychologicalServiceTypesRepository PsychologicalServiceTypes { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}