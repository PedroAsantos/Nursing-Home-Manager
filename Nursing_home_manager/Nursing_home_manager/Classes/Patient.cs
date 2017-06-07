using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    public class Patient
    {
        private string _Nif;
        private String _Name;
        private String _sex;
        private bool _isMale;
        private bool _isFemale;
        private int _Phone;
        private int _Age;
        private string _Check_in;
        private string _Check_out;
        private bool _Authorization_to_leave;
        private string _Entry_Date;
        private string _Exit_Date;
        private int _BedNumber;
        private int _RoomNumber;
        private List<Disease> _DiseaseList;
        private string _DependentName;
        private string _DependentCC;
        private int _DependentPhone;
        private string _DependentAddress;
        private string _DependentKinship;

        public Patient(string Nif, string name, string sex, int phone, int age, string check_in, string check_out, bool authorization_to_leave, string Entry_Date, string Exit_Date, int BedNumber, int RoomNumber,List<Disease> diseaseList, string DependentName, string DependentCC, int DependentPhone, string DependentAddress, string DependentKinship)
        {
            this._Nif = Nif;
            this._Name = name;
            this._sex = sex;
            this._Phone = phone;
            this._Age = age;
            this._Check_in = check_in;
            this._Check_out = check_out;
            this._Authorization_to_leave = authorization_to_leave;
            this._Entry_Date = Entry_Date;
            this._Exit_Date = Exit_Date;
            this._BedNumber = BedNumber;
            this._RoomNumber = RoomNumber;
            this._DiseaseList = diseaseList;
            this._DependentName = DependentName;
            this._DependentCC = DependentCC;
            this._DependentPhone = DependentPhone;
            this._DependentAddress = DependentAddress;
            this._DependentKinship = DependentKinship;
    }
        public Patient() 
	    {
        }

        public string Nif
        {
            get { return _Nif; }
            set { _Nif = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public bool isMale
        {
           get { return _isMale; }
           set { _isMale = value; }
        }
        public bool isFemale
        {
            get { return _isFemale; }
            set { _isFemale = value; }
        }
        public int Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        public string Check_in
        {
            get { return _Check_in; }
            set { _Check_in = value; }
        }
        public string Check_out
        {
            get { return _Check_out; }
            set { _Check_out = value; }
        }
        public bool Authorization_to_leave
        {
            get { return _Authorization_to_leave; }
            set { _Authorization_to_leave = value; }
        }
        public string Entry_Date
        {
            get { return _Entry_Date; }
            set { _Entry_Date = value; }
        }
        public string Exit_Date
        {
            get { return _Exit_Date; }
            set { _Exit_Date = value; }
        }
        public int BedNumber
        {
            get { return _BedNumber; }
            set { _BedNumber = value; }
        }
        public int RoomNumber
        {
            get { return _RoomNumber; }
            set { _RoomNumber = value; }
        }
        public List<Disease> DiseaseList
        {
            get { return _DiseaseList; }
            set { _DiseaseList = value; }
        }

        public string DependentName
        {
            get { return _DependentName; }
            set { _DependentName = value; }
        }
        public string DependentCC
        {
            get { return _DependentCC; }
            set { _DependentCC = value; }
        }
        public int DependentPhone
        {
            get { return _DependentPhone; }
            set { _DependentPhone = value; }
        }
        public string DependentAddress
        {
            get { return _DependentAddress; }
            set { _DependentAddress = value; }
        }
        public string DependentKinship
        {
            get { return _DependentKinship; }
            set { _DependentKinship = value; }
        }

    }
}
