using System.Data.Entity.ModelConfiguration;
using AppCBMP.Model;
using Model;

namespace AppCBMP.DAL.EntityConfiguration
{
    public class ReferralConfiguration : EntityTypeConfiguration<Referral>
    {
        public ReferralConfiguration()
        {
            Property(r => r.Name).
                IsRequired().
                HasMaxLength(128);
        }
    }
}