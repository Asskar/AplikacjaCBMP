using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using AppCBMP.DAL;
using AppCBMP.DAL.Persistence;
using AppCBMP.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AppCBMP.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private RelayCommand<string> _addNewPositionToListCommand;
        private RelayCommand<Position> _addPositionToListCommand;
        private RelayCommand<Position> _removePositionFromListCommand;
        private RelayCommand<Person> _removePersonFromListCommand;
        private RelayCommand<Person> _printPersonCommand;
        private RelayCommand<Person> _addPersonToInvoiceCommand;
        private RelayCommand _addPersonToDbCommand;
        private Registration _registration;
        public List<string> ListaImion { get; set; }

        public RegistrationViewModel()
        {
            _registration = new Registration(new UnitOfWork(new AppDataContext()));
            _addPersonToDbCommand = new RelayCommand(AddPersonToDb, IsValid);
            _addPositionToListCommand = new RelayCommand<Position>(AddPositionToService);
            _removePositionFromListCommand = new RelayCommand<Position>(RemovePositionFromService);
            _removePersonFromListCommand = new RelayCommand<Person>(RemovePersonFromList);
            _printPersonCommand = new RelayCommand<Person>(PrintPerson);
            _addPersonToInvoiceCommand = new RelayCommand<Person>(AddPersonToInvoice);
            _addNewPositionToListCommand= new RelayCommand<string>(AddNewPositionToList);
            ListaImion=new List<string>() {"Michał","Iga","Miłosz"};
        }

        public Registration Registration
        {
            get { return _registration; }
            set { Set(() => Registration, ref _registration, value); }
        }

        public RelayCommand AddPersonToDbCommand
        {
            get { return _addPersonToDbCommand; }
            set { Set(() => AddPersonToDbCommand, ref _addPersonToDbCommand, value); }
        }
        public RelayCommand<string> AddNewPositionToListCommand
        {
            get { return _addNewPositionToListCommand; }
            set { Set(() => AddNewPositionToListCommand, ref _addNewPositionToListCommand, value); }
        }
        public RelayCommand<Position> AddPositionToListCommand
        {
            get { return _addPositionToListCommand; }
            set { Set(() => AddPositionToListCommand, ref _addPositionToListCommand, value); }
        }
        public RelayCommand<Position> RemovePositionFromListCommand
        {
            get { return _removePositionFromListCommand; }
            set { Set(() => RemovePositionFromListCommand, ref _removePositionFromListCommand, value); }
        }
        public RelayCommand<Person> RemovePersonFromListCommand
        {
            get { return _removePersonFromListCommand; }
            set { Set(() => RemovePersonFromListCommand, ref _removePersonFromListCommand, value); }
        }
        public RelayCommand<Person> PrintPersonCommand
        {
            get { return _printPersonCommand; }
            set { Set(() => PrintPersonCommand, ref _printPersonCommand, value); }
        }
        public RelayCommand<Person> AddPersonToInvoiceCommand
        {
            get { return _addPersonToInvoiceCommand; }
            set { Set(() => AddPersonToInvoiceCommand, ref _addPersonToInvoiceCommand, value); }
        }


        private void AddPersonToDb()
        {
            _registration.CompleteRegistration();
        }

        private void AddNewPositionToList(string s)
        {
            _registration.AddNewPositionToDb(s);
        }
        private void AddPositionToService(Position p)
        {
            _registration.AddPositionToService(p);
            AddPositionToListCommand = new RelayCommand<Position>(AddPositionToService);
        }
        private void RemovePositionFromService(Position p)
        {
            _registration.RemovePositionFromService(p);
            RemovePositionFromListCommand = new RelayCommand<Position>(RemovePositionFromService);
        }

        private void AddPersonToInvoice(Person obj)
        {
            string message = $"Dodano do fauktury {obj.FirstName} {obj.LastName}";
            MessageBox.Show(message);
        }

        private void PrintPerson(Person obj)
        {
            string message = $"drukuje się osoba: {obj.FirstName} {obj.LastName}";
            MessageBox.Show(message);
        }

        private void RemovePersonFromList(Person obj)
        {
            string message = $"Usunięto z listy: {obj.FirstName} {obj.LastName}";
            MessageBox.Show(message);
            _registration.Persons.Remove(obj);
        }
        private bool IsValid()
        {
            return true;
        }
    }
}