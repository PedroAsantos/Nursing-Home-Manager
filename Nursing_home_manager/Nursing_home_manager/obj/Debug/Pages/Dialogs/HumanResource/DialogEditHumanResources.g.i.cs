﻿#pragma checksum "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8B3AB846F86BF982243886C040872868"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// DialogEditHumanResources
    /// </summary>
    public partial class DialogEditHumanResources : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_HumanResourse;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Schedule;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame Frame;
        
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
            System.Uri resourceLocater = new System.Uri("/Nursing_home_manager;component/pages/dialogs/humanresource/dialogedithumanresour" +
                    "ces.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
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
            this.Button_HumanResourse = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
            this.Button_HumanResourse.Click += new System.Windows.RoutedEventHandler(this.Click_to_MainPage);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Button_Schedule = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
            this.Button_Schedule.Click += new System.Windows.RoutedEventHandler(this.Click_toSchedule);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Frame = ((System.Windows.Controls.Frame)(target));
            
            #line 33 "..\..\..\..\..\Pages\Dialogs\HumanResource\DialogEditHumanResources.xaml"
            this.Frame.ContentRendered += new System.EventHandler(this.myFrame_ContentRendered);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
