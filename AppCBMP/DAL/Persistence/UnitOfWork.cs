﻿using AppCBMP.DAL.Repositories;

namespace AppCBMP.DAL.Persistence
{
    public class UnitOfWork
    {
        public UnitOfWork(AppDataContext context)
        {
            _context = context;
            Company = new CompanyRepository(context);
            Referral = new ReferralRepository(context);
            Position = new PositionRepository(context);
            Person = new PersonRepository(context);
            Service = new ServiceRepository(context);
        }



        private readonly AppDataContext _context;

        public CompanyRepository Company { get; set; }
        public ReferralRepository Referral { get; set; }
        public PositionRepository Position { get; set; }
        public PersonRepository Person { get; set; }
        public ServiceRepository Service { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}