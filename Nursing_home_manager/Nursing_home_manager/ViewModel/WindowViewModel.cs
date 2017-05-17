using Nursing_home_manager;
using Nursing_home_manager.DataModels;
using Nursing_home_manager.Word;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Nursing_home_manager
{

    ///<summary>
    ///The view Model for the custom flat Window
    ///</summary>
    public class WindowViewModel : BaseViewModel
    {
        private Window mWindow;

        /// <summary>
        /// The current page of application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.PatientsPage;
        public WindowViewModel(Window window)
        {
            mWindow = window;
            
        }

    }
}
