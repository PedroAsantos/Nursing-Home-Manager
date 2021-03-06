﻿using Nursing_home_manager.Classes;
using Nursing_home_manager.Pages.Dialogs.Patients;
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
    /// Interaction logic for DialogEditPatient.xaml
    /// </summary>
    public partial class DialogEditPatient : Window
    {
        private Patient patient;
        DialogPatientMainPage dialogPatientMainPage;
        DialogPatientAppointmentPage dialogPatientAppointmentPage;
        DialogPatientMedicinesPage dialogPatientMedicinesPage;
        public DialogEditPatient(Patient patient)
        {
            this.patient = patient;
            this.DataContext = patient;
            InitializeComponent();
            Click_to_MainPage(null,null);
        }

        private void Click_to_MainPage(object sender, RoutedEventArgs e)
        {
            dialogPatientMainPage = new DialogPatientMainPage(patient);
            Frame.Content = dialogPatientMainPage;
        }
        private void Click_toMedicines(object sender, RoutedEventArgs e)
        {
            dialogPatientMedicinesPage = new DialogPatientMedicinesPage(patient);
            Frame.Content = dialogPatientMedicinesPage;
             
        }
        private void Click_toAppointments(object sender, RoutedEventArgs e)
        {
            dialogPatientAppointmentPage = new DialogPatientAppointmentPage(patient);
            Frame.Content = dialogPatientAppointmentPage;
        }
        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            Frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            var bc = new BrushConverter();
            if (Frame.Content == dialogPatientMainPage) {
                Button_Patient.Background = (Brush)bc.ConvertFrom("#854DEE");
                Button_MedicineData.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Appointments.Background = (Brush)bc.ConvertFrom("#512DA8");
            }
            else if(Frame.Content == dialogPatientMedicinesPage)
            {
                Button_MedicineData.Background = (Brush)bc.ConvertFrom("#854DEE");
                Button_Patient.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Appointments.Background = (Brush)bc.ConvertFrom("#512DA8");
            }else
            {
                Button_MedicineData.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Patient.Background = (Brush)bc.ConvertFrom("#512DA8");
                Button_Appointments.Background = (Brush)bc.ConvertFrom("#854DEE");
            }
        }
    }
}
