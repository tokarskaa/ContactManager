using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.IO;

namespace ContactManager
{
    class Serializers
    {
        public static void Serialize(ObservableCollection<Person> p)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "xml files |*.xml";
            sf.FilterIndex = 2;
            if (sf.ShowDialog() == true)
            {
                string path = sf.FileName;
                using (StreamWriter wr = new StreamWriter(path))
                {
                    xs.Serialize(wr, p);
                }
            }
        }

        public static void Deserialize(ObservableCollection<Person> p)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog()==true)
            {
                string path = of.FileName;
                ObservableCollection<Person> imported = new ObservableCollection<Person>();
                using (StreamReader rd = new StreamReader(path))
                {
                    imported = xs.Deserialize(rd) as ObservableCollection<Person>;
                }
                foreach (var person in imported)
                    p.Add(person);
            }
            
        }
    }
}
