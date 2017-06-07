﻿#pragma checksum "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F294226ACE24EAE1596808B108A741D1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using Nursing_home_manager.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Nursing_home_manager.Pages {
    
    
    /// <summary>
    /// DialogPatientMedicinesPage
    /// </summary>
    public partial class DialogPatientMedicinesPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listView;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_deleteButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_medicineName;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_dose;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_day;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_hour;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_minute;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_periodo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Nursing_home_manager;component/pages/dialogs/patients/dialogpatientmedicinespage" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.listView = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.bt_deleteButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
            this.bt_deleteButton.Click += new System.Windows.RoutedEventHandler(this.Click_deleteMedicineButton);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tb_medicineName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tb_dose = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
            this.tb_dose.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cb_day = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.cb_hour = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.cb_minute = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.cb_periodo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            
            #line 104 "..\..\..\..\..\Pages\Dialogs\Patients\DialogPatientMedicinesPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Add_Medicine);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

