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
using System.Windows.Shapes;

namespace Nursing_home_manager.Pages.Dialogs.Visits
{
    /// <summary>
    /// Interaction logic for DialogAddVisit.xaml
    /// </summary>
    public partial class DialogAddVisit : Window
    {
        private ObservableCollection<Patient> listPatients;
        private int numberPage = 1;
        private Patient selectedPatient;
        private Visit selectedVisit;
        public DialogAddVisit()
        {
            InitializeComponent();
            b_addVisitor.IsEnabled = false;
            b_addVisitor.Opacity = 0;
            ToggleButton_Click(null,null);
        }

        private void Button_Click_AddVisitor(object sender, RoutedEventArgs e)
        {
            DialogAddVisitor dialogAddVisitor = new DialogAddVisitor();
            if (dialogAddVisitor.ShowDialog() == true)
            {
                selectedVisit = new Visit()
                {
                    VisitName = dialogAddVisitor.VisitorName,
                    VisitCC = dialogAddVisitor.VisitorCC
                };
                txb_visitorNameIs.Text = "Name:";
                txb_name.Text = selectedVisit.VisitName;
                txb_name.Opacity = 1;
                txt_finalCC.Opacity = 1;
                txb_CC.Text = selectedVisit.VisitCC;
                InitializeVisitorsList(null, null);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(toggle.IsChecked == false)
            {
                txt_name.Text = "Patient Name:";
                tb_Name.KeyUp += InitializePatientList;
                tb_Name.Text = "";
                txt_Nif.Text = "Patient NIF:";
                tb_nif.KeyUp += InitializePatientList;
                tb_nif.Text = "";
                txt_Phone.Text = "Patient Phone:";
                tb_phone.KeyUp += InitializePatientList;
                tb_phone.Text = "";
                tb_roomNumber.Opacity = 1;
                tb_roomNumber.Text = "";
                tb_roomNumber.KeyUp += InitializePatientList;
                txt_roomNumber.Opacity = 1;
                gridColumnNif.Header = "NIF";
                b_addVisitor.IsEnabled = false;
                b_addVisitor.Opacity = 0;
                b_select.Content = "Select Patient";
                InitializePatientList(null, null);
                numberPage = 1;
                bt_beforePage.Opacity = 0;
                bt_beforePage.IsEnabled = false;

                gridColumnName.DisplayMemberBinding = new Binding("Name");
                gridColumnNif.DisplayMemberBinding = new Binding("Nif");
                gridColumnPhone.DisplayMemberBinding = new Binding("Phone");
            }
            else
            {
                txt_name.Text = "Visitor Name:";
                tb_Name.KeyUp += InitializeVisitorsList;
                tb_Name.Text = "";
                txt_Nif.Text = "Visitor NIF:";
                tb_nif.Text = "";
                tb_nif.KeyUp += InitializeVisitorsList;
                txt_Phone.Text = "Visitor Phone:";
                tb_phone.KeyUp += InitializeVisitorsList;
                tb_phone.Text = "";
                tb_roomNumber.Opacity = 0;
                txt_roomNumber.Opacity = 0;
                gridColumnNif.Header = "CC";
                b_addVisitor.IsEnabled = true;
                b_addVisitor.Opacity = 1;
                b_select.Content = "Select Visitor";
                InitializeVisitorsList(null, null);
                numberPage = 1;
                bt_beforePage.Opacity = 0;
                bt_beforePage.IsEnabled = false;
                gridColumnName.DisplayMemberBinding = new Binding("VisitName");
                gridColumnNif.DisplayMemberBinding = new Binding("VisitCC");
                gridColumnPhone.DisplayMemberBinding = new Binding("VisitPhone");

            }
        }
        private void InitializeVisitorsList(object sender, KeyEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getVisitors(@VisitorName,@VisitorCC,@VisitorPhone,@PageNumber,@RowsPage)", con.Con);
                cmd.Parameters.AddWithValue("@PageNumber", numberPage);
                cmd.Parameters.AddWithValue("@RowsPage", 11);


                if (tb_nif.Text != "")
                    cmd.Parameters.AddWithValue("@VisitorCC", tb_nif.Text);
                else
                    cmd.Parameters.AddWithValue("@VisitorCC", DBNull.Value);

                if (tb_Name.Text != "")
                    cmd.Parameters.AddWithValue("@VisitorName", tb_Name.Text);
                else
                    cmd.Parameters.AddWithValue("@VisitorName", DBNull.Value);

                if (tb_phone.Text != "")
                    cmd.Parameters.AddWithValue("@VisitorPhone", tb_phone.Text);
                else
                    cmd.Parameters.AddWithValue("@VisitorPhone", DBNull.Value);

                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                ObservableCollection<Visit> listVisitors = new ObservableCollection<Visit>();
                while (reader.Read())
                {
                    Visit visit = new Visit();
                    if (reader["CC"] != DBNull.Value)
                        visit.VisitCC = reader["CC"].ToString();
                    if (reader["Name"] != DBNull.Value)
                        visit.VisitName = reader["Name"].ToString();
                    if (reader["Phone"] != DBNull.Value)
                        visit.VisitPhone = reader.GetInt32(2);


                    listVisitors.Add(visit);
                }
                //make your query
                listView.ItemsSource = listVisitors;
                con.conClose();//close your connection
                if (listVisitors.Count < 11)
                {
                    bt_nextPage.Opacity = 0;
                    bt_nextPage.IsEnabled = false;
                }
                else
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
        private void InitializePatientList(object sender, KeyEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getPatients(@PatientNif,@PatientName,@Sex,@authorization,@RoomNumber,@PhoneNUmber,@Checkout,@PageNumber,@RowsPage,@sortOrder,@sortColumn)", con.Con);
                cmd.Parameters.AddWithValue("@PageNumber", numberPage);
                cmd.Parameters.AddWithValue("@RowsPage", 11);
                cmd.Parameters.AddWithValue("@sortOrder", DBNull.Value);
                cmd.Parameters.AddWithValue("@sortColumn", DBNull.Value);
                if (tb_nif.Text != "")
                    cmd.Parameters.AddWithValue("@PatientNif", tb_nif.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientNif", DBNull.Value);

                if (tb_Name.Text != "")
                    cmd.Parameters.AddWithValue("@PatientName", tb_Name.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientName", DBNull.Value);

            
                if (tb_roomNumber.Text != "")
                    cmd.Parameters.AddWithValue("@RoomNumber", tb_roomNumber.Text);
                else
                    cmd.Parameters.AddWithValue("@RoomNumber", DBNull.Value);

                if (tb_phone.Text != "")
                    cmd.Parameters.AddWithValue("@PhoneNUmber", tb_phone.Text);
                else
                    cmd.Parameters.AddWithValue("@PhoneNUmber", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Checkout", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Sex", DBNull.Value);
                    cmd.Parameters.AddWithValue("@authorization", DBNull.Value);


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
                    if (reader["Phone"] != DBNull.Value)
                        patient.Phone = reader.GetInt32(3);
            
                    listPatients.Add(patient);
                }
                //make your query
                listView.ItemsSource = listPatients;
                con.conClose();//close your connection
                if (listPatients.Count < 11)
                {
                    bt_nextPage.Opacity = 0;
                    bt_nextPage.IsEnabled = false;
                }
                else
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
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            numberPage += 1;
            if (numberPage > 1)
            {
                bt_beforePage.Opacity = 1;
                bt_beforePage.IsEnabled = true;
            }
            if(toggle.IsChecked == false)
            {
                InitializePatientList(null,null);
            }else
            {
                InitializeVisitorsList(null,null);
            }
          
        }
        private void Button_BeforePage(object sender, RoutedEventArgs e)
        {

            numberPage -= 1;
            if (numberPage == 1)
            {
                bt_beforePage.Opacity = 0;
                bt_beforePage.IsEnabled = false;
            }
            if (toggle.IsChecked == false)
            {
                InitializePatientList(null, null);
            }
            else
            {
                InitializeVisitorsList(null, null);
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (selectedPatient == null)
            {
                MessageBox.Show("You must select a patient.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (selectedVisit == null)
            {
                MessageBox.Show("You must select a visitor.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }else
            {
                addVisit(null, null);
            }
        }

        private void addVisit(object sender, RoutedEventArgs e)
        {

            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.
            SqlTransaction tran = con.Con.BeginTransaction();
            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Visit", con.Con, tran);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIF", selectedPatient.Nif);
                    cmd.Parameters.AddWithValue("@CC", selectedVisit.VisitCC);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                   
                    cmd.ExecuteNonQuery();

                    tran.Commit();


                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error." + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                    tran.Rollback();
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

            this.DialogResult = true;
        }

        private void Click_Selected(object sender, RoutedEventArgs e)
        {

            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select a person.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    listView_MouseDoubleClick(null, null);
                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (toggle.IsChecked == false)
            {
                selectedPatient = (Patient)listView.SelectedItem;
                txb_patientNameIs.Text = "Name:";
                txb_patientName.Text = selectedPatient.Name;
                txb_patientName.Opacity = 1;
                txb_nifName.Opacity = 1;
                txb_Nif.Text = selectedPatient.Nif;
            }
            else
            {
                selectedVisit = (Visit)listView.SelectedItem;
                txb_visitorNameIs.Text = "Name:";
                txb_name.Text = selectedVisit.VisitName;
                txb_name.Opacity = 1;
                txt_finalCC.Opacity = 1;
                txb_CC.Text = selectedVisit.VisitCC;
            }
        }
    }
}
