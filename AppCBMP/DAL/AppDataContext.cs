using System.Data.Entity;
using AppCBMP.Model;

namespace AppCBMP.DAL
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("DefaultConnection")
        {

        }

        public static AppDataContext Create()
        {
            return new AppDataContext();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PsychologicalExamination> PsychologicalExaminations { get; set; }
        public DbSet<PsychologicalExaminationType> PsychologicalExaminationTypes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Referral> Referrals { get; set; }
    }
}