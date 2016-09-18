using System.Data.Entity.ModelConfiguration;
using AppCBMP.Model;
using Model;

namespace AppCBMP.DAL.EntityConfiguration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(p => p.Pesel)
                .IsRequired();

            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(64);

            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(128);

            Property(p => p.BirthPlace)
                .IsRequired()
                .HasMaxLength(128);

            Property(p => p.PostCode)
                .IsRequired()
                .HasMaxLength(16);

            Property(p => p.City).
                IsRequired().
                HasMaxLength(128);

            Property(p => p.Street).
                IsRequired().
                HasMaxLength(128);

            Property(p => p.HouseNumber).
                IsRequired().
                HasMaxLength(10);

            Property(p => p.Education).
                IsRequired().
                HasMaxLength(64);

        }
    }
}