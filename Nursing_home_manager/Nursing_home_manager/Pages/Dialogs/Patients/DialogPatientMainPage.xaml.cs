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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nursing_home_manager.Pages
{
    /// <summary>
    /// Interaction logic for DialogPatientMainPage.xaml
    /// </summary>
    public partial class DialogPatientMainPage : Page
    {

        private List<Disease> listDisease = new List<Disease>();
        private List<Disease> inicialDiseaseList = new List<Disease>();
        private Patient patient;
        public DialogPatientMainPage(Patient patient)
        {
            this.patient = patient;
            patient.DiseaseList.ForEach((item) =>
            {
                inicialDiseaseList.Add(item);
            });
            this.DataContext = patient;
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
                ds.Tables["t"].Rows.Add(patient.RoomNumber);
                DataTable d = ds.Tables["t"].DefaultView.ToTable(true, "RoomNumber");
                d.PrimaryKey = new DataColumn[] { d.Columns["RoomNumber"] };
                cb_roomNumber.ItemsSource = d.DefaultView;
                cb_roomNumber.DisplayMemberPath = "RoomNumber";
                con.conClose();
                int c = -1;
                foreach (DataRowView item in cb_roomNumber.Items)
                {
                    c += 1;
                    if(patient.RoomNumber == (int)item["RoomNumber"])
                    {
                        cb_roomNumber.SelectedIndex = c;
                        break;
                    }
                }
                //close your connection
                /*  foreach (DataRowView item in cb_roomNumber.Items)
                  {
                      Console.WriteLine(item.Row["RoomNumber"]);
                  }*/
                //      if (cb_roomNumber.Items.Count > 0)
                //    {
                //       cb_roomNumber.SelectedItem = patient.RoomNumber;
                //  }


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
                DataRowView selectRoom = (DataRowView)cb_roomNumber.SelectedItem;
                if ((int)selectRoom.Row["RoomNumber"] == patient.RoomNumber)
                {
                    ds.Tables["t"].Rows.Add(patient.BedNumber);
                }
                DataTable d = ds.Tables["t"].DefaultView.ToTable(true, "BedNumber");
                d.PrimaryKey = new DataColumn[] { d.Columns["BedNumber"] };
                cb_bedNumber.ItemsSource = d.DefaultView;
                cb_bedNumber.DisplayMemberPath = "BedNumber";
                con.conClose();//close your connection
                if (cb_bedNumber.Items.Count > 0 && (int)selectRoom.Row["RoomNumber"] == patient.RoomNumber)
                {
                    cb_bedNumber.SelectedIndex = cb_bedNumber.Items.Count - 1;
                }
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
                    SqlCommand cmd = new SqlCommand("sp_updatePATIENT", con.Con, tran);
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
                    cmd.Parameters.AddWithValue("@Phone", Int32.Parse(tb_phone.Text));
                    cmd.Parameters.AddWithValue("@Age", Int32.Parse(tb_age.Text));  //TEMPORARIO mudar para ano

                    cmd.Parameters.AddWithValue("@Check_in", patient.Check_in);
                    cmd.Parameters.AddWithValue("@Check_out", DBNull.Value); //TEMPORARIO
                    cmd.Parameters.AddWithValue("@Authorization_to_leave", checkBox_AuthorizationToLeave.IsChecked);
                    cmd.Parameters.AddWithValue("@E_BedNumber", Convert.ToInt32(((DataRowView)cb_bedNumber.SelectedItem)["BedNumber"].ToString()));
                    cmd.Parameters.AddWithValue("@Entry_Date", patient.Entry_Date);
                    cmd.Parameters.AddWithValue("@Exit_Date", DBNull.Value); //TEMPORARIO
                    cmd.ExecuteNonQuery();
                    List<Disease> finalDiseaseList = (List<Disease>)listView.ItemsSource;
                    int found = 0;
                    foreach (Disease inicialDis in inicialDiseaseList)
                    {
                        found = 0;
                        foreach (Disease finalDis in finalDiseaseList)
                        {
                            if (inicialDis.compareName(finalDis) == 1)
                            {
                                found = 1;
                                Console.WriteLine(inicialDis.Name+","+inicialDis.Severity+" -- "+ finalDis.Name+""+finalDis.Severity);
                                if (inicialDis.compareSeverity(finalDis) == 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = " dbo.updateSeriousnes";
                                    cmd.Parameters.AddWithValue("@E_NIF", patient.Nif);
                                    cmd.Parameters.AddWithValue("@E_Name", finalDis.Name);
                                    cmd.Parameters.AddWithValue("@seirybesdsdf", finalDis.Severity);
                                    cmd.ExecuteNonQuery();
                                    break;
                                }else
                                {
                                    break;
                                }
                            }
                        }
                        if (found == 0)
                        {
                            Console.WriteLine("delete");
                            //delete disease
                            cmd.Parameters.Clear();
                            cmd.CommandText = "dbo.deleteDisease";
                            cmd.Parameters.AddWithValue("@E_NIF", patient.Nif);
                            cmd.Parameters.AddWithValue("@E_Name", inicialDis.Name);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    found = 0;
                    foreach (Disease finalDis in finalDiseaseList)
                    {
                        found = 0;
                        foreach (Disease inicialDis in inicialDiseaseList)
                        {
                            if (finalDis.compareName(inicialDis) == 1)
                            {
                                found = 1;
                                break;
                            }
                        }
                        if (found == 0)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "dbo.sp_newDiagnosed";
                            cmd.Parameters.AddWithValue("@E_NIF", patient.Nif);
                            cmd.Parameters.AddWithValue("@E_Name", finalDis.Name);
                            cmd.Parameters.AddWithValue("@Seriousness", finalDis.Severity);
                            cmd.Parameters.AddWithValue("@Disable", false);
                            cmd.ExecuteNonQuery();
                        }
                        found = 0;
                    }

                    tran.Commit();
                }
                catch(SqlException ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: "+ ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);

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
        private void Button_Click_AddDisease(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem)cb_severity.SelectedItem).Content != null && ((ComboBoxItem)cb_severity.SelectedItem).Content != null)
            {
                Int32.Parse(((ComboBoxItem)cb_severity.SelectedItem).Content.ToString());
                addDisease(tb_disease.Text, Convert.ToInt32(((ComboBoxItem)cb_severity.SelectedItem).Content.ToString()));
                listView.ItemsSource = null;
                listView.Items.Clear();

                ListViewItem item = new ListViewItem();
                listView.ItemsSource = patient.DiseaseList;
                cb_severity.SelectedItem = ((ComboBoxItem)cb_severity.Items[0]);
                tb_disease.Text = "";
            }
           


        }
        private void addDisease(string name, int severity)
        {
            patient.DiseaseList.Add(new Disease() { Name = name, Severity = severity });
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

                    var itemToRemove = patient.DiseaseList.Single(r => r.Name == listView.SelectedItems[0].ToString());
                    listView.ItemsSource = null;
                    patient.DiseaseList.Remove(itemToRemove);
                    listView.ItemsSource = patient.DiseaseList;
                }
            else
            {
                MessageBox.Show("You have no diseases added.", "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    }

}
