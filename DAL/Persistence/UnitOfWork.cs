using DAL.Repositories;

namespace DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
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
            Psychologist= new PsychologistRepository(context);
            Localization= new LocalizationRepository(context);
        }

        public CompanyRepository Company { get; set; }
        public ReferralRepository Referral { get; set; }
        public LocalizationRepository Localization { get; set; }
        public PositionRepository Position { get; set; }
        public PersonRepository Person { get; set; }
        public PsychologicalServiceRepository PsychologicalService { get; set; }
        public PsychologicalServiceTypesRepository PsychologicalServiceTypes { get; set; }
        public PsychologistRepository Psychologist { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}