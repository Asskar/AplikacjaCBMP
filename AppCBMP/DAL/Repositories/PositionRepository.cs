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

        public void Add(Position position)
        {
            _context.Positions.Add(position);
        }
        public bool CheckIfExists(string positionName)
        {
            return _context.Positions.Any(p => p.Name == positionName);
        }

        public Position GetPosition(string positionName)
        {
            return _context.Positions.First(p => p.Name == positionName);
        }
    }
}