using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL.Repositories
{
    public class LocalizationRepository
    {
        private readonly AppDataContext _context;

        public LocalizationRepository(AppDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Localization> GetAllLocalizations()
        {
            return _context.Localizations;
        }

        public Localization GetLocalization(int id)
        {
            return _context.Localizations.First(l => l.Id == id);
        }
    }
}