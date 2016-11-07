using System.Collections;
using System.Collections.ObjectModel;
using Model;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class PersonsPsychologicalServices : IEnumerable
    {
        public Person Person { get; set; }
        public ObservableCollection<PsychologicalService> PsychologicalServices { get; set; }

        public IEnumerator GetEnumerator()
        {
            return PsychologicalServices.GetEnumerator();
        }
    }
}