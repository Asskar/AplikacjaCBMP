namespace Reporting
{
    public static class ReportingConfiguration
    {
        public static string ReportsePath { get; } = @"C:\CBMP\Reports\";
        public static string OutputFilesPath { get; } = ReportsePath + @"output\";

    }
}