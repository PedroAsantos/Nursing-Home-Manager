using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{


     public class Visit
    {
        private string _PatientName;
        private string _PatientNif;
        private string _VisitName;
        private string _VisitCC;
        private int _VisitPhone;
        private string _VisitAddress;
        private string _Relationship;
        private string _KinshipDegree;
        private DateTime _Date;
        public Visit()
        {

        }

        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }
        public string PatientNif
        {
            get { return _PatientNif; }
            set { _PatientNif = value; }
        }
        public string VisitName
        {
            get { return _VisitName; }
            set { _VisitName = value; }
        }
        public string VisitCC
        {
            get { return _VisitCC; }
            set { _VisitCC = value; }
        }
        public int VisitPhone
        {
            get { return _VisitPhone; }
            set { _VisitPhone = value; }
        }
        public string VisitAddress
        {
            get { return _VisitAddress; }
            set { _VisitAddress = value; }
        }
        public string Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; }
        }
        public string KinshipDegree
        {
            get { return _KinshipDegree; }
            set { _KinshipDegree = value; }
        }
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

    }

}
