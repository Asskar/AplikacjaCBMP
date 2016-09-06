using System.Collections.Generic;

namespace AppCBMP.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PsychologicalExamination> PsychologicalExaminations { get; set; }
    }
}