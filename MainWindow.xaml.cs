using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.Win32;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        public MainWindow()
        {
            DataContext = new ViewModel();
            viewModel = DataContext as ViewModel;
            viewModel.InitModel();   
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.InitPersons();
        }

        private void EditPersons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "PictureSource")
                e.Cancel = true;
            if (e.PropertyName == "BirthDate")
                e.Cancel = true;            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(viewModel.Persons).Refresh();
            viewModel.FilterString = txtBox.Text;
            viewModel.FilteredPersons.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPerson();
            var addPerson = new AddPerson();
            Application.Current.MainWindow.Opacity = 0.5;
            addPerson.Closed += AddPerson_Closed;
            addPerson.Owner = this;
            addPerson.vm = viewModel;
            addPerson.ShowDialog();
        }

        private void AddPerson_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Opacity = 1;
        }

        private void picture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog()==true)
            {
                viewModel.CurrentChosen.PictureSource = op.FileName;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Serializers.Deserialize(viewModel.Persons);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Serializers.Serialize(viewModel.Persons);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveCurrentChosen();
        }
    }

  

    
}
