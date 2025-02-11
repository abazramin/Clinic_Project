using Clinic_Busniess;
using Clinic_Gui.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clinic_Gui.Persons
{
    public partial class frmAddUpdatePerson : Form
    {

        public  delegate void DataBackEventHandler(object sender , int PersonID);

        public DataBackEventHandler DateBack;

        public enum enMode { Add = 0, Update = 1 }

        public enum enGendor {  Famle = 0 , Male = 1 }

        private enMode _Mode = enMode.Add;

        private int _PersonID = -1;

        clsPerson _person;


        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }


        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            //
            _PersonID = PersonID;
        }


        private void _RestAllValue()
        {
            if (_Mode == enMode.Add)
            {
                lbTitle.Text = "Add New Person";
                _person = new clsPerson();
            } else
            {
                lbTitle.Text = "Update Person";
            }


            dtDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtDateOfBirth.Value = dtDateOfBirth.MaxDate;
            dtDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            //
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            rtMale.Checked = true;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void _LoadDate()
        {
            _person = clsPerson.Find(_PersonID);

            if (_person == null)
            {
                MessageBox.Show("Person not found [ " + _PersonID + " ] ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtName.Text = _person.Name;
            dtDateOfBirth.Value = _person.DateOfBirth;

            if (_person.Gendor == 1)
            {
                rtMale.Checked = true;
            }
            else
            {
                rtFamle.Checked = true;
            }

            txtPhone.Text = _person.Phone_Number;
            txtEmail.Text = _person.Email;
            txtAddress.Text = _person.Address;
        }


        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _RestAllValue();

            if (_Mode == enMode.Update)
            {
                _LoadDate();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Error in Vaildation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //
            _person.Name = txtName.Text.Trim();
            _person.DateOfBirth = dtDateOfBirth.Value;


            if (rtMale.Checked)
            {
                _person.Gendor = (byte)enGendor.Male;
            }
            else
            {
                _person.Gendor = (byte)enGendor.Famle;
            }

            _person.Phone_Number = txtPhone.Text.Trim();
            _person.Email = txtEmail.Text.Trim();
            _person.Address = txtAddress.Text.Trim();


            if (_person.Save())
            {
                lbPersonID.Text = _person.PersonID.ToString();
                lbTitle.Text = "Update Person";
                _Mode = enMode.Update;
                MessageBox.Show("Date Saved Sccessfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                //
                DateBack.Invoke(this, _person.PersonID); 
            } else
            {
                MessageBox.Show("Date Not Saved Sccessfully", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }
    }
}

