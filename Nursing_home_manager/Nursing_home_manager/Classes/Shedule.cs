using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    public class Shedule
    {
        private string _Day;
        private TimeSpan _EntryHour;
        private TimeSpan _ExitHour;
        private int _ID;
        private int _NumberOfFaults;
        private bool _FinalDate;
        public Shedule()
        {

        }

        public string Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        public bool FinalDate
        {
            get { return _FinalDate; }
            set { _FinalDate = value; }
        }
        public TimeSpan EntryHour
        {
            get { return _EntryHour; }
            set { _EntryHour = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int NumberOfFaults
        {
            get { return _NumberOfFaults; }
            set { _NumberOfFaults = value; }
        }
        public TimeSpan ExitHour
        {
            get { return _ExitHour; }
            set { _ExitHour = value; }
        }
  
    }
}
