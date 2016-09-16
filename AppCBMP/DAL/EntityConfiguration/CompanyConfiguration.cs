using System.Data.Entity.ModelConfiguration;
using AppCBMP.Model;

namespace AppCBMP.DAL.EntityConfiguration
{
    public class CompanyConfigiration : EntityTypeConfiguration<Company>
    {
        public CompanyConfigiration()
        {
            Property(c => c.Name).
                IsRequired().
                HasMaxLength(128);
        }
    }
}