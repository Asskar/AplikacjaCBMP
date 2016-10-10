using Model;

namespace AppCBMP.Registration.ViewModel.Components.ServiceComponents
{
    public class CompanyReferralPersonMessage
    {
        public Company Company { get; set; }
        public Referral Referral { get; set; } 
        public Person Person { get; set; }
        public PsychologicalServiceType PsychologicalServiceType { get; set; }
    }
}