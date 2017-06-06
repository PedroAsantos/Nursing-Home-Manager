using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    public class Medicine
    {
        public Medicine()
        {

        }

        private string _Name;
        private TimeSpan _Time;
        private string _Periodo;
        private string _Day;
        private int _Dose;
        private int _ID;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }
        public string Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        public TimeSpan Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        public int Dose
        {
            get { return _Dose; }
            set { _Dose = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
    }
}
