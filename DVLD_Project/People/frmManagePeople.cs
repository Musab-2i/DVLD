using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.People;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmManagePeople : frmBase
    {
        private DataTable _dtAllPeople;
        
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetPeopleList();
            _dtAllPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName"
                                                              , "SecondName", "ThirdName", "LastName"
                                                              , "GendorCaption", "DateOfBirth", "CountryName"
                                                              , "PhoneNo", "Email");

            dgvAllPeople.DataSource = _dtAllPeople;
            _CalculatTotalPeople();

        }
        private void _CalculatTotalPeople()
        {
            lblDataRecords.Text = dgvAllPeople.Rows.Count.ToString();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            dgvAllPeople.DataSource = _dtAllPeople;
            cbFilterBy.SelectedIndex = 0;
            _CalculatTotalPeople();

            if (dgvAllPeople.Rows.Count > 0)
            {
                dgvAllPeople.Columns[0].HeaderText = "Person ID";
                dgvAllPeople.Columns[0].Width = 80;

                dgvAllPeople.Columns[1].HeaderText = "National No";
                dgvAllPeople.Columns[1].Width = 100;

                dgvAllPeople.Columns[2].HeaderText = "First Name";
                dgvAllPeople.Columns[2].Width = 100;

                dgvAllPeople.Columns[3].HeaderText = "Second Name";
                dgvAllPeople.Columns[3].Width = 100;

                dgvAllPeople.Columns[4].HeaderText = "Third Name";
                dgvAllPeople.Columns[4].Width = 100;

                dgvAllPeople.Columns[5].HeaderText = "Last Name";
                dgvAllPeople.Columns[5].Width = 100;

                dgvAllPeople.Columns[6].HeaderText = "Gendor";
                dgvAllPeople.Columns[6].Width = 60;

                dgvAllPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvAllPeople.Columns[7].Width = 140;

                dgvAllPeople.Columns[8].HeaderText = "Nationality";
                dgvAllPeople.Columns[8].Width = 90;

                dgvAllPeople.Columns[9].HeaderText = "Phone";
                dgvAllPeople.Columns[9].Width = 100;

                dgvAllPeople.Columns[10].HeaderText = "Email";
                dgvAllPeople.Columns[10].Width = 170;
            }
        }

        private void _OpenAddUpdateUpdatePersonForm(int PersonID)
        {
            Form frm = new frmAddUpdatePerson(PersonID);
            frm.ShowDialog();
            _RefreshPeopleList();
        }
        private void btnAddNewEdit_Click(object sender, EventArgs e)
        {
            _OpenAddUpdateUpdatePersonForm(-1);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenAddUpdateUpdatePersonForm(-1);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenAddUpdateUpdatePersonForm((int)dgvAllPeople.CurrentRow.Cells[0].Value);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmShowPersonInfo((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure want to Delete Person With ID: .:[ {(int)dgvAllPeople.CurrentRow.Cells[0].Value} ]:."
                ,"Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson((int)dgvAllPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvAllPeople_DoubleClick(object sender, EventArgs e)
        {
            Form form = new frmShowPersonInfo((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                _CalculatTotalPeople();
                return;
            }

            if (FilterColumn == "PersonID")
            {
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            _CalculatTotalPeople();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
            {
                // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
                }
            }
        }
    }
}
