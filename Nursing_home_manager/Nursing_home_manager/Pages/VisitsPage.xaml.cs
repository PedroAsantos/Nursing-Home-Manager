using Nursing_home_manager.Classes;
using Nursing_home_manager.Pages.Dialogs.Visits;
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
    /// Interaction logic for VisitsPage.xaml
    /// </summary>
    public partial class VisitsPage : Page
    {
        private int numberPage = 1;
        public VisitsPage()
        {
            InitializeComponent();
            loadVisitsList(null,null);
            bt_beforePage.Opacity = 0;
            bt_beforePage.IsEnabled = false;
        }
        private void loadVisitsList(object sender, KeyEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getVisits(@PatientNif,@PatientName,@VisitorName,@VisitorCC,@VisitorPhone,@Date,@PageNumber,@RowsPage)", con.Con);
                cmd.Parameters.AddWithValue("@PageNumber", numberPage);
                cmd.Parameters.AddWithValue("@RowsPage", 19);

                if (tb_patientName.Text != "")
                    cmd.Parameters.AddWithValue("@PatientNif", tb_patientName.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientNif", DBNull.Value);

                if (tb_patientNif.Text != "")
                    cmd.Parameters.AddWithValue("@PatientName", tb_patientNif.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientName", DBNull.Value);

                if (tb_visitorName.Text != "")
                    cmd.Parameters.AddWithValue("@VisitorName", tb_visitorName.Text);
                else
                    cmd.Parameters.AddWithValue("@VisitorName", DBNull.Value);

                if (tb_visitorCC.Text != "")
                    cmd.Parameters.AddWithValue("@VisitorCC", tb_visitorCC.Text);
                else
                    cmd.Parameters.AddWithValue("@VisitorCC", DBNull.Value);

                if (tb_visitorphone.Text != "")
                    cmd.Parameters.AddWithValue("@VisitorPhone", tb_visitorphone.Text);
                else
                    cmd.Parameters.AddWithValue("@VisitorPhone", DBNull.Value);

                if (dp_datepicker.SelectedDate != null)
                    cmd.Parameters.AddWithValue("@Date", dp_datepicker.SelectedDate);
                else
                    cmd.Parameters.AddWithValue("@Date", DBNull.Value);


                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                ObservableCollection<Visit> listVisits = new ObservableCollection<Visit>();
                while (reader.Read())
                {
                    Visit visit = new Visit();
                    if (reader["PatientName"] != DBNull.Value)
                        visit.PatientName = reader["PatientName"].ToString();
                    if (reader["NIF"] != DBNull.Value)
                        visit.PatientNif = reader["NIF"].ToString();
                    if (reader["CC"] != DBNull.Value)
                        visit.VisitCC = reader["CC"].ToString();
                    if (reader["Phone"] != DBNull.Value)
                        visit.VisitPhone = reader.GetInt32(4);
                    if (reader["Date"] != DBNull.Value)
                        visit.Date = reader.GetDateTime(6);
                    if (reader["Address"] != DBNull.Value)
                        visit.VisitAddress = reader.GetString(5);
                    if (reader["KinshipDegree"] != DBNull.Value)
                        visit.KinshipDegree = reader["KinshipDegree"].ToString();
                    if (reader["Relationship"] != DBNull.Value)
                        visit.KinshipDegree = reader["Relationship"].ToString();

                    listVisits.Add(visit);
                }
                //make your query
                visitsList.ItemsSource = listVisits;
                if (listVisits.Count < 19)
                {
                    bt_nextPage.Opacity = 0;
                    bt_nextPage.IsEnabled = false;
                }
                else
                {
                    bt_nextPage.Opacity = 1;
                    bt_nextPage.IsEnabled = true;
                }
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private void Button_AddVisit(object sender, RoutedEventArgs e)
        {
            DialogAddVisit dialogAddVisit = new DialogAddVisit();
            if (dialogAddVisit.ShowDialog() == true)
            {
                loadVisitsList(null, null);
            }
        }

        private void dp_datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadVisitsList(null, null);
        }
        private void Button_Click_Cancel_Date(object sender, RoutedEventArgs e)
        {
            dp_datepicker.SelectedDate = null;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            numberPage += 1;
            if (numberPage > 1)
            {
                bt_beforePage.Opacity = 1;
                bt_beforePage.IsEnabled = true;
            }
            loadVisitsList(null, null);
        }
        private void Button_BeforePage(object sender, RoutedEventArgs e)
        {

            numberPage -= 1;
            if (numberPage == 1)
            {
                bt_beforePage.Opacity = 0;
                bt_beforePage.IsEnabled = false;
            }
            loadVisitsList(null, null);
        }

        private void visitsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
