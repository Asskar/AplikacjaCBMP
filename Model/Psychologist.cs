using System.Collections.Generic;

namespace Model
{
    public class Psychologist
    {
        public int Id { get; set; }
        public string Initials { get; set; }
        
        public ICollection<PsychologicalService> PsychologicalServices { get; set; }  
    }
}