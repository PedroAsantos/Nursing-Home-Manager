﻿#pragma checksum "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "31706F1DE4EB7D4B5DABA9CFE0BF1903"
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
using Nursing_home_manager.Pages.Dialogs.HumanResource;
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


namespace Nursing_home_manager.Pages.Dialogs.HumanResource {
    
    
    /// <summary>
    /// DialogHumanResourceMainPage
    /// </summary>
    public partial class DialogHumanResourceMainPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_name;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_NIF;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_phone;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_address;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_salary;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_designation;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_editsalary;
        
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
            System.Uri resourceLocater = new System.Uri("/Nursing_home_manager;component/pages/dialogs/humanresource/dialoghumanresourcema" +
                    "inpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
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
            this.tb_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.tb_NIF = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
            this.tb_NIF.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tb_phone = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
            this.tb_phone.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tb_address = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tb_salary = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
            this.tb_salary.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_designation = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.tb_editsalary = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
            this.tb_editsalary.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 57 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogHumanResourceMainPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Add);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

