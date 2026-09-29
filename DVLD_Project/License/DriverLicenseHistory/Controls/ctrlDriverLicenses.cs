using BusinessLayer;
using DVLD_Project.License.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.License.DriverLicenseHistory.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver Driver;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _CalculateInternationalLicenses()
        {
            lblInternationalLicenseDataRecords.Text = dgvInternationalLicenses.RowCount.ToString();

        }
        private void _LoadInternationalLicenses()
        {
            DataTable AllInternationalLicense = Driver.GetInternationalLicenses();
            dgvInternationalLicenses.DataSource = AllInternationalLicense;
            _CalculateInternationalLicenses();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns["InternationalLicenseID"].Width = 100;

                dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns["ApplicationID"].Width = 100;

                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].Width = 232;

                dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns["IssueDate"].Width = 150;

                dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns["ExpirationDate"].Width = 150;

                dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns["IsActive"].Width = 80;
            }
        }
        private void _CalculateLocalLicenses()
        {
            lblLocalLicenseDataRecords.Text = dgvLocalLicenses.RowCount.ToString();
        }
        private void _LoadLocalLicenses()
        {
            DataTable AllLocalLicense = Driver.GetLocalLicenses();
            dgvLocalLicenses.DataSource = AllLocalLicense;
            _CalculateLocalLicenses();

            if (dgvLocalLicenses.Rows.Count > 0)
            {
                dgvLocalLicenses.Columns["LicenseID"].HeaderText = "License ID";
                dgvLocalLicenses.Columns["LicenseID"].Width = 100;

                dgvLocalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvLocalLicenses.Columns["ApplicationID"].Width = 100;

                dgvLocalLicenses.Columns["ClassName"].HeaderText = "Class Name";
                dgvLocalLicenses.Columns["ClassName"].Width = 232;

                dgvLocalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns["IssueDate"].Width = 150;

                dgvLocalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns["ExpirationDate"].Width = 150;

                dgvLocalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvLocalLicenses.Columns["IsActive"].Width = 80;
            }
        }

        public void LoadLicensesHistory(int DriverID)
        {
            Driver = clsDriver.FindByDriverID(DriverID);
            if(Driver == null)
            {
                MessageBox.Show($"No Driver was found with ID [{DriverID}].", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = DriverID;
            _LoadLocalLicenses();
            _LoadInternationalLicenses();
        }

        private void dgvLocalLicenses_DoubleClick(object sender, EventArgs e)
        {
            if (dgvLocalLicenses.CurrentRow != null)
            {
                Form form = new frmShowLocalLicenseInfo((int)dgvLocalLicenses.CurrentRow.Cells["LicenseID"].Value);
                form.ShowDialog();
            }
        }

        private void dgvInternationalLicenses_DoubleClick(object sender, EventArgs e)
        {
            if (dgvInternationalLicenses.CurrentRow != null)
            {
                Form form = new frmShowInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells["InternationalLicenseID"].Value);
                form.ShowDialog();
            }
        }
    }
}
