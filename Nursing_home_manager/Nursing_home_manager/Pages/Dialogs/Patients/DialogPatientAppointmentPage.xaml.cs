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

namespace Nursing_home_manager.Pages.Dialogs.Patients
{
    /// <summary>
    /// Interaction logic for DialogPatientAppointmentPage.xaml
    /// </summary>
    public partial class DialogPatientAppointmentPage : Page
    {
        private Patient patient;
        private ObservableCollection<Appointment> listAppointments;
        public DialogPatientAppointmentPage(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
            putDoctors();
            putAppointments();
        }
        private void putAppointments()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getPatientAppointments(" + patient.Nif + ")", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();

                listAppointments = new ObservableCollection<Appointment>();
                while (reader.Read())
                {
                    Appointment appointment = new Appointment();
                    if (reader["Name"] != DBNull.Value)
                        appointment.DoctorName = reader.GetString(0);
                    if (reader["Date"] != DBNull.Value)
                        appointment.Date = reader.GetDateTime(1);
                    if (reader["Speciality"] != DBNull.Value)
                        appointment.Speciality = reader.GetString(2);
                    if (reader["ID"] != DBNull.Value)
                        appointment.ID = reader.GetInt32(3);

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
                //make your query
                listView.ItemsSource = listAppointments;
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private void putDoctors()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                //make your query
                SqlDataAdapter category_data = new SqlDataAdapter("SELECT * from dbo.getDoctors()", con.Con);
                DataSet ds = new DataSet();
                category_data.Fill(ds, "t");
                DataTable d = ds.Tables["t"].DefaultView.ToTable(true, "Name","NIF");
                cb_doctor.ItemsSource = d.DefaultView;
                cb_doctor.DisplayMemberPath = "Name";
                con.conClose();

                foreach (DataRowView item in cb_doctor.Items)
                {
                    Console.WriteLine(item["NIF"]);
                }

            }
        }
        private void Click_AddDoctorDialog(object sender, RoutedEventArgs e)
        {
            DialogAddDoctor dialogAddDoctor = new DialogAddDoctor();
            if (dialogAddDoctor.ShowDialog() == true)
            {
                putDoctors();
            }
        }
        private void Click_deleteAppointment(object sender, RoutedEventArgs e)
        {

            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select appointment to delete it.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Appointment appointment = (Appointment)listView.SelectedItem;
                    if (!appointment.Occurred)
                        deleteAppointment(appointment);
                    else
                    {
                       MessageBox.Show("Can't delete a occurred appointment!", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void deleteAppointment(Appointment appointment)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_deleteAppointment", con.Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", appointment.ID);
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
            putAppointments();
        }
        private void Button_Click_Add_Appointment(object sender, RoutedEventArgs e)
        {

            TimeSpan time;
            DateTime dt;
            string timeString = ((ComboBoxItem)cb_hour.SelectedItem).Content.ToString() + ":" + ((ComboBoxItem)cb_minute.SelectedItem).Content.ToString()
                + " " + ((ComboBoxItem)cb_periodo.SelectedItem).Content.ToString();
            bool res = DateTime.TryParse(timeString, out dt);
            time = dt.TimeOfDay;

            DateTime date = (DateTime) dp_datePicker.SelectedDate;
            date = date.Add(time);
            Console.WriteLine(date.ToString());
            if (DateTime.Compare(date, DateTime.Now)<0)
            {
                MessageBox.Show("Invalid Date, you have to add a date in the future.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }else
            {
                Button_Click_Add_AppointmentSend(null, null,date);
            }
        }
        private void Button_Click_Add_AppointmentSend(object sender, RoutedEventArgs e,DateTime date)
        {
            


            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("sp_addPatientAppointment", con.Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientNif", patient.Nif);
                    cmd.Parameters.AddWithValue("@DoctorNif",((DataRowView)cb_doctor.SelectedItem)["NIF"].ToString());
                    cmd.Parameters.AddWithValue("@Date ", date);
                    cmd.Parameters.AddWithValue("@Speciality", ((ComboBoxItem)cb_Speciality.SelectedItem).Content.ToString());
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
            putAppointments();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            putAppointments();
        }
    }
}
