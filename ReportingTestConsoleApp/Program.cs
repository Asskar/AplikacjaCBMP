using Model;
using System.Collections.Generic;
using Model.Reporting;

namespace ReportingTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var repoting = new ServiceReportGenerator();
            var psychologicalService = new PsychologicalService();
            psychologicalService.Person = new Person();
            psychologicalService.Person.Pesel = "86030403771";
            psychologicalService.Positions = new List<Position>
            {
                new Position() { Name = "nieBadanie" }
            };
            psychologicalService.PsychologicalServiceType = new PsychologicalServiceType
            {
                Name = "MP"
            };
            repoting.Generate(psychologicalService);
        }
    }
}
