using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;

namespace ContactManager
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Person currentChosen;
        public Person CurrentChosen
        {
            get { return currentChosen; }
            set { currentChosen = value;  NotifyPropertyChanged(); }
        }
        private Person personToAdd;
        public Person PersonToAdd
        {
            get { return personToAdd; }
            set { personToAdd = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<Person> persons;
        public ObservableCollection<Person> Persons
        {
            get { return persons; }
            set { persons = value;  NotifyPropertyChanged(); }
        }
        public ICollectionView FilteredPersons { get; private set; }
        private string filterString;
        public string FilterString
        {
            get { return filterString; }
            set { filterString = value; NotifyPropertyChanged(); }
        }
        public void InitModel()
        {
            Persons = new ObservableCollection<Person>();
            FilteredPersons = CollectionViewSource.GetDefaultView(Persons);
            FilteredPersons.Filter = ListFilter;
        }
        public void InitPersons()
        {
            for (int i = 0; i < 5; i++)
            {
                Persons.Add(new Person { Name = "John", LastName = "Kowalski", PhoneNumber = 12345, Sex = Sex.Male, BirthDate = new DateTime(1996, 8, 17) , City = "Warsaw", PictureSource="/ContactManager;component/Resources/man.png"});
                Persons.Add(new Person { Name = "Jane", LastName = "KOwalska", PhoneNumber = 3456, Sex = Sex.Female, BirthDate = new DateTime(1887, 4, 13), City="New York" , PictureSource="/ContactManager;component/Resources/woman.jpg"});
            }
        }
        public void AddPerson()
        {
            PersonToAdd = new Person();
        }
        public void RemoveCurrentChosen()
        {
            Persons.Remove(CurrentChosen);
        }
        private bool ListFilter(object item)
        {
            if (String.IsNullOrEmpty(FilterString))
                return true;
            string[] filters = filterString.Split(' ');
            int count = filters.Length;
            if (string.IsNullOrEmpty(filters.Last<string>()))
                count--;
            Person p = item as Person;
            for (int i = 0; i < count; i++)
            {
                if (p.Name.ToLower().Contains(filters[i].ToLower()) || p.LastName.ToLower().Contains(filters[i].ToLower()) || p.City.ToLower().Contains(filters[i].ToLower()) || p.PhoneNumber.ToString().Contains(filters[i]))
                    return true;
            }
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //public enum Sex { Male, Female};
    //public class Person : INotifyPropertyChanged
    //{
        
    //    private DateTime birthDate;
    //    public DateTime BirthDate
    //    {
    //        get { return birthDate; }
    //        set { birthDate = value;  NotifyPropertyChanged(); }
    //    }
    //    private string name;
    //    public string Name
    //    {
    //        get { return name; }
    //        set { name = value;  NotifyPropertyChanged(); }
    //    }

    //    private string lastName;
    //    public string LastName
    //    {
    //        get { return lastName;}
    //        set { lastName = value; NotifyPropertyChanged(); }
    //    }

    //    private int phoneNumber;
    //    public int PhoneNumber
    //    {
    //        get { return phoneNumber; }
    //        set { phoneNumber = value;  NotifyPropertyChanged(); }
    //    }

    //    private Sex sex;
    //    public Sex Sex
    //    {
    //        get { return sex; }
    //        set { sex = value; NotifyPropertyChanged(); }
    //    }
    //    private string city;
    //    public string City
    //    {
    //        get { return city; }
    //        set { city = value;  NotifyPropertyChanged(); }
    //    }
    //    public string pictureSource;
    //    public string PictureSource
    //    {
    //        get { return pictureSource; }
    //        set { pictureSource = value; SetPictureSource(pictureSource); NotifyPropertyChanged(); }
    //    }
        
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    //    {
    //        var handler = PropertyChanged;
    //        if (handler != null)
    //            handler(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    private void SetPictureSource(string source = null)
    //    {
    //        if (source == null)
    //        {
    //            if (Sex == Sex.Male)
    //                pictureSource= "/ContactManager;component/Resources/man.png";
    //            else
    //                pictureSource= "/ContactManager;component/Resources/woman.jpg";
    //        }
    //        pictureSource= source;
    //    }
    //    public override string ToString()
    //    {
    //        return base.ToString();
    //    }
    //}
}
