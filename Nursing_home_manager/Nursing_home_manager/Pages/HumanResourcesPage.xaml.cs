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
    /// Interaction logic for HumanResourcesPage.xaml
    /// </summary>
    public partial class HumanResourcesPage : Page
    {

        private ObservableCollection<HumanResource> listHumanResources;
        public HumanResourcesPage()
        {
            InitializeComponent();
            InitializeHumanResourceList();
        }
        private void InitializeHumanResourceList()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getHumanResources()", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                listHumanResources = new ObservableCollection<HumanResource>();
                while (reader.Read())
                {
                    HumanResource humanResource = new HumanResource();
                    if (reader["NIF"] != DBNull.Value)
                        humanResource.Nif = reader["NIF"].ToString();
                    if (reader["Name"] != DBNull.Value)
                        humanResource.Name = reader["Name"].ToString();
                    if (reader["Address"] != DBNull.Value)
                        humanResource.Name = reader["Address"].ToString();
                    if (reader["Phone"] != DBNull.Value)
                        humanResource.PhoneNumber = reader.GetInt32(2);
                    if (reader["StartDate"] != DBNull.Value)
                        humanResource.StartDate = reader.GetDateTime(5);
                    if (reader["Salary"] != DBNull.Value)
                        humanResource.Salary = reader.GetInt32(4);
                    if (reader["Designation"] != DBNull.Value)
                        humanResource.Type = reader["Designation"].ToString();
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
                    listHumanResources.Add(humanResource);
                }
                //make your query
                humanResourcesList.ItemsSource = listHumanResources;
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
    }
}
