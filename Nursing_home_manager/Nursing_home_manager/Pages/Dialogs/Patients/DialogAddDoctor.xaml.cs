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
        private void Button_Add(object sender, RoutedEventArgs e)
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
                    cmd.Parameters.AddWithValue("@Phone", Int32.Parse(tb_phone.Text));
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
