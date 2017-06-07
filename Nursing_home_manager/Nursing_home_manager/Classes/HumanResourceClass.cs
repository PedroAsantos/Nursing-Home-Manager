using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    public class HumanResourceClass
    {
        private string _Name;
        private string _Address;
        private int _PhoneNUmber;
        private string _NIF;
        private int _Salary;
        private string _StartDate;
        private string _Type;
        public HumanResourceClass()
        {

        }

        public string Nif
        {
            get { return _NIF; }
            set { _NIF = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public int PhoneNumber
        {
            get { return _PhoneNUmber; }
            set { _PhoneNUmber = value; }
        }
        public int Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }

}
