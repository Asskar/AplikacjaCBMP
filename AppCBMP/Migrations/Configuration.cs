using DAL;
using Model;
using System.Collections.Generic;

namespace AppCBMP.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var persons = new List<Person>
            {
                new Person
                {
                    Pesel = "86030403770",
                    FirstName = "Piotr",
                    LastName = "Kiliñski",
                    BirthPlace = "Czêstochowa",
                    PhoneNumber = 535930370,
                    Street = "Irzykowskiego",
                    City = "Czêstochowa",
                    PostCode = "42-217",
                    HouseNumber = "3/24",
                    Education = "œrednie"
                }
            };

            var psychologists = new List<Psychologist> {
                new Psychologist() { Initials = "KK"},
                new Psychologist() { Initials = "TK" }};

            var localizations = new List<Localization>
            {
                new Localization()
                {
                    Name = "Nasza Przychodnia",
                    City = "Czêstochowa",
                    PostCode = "42-215",
                    Street = "Wolnoœci",
                    HouseNumber = "46",
                    PhoneNumber = "535-930-370"
                }
            };

            context.Psychologists.AddRange(psychologists);
            context.Localizations.AddRange(localizations);
            context.Persons.AddRange(persons);
            context.SaveChanges();
        }
    }
}
