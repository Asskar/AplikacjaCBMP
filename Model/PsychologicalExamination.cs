using System.Collections.Generic;

namespace Model
{
    public class PsychologicalExamination : Service
    {
        public int TypeId { get; set; }
        public PsychologicalExaminationType Type { get; set; }
        
        public ICollection<Position> Positions { get; set; }
    }
}