using AppCBMP.Registration.ViewModel.Components.ServiceComponents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AppCBMP.Registration.ViewModel.Components
{
    public class PersonListViewModel:ViewModelBase
    {

        private RelayCommand<PsychologicalService> _removeSingleServiceCommand;
        private RelayCommand<PsychologicalService> _printSingleServiceCommand;
        private RelayCommand<Person> _removePersonCommand;
        private RelayCommand<Person> _printAllPersonsServicesCommand;
        private ObservableCollection<Person> _persons;


        public PersonListViewModel()
        {
            Messenger.Default.Register<PersonPsychServiceMessage>(this, HandleMessage);
            Persons= new ObservableCollection<Person>();
            PrintAllPersonsServicesCommand = new RelayCommand<Person>(PrintAllForSinglePerson);
            RemovePersonCommand = new RelayCommand<Person>(RemovePerson);
            PrintSingleServiceCommand = new RelayCommand<PsychologicalService>(PrintSingleService);
            RemoveSingleServiceCommand = new RelayCommand<PsychologicalService>(RemoveSingleService);

            //design data
            var person = new Person()
            {
                Pesel = "86030403770",
                FirstName = new FirstName {Name = "Piotr"},
                LastName = "Kiliński",
                BirthPlace = "Częstochowa",
                PhoneNumber = 535930370,
                Street = "Irzykowskiego",
                City = "Częstochowa",
                PostCode = "42-217",
                HouseNumber = "3/24",
                Education = "średnie"
            };
            person.PsychologicalServices = new ObservableCollection<PsychologicalService>()
            {
                new PsychologicalService()
                {
                    Positions = new ObservableCollection<Position>() {new Position() {Name = "dupa13212"}},
                    Person = person
                }
            };
            Persons.Add(person);

        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { Set(ref _persons, value); }
        }
        
        public RelayCommand<Person> PrintAllPersonsServicesCommand
        {
            get { return _printAllPersonsServicesCommand; }
            set { Set(ref _printAllPersonsServicesCommand, value); }
        }
        
        public RelayCommand<Person> RemovePersonCommand
        {
            get { return _removePersonCommand; }
            set { Set(ref _removePersonCommand, value); }
        }
       
        public RelayCommand<PsychologicalService> PrintSingleServiceCommand
        {
            get { return _printSingleServiceCommand; }
            set { Set(ref _printSingleServiceCommand, value); }
        }
       
        public RelayCommand<PsychologicalService> RemoveSingleServiceCommand
        {
            get { return _removeSingleServiceCommand; }
            set { Set(ref _removeSingleServiceCommand, value); }
        }

        
        private void PrintAllForSinglePerson(Person person)
        {
            MessageBox.Show("Drukuje wszystko");
            //TODO PIOTREK

        }

        private void PrintSingleService(PsychologicalService psychologicalService)
        {
            MessageBox.Show("Drukuje jedno badanie");
            psychologicalService.Print();
        }

        private void RemovePerson(Person person)
        {
            MessageBox.Show("Usuwam osobe");
            Persons.Remove(person);
            
        }

        private void RemoveSingleService(PsychologicalService psychologicalService)
        {
            MessageBox.Show("Usuwam jedno badanie");
            Persons.First(p => p.Id == psychologicalService.PersonId).
                PsychologicalServices.Remove(psychologicalService);
            RaisePropertyChanged(()=>Persons);
        }

        private void HandleMessage(PersonPsychServiceMessage message)
        {
            if (!Persons.Contains(message.Person))
                Persons.Add(message.Person);
            if(Persons.First(p=> p==message.Person).PsychologicalServices==null)
                Persons.First(p => p == message.Person).PsychologicalServices= new ObservableCollection<PsychologicalService>() {};
            Persons.First(p=> p==message.Person).PsychologicalServices.Add(message.PsychologicalService);
            
        }

    }
}