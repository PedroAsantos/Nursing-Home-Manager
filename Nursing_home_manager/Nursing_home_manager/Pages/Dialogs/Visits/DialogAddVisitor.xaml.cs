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

namespace Nursing_home_manager.Pages.Dialogs.Visits
{
    /// <summary>
    /// Interaction logic for DialogAddVisitor.xaml
    /// </summary>
    public partial class DialogAddVisitor : Window
    {
     
        public DialogAddVisitor()
        {
            InitializeComponent();
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
                    SqlCommand cmd = new SqlCommand("sp_newVisitor", con.Con, tran);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CC", tb_CC.Text);
                    cmd.Parameters.AddWithValue("@Name", tb_name.Text);
                    cmd.Parameters.AddWithValue ("@Address", tb_address.Text);
                    cmd.Parameters.AddWithValue("@Phone", tb_phone.Text);
                    cmd.Parameters.AddWithValue("@RelationShip", tb_relationShip.Text);
                    cmd.Parameters.AddWithValue("@Family", cb_checkBox.IsChecked);
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


            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
     
            this.DialogResult = true;
        }
        public string VisitorName
        {
            get { return tb_name.Text; }
        }
        public string VisitorCC
        {
            get { return tb_CC.Text; }
        }
    }
}
