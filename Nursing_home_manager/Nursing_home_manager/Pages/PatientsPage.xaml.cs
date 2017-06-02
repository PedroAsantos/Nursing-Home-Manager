using Nursing_home_manager.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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

namespace Nursing_home_manager.Pages
{
    /// <summary>
    /// Interaction logic for PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {

        private ObservableCollection<Patient> listPatients;
        public PatientsPage()
        {
            InitializeComponent();
            InitializePatientList();
        }

        private void InitializePatientList()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getPatients()", con.Con);
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
                        patient.Check_in = reader.GetDateTime(5);
                    if (reader["Check_out"] != DBNull.Value)
                    {
                        var sad = reader.GetDateTime(2);
                        patient.Check_out = new DateTime(sad.Year, sad.Month, sad.Day);
                        Console.WriteLine(patient.Check_out);
                    }
                    if (reader["Authorization_to_leave"] != DBNull.Value)
                        patient.Authorization_to_leave = reader.GetBoolean(7);
                    if (reader["Entry_Date"] != DBNull.Value)
                        patient.Entry_Date = reader.GetDateTime(9);
                    if (reader["Exit_Date"] != DBNull.Value)
                        patient.Exit_Date = reader.GetDateTime(10);
                    if (reader["E_BedNumber"] != DBNull.Value)
                        patient.BedNumber = reader.GetInt32(8);
                    if (reader["DependentName"] != DBNull.Value)
                        patient.DependentName = reader["NIF"].ToString();
                    if (reader["DependentCC"] != DBNull.Value)
                        patient.DependentCC = reader["DependentCC"].ToString();
                    if (reader["DependentAddress"] != DBNull.Value)
                        patient.DependentAddress = reader["DependentAddress"].ToString();
                    if (reader["DependentPhone"] != DBNull.Value)
                        patient.DependentPhone = reader.GetInt32(15);
                    if (reader["Relationship"] != DBNull.Value)
                        patient.DependentKinship = reader["Relationship"].ToString();

                    /*
                    SqlCommand cmd1 = new SqlCommand("SELECT * from dbo.getPatientDiseases(" + patient.Nif + ")", con.Con);
                        SqlDataReader reader1 = cmd1.ExecuteReader();

                        // reader = cmd.ExecuteReader();
                        List<Disease> listDiseases = new List<Disease>();
                        while (reader1.Read())
                        {
                            Disease disease = new Disease();
                            if (reader1["Name"] != DBNull.Value)
                                disease.Name = reader1.GetString(0);
                            if (reader1["Severity"] != DBNull.Value)
                                disease.Severity = reader1.GetInt32(1);
                            listDiseases.Add(disease);
                        }
                        patient.DiseaseList = listDiseases;
                        //patientsList.Items.Add(patient);
                        listPatients.Add(patient);
                        //make your query
                        patientsList.ItemsSource = listPatients;
                        con.conClose();//close your connection  //so quando entro no edit é que ele adiciona as doenças
                    */
                    //patientsList.Items.Add(patient);
                    listPatients.Add(patient); 
                }
                //make your query
                 patientsList.ItemsSource = listPatients;
                con.conClose();//close your connection

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
               InitializePatientList();
            }
        }
    }
}
