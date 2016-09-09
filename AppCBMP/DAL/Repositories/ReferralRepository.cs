using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;

namespace AppCBMP.DAL.Repositories
{
    public class ReferralRepository
    {
        private readonly AppDataContext _context;

        public ReferralRepository(AppDataContext context)
        {
            _context = context;
        }

        public Referral AddReferral(Referral referral)
        {
            if (_context.Referrals.Any(r => r.Name == referral.Name))
                return _context.Referrals.Single(r => r.Name == referral.Name);
            _context.Referrals.Add(referral);
            return referral;
        }

        public IEnumerable<Referral> GetFilterdReferrals(string filter)
        {
            return _context.Referrals.Where(r => r.Name.Contains(filter)).
                ToList();
        }

        public IEnumerable<Referral> GetAllReferrals()
        {
            return _context.Referrals.ToList();
        }

        public bool CheckIfExists(string referralName)
        {
            return _context.Referrals.Any(r => r.Name == referralName);
        }
        public void Add(Referral referral)
        {
            _context.Referrals.Add(referral);
        }

        public Referral GetReferral(string referralName)
        {
            return _context.Referrals.First(r => r.Name == referralName);
        }
    }
}