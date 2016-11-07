using System.Data.Entity.ModelConfiguration;
using Model;

namespace DAL.EntityConfiguration
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