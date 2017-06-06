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
 

        public Shedule()
        {

        }

        public string Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        public TimeSpan EntryHour
        {
            get { return _EntryHour; }
            set { _EntryHour = value; }
        }
        public TimeSpan ExitHour
        {
            get { return _ExitHour; }
            set { _ExitHour = value; }
        }
  
    }
}
