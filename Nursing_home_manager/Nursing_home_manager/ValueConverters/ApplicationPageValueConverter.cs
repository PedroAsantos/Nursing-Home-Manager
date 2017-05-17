using Nursing_home_manager.DataModels;
using Nursing_home_manager.Pages;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Nursing_home_manager
{

    /// <summary>
    /// Converts the <see cref="ApplicationPage!"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage) value)
            {
                case ApplicationPage.PatientsPage:
                    return new PatientsPage();
                case ApplicationPage.HumanResourcesPage:
                    return new HumanResourcesPage();
                default:
                    Debugger.Break();
                    return null;

            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
