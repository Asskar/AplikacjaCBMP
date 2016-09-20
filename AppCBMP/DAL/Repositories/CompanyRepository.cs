using System.Collections.Generic;
using System.Linq;
using AppCBMP.Model;
using Model;

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

        public bool CheckIfExists(string companyName)
        {
            return _context.Companies.Any(c => c.Name == companyName);
        }

        public void Add(Company company)
        {
            _context.Companies.Add(company);
        }

        public Company GetCompany(string companyName)
        {
            return _context.Companies.First(c => c.Name == companyName);
        }

        public Company SelectOrAdd(Company company)
        {
            if (CheckIfExists(company.Name))
                return GetCompany(company.Name);
            Add(company);
            _context.SaveChanges();
            return GetCompany(company.Name);
        }
    }
}