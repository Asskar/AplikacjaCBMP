using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;

namespace AppCBMP.DAL.Repositories
{
    public class PositionRepository
    {
        private readonly AppDataContext _context;

        public PositionRepository(AppDataContext context)
        {
            _context = context;
        }
        public IEnumerable<Position> GetAllPositions()
        {
            return _context.Positions.ToList();
        }

        public IEnumerable<Position> GetFilterdPositions(string filter)
        {
            return _context.Positions.Where(p => p.Name.Contains(filter)).
                ToList();
        }

    }
}