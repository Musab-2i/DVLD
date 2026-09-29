using BusinessLayer;
using DVLD_Project.Applications.LocalDrivingLicenseApplications;
using DVLD_Project.Global_Forms;
using DVLD_Project.License;
using DVLD_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Drivers
{
    public partial class frmDrivers : frmBase
    {
        private static DataTable _dtAllDrivers;
        private DataTable _dtDrivers;
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void _CalculatTotalDrivers()
        {
            lblDataRecords.Text = dgvAllDrivers.Rows.Count.ToString();
        }

        private void _RefreshDriversList()
        {

            _dtAllDrivers = clsDriver.GetDriversList();
            _dtDrivers = _dtAllDrivers.DefaultView.ToTable(false, "DriverID", "PersonID"
                                                                        , "NationalNo", "FullName"
                                                                        , "CreatedDate", "ActiveLicense");
            dgvAllDrivers.DataSource = _dtDrivers;
            _CalculatTotalDrivers();
        }
        
        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();

            cbFilterBy.SelectedIndex = 0;

            if (dgvAllDrivers.Rows.Count > 0)
            {
                dgvAllDrivers.Columns[0].HeaderText = "Driver ID";
                dgvAllDrivers.Columns[0].Width = 80;

                dgvAllDrivers.Columns[1].HeaderText = "Person ID";
                dgvAllDrivers.Columns[1].Width = 80;

                dgvAllDrivers.Columns[2].HeaderText = "National No";
                dgvAllDrivers.Columns[2].Width = 85;

                dgvAllDrivers.Columns[3].HeaderText = "Full Name";
                dgvAllDrivers.Columns[3].Width = 250;

                dgvAllDrivers.Columns[4].HeaderText = "Created Date";
                dgvAllDrivers.Columns[4].Width = 122;

                dgvAllDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvAllDrivers.Columns[5].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActiveFilter.Visible = true;
                cbIsActiveFilter.Focus();
                cbIsActiveFilter.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActiveFilter.Visible = false;
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                _CalculatTotalDrivers();
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "DriverID")
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            _CalculatTotalDrivers();
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            switch (cbIsActiveFilter.Text)
            {
                case "Yes":
                    _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = 1", FilterColumn);
                    break;

                case "No":
                    _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = 0", FilterColumn);
                    break;

                default:
                    _dtDrivers.DefaultView.RowFilter = "";
                    break;
            }
            _CalculatTotalDrivers();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
            {
                // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmShowPersonInfo(dgvAllDrivers.CurrentRow.Cells["NationalNo"].Value.ToString());
            form.ShowDialog();
            _RefreshDriversList();
        }

        private void showPersonLicenseHestoyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.Find(dgvAllDrivers.CurrentRow.Cells["NationalNo"].Value.ToString());
            if (Person != null)
            {
                Form frm = new frmDriverLicensesHistory(Person.PersonID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No active license found for this application.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
