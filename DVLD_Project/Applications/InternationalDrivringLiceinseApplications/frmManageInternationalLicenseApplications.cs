using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.InternationalLicense;
using DVLD_Project.License;
using DVLD_Project.License.International_Licenses;
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

namespace DVLD_Project.Applications.InternationalDrivringLiceinseApplications
{
    public partial class frmManageInternationalLicenseApplications : frmBase
    {
        private DataTable _dtAllInternationalApplications;
        public frmManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void _CalculatTotalInternationalApplication()
        {
            lblDataRecords.Text = dgvAllInternationalApplications.Rows.Count.ToString();
        }
        private void _RefreshInternationalApplicationList()
        {
            _dtAllInternationalApplications = clsInternationalLicenses.GetAllInternationalLicenses();
            _dtAllInternationalApplications = _dtAllInternationalApplications.DefaultView.ToTable(false, "InternationalLicenseID", "ApplicationID"
                                                         , "DriverID", "IssuedUsingLocalLicenseID", "IssueDate"
                                                         , "ExpirationDate", "IsActive");
            dgvAllInternationalApplications.DataSource = _dtAllInternationalApplications;
            _CalculatTotalInternationalApplication();
        }
        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshInternationalApplicationList();
            cbFilterBy.SelectedIndex = 0;

            if (dgvAllInternationalApplications.Rows.Count > 0)
            {
                dgvAllInternationalApplications.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
                dgvAllInternationalApplications.Columns["InternationalLicenseID"].Width = 105;

                dgvAllInternationalApplications.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvAllInternationalApplications.Columns["ApplicationID"].Width = 100;

                dgvAllInternationalApplications.Columns["DriverID"].HeaderText = "Driver ID";
                dgvAllInternationalApplications.Columns["DriverID"].Width = 110;

                dgvAllInternationalApplications.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
                dgvAllInternationalApplications.Columns["IssuedUsingLocalLicenseID"].Width = 100;

                dgvAllInternationalApplications.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvAllInternationalApplications.Columns["IssueDate"].Width = 300;

                dgvAllInternationalApplications.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvAllInternationalApplications.Columns["ExpirationDate"].Width = 300;

                dgvAllInternationalApplications.Columns["IsActive"].HeaderText = "IsActive";
                dgvAllInternationalApplications.Columns["IsActive"].Width = 100;
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

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            switch (cbIsActiveFilter.Text)
            {
                case "Yes":
                    _dtAllInternationalApplications.DefaultView.RowFilter = string.Format("[{0}] = 1", FilterColumn);
                    break;

                case "No":
                    _dtAllInternationalApplications.DefaultView.RowFilter = string.Format("[{0}] = 0", FilterColumn);
                    break;

                default:
                    _dtAllInternationalApplications.DefaultView.RowFilter = "";
                    break;
            }
            _CalculatTotalInternationalApplication();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
            }
        }

        private void _ShowInternationalLicenseInfo()
        {
            if (dgvAllInternationalApplications.CurrentRow == null) return;

            int InternationalLicenseID = (int)dgvAllInternationalApplications.CurrentRow.Cells["InternationalLicenseID"].Value;

            Form form = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            form.ShowDialog();

            _RefreshInternationalApplicationList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAllInternationalApplications.CurrentRow == null) return;
            
            int PersonID = clsInternationalLicenses.Find
                ((int)dgvAllInternationalApplications.CurrentRow.Cells["InternationalLicenseID"].Value).PersonInfo.PersonID;

            Form form = new frmShowPersonInfo(PersonID);
            form.ShowDialog();
            _RefreshInternationalApplicationList();
        }

        private void ShowLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowInternationalLicenseInfo();
        }

        private void showPersonLicenseHestoyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAllInternationalApplications.CurrentRow == null) return;

            int PresonID = clsInternationalLicenses.Find
                ((int)dgvAllInternationalApplications.CurrentRow.Cells["InternationalLicenseID"].Value).PersonInfo.PersonID;

            clsPerson Person = clsPerson.Find(PresonID);
            
            if (Person != null)
            {
                Form frm = new frmDriverLicensesHistory(Person.PersonID);
                frm.ShowDialog();
                _RefreshInternationalApplicationList();
            }
            else
            {
                MessageBox.Show("No active license found for this application.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllInternationalApplications.DefaultView.RowFilter = "";
            }
            else
            {
                _dtAllInternationalApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }

            _CalculatTotalInternationalApplication();
        }

        private void dgvAllInternationalApplications_DoubleClick(object sender, EventArgs e)
        {
            _ShowInternationalLicenseInfo();
        }

        private void btnAddNewEdit_Click(object sender, EventArgs e)
        {
            Form form = new frmIssueInternationalLicense();
            form.ShowDialog();
            _RefreshInternationalApplicationList();
        }
    }
}
