using System.Collections.Generic;

namespace AppCBMP.Model
{
    public class PsychologicalExamination : Service
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int ReferralId { get; set; }
        public Referral Referral { get; set; }
        public int TypeId { get; set; }
        public PsychologicalExaminationType Type { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}