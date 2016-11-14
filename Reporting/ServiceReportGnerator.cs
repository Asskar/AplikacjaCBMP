using Model;
using Nustache.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Reporting
{
    public class ServiceReportGnerator
    {
        private ReportDeterminer _determiner;

        public ServiceReportGnerator()
        {

        }

        public void Generate(PsychologicalService psychologicalService)
        {
            //string report = _determiner.GetRaport(psychologicalService);
            var report = File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, "MP.cstex"));

            //string result;
            //using (var service = IsolatedRazorEngineService.Create(Helpers.SandboxCreator))
            //{
            //    result = service.RunCompile(report, "key", typeof(PsychologicalService), psychologicalService);
            //}

            var result = Render.StringToString(report, psychologicalService);


            var outputPath = Path.Combine(ReportingConfiguration.OutputFilesPath, DateTime.Now.ToString("yy-MM-dd"));

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var fileName = $"{DateTime.Now.ToString("yy-MM-dd")}_{psychologicalService.Person.Pesel}";

            File.WriteAllText(Path.Combine(outputPath, $"{fileName}.tex"), result);

            RunTexProcess("texify.exe", $"--batch --clean --pdf {fileName}.tex", outputPath);
        }
        private static void RunTexProcess(string command, string arguments, string templatePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = Path.Combine(templatePath);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }
    }

    class ReportDeterminer
    {
        private readonly IList<string> _specialistic_examinations = new List<string>
            {
                "bron",
                "kierowcaPowyzej3,5"
            };

        public string GetRaport(PsychologicalService psychologicalService)
        {
            var name = psychologicalService.Positions.First().Name;

            if (psychologicalService.Positions.Count == 1 && _specialistic_examinations.Contains(name))           
                    return File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, name));
            
            if (psychologicalService.PsychologicalServiceType.Name == "MP")
                return File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, "MP.cstex"));
            
            return File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, "P.cstex"));
        }
    }
}
