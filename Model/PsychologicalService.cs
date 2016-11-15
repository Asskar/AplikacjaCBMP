using Model.Reporting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Model
{
    public class PsychologicalService
    {
        private readonly ServiceReportGenerator _serviceReportGenerator;
        public PsychologicalService()
        {
            //ToDo: inject with IOC (singleton?)
            _serviceReportGenerator = new ServiceReportGenerator();
        }

        public int Id { get; set; }
        public DateTime DateTimeOfService { get; set; }
        public double? Price { get; set; }
        public int Number { get; set; }

        public int PsychologistId { get; set; }
        public Psychologist Psychologist { get; set; }

        public int LocalizationId { get; set; }
        public Localization Localization { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int ReferralId { get; set; }
        public Referral Referral { get; set; }

        public int PsychologicalServiceTypeId { get; set; }
        public PsychologicalServiceType PsychologicalServiceType { get; set; }
        public ICollection<Position> Positions { get; set; }

        public void Print()
        {
            //var path = _serviceReportGenerator.Generate(this);
            var path = @"C:\CBMP\test.pdf";
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = path;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }
    }
}