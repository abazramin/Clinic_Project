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

        ////only select the columns that you want to show in the grid
        //private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false,  "PersonID", "Name",
        //                                                 "DateOfBirth", "Gendor", "Phone_Number", "Email",
        //                                                 "Address");

      

        private void frmPersonList_Load(object sender, EventArgs e)
        {
            //_dtAllPeople.DefaultView.RowFilter = "";
            dgvPerson.DataSource = _dtAllPeople;
            cbFilterBy.SelectedIndex = 0;
            lbRecordCount.Text = dgvPerson.Rows.Count.ToString();

            if (dgvPerson.Rows.Count >= 0)
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

                dgvPerson.Columns[6].HeaderText = "Address";
                dgvPerson.Columns[6].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            //if the text box is visible, then clear the combo box
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColums = "";


            switch (FilterColums)
            {
                case "Person ID":
                    FilterColums = "PersonID";
                    break;
                case "Name":
                    FilterColums = "Name";
                    break;
                case "Date Of Birth":
                    FilterColums = "DateOfBirth";
                    break;
                case "Gendor":
                    FilterColums = "Gendor";
                    break;
                case "Phone":
                    FilterColums = "Phone_Number";
                    break;
                case "Email":
                    FilterColums = "Email";
                    break;
                case "Address":
                    FilterColums = "Address";
                    break;
                default:
                    FilterColums = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColums == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                 lbRecordCount.Text = dgvPerson.Rows.Count.ToString();
                  return;
            }


            if (FilterColums == "PersonID")
                //in this case we deal with integer not string.

                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColums, txtFilterValue.Text.Trim());
            else
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColums, txtFilterValue.Text.Trim());

                lbRecordCount.Text = dgvPerson.Rows.Count.ToString();

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdate = new frmAddUpdatePerson();
            frmAddUpdate.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdate = new frmAddUpdatePerson((int)dgvPerson.CurrentRow.Cells[0].Value);

            frmAddUpdate.ShowDialog();

        }
    }
}
