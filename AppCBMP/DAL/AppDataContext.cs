﻿using System.Data.Entity;
using AppCBMP.DAL.EntityConfiguration;
using AppCBMP.Model;
using Model;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfigiration());
            modelBuilder.Configurations.Add(new ReferralConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<FirstName> FirstNames { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PsychologicalService> PsychologicalServices { get; set; }
        public DbSet<PsychologicalServiceType> PsychologicalServiceTypes { get; set; }
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Localization> Localizations { get; set; }
    }
}