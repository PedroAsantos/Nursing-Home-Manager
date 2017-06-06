using Nursing_home_manager.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nursing_home_manager.Pages
{
    /// <summary>
    /// Interaction logic for DialogPatientMedicinesPage.xaml
    /// </summary>
    public partial class DialogPatientMedicinesPage : Page
    {
        ObservableCollection<Medicine> listMedicine;
        private Patient patient;
        public DialogPatientMedicinesPage(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
            initializeMedicineList();
        }

        private void initializeMedicineList()
        {

            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getPatientMedicines("+patient.Nif+")", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                listMedicine = new ObservableCollection<Medicine>();
                while (reader.Read())
                {
                    Medicine med = new Medicine();
                    if (reader["Name"] != DBNull.Value)
                        med.Name = reader["Name"].ToString();
                    if (reader["Dose"] != DBNull.Value)
                        med.Dose = reader.GetInt32(1);
                    if (reader["E_Day"] != DBNull.Value)
                        med.Day = reader["E_Day"].ToString();
                    if (reader["E_Hour"] != DBNull.Value)
                        med.Time = reader.GetTimeSpan(3);
                    if (reader["E_ID"] != DBNull.Value)
                        med.ID = reader.GetInt32(4);

                    listMedicine.Add(med);
                }
                //make your query
                listView.ItemsSource = listMedicine;
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }

        }
        private void Click_deleteMedicineButton(object sender, RoutedEventArgs e)
        {

            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select diseases to delete them.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Medicine med = (Medicine)listView.SelectedItem;
                    deleteMedicine(med);


                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void deleteMedicine(Medicine med)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_deleteMedicine", con.Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@E_NIF", patient.Nif);
                    cmd.Parameters.AddWithValue("@E_Hour", med.Time);
                    cmd.Parameters.AddWithValue("@E_Day", med.Day);
                    cmd.Parameters.AddWithValue("@medicineID", med.ID);
                    cmd.ExecuteNonQuery();


                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                finally
                {
                    con.conClose();//close connection
                }
            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
            listView.ItemsSource = null;
            listView.Items.Clear();
            initializeMedicineList();

        }

        private void Button_Click_Add_Medicine(object sender, RoutedEventArgs e)
        {

            if (((ComboBoxItem)cb_hour.SelectedItem).Content != null && ((ComboBoxItem)cb_day.SelectedItem).Content != null && ((ComboBoxItem)cb_periodo.SelectedItem).Content != null && ((ComboBoxItem)cb_minute.SelectedItem).Content != null)
            {
                TimeSpan time;
                DateTime dt;
                string timeString = ((ComboBoxItem)cb_hour.SelectedItem).Content.ToString()+":"+((ComboBoxItem)cb_minute.SelectedItem).Content.ToString() 
                    +" "+ ((ComboBoxItem)cb_periodo.SelectedItem).Content.ToString();
                bool res = DateTime.TryParse(timeString, out dt);
                time = dt.TimeOfDay;

                Medicine med = new Medicine()
                    {
                        Name = tb_medicineName.Text,
                        Time = time,
                        Periodo = ((ComboBoxItem)cb_periodo.SelectedItem).Content.ToString(),
                        Day = ((ComboBoxItem)cb_day.SelectedItem).Content.ToString(),
                        Dose = Convert.ToInt32(tb_dose.Text)
                };
                
                addMedicinetToDb(med);
                listView.ItemsSource = null;
                listView.Items.Clear();
                initializeMedicineList();
                //  ListViewItem item = new ListViewItem();
                //listView.ItemsSource = listMedicine;
                cb_day.SelectedItem = ((ComboBoxItem)cb_day.Items[0]);
                cb_hour.SelectedItem = ((ComboBoxItem)cb_hour.Items[1]);
                cb_minute.SelectedItem = ((ComboBoxItem)cb_minute.Items[0]);
                cb_periodo.SelectedItem = ((ComboBoxItem)cb_periodo.Items[0]);
                tb_dose.Text = "";
                tb_medicineName.Text = "";
          
            }

        }
      
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        private void addMedicinetToDb(Medicine med)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.
            
            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_newTaking", con.Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@E_NIF",patient.Nif );
                    cmd.Parameters.AddWithValue("@E_Hour", med.Time);
                    cmd.Parameters.AddWithValue("@E_Day", med.Day);
                    cmd.Parameters.AddWithValue("@medicineName", med.Name);
                    cmd.Parameters.AddWithValue("@Dose", med.Dose);
                    cmd.ExecuteNonQuery();
                

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                finally
                {
                    con.conClose();//close connection
                }
            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
    }
}
