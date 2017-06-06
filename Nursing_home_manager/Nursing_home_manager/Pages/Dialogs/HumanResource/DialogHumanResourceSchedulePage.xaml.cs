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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nursing_home_manager.Pages.Dialogs.HumanResource
{
    /// <summary>
    /// Interaction logic for DialogHumanResourceSchedulePage.xaml
    /// </summary>
    public partial class DialogHumanResourceSchedulePage : Page
    {
        private HumanResourceClass HumanResource;
        public DialogHumanResourceSchedulePage(HumanResourceClass HumanResource)
        {
            this.HumanResource = HumanResource;
            InitializeComponent();
        }
        private void updateScheduleList()
        {

        }
        private void Click_deleteSchedule(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Add_Shift(object sender, RoutedEventArgs e)
        {

            if (((ComboBoxItem)cb_entryhour.SelectedItem).Content != null && ((ComboBoxItem)cb_entryday.SelectedItem).Content != null 
                && ((ComboBoxItem)cb_entryperiodo.SelectedItem).Content != null && ((ComboBoxItem)cb_entryminute.SelectedItem).Content != null
                && ((ComboBoxItem)cb_exithour.SelectedItem).Content != null && ((ComboBoxItem)cb_exitperiodo.SelectedItem).Content != null && ((ComboBoxItem)cb_exitminute.SelectedItem).Content != null)
            {
                Shedule schedule;
                TimeSpan entryHour;
                TimeSpan exitHour;

                if (String.Compare(((ComboBoxItem)cb_entryperiodo.SelectedItem).Content.ToString(), "AM") == 0)
                {
                    entryHour = new TimeSpan(Convert.ToInt32(((ComboBoxItem)cb_entryhour.SelectedItem).Content.ToString()), Convert.ToInt32(((ComboBoxItem)cb_entryminute.SelectedItem).Content.ToString()), 0);
                }
                else
                {

                    entryHour = new TimeSpan(Convert.ToInt32(((ComboBoxItem)cb_entryhour.SelectedItem).Content.ToString()), Convert.ToInt32(((ComboBoxItem)cb_entryminute.SelectedItem).Content.ToString()), 0);
                }

                if (String.Compare(((ComboBoxItem)cb_exitperiodo.SelectedItem).Content.ToString(), "AM") == 0)
                {
                  exitHour = new TimeSpan(Convert.ToInt32(((ComboBoxItem)cb_entryhour.SelectedItem).Content.ToString()), Convert.ToInt32(((ComboBoxItem)cb_entryminute.SelectedItem).Content.ToString()), 0);
                }
                else
                {

                    exitHour = new TimeSpan(Convert.ToInt32(((ComboBoxItem)cb_entryhour.SelectedItem).Content.ToString()), Convert.ToInt32(((ComboBoxItem)cb_entryminute.SelectedItem).Content.ToString()), 0);
                }

                schedule = new Shedule()
                {
                    Day = ((ComboBoxItem)cb_entryday.SelectedItem).Content.ToString(),
                    EntryHour = entryHour,
                    ExitHour = exitHour
                };

                addScheduletToDb(schedule);
                listView.ItemsSource = null;
                listView.Items.Clear();
                updateScheduleList();
                //  ListViewItem item = new ListViewItem();
                //listView.ItemsSource = listMedicine;
                cb_entryday.SelectedItem = ((ComboBoxItem)cb_entryday.Items[0]);
                cb_entryhour.SelectedItem = ((ComboBoxItem)cb_entryhour.Items[1]);
                cb_entryminute.SelectedItem = ((ComboBoxItem)cb_entryminute.Items[0]);
                cb_entryperiodo.SelectedItem = ((ComboBoxItem)cb_entryperiodo.Items[0]);
                cb_exithour.SelectedItem = ((ComboBoxItem)cb_exithour.Items[1]);
                cb_exitminute.SelectedItem = ((ComboBoxItem)cb_exitminute.Items[0]);
                cb_exitperiodo.SelectedItem = ((ComboBoxItem)cb_exitperiodo.Items[0]);
            
            }

        }
        private void addScheduletToDb(Shedule schedule)
        {

        }
    }
}
