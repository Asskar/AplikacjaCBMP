using System.Collections.Generic;

namespace Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte ReportType { get; set; } = 0;

        public ICollection<PsychologicalService> PsychologicalServices { get; set; }
    }
}