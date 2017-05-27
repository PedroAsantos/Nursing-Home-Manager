using Nursing_home_manager.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Nursing_home_manager.Pages
{
    /// <summary>
    /// Interaction logic for DialogAddPatient.xaml
    /// </summary>
    public partial class DialogAddPatient : Window
    {
        List<Disease> listDisease = new List<Disease>();
        public DialogAddPatient()
        {
            InitializeComponent();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
             
        }
       
        private void addDisease(string name, int severity)
        {
            listDisease.Add(new Disease() { Name = name, Severity = severity });
        }

        private void Button_Click_AddDisease(object sender, RoutedEventArgs e)
        {
            addDisease(tb_disease.Text,1);
            listView.ItemsSource = null;
            listView.Items.Clear();

            ListViewItem item = new ListViewItem();
            listView.ItemsSource = listDisease;
            tb_severity.Text = "";
            tb_disease.Text = "";

        }
    }
}
