using Nustache.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Model.Reporting
{
    public class ServiceReportGenerator
    {
        private ReportDeterminer _determiner;
        
        public string Generate(PsychologicalService psychologicalService)
        {
            //TODO: refactor to select propper report template
            //var report = _determiner.GetRaport(psychologicalService);
            var report = File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, "MP.cstex"));
            
            var result = Render.StringToString(report, psychologicalService);
            result = ReplaceDecimalValuesWithCharacters(result);

            var outputPath = Path.Combine(ReportingConfiguration.OutputFilesPath, DateTime.Now.ToString("yy-MM-dd"));

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var fileName = $"{DateTime.Now.ToString("yy-MM-dd")}_{psychologicalService.Person.Pesel}";

            File.WriteAllText(Path.Combine(outputPath, $"{fileName}.tex"), result);

            RunTexProcess("miktex-xetex.exe", $"--interaction=batchmode --undump=xelatex {fileName}.tex", outputPath);
            //RunTexProcess("texify.exe", $"--batch --clean --pdf {fileName}.tex", outputPath);

            return Path.Combine(outputPath,$"{fileName}.pdf");
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

        public string ReplaceDecimalValuesWithCharacters(string source)
        {
            source = source.Replace("&#261;", "ą");
            source = source.Replace("&#263;", "ć");
            source = source.Replace("&#281;", "ę");
            source = source.Replace("&#322;", "ł");
            source = source.Replace("&#324;", "ń");
            source = source.Replace("&#243;", "ó");
            source = source.Replace("&#247;", "ś");
            source = source.Replace("&#378;", "ź");
            source = source.Replace("&#380;", "ż");

            source = source.Replace("&#260;", "Ą");
            source = source.Replace("&#262;", "Ć");
            source = source.Replace("&#280;", "Ę");
            source = source.Replace("&#321;", "Ł");
            source = source.Replace("&#323;", "Ń");
            source = source.Replace("&#211;", "Ó");
            source = source.Replace("&#346;", "Ś");
            source = source.Replace("&#377;", "Ź");
            source = source.Replace("&#379;", "Ż");

            return source;
        }
    }

    class ReportDeterminer
    {
        private readonly IList<string> _socialisticExaminations = new List<string>
            {
                "bron",
                "kierowcaPowyzej3,5"
            };

        public string GetRaport(PsychologicalService psychologicalService)
        {
            var name = psychologicalService.Positions.First().Name;

            if (psychologicalService.Positions.Count == 1 && _socialisticExaminations.Contains(name))           
                    return File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, name));
            
            if (psychologicalService.PsychologicalServiceType.Name == "MP")
                return File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, "MP.cstex"));
            
            return File.ReadAllText(Path.Combine(ReportingConfiguration.ReportsePath, "P.cstex"));
        }
    }
}
