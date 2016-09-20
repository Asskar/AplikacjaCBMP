using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;
using Model;

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
            return _context.Positions;
        }

        public IEnumerable<Position> GetFilterdPositions(string filter)
        {
            return filter != null
                ? _context.Positions.Where(p => p.Name.Contains(filter))
                : GetAllPositions();
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

        public Position SelectOrAdd(Position position)
        {

            if (CheckIfExists(position.Name))
                return GetPosition(position.Name);
            Add(position);
            _context.SaveChanges();
            return GetPosition(position.Name);
        }
    }
}