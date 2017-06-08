using Nursing_home_manager.Classes;
using Nursing_home_manager.Pages.Dialogs.HumanResource;
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
    /// Interaction logic for HumanResourcesPage.xaml
    /// </summary>
    public partial class HumanResourcesPage : Page
    {

        private ObservableCollection<HumanResourceClass> listHumanResources;
        private int numberPage = 1;
        public HumanResourcesPage()
        {
            InitializeComponent();
            loadHumanResourceList(null,null);
            putDesignations();
            bt_beforePage.Opacity = 0;
            bt_beforePage.IsEnabled = false;
            cb_designation.SelectionChanged += cb_SelectionChanged;
        }
        private void loadHumanResourceList(object sender, KeyEventArgs e)
        {
            Sqlconnect con = new Sqlconnect();//instantiate a new object 'Con' from the class Sqlconnect.cs
            con.conOpen();//method to open the connection.

            //you should test if the connection is open or not
            if (con != null && con.Con.State == ConnectionState.Open)//youtest if the object exist and if his state is open  && con.State == ConnectionState.Open
            {

                SqlCommand cmd = new SqlCommand("SELECT * from dbo.getHumanResources(@WorkerName,@WorkerNif,@WorkerPhone,@WorkerAddress,@Designation,@PageNumber,@RowsPage)", con.Con);
                cmd.Parameters.AddWithValue("@PageNumber", numberPage);
                cmd.Parameters.AddWithValue("@RowsPage", 19);
                if (tb_workerName.Text != "")
                    cmd.Parameters.AddWithValue("@WorkerName", tb_workerName.Text);
                else
                    cmd.Parameters.AddWithValue("@WorkerName", DBNull.Value);

                if (tb_workerNif.Text != "")
                    cmd.Parameters.AddWithValue("@WorkerNif", tb_workerNif.Text);
                else
                    cmd.Parameters.AddWithValue("@WorkerNif", DBNull.Value);

                if (tb_workerPhone.Text != "")
                    cmd.Parameters.AddWithValue("@WorkerPhone", tb_workerPhone.Text);
                else
                    cmd.Parameters.AddWithValue("@WorkerPhone", DBNull.Value);

                if (tb_workerAddress.Text != "")
                    cmd.Parameters.AddWithValue("@WorkerAddress", tb_workerAddress.Text);
                else
                    cmd.Parameters.AddWithValue("@WorkerAddress", DBNull.Value);

                if (cb_designation.SelectedItem != null)
                    cmd.Parameters.AddWithValue("@Designation", ((DataRowView)cb_designation.SelectedItem)["Designation"].ToString());
                else 
                    cmd.Parameters.AddWithValue("@Designation", DBNull.Value);

                SqlDataReader reader = cmd.ExecuteReader();
                //patientsList.Items.Clear();
                //patientsList.ItemsSource =
                listHumanResources = new ObservableCollection<HumanResourceClass>();
                while (reader.Read())
                {
                    HumanResourceClass humanResource = new HumanResourceClass();
                    if (reader["NIF"] != DBNull.Value)
                        humanResource.Nif = reader["NIF"].ToString();
                    if (reader["Name"] != DBNull.Value)
                        humanResource.Name = reader["Name"].ToString();
                    if (reader["Address"] != DBNull.Value)
                        humanResource.Address = reader["Address"].ToString();
                    if (reader["Phone"] != DBNull.Value)
                        humanResource.PhoneNumber = reader.GetInt32(2);
                    if (reader["StartDate"] != DBNull.Value)
                        humanResource.StartDate = reader.GetDateTime(5).ToShortDateString();
                    if (reader["Salary"] != DBNull.Value)
                        humanResource.Salary = reader.GetInt32(4);
                    if (reader["Designation"] != DBNull.Value)
                        humanResource.Type = reader["Designation"].ToString();
             
                    listHumanResources.Add(humanResource);
                }
                //make your query
                humanResourcesList.ItemsSource = listHumanResources;
                if (listHumanResources.Count < 19)
                {
                    bt_nextPage.Opacity = 0;
                    bt_nextPage.IsEnabled = false;
                }
                else
                {
                    bt_nextPage.Opacity = 1;
                    bt_nextPage.IsEnabled = true;
                }
                con.conClose();//close your connection

            }
            else
            {
                MessageBox.Show("Database Not Open.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//close the event
            }
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
        private void Button_AddHumanResource(object sender, RoutedEventArgs e)
        {
            DialogAddHumanResource dialogAddHumanResource = new DialogAddHumanResource();
            if (dialogAddHumanResource.ShowDialog() == true)
            {
                loadHumanResourceList(null,null);
            }
        }
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            HumanResourceClass HumanResource = (HumanResourceClass)(sender as ListView).SelectedItem;
        
            if (HumanResource != null)
            {
                DialogEditHumanResources dialogEditHumanResources = new DialogEditHumanResources(HumanResource);

                if (dialogEditHumanResources.ShowDialog() == true)
                {
                    loadHumanResourceList(null, null);
                }
            }
        }
        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            numberPage += 1;
            if (numberPage > 1)
            {
                bt_beforePage.Opacity = 1;
                bt_beforePage.IsEnabled = true;
            }
            loadHumanResourceList(null, null);
        }
        private void Button_BeforePage(object sender, RoutedEventArgs e)
        {

            numberPage -= 1;
            if (numberPage == 1)
            {
                bt_beforePage.Opacity = 0;
                bt_beforePage.IsEnabled = false;
            }
            loadHumanResourceList(null, null);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadHumanResourceList(null, null);
        }

        private void Button_Click_ClearFields(object sender, RoutedEventArgs e)
        {
            tb_workerAddress.Text = "";
            tb_workerName.Text = "";
            tb_workerNif.Text = "";
            tb_workerPhone.Text = "";
            cb_designation.SelectedIndex = -1;
        }
    }
}
