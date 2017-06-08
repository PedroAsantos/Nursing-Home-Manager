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
    /// Interaction logic for PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {

        private ObservableCollection<Patient> listPatients;
        private int numberPage;
        public PatientsPage()
        {
            this.numberPage = 1;
            InitializeComponent();
            InitializePatientList(null,null);
            bt_beforePage.Opacity = 0;
            bt_beforePage.IsEnabled = false;
        }

        private void InitializePatientList(object sender, KeyEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getPatients(@PatientNif,@PatientName,@Sex,@authorization,@RoomNumber,@PhoneNUmber,@Checkout,@PageNumber,@RowsPage)", con.Con);
                cmd.Parameters.AddWithValue("@PageNumber", numberPage);
                cmd.Parameters.AddWithValue("@RowsPage", 15);
                if (tb_patientNif.Text != "")
                    cmd.Parameters.AddWithValue("@PatientNif", tb_patientNif.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientNif",DBNull.Value);

                if (tb_patientName.Text != "")
                    cmd.Parameters.AddWithValue("@PatientName", tb_patientName.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientName", DBNull.Value);

                if (cb_female.IsChecked == true && cb_male.IsChecked == false)
                    cmd.Parameters.AddWithValue("@Sex", "f");
                else if(cb_female.IsChecked == false && cb_male.IsChecked == true)
                    cmd.Parameters.AddWithValue("@Sex", "m");
                else
                    cmd.Parameters.AddWithValue("@Sex", DBNull.Value);

                if (cb_authorizationYes.IsChecked == true && cb_authorizationNo.IsChecked == false)
                    cmd.Parameters.AddWithValue("@authorization", true);
                else if (cb_authorizationYes.IsChecked == false && cb_authorizationNo.IsChecked == true)
                    cmd.Parameters.AddWithValue("@authorization", false);
                else
                    cmd.Parameters.AddWithValue("@authorization", DBNull.Value);

                if (tb_patientRoom.Text != "")
                    cmd.Parameters.AddWithValue("@RoomNumber", tb_patientRoom.Text);
                else
                    cmd.Parameters.AddWithValue("@RoomNumber", DBNull.Value);

                if (tb_patientPhone.Text != "")
                    cmd.Parameters.AddWithValue("@PhoneNUmber", tb_patientPhone.Text);
                else
                    cmd.Parameters.AddWithValue("@PhoneNUmber", DBNull.Value);

                if (cb_checkout.IsChecked == false)
                    cmd.Parameters.AddWithValue("@Checkout", true);
                else
                    cmd.Parameters.AddWithValue("@Checkout", DBNull.Value);



                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                listPatients = new ObservableCollection<Patient>();
                while (reader.Read())
                {
                    Patient patient = new Patient();
                    if (reader["NIF"] != DBNull.Value)
                        patient.Nif = reader["NIF"].ToString();
                    if (reader["Name"] != DBNull.Value)
                        patient.Name = reader["Name"].ToString();
                    if (reader["sex"] != DBNull.Value)
                    {
                        patient.Sex = reader["sex"].ToString();
                        if (String.Compare(patient.Sex, "m") == 0)
                        {
                            patient.isMale = true;
                        }else
                        {
                            patient.isFemale = true;
                        }
                    }
                    if (reader["Phone"] != DBNull.Value)
                        patient.Phone = reader.GetInt32(3);
                    if (reader["Age"] != DBNull.Value)
                        patient.Age = reader.GetInt32(4);
                    if(reader["Check_in"] != DBNull.Value)
                        patient.Check_in = reader.GetDateTime(5).ToShortDateString();
                    if (reader["Check_out"] != DBNull.Value)
                        patient.Check_out = reader.GetDateTime(6).ToShortDateString();
                    if (reader["Authorization_to_leave"] != DBNull.Value)
                        patient.Authorization_to_leave = reader.GetBoolean(7);
                    if (reader["RoomNumber"] != DBNull.Value)
                        patient.RoomNumber = reader.GetInt32(11);
                    if (reader["Entry_Date"] != DBNull.Value)
                        patient.Entry_Date = reader.GetDateTime(9).ToShortDateString();
                    if (reader["Exit_Date"] != DBNull.Value)
                        patient.Exit_Date = reader.GetDateTime(10).ToShortDateString();
                    if (reader["E_BedNumber"] != DBNull.Value)
                        patient.BedNumber = reader.GetInt32(8);
                    if (reader["DependentName"] != DBNull.Value)
                        patient.DependentName = reader["DependentName"].ToString();
                    if (reader["DependentCC"] != DBNull.Value)
                        patient.DependentCC = reader["DependentCC"].ToString();
                    if (reader["DependentAddress"] != DBNull.Value)
                        patient.DependentAddress = reader["DependentAddress"].ToString();
                    if (reader["DependentPhone"] != DBNull.Value)
                        patient.DependentPhone = reader.GetInt32(15);
                    if (reader["Relationship"] != DBNull.Value)
                        patient.DependentKinship = reader["Relationship"].ToString();

                    listPatients.Add(patient); 
                }
                //make your query
                patientsList.ItemsSource = listPatients;
                con.conClose();//close your connection
                if(listPatients.Count < 15)
                {
                    bt_nextPage.Opacity = 0;
                    bt_nextPage.IsEnabled = false;
                }else
                {
                    bt_nextPage.Opacity = 1;
                    bt_nextPage.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private void listView_Click(object sender, RoutedEventArgs e)
        {   
            Patient  patient = (Patient)(sender as ListView).SelectedItem;
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getPatientDiseases(" + patient.Nif + ")", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Disease> listDiseases = new List<Disease>();
                while (reader.Read())
                {
                    Disease disease = new Disease();
                    if (reader["E_name"] != DBNull.Value)
                        disease.Name = reader.GetString(0);
                    if (reader["Seriousness"] != DBNull.Value)
                        disease.Severity = reader.GetInt32(1);
                    listDiseases.Add(disease);
                }
                patient.DiseaseList = listDiseases;
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }

                if (patient != null)
                {
                    DialogEditPatient dialogEditPatient = new DialogEditPatient(patient);
               
                    if(dialogEditPatient.ShowDialog() == true)
                    {

                    }
                }
        }
        private void Button_AddPatient(object sender, RoutedEventArgs e)
        {
            DialogAddPatient dialogAddPatient = new DialogAddPatient();
            if (dialogAddPatient.ShowDialog() == true)
            {
               InitializePatientList(null, null);
            }
        }
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            numberPage += 1;
            if (numberPage > 1)
            {
                bt_beforePage.Opacity = 1;
                bt_beforePage.IsEnabled = true;
            }
            InitializePatientList(null,null);
        }
        private void Button_BeforePage(object sender, RoutedEventArgs e)
        {
           
            numberPage -= 1;
            if (numberPage==1)
            {
                 bt_beforePage.Opacity = 0;
                 bt_beforePage.IsEnabled = false;
            }
            InitializePatientList(null, null);
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            InitializePatientList(null, null);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
