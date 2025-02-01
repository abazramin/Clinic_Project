using Clinic_Busniess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic_Gui.Persons
{
    public partial class frmPersonList : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPerson();
        public frmPersonList()
        {
            InitializeComponent();
        }

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "Name",
                                                         "DateOfBirth", "Gendor", "Phone_Number", "Email",
                                                         "Address");

        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPerson();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "Name",
                                                         "DateOfBirth", "Gendor", "Phone_Number", "Email",
                                                         "Address");

            dgvPerson.DataSource = _dtPeople;
            lbRecordCount.Text = dgvPerson.Rows.Count.ToString();
        }

        private void frmPersonList_Load(object sender, EventArgs e)
        {
            dgvPerson.DataSource = _dtPeople;
            //cbFilterBy.SelectedIndex = 0;
            lbRecordCount.Text = dgvPerson.Rows.Count.ToString();
            if (dgvPerson.Rows.Count > 0)
            {

                dgvPerson.Columns[0].HeaderText = "Person ID";
                dgvPerson.Columns[0].Width = 110;

                dgvPerson.Columns[1].HeaderText = "Name";
                dgvPerson.Columns[1].Width = 120;


                dgvPerson.Columns[2].HeaderText = "Date Of Birth";
                dgvPerson.Columns[2].Width = 120;

                dgvPerson.Columns[3].HeaderText = "Gendor";
                dgvPerson.Columns[3].Width = 140;


                dgvPerson.Columns[4].HeaderText = "Phone";
                dgvPerson.Columns[4].Width = 120;

                dgvPerson.Columns[5].HeaderText = "Email";
                dgvPerson.Columns[5].Width = 120;

                dgvPerson.Columns[5].HeaderText = "Address";
                dgvPerson.Columns[5].Width = 120;
            }
        }
    }
}
