using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;

namespace AppCBMP.DAL.Repositories
{
    public class CompanyRepository
    {
        private readonly AppDataContext _context;

        public CompanyRepository(AppDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public IEnumerable<Company> GetFilterdCompanies(string filter)
        {
            return _context.Companies.Where(c => c.Name.Contains(filter)).
                ToList();
        }

        public Company AddCompany(Company company)
        {
            if (_context.Companies.Any(c => c.Name == company.Name))
                return _context.Companies.Single(c => c.Name == company.Name);
            _context.Companies.Add(company);
            return company;
        }
    }
}