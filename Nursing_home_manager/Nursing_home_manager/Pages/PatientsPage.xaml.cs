using Nursing_home_manager.Classes;
using System;
using System.Collections.Generic;
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

        private List<Patient> listPatients;
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

                SqlCommand cmd = new SqlCommand("SELECT Nif,Name,sex,Phone,Age,Check_in,Check_out,Authorization_to_leave,Entry_Date,BedNumber  from exemplo1.patient JOIN exemplo1.BED on exemplo1.patient.E_BedNumber = exemplo1.bed.BedNumber", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();
                patientsList.Items.Clear();
                listPatients = new List<Patient>();
                while (reader.Read())
                {
                    Patient patient = new Patient();
                    patient.Nif = reader["NIF"].ToString();
                    patient.Name = reader["Name"].ToString();
                    patient.Sex = reader["sex"].ToString();
                    patient.Phone = reader.GetInt32(3);
                    patient.Age = reader.GetInt32(4);
                    patient.Check_in = reader.GetDateTime(5);
                    patient.Check_out = reader.GetDateTime(6);
                    patient.Authorization_to_leave = reader.GetBoolean(7);
                    patient.Entry_Date = reader.GetDateTime(9);
                    patient.Exit_Date = reader.GetDateTime(10);
                    patient.BedNumber = reader.GetInt32(8);
                    patient.RoomNumber = reader.GetInt32(11);
                    patientsList.Items.Add(patient);
                 //   listPatients.Add(patient); a lista não é precisa?
                }
                //make your query

                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }

        private void Button_AddPatient(object sender, RoutedEventArgs e)
        {
            DialogAddPatient dialogAddPatient = new DialogAddPatient();
            if (dialogAddPatient.ShowDialog() == true)
            {
                Console.WriteLine("qwe");
            }
        }
    }
}
