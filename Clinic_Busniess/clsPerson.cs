using Clinic_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Busniess
{
    public class clsPerson
    {
        public enum enMode { Add = 0 , Update = 1 }
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


        private clsPerson(int PersonID , string Name, DateTime DateOfBirth, byte Gendor, string Phone_Number,
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
            this.PersonID = clsPersonData.AddNewPerson(this.Name , this.DateOfBirth , this.Gendor, this.Phone_Number, this.Email, this.Address);

            return (this.PersonID > 0);
        }


        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID , this.Name , this.DateOfBirth, this.Gendor, this.Phone_Number, this.Email, this.Address);
        }
    }
}
