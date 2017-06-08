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
    /// Interaction logic for AppointmentsPage.xaml
    /// </summary>
    public partial class AppointmentsPage : Page
    {

        private int numberPage = 1;
        public AppointmentsPage()
        {
            InitializeComponent();
            loadAppointmentsList(null,null);
            cb_Speciality.SelectionChanged += dp_datepicker_SelectedDateChanged;
            bt_beforePage.Opacity = 0;
            bt_beforePage.IsEnabled = false;
        }

      
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            loadAppointmentsList(null, null);
        }

        private void loadAppointmentsList(object sender, KeyEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.searchAppointments(@DoctorNif,@PatientNif,@DoctorName,@PatientName,@Date,@Speciality,@PageNumber,@RowsNumber)", con.Con);
                cmd.Parameters.AddWithValue("@PageNumber", numberPage);
                cmd.Parameters.AddWithValue("@RowsNumber", 22);
                if (tb_doctorNif.Text != "")
                    cmd.Parameters.AddWithValue("@DoctorNif", tb_doctorNif.Text);
                else
                    cmd.Parameters.AddWithValue("@DoctorNif", DBNull.Value);

                if (tb_patientNif.Text != "")
                    cmd.Parameters.AddWithValue("@PatientNif", tb_patientNif.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientNif", DBNull.Value);

                if (tb_doctorName.Text != "")
                     cmd.Parameters.AddWithValue("@DoctorName", tb_doctorName.Text);
                else
                    cmd.Parameters.AddWithValue("@DoctorName", DBNull.Value);
                if (tb_patientName.Text != "")
                    cmd.Parameters.AddWithValue("@PatientName", tb_patientName.Text);
                else
                    cmd.Parameters.AddWithValue("@PatientName", DBNull.Value);
                
                if (dp_datepicker.SelectedDate!=null)
                    cmd.Parameters.AddWithValue("@Date", dp_datepicker.SelectedDate);
                else
                    cmd.Parameters.AddWithValue("@Date", DBNull.Value);

                if (cb_Speciality.SelectedIndex != 0)
                    cmd.Parameters.AddWithValue("@Speciality", ((ComboBoxItem)cb_Speciality.SelectedItem).Content.ToString());
                else
                    cmd.Parameters.AddWithValue("@Speciality", DBNull.Value);
                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                ObservableCollection<Appointment> listAppointments = new ObservableCollection<Appointment>();
                while (reader.Read())
                {
                    Appointment appointment = new Appointment();
                    if (reader["DoctorNIF"] != DBNull.Value)
                        appointment.DoctorNif = reader["DoctorNIF"].ToString();
                    if (reader["DoctorName"] != DBNull.Value)
                        appointment.DoctorName = reader["DoctorName"].ToString();
                    if (reader["PatientNIF"] != DBNull.Value)
                        appointment.PatientNif = reader["PatientNIF"].ToString();
                    if (reader["PatientName"] != DBNull.Value)
                        appointment.PatientName = reader.GetString(3);
                    if (reader["Date"] != DBNull.Value)
                        appointment.Date = reader.GetDateTime(4);
                    if (reader["Speciality"] != DBNull.Value)
                        appointment.Speciality = reader.GetString(5);

                    if (DateTime.Compare(appointment.Date, DateTime.Now) < 0)
                    {
                        appointment.Occurred = true;
                        if (checkBox.IsChecked.Value == false)
                        {
                            listAppointments.Add(appointment);
                        }
                    }
                    else
                    {
                        appointment.Occurred = false;
                        listAppointments.Add(appointment);
                    }

                }
            
                appointmentList.ItemsSource = null;
                appointmentList.Items.Clear();
                appointmentList.ItemsSource = listAppointments;
                if (listAppointments.Count < 22)
                {
                    bt_nextPage.Opacity = 0;
                    bt_nextPage.IsEnabled = false;
                }
                else
                {
                    bt_nextPage.Opacity = 1;
                    bt_nextPage.IsEnabled = true;
                }
                con.conClose();
            }
        }

        private void Button_Click_Cancel_Date(object sender, RoutedEventArgs e)
        {
            dp_datepicker.SelectedDate = null;
        }
      
        private void dp_datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadAppointmentsList(null, null);
        }
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            numberPage += 1;
            if (numberPage > 1)
            {
                bt_beforePage.Opacity = 1;
                bt_beforePage.IsEnabled = true;
            }
            loadAppointmentsList(null,null);
        }
        private void Button_BeforePage(object sender, RoutedEventArgs e)
        {

            numberPage -= 1;
            if (numberPage == 1)
            {
                bt_beforePage.Opacity = 0;
                bt_beforePage.IsEnabled = false;
            }
            loadAppointmentsList(null,null);
        }
    }
}
