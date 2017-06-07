using Nursing_home_manager.Classes;
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
using System.Windows.Shapes;

namespace Nursing_home_manager.Pages.Dialogs.HumanResource
{
    /// <summary>
    /// Interaction logic for DialogEditHumanResources.xaml
    /// </summary>
    public partial class DialogEditHumanResources : Window
    {
        private HumanResourceClass HumanResource;
        private DialogHumanResourceMainPage dialogHumanResourceMainPage;
        private DialogHumanResourceSchedulePage dialogPatientMedicinesPage;
        private DialogHumanResourceFaultsPage dialogHumanResourceFaultsPage;
        public DialogEditHumanResources(HumanResourceClass HumanResource)
        {
            this.HumanResource = HumanResource;
            this.DataContext = HumanResource;
            InitializeComponent();
            Click_to_MainPage(null, null);
        }

        private void Click_to_MainPage(object sender, RoutedEventArgs e)
        {
            dialogHumanResourceMainPage = new DialogHumanResourceMainPage(HumanResource);
            Frame.Content = dialogHumanResourceMainPage;
        }
        private void Click_toSchedule(object sender, RoutedEventArgs e)
        {
            dialogPatientMedicinesPage = new DialogHumanResourceSchedulePage(HumanResource);
            Frame.Content = dialogPatientMedicinesPage;

        }
        private void Click_toFatuls(object sender, RoutedEventArgs e)
        {
            dialogHumanResourceFaultsPage = new DialogHumanResourceFaultsPage(HumanResource);
            Frame.Content = dialogHumanResourceFaultsPage;
        }
        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            Frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            var bc = new BrushConverter();
            if (Frame.Content == dialogHumanResourceMainPage)
            {
                Button_HumanResourse.Background = (Brush)bc.ConvertFrom("#854DEE");
                Button_Schedule.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Faults.Background = (Brush)bc.ConvertFrom("#512DA8");
            }
            else if(Frame.Content == dialogPatientMedicinesPage)
            {
                Button_Schedule.Background = (Brush)bc.ConvertFrom("#854DEE");
                Button_HumanResourse.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Faults.Background = (Brush)bc.ConvertFrom("#512DA8");
            }else
            {
                Button_Schedule.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_HumanResourse.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Faults.Background = (Brush)bc.ConvertFrom("#854DEE");
            }
        }
    }
}
