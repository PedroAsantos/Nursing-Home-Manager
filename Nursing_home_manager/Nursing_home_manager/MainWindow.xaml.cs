using Nursing_home_manager.Pages;
using System;
using System.Collections.Generic;
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

namespace Nursing_home_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new WindowViewModel(this);
        }

        private void Button_Patients_Click(object sender, RoutedEventArgs e)
        {
            PatientsPage patientsPage = new PatientsPage();
            MainFrame.Content = patientsPage;
        }

        private void Button_Human_Resources_Click(object sender, RoutedEventArgs e)
        {
            HumanResourcesPage humanResourcesPage= new HumanResourcesPage();
            MainFrame.Content = humanResourcesPage; 
        }
        private void Button_Appointments_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsPage appointmentsPage = new AppointmentsPage();
            MainFrame.Content = appointmentsPage;
        }
        private void Button_Visits_Click(object sender, RoutedEventArgs e)
        {
            VisitsPage visitsPage = new VisitsPage();
            MainFrame.Content = visitsPage;
        }
        private void Button_Manage_Click(object sender, RoutedEventArgs e)
        {
            ManagePage managePage = new ManagePage();
            MainFrame.Content = managePage;
        }
    }
}
