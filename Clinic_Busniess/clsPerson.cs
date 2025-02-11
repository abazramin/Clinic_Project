using Clinic_Data;
using System;
using System.Data;



namespace Clinic_Busniess
{
    public class clsPerson
    {
        public enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;
        public int PersonID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }



        public clsPerson()
        {
            this.PersonID = -1;
            this.Name = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Phone_Number = "";
            this.Email = "";
            this.Address = "";
            // set mode
            _Mode = enMode.Add;
        }


        private clsPerson(int PersonID, string Name, DateTime DateOfBirth, byte Gendor, string Phone_Number,
            string Email, string Address)
        {
            this.PersonID = PersonID;
            this.Name = Name;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Phone_Number = Phone_Number;
            this.Email = Email;
            this.Address = Address;
            // set mode
            _Mode = enMode.Update;
        }


        private bool _AddNewPerson()
        {
            // call date access
            this.PersonID = clsPersonData.AddNewPerson(this.Name, this.DateOfBirth, this.Gendor, this.Phone_Number, this.Email, this.Address);

            return (this.PersonID != -1);
        }


        private bool _UpdatePerson()
        {
            // call data acces
            return clsPersonData.UpdatePerson(this.PersonID, this.Name, this.DateOfBirth, this.Gendor, this.Phone_Number, this.Email, this.Address);
        }


        public static clsPerson Find(int PersonID)
        {
            string Name = "", Phone_Number = "", Email = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            bool isFound = clsPersonData.GetPersonByID(PersonID, ref Name, ref DateOfBirth, ref Gendor, ref Phone_Number, ref Email, ref Address);

            if (isFound)
            {
                return new clsPerson(PersonID, Name, DateOfBirth, Gendor, Phone_Number, Email, Address);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string Name)
        {
            int PersonID = -1;
            string Phone_Number = "", Email = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            bool isFound = clsPersonData.GetPersonByName(Name, ref PersonID, ref DateOfBirth, ref Gendor, ref Phone_Number, ref Email, ref Address);

            if (isFound)
            {
                return new clsPerson(PersonID, Name, DateOfBirth, Gendor, Phone_Number, Email, Address);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static DataTable GetAllPerson()
        {
            // call date access
            return clsPersonData.GetAllPerson();
        }


        public static bool isExist(int PersonID)
        {
            return clsPersonData.IsExsit(PersonID);
        }

        public static bool isExist(string Name)
        {
            return clsPersonData.IsExsit(Name);
        }

    }
}
