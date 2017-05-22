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
    public enum Sex { Male, Female };
    public class Person : INotifyPropertyChanged
    {

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; NotifyPropertyChanged(); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyPropertyChanged(); }
        }

        private int phoneNumber;
        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; NotifyPropertyChanged(); }
        }

        private Sex sex;
        public Sex Sex
        {
            get { return sex; }
            set { sex = value; NotifyPropertyChanged(); }
        }
        private string city;
        public string City
        {
            get { return city; }
            set { city = value; NotifyPropertyChanged(); }
        }
        public string pictureSource;
        public string PictureSource
        {
            get { return pictureSource; }
            set { pictureSource = value; SetPictureSource(pictureSource); NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SetPictureSource(string source = null)
        {
            if (source == null)
            {
                if (Sex == Sex.Male)
                    pictureSource = "/ContactManager;component/Resources/man.png";
                else
                    pictureSource = "/ContactManager;component/Resources/woman.jpg";
            }
            pictureSource = source;
        }
    }
}
