using Nursing_home_manager.Classes;
using System;
using System.Collections.Generic;
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


namespace Nursing_home_manager.Pages
{
    /// <summary>
    /// Interaction logic for DialogAddPatient.xaml
    /// </summary>
    public partial class DialogAddPatient : Window
    {
        List<Disease> listDisease = new List<Disease>();
        public DialogAddPatient()
        {
            InitializeComponent();
            putRooms();
        }
        private void putRooms()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                //make your query
                SqlDataAdapter category_data = new SqlDataAdapter("SELECT * from dbo.getFreeRoomsAndBeds()", con.Con);
                DataSet ds = new DataSet();
                category_data.Fill(ds, "t");
                cb_roomNumber.ItemsSource = ds.Tables["t"].DefaultView;
                cb_roomNumber.DisplayMemberPath = "RoomNumber";
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
 
        private void cb_roomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                //make your query
                SqlDataAdapter category_data = new SqlDataAdapter("SELECT * from dbo.getFreeBeds(" + Convert.ToInt32(((DataRowView)cb_roomNumber.SelectedItem)["RoomNumber"].ToString()) + ")", con.Con);
                DataSet ds = new DataSet();
                category_data.Fill(ds, "t");
                cb_bedNumber.ItemsSource = ds.Tables["t"].DefaultView;
                cb_bedNumber.DisplayMemberPath = "BedNumber";
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private bool verifications()
        {
            if (string.IsNullOrEmpty(tb_NIF.Text))
            {
                MessageBox.Show("The field nif can not be empty", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tb_name.Text))
            {
                MessageBox.Show("The field name of patient can not be empty", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (tb_name.Text.Length > 30)
            {
                MessageBox.Show("The field name can not have more than 30 characters", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (tb_NIF.Text.Length != 9)
            {
                MessageBox.Show("The field NIF must have 9 numbers", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!string.IsNullOrEmpty(tb_phone.Text) && tb_phone.Text.Length != 9)
            {
                MessageBox.Show("The field Phone must have 9 numbers", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!string.IsNullOrEmpty(tb_dependentPhone.Text) && tb_dependentPhone.Text.Length != 9)
            {
                MessageBox.Show("The field Phone of dependent must have 9 numbers", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (tb_dependentName.Text.Length > 30)
            {
                MessageBox.Show("The field name can not have more than 30 characters", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (cb_roomNumber.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a room", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if(cb_bedNumber.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a bed.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!string.IsNullOrEmpty(tb_dependentCC.Text) && tb_dependentCC.Text.Length != 9)
            {
                MessageBox.Show("The field CC of dependent must have 9 numbers", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!string.IsNullOrEmpty(tb_dependentAddress.Text) && tb_dependentAddress.Text.Length > 30)
            {
                MessageBox.Show("The field Address can not have more than 30 characteres", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!string.IsNullOrEmpty(tb_dependentKinship.Text) && tb_dependentKinship.Text.Length > 30)
            {
                MessageBox.Show("The field Address can not have more than 30 characteres", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (verifications())
            {
                Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
                con.conOpen();//method to open the connection.
                SqlTransaction tran = con.Con.BeginTransaction();
                //you should test if the connection is open or not
                if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
                {

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_insertPATIENT", con.Con, tran);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NIF", Int32.Parse(tb_NIF.Text));
                        cmd.Parameters.AddWithValue("@Name", tb_name.Text);
                        if (radioButton_Male.IsChecked == true)
                        {
                            cmd.Parameters.AddWithValue("@Sex", "m");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sex", "f");
                        }
                        if(!string.IsNullOrEmpty(tb_phone.Text))
                             cmd.Parameters.AddWithValue("@Phone", Int32.Parse(tb_phone.Text));
                        else
                            cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                        if(!string.IsNullOrEmpty(tb_age.Text))
                            cmd.Parameters.AddWithValue("@Age", Int32.Parse(tb_age.Text));  
                        else
                            cmd.Parameters.AddWithValue("@Age", DBNull.Value);
                        DateTime myDateTime = DateTime.Now;
                        cmd.Parameters.AddWithValue("@Check_in", myDateTime);
                        cmd.Parameters.AddWithValue("@Check_out", DBNull.Value); 
                        cmd.Parameters.AddWithValue("@Authorization_to_leave", checkBox_AuthorizationToLeave.IsChecked);
                        cmd.Parameters.AddWithValue("@E_BedNumber", Convert.ToInt32(((DataRowView)cb_bedNumber.SelectedItem)["BedNumber"].ToString()));
                        cmd.Parameters.AddWithValue("@Entry_Date", myDateTime);
                        Console.WriteLine(myDateTime.ToString());
                        DateTime myDateTime3 = DateTime.Now;
                        cmd.Parameters.AddWithValue("@Exit_Date", DBNull.Value); 
                        cmd.ExecuteNonQuery();
                        List<Disease> tempList = (List<Disease>)listView.ItemsSource;
                        if (tempList != null)
                        {
                            foreach (Disease dis in tempList)
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "dbo.sp_newDiagnosed";
                                cmd.Parameters.AddWithValue("@E_NIF", Int32.Parse(tb_NIF.Text));
                                cmd.Parameters.AddWithValue("@E_Name", dis.Name);
                                cmd.Parameters.AddWithValue("@Seriousness", dis.Severity);
                                cmd.Parameters.AddWithValue("@Disable", false);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        cmd.Parameters.Clear();
                        if (!string.IsNullOrEmpty(tb_dependentCC.Text))
                        {
                            cmd.CommandText = "dbo.sp_newDependent";

                            cmd.Parameters.AddWithValue("@E_NIF", Int32.Parse(tb_NIF.Text));
                            cmd.Parameters.AddWithValue("@Name", tb_dependentName.Text);
                            if (!string.IsNullOrEmpty(tb_dependentCC.Text))
                                cmd.Parameters.AddWithValue("@CC ", Int32.Parse(tb_dependentCC.Text));
                            else
                                cmd.Parameters.AddWithValue("@CC ", DBNull.Value);
                            if (!string.IsNullOrEmpty(tb_dependentPhone.Text))
                                cmd.Parameters.AddWithValue("@Phone", Int32.Parse(tb_dependentPhone.Text));
                            else
                                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Address", tb_dependentAddress.Text);
                            cmd.Parameters.AddWithValue("@Relationship", tb_dependentKinship.Text);
                            cmd.ExecuteNonQuery();
                        }
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


           
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
             
        }
       
        private void addDisease(string name, int severity)
        {
            listDisease.Add(new Disease() { Name = name, Severity = severity });
        }

        private void Button_Click_AddDisease(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem)cb_severity.SelectedItem).Content != null && ((ComboBoxItem)cb_severity.SelectedItem).Content!=null)
            {
                Int32.Parse(((ComboBoxItem)cb_severity.SelectedItem).Content.ToString());
                addDisease(tb_disease.Text, Convert.ToInt32(((ComboBoxItem)cb_severity.SelectedItem).Content.ToString()));
                listView.ItemsSource = null;
                listView.Items.Clear();

                ListViewItem item = new ListViewItem();
                listView.ItemsSource = listDisease;
                cb_severity.SelectedItem = ((ComboBoxItem)cb_severity.Items[0]);
                tb_disease.Text = "";
            }
              

        }
        private void Button_DeleteDisease(object sender, RoutedEventArgs e)
        {
            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select diseases to delete them.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    
                    var itemToRemove = listDisease.Single(r => r.Name ==listView.SelectedItems[0].ToString() );
                    listView.ItemsSource = null;
                    listDisease.Remove(itemToRemove);
                    listView.ItemsSource = listDisease;
                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

      
    }
}
