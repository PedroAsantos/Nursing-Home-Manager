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

namespace Nursing_home_manager.Pages.Dialogs.Patients
{
    /// <summary>
    /// Interaction logic for DialogAddDoctor.xaml
    /// </summary>
    public partial class DialogAddDoctor : Window
    {
        public DialogAddDoctor()
        {
            InitializeComponent();
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
            return true;
        }
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (verifications())
            {

                Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
                con.conOpen();//method to open the connection.

                //you should test if the connection is open or not
                if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
                {

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_newDoctor", con.Con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NIF", tb_NIF.Text);
                        cmd.Parameters.AddWithValue("@Name", tb_name.Text);
                        if(!string.IsNullOrEmpty(tb_phone.Text))
                            cmd.Parameters.AddWithValue("@Phone", Int32.Parse(tb_phone.Text));
                        else
                            cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", tb_location.Text);
                        cmd.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error." + ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    finally
                    {
                        con.conClose();//close connection
                    }

                    this.DialogResult = true;
                }
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
