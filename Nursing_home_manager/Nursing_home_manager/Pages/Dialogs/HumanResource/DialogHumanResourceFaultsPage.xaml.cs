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

namespace Nursing_home_manager.Pages.Dialogs.HumanResource
{
    /// <summary>
    /// Interaction logic for DialogHumanResourceFaultsPage.xaml
    /// </summary>
    public partial class DialogHumanResourceFaultsPage : Page
    {
        private HumanResourceClass HumanResource;
        ObservableCollection<Shedule> listSchedule;
        public DialogHumanResourceFaultsPage(HumanResourceClass HumanResource)
        {
            this.HumanResource = HumanResource;
            InitializeComponent();
            updateList();
        }
        private void updateList()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getHumanResourceFaults(" + HumanResource.Nif +  ")", con.Con);
                SqlDataReader reader = cmd.ExecuteReader();

                listSchedule = new ObservableCollection<Shedule>();
                while (reader.Read())
                {
                    Shedule schedule = new Shedule();
                    if (reader["Day"] != DBNull.Value)
                        schedule.Day = reader["Day"].ToString();
                    if (reader["BeginOfWorkShift"] != DBNull.Value)
                        schedule.EntryHour = reader.GetTimeSpan(1);
                    if (reader["EndOfWorkShift"] != DBNull.Value)
                        schedule.ExitHour = reader.GetTimeSpan(2);
                    if (reader["E_IDShift"] != DBNull.Value)
                        schedule.ID = reader.GetInt32(3);
                    if (reader["FinalDate"] != DBNull.Value)
                    {
                        schedule.FinalDate = false;
                    }else
                    {
                        schedule.FinalDate = true;
                    }

                    if (reader["NumberOfFaults"] != DBNull.Value)
                        schedule.NumberOfFaults = reader.GetInt32(5);
                    if(checkBox.IsChecked.Value == false )
                        listSchedule.Add(schedule);
                    else if(schedule.FinalDate)
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
        private void Click_addAllDayFault(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach(Shedule sch in listSchedule)
            {
                if (sch.FinalDate && String.Compare(sch.Day, ((ComboBoxItem)cb_dayFault.SelectedItem).Content.ToString(),0)==0)
                {
                    count++;
                    addFaultToShift(sch);
                }
            }
            if (count == 0)
            {
                MessageBox.Show("This human resource doens't have shifts in this day.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void Click_addFault(object sender, RoutedEventArgs e)
        {

            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select diseases to delete them.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Shedule sch = (Shedule)listView.SelectedItem;
                    if(sch.FinalDate == false)
                        MessageBox.Show("You cant add a fault of a shift not active!", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                        addFaultToShift(sch);


                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void addFaultToShift(Shedule sch)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_addFaulttoHumanResource", con.Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIF", HumanResource.Nif);
                    cmd.Parameters.AddWithValue("@beginofWorkShift", sch.EntryHour);
                    cmd.Parameters.AddWithValue("@endofWorkShift ", sch.ExitHour);
                    cmd.Parameters.AddWithValue("@day", sch.Day);
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
            updateList();
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            updateList();
        }
    }
}
