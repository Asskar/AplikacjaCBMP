using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL.Repositories
{
    public class ReferralRepository
    {
        private readonly AppDataContext _context;

        public ReferralRepository(AppDataContext context)
        {
            _context = context;
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
        public void Add(string referralName)
        {
            _context.Referrals.Add(new Referral() {Name = referralName});
            _context.SaveChanges();
        }

        public Referral GetReferral(string referralName)
        {
            return _context.Referrals.First(r => r.Name == referralName);
        }

        public Referral SelectOrAdd(Referral referral)
        {
            if (CheckIfExists(referral.Name))
                return GetReferral(referral.Name);
            Add(referral);
            _context.SaveChanges();
            return GetReferral(referral.Name);
        }

        public Referral GetReferral(int id)
        {
            return _context.Referrals.First(r => r.Id == id);
        }
    }
}