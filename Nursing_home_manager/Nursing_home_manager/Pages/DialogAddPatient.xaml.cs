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
            int x = 0;
            Int32.Parse(((ComboBoxItem)cb_severity.SelectedItem).Content.ToString());
            addDisease(tb_disease.Text, Convert.ToInt32(((ComboBoxItem)cb_severity.SelectedItem).Content.ToString()));
            listView.ItemsSource = null;
            listView.Items.Clear();

            ListViewItem item = new ListViewItem();
            listView.ItemsSource = listDisease;
            cb_severity.SelectedItem = ((ComboBoxItem)cb_severity.Items[0]);
            tb_disease.Text = "";

        }
        private void Button_DeleteDisease(object sender, RoutedEventArgs e)
        {
            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select diseases to delete them.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    
                    var itemToRemove = listDisease.Single(r => r.Name ==listView.SelectedItems[0].ToString() );
                    listView.ItemsSource = null;
                    listDisease.Remove(itemToRemove);
                    listView.ItemsSource = listDisease;
                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
