using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    public class Appointment
    {
        private DateTime _Date;
        private string _DoctorName;
        private string _Speciality;
        private int _ID;
        private bool _Occurred;
        public Appointment()
        {

        }

        public string DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public string Speciality
        {
            get { return _Speciality; }
            set { _Speciality = value; }
        }
        public bool Occurred
        {
            get { return _Occurred; }
            set { _Occurred = value; }
        }
    }
}
