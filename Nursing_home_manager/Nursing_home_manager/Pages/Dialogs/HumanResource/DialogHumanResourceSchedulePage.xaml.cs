using Nursing_home_manager.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

namespace Nursing_home_manager.Pages.Dialogs.HumanResource
{
    /// <summary>
    /// Interaction logic for DialogHumanResourceSchedulePage.xaml
    /// </summary>
    public partial class DialogHumanResourceSchedulePage : Page
    {
        private HumanResourceClass HumanResource;
        public DialogHumanResourceSchedulePage(HumanResourceClass HumanResource)
        {
            this.HumanResource = HumanResource;
            InitializeComponent();
            updateScheduleList();
        }
        private void updateScheduleList()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getHumanResourceSchedule(" + HumanResource.Nif + ")", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();
               
                ObservableCollection<Shedule> listSchedule = new ObservableCollection<Shedule>();
                while (reader.Read())
                {
                    Shedule schedule = new Shedule();
                    if (reader["Day"] != DBNull.Value)
                        schedule.Day= reader["Day"].ToString();
                    if (reader["BeginOfWorkShift"] != DBNull.Value)
                        schedule.EntryHour = reader.GetTimeSpan(1);
                    if (reader["EndOfWorkShift"] != DBNull.Value)
                        schedule.ExitHour = reader.GetTimeSpan(2);
                    if (reader["E_IDShift"] != DBNull.Value)
                        schedule.ID = reader.GetInt32(3);

                    listSchedule.Add(schedule);
                }
                //make your query
                listView.ItemsSource = listSchedule;
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private void Click_deleteSchedule(object sender, RoutedEventArgs e)
        {
            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select diseases to delete them.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Shedule schedule = (Shedule) listView.SelectedItem;
                    deleteSchedule(schedule);


                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void deleteSchedule(Shedule schedule)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_finishShiftOfHumanResources", con.Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", schedule.ID);
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
            updateScheduleList();
        }
        private void Button_Click_Add_Shift(object sender, RoutedEventArgs e)
        {

            if (((ComboBoxItem)cb_entryhour.SelectedItem).Content != null && ((ComboBoxItem)cb_entryday.SelectedItem).Content != null 
                && ((ComboBoxItem)cb_entryperiodo.SelectedItem).Content != null && ((ComboBoxItem)cb_entryminute.SelectedItem).Content != null
                && ((ComboBoxItem)cb_exithour.SelectedItem).Content != null && ((ComboBoxItem)cb_exitperiodo.SelectedItem).Content != null && ((ComboBoxItem)cb_exitminute.SelectedItem).Content != null)
            {
                Shedule schedule;
                TimeSpan entryHour;
                TimeSpan exitHour;

             
                  //  entryHour = new TimeSpan(Convert.ToInt32(((ComboBoxItem)cb_entryhour.SelectedItem).Content.ToString()), Convert.ToInt32(((ComboBoxItem)cb_entryminute.SelectedItem).Content.ToString()), 0);
                string entryHourString = ((ComboBoxItem)cb_entryhour.SelectedItem).Content.ToString() +":"+ ((ComboBoxItem)cb_entryminute.SelectedItem).Content.ToString() + " " + ((ComboBoxItem)cb_entryperiodo.SelectedItem).Content.ToString();
                DateTime dt;
                bool res = DateTime.TryParse(entryHourString, out dt);
                entryHour = dt.TimeOfDay;
                string exitHourString = ((ComboBoxItem)cb_exithour.SelectedItem).Content.ToString() + ":" + ((ComboBoxItem)cb_exitminute.SelectedItem).Content.ToString() + " " + ((ComboBoxItem)cb_exitperiodo.SelectedItem).Content.ToString(); 
                res = DateTime.TryParse(exitHourString, out dt);
                exitHour = dt.TimeOfDay;

                schedule = new Shedule()
                {
                    Day = ((ComboBoxItem)cb_entryday.SelectedItem).Content.ToString(),
                    EntryHour = entryHour,
                    ExitHour = exitHour
                };

                addScheduletToDb(schedule);
                listView.ItemsSource = null;
                listView.Items.Clear();
                updateScheduleList();
                //  ListViewItem item = new ListViewItem();
                //listView.ItemsSource = listMedicine;
                cb_entryday.SelectedItem = ((ComboBoxItem)cb_entryday.Items[0]);
                cb_entryhour.SelectedItem = ((ComboBoxItem)cb_entryhour.Items[1]);
                cb_entryminute.SelectedItem = ((ComboBoxItem)cb_entryminute.Items[0]);
                cb_entryperiodo.SelectedItem = ((ComboBoxItem)cb_entryperiodo.Items[0]);
                cb_exithour.SelectedItem = ((ComboBoxItem)cb_exithour.Items[1]);
                cb_exitminute.SelectedItem = ((ComboBoxItem)cb_exitminute.Items[0]);
                cb_exitperiodo.SelectedItem = ((ComboBoxItem)cb_exitperiodo.Items[0]);
            
            }

        }
        private void addScheduletToDb(Shedule schedule)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.
            SqlTransaction tran = con.Con.BeginTransaction();
            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_newSchedule", con.Con,tran);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIF", HumanResource.Nif);
                    DateTime now = DateTime.Now;
                    cmd.Parameters.AddWithValue("@inicialDate", now);
                    cmd.Parameters.AddWithValue("@finalDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@beginofWorkShift", schedule.EntryHour);
                    cmd.Parameters.AddWithValue("@endofWorkShift", schedule.ExitHour);
                    cmd.Parameters.AddWithValue("@day", schedule.Day);
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
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
        }
    }
}
