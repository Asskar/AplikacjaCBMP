using System;

namespace Model
{
    public class Service
    {
        public int Id { get; set; }
        public DateTime DateTimeOfService { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int ReferralId { get; set; }
        public Referral Referral { get; set; }

        //public int InvoiceId { get; set; }
        //public Invoice Invoice { get; set; }
    }
}