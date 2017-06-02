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

namespace Nursing_home_manager.Pages.Dialogs.HumanResource
{
    /// <summary>
    /// Interaction logic for DialogAddHumanResource.xaml
    /// </summary>
    public partial class DialogAddHumanResource : Window
    {
        public DialogAddHumanResource()
        {
            InitializeComponent();
            putDesignations();
        }
        private void putDesignations()
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
                cb_designation.ItemsSource = ds.Tables["t"].DefaultView;
                cb_designation.DisplayMemberPath = "Designation";
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.
            SqlTransaction tran = con.Con.BeginTransaction();
            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_insertHumanResources", con.Con, tran);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIF", tb_NIF.Text);
                    cmd.Parameters.AddWithValue("@Name", tb_name.Text);
                    cmd.Parameters.AddWithValue("@Phone", Int32.Parse(tb_phone.Text));
                    cmd.Parameters.AddWithValue("@Address", tb_address.Text);
                    DateTime startDate = DateTime.Now;
                    cmd.Parameters.AddWithValue("@Start_Date", startDate);
                    cmd.Parameters.AddWithValue("@Salary", Int32.Parse(tb_salary.Text));
                    cmd.Parameters.AddWithValue("@E_IDType", Convert.ToInt32(((DataRowView)cb_designation.SelectedItem)["id"].ToString()));
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

                this.DialogResult = true;
            }
        }
    }
}
