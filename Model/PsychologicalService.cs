using System;
using System.Collections.Generic;

namespace Model
{
    public class PsychologicalService
    {
        public int Id { get; set; }
        public DateTime DateTimeOfService { get; set; }
        public double? Price { get; set; }
        public string PsychologicalServiceNumber { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int ReferralId { get; set; }
        public Referral Referral { get; set; }

        public int PsychologicalServiceTypeId { get; set; }
        public PsychologicalServiceType PsychologicalServiceType { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}