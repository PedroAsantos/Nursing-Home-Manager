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
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        public ManagePage()
        {
            InitializeComponent();
            initializeTypelist();
        }

        private void Button_Click_Add_Room(object sender, RoutedEventArgs e)
        {
            int newRoomId = -1;
            if(string.IsNullOrEmpty(cb_capacity.Text))
            {   
                MessageBox.Show("You must select a capacity to add the room.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }else
            {
                Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
                con.conOpen();//method to open the connection.
                SqlTransaction tran = con.Con.BeginTransaction();
                //you should test if the connection is open or not
                if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
                {

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_newRoom", con.Con, tran);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@capacity", Convert.ToInt32(((ComboBoxItem)cb_capacity.SelectedItem).Content.ToString()));
                        cmd.Parameters.Add("@NewRoomId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        newRoomId =Convert.ToInt32(cmd.Parameters["@NewRoomId"].Value);

                        tran.Commit();


                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error." + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                        tran.Rollback();
                    }
                    finally
                    {
                        con.conClose();//close 
                        string messageBoxText = "The Room "+ newRoomId.ToString() +" was created with " + ((ComboBoxItem)cb_capacity.SelectedItem).Content.ToString() + " beds";
                        string caption = "Nursing Home Manager";
                        cb_capacity.SelectedIndex = -1;
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Information;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                        
                    }


                }
                else
                {
                    MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;//close the event
                }
            }
        }

        private void Button_Click_Add_Type(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tb_type.Text))
            {
                MessageBox.Show("Field Type is empty", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
                con.conOpen();//method to open the connection.
              
                //you should test if the connection is open or not
                if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
                {

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_newType", con.Con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@type", tb_type.Text);
                        cmd.ExecuteNonQuery();


                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error." + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                       
                    }
                    finally
                    {
                        con.conClose();//close connection
                        tb_type.Text = "";
                        initializeTypelist();
                    }


                }
                else
                {
                    MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;//close the event
                }
            }
        }
        private void initializeTypelist()
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {
                //make your query
                SqlDataAdapter category_data = new SqlDataAdapter("SELECT * from dbo.getHumanTypes()", con.Con);
                DataSet ds = new DataSet();
                category_data.Fill(ds, "t");
                DataTable d = ds.Tables["t"].DefaultView.ToTable(true, "Designation","id");
                List<HumanResourceClass> listTypes = new List<HumanResourceClass>();
                foreach (DataRowView row in d.DefaultView)
                {   //only to not create a new class only fo this porpuse; bad practice
                    HumanResourceClass type = new HumanResourceClass(); 
                    type.Type = (string)row["Designation"];
                    type.Salary = (int)row["ID"];
                    listTypes.Add(type);
                }
                   
                

                listView.ItemsSource = listTypes;
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private void Delete_Type_Click(object sender, RoutedEventArgs e)
        {

            if (listView.Items.Count > 0)
                if (listView.SelectedItem == null)
                {
                    MessageBox.Show("You must select a type to delete.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
                    con.conOpen();//method to open the connection.

                    //you should test if the connection is open or not
                    if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
                    {

                        try
                        {
                            SqlCommand cmd = new SqlCommand("sp_deleteType", con.Con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", ((HumanResourceClass)listView.SelectedItem).Salary );
                            cmd.ExecuteNonQuery();


                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Error." + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        finally
                        {
                            con.conClose();//close connection
                            tb_type.Text = "";
                            initializeTypelist();
                        }


                    }
                    else
                    {
                        MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;//close the event
                    }


                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }
    }
}
