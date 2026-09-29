using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.License;
using DVLD_Project.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.Applications.LocalDrivingLicenseApplications
{
    public partial class frmManageLocalLicenseApplications : frmBase
    {

        private DataTable _dtAllLocalApplications;

        public frmManageLocalLicenseApplications()
        {
            InitializeComponent();
        }
        private void _CalculatTotalLocalApplication()
        {
            lblDataRecords.Text = dgvAllLocalApplications.Rows.Count.ToString();
        }

        private void _RefreshLocalApplicationList()
        {
            _dtAllLocalApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            _dtAllLocalApplications = _dtAllLocalApplications.DefaultView.ToTable(false, "LocalDrivingLicenseApplicationID", "ClassName"
                                                         , "NationalNo", "FullName", "ApplicationDate"
                                                         , "PassedTest", "Status");
            dgvAllLocalApplications.DataSource = _dtAllLocalApplications;
            _CalculatTotalLocalApplication();
        }
        private void frmManageLocalLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshLocalApplicationList();
            cbFilterBy.SelectedIndex = 0;

            if (dgvAllLocalApplications.Rows.Count > 0)
            {
                dgvAllLocalApplications.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L.AppID";
                dgvAllLocalApplications.Columns["LocalDrivingLicenseApplicationID"].Width = 100;

                dgvAllLocalApplications.Columns["ClassName"].HeaderText = "Driving Class";
                dgvAllLocalApplications.Columns["ClassName"].Width = 220;

                dgvAllLocalApplications.Columns["NationalNo"].HeaderText = "National No";
                dgvAllLocalApplications.Columns["NationalNo"].Width = 110;

                dgvAllLocalApplications.Columns["FullName"].HeaderText = "Full Name";
                dgvAllLocalApplications.Columns["FullName"].Width = 285;

                dgvAllLocalApplications.Columns["ApplicationDate"].HeaderText = "Application Date";
                dgvAllLocalApplications.Columns["ApplicationDate"].Width = 200;

                dgvAllLocalApplications.Columns["PassedTest"].HeaderText = "Passed Test";
                dgvAllLocalApplications.Columns["PassedTest"].Width = 100;

                dgvAllLocalApplications.Columns["Status"].HeaderText = "Status";
                dgvAllLocalApplications.Columns["Status"].Width = 100;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Status")
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
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalApplications.DefaultView.RowFilter = "";
                _CalculatTotalLocalApplication();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                if (txtFilterValue.Text.Trim() != "")
                    _dtAllLocalApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllLocalApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            _CalculatTotalLocalApplication();
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbIsActiveFilter.Text)
            {
                case "New":
                    FilterColumn = "New";
                    break;

                case "Cancelled":
                    FilterColumn = "Cancelled";
                    break;

                case "Completed":
                    FilterColumn = "Completed";
                    break;

                default:
                    FilterColumn = "All";
                    break;
            }

            if (FilterColumn == "All")
            {
                _dtAllLocalApplications.DefaultView.RowFilter = "";
                _CalculatTotalLocalApplication();
                return;
            }

            _dtAllLocalApplications.DefaultView.RowFilter = string.Format("[Status] LIKE '{0}%'", FilterColumn);
            _CalculatTotalLocalApplication();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
            {
                // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
                }
            }
        }

        private void _ShowTestForm(clsTestTypes.enTestType testType)
        {
            Form form = new frmListTestAppointments((int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value, testType);
            form.ShowDialog();
            _RefreshLocalApplicationList();
        }

        private void visionTesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowTestForm(clsTestTypes.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowTestForm(clsTestTypes.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowTestForm(clsTestTypes.enTestType.StreetTest);
        }

        private void _OpenAddUpdateLocalDrivingLicenseApplicationsForm(string Case)
        {
            Form form;
            switch (Case)
            {
                case "AddNew":
                    form = new frmAddUpdateLocalDrivingLicenseApplications();
                    form.ShowDialog();
                    break;
                case "Update":
                    form = new frmAddUpdateLocalDrivingLicenseApplications((int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
                    form.ShowDialog();
                    break;
            }
            _RefreshLocalApplicationList();

        }

        private void btnAddNewEdit_Click(object sender, EventArgs e)
        {
            _OpenAddUpdateLocalDrivingLicenseApplicationsForm("AddNew");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenAddUpdateLocalDrivingLicenseApplicationsForm("Update");
        }

        private void _ResetContextMenuState()
        {
            showDetailsToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            DeleteApplicationToolStripMenuItem.Enabled = true;
            CancelApplicationToolStripMenuItem.Enabled = true;
            ScheduleTestToolStripMenuItem.Enabled = true;
            visionTesToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = true;
            scheduleStreetTestToolStripMenuItem.Enabled = true;

            IssueDrivingLicenseToolStripMenuItem.Enabled = true;
            ShowLicenseToolStripMenuItem.Enabled = true;
            showPersonLicenseHestoyToolStripMenuItem.Enabled = true;
        }
        private void _AdjustTheContextMenuBasedOnTheNewSituation()
        {
            ShowLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHestoyToolStripMenuItem.Enabled = false;
            IssueDrivingLicenseToolStripMenuItem.Enabled = false;

            switch ((int)dgvAllLocalApplications.CurrentRow.Cells["PassedTest"].Value) 
            {
                case 0:
                    visionTesToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;

                case 1:
                    visionTesToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;

                case 2:
                    visionTesToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    break;

                case 3:
                    ScheduleTestToolStripMenuItem.Enabled = false;
                    IssueDrivingLicenseToolStripMenuItem.Enabled = true;
                    break;
            }
        }
        private void _AdjustTheContextMenuBasedOnTheCompletedSituation()
        {
            editToolStripMenuItem.Enabled = false;
            DeleteApplicationToolStripMenuItem.Enabled = false;
            CancelApplicationToolStripMenuItem.Enabled = false;
            ScheduleTestToolStripMenuItem.Enabled = false;
            IssueDrivingLicenseToolStripMenuItem.Enabled = false;

            showDetailsToolStripMenuItem.Enabled = true;
            ShowLicenseToolStripMenuItem.Enabled = true;
            showPersonLicenseHestoyToolStripMenuItem.Enabled = true;
        }
        private void _AdjustTheContextMenuBasedOnTheCancelledSituation()
        {
            showDetailsToolStripMenuItem.Enabled = true;
            DeleteApplicationToolStripMenuItem.Enabled = true;
            
            editToolStripMenuItem.Enabled = false;
            CancelApplicationToolStripMenuItem.Enabled = false;
            ScheduleTestToolStripMenuItem.Enabled = false;
            IssueDrivingLicenseToolStripMenuItem.Enabled = false;
            ShowLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHestoyToolStripMenuItem.Enabled = false;
        }

        private void cmsLocalLicenseApp_Opening(object sender, CancelEventArgs e)
        {
            if (dgvAllLocalApplications.Rows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            _ResetContextMenuState();
            switch ((string)dgvAllLocalApplications.CurrentRow.Cells["Status"].Value)
            {
                case "New":
                    _AdjustTheContextMenuBasedOnTheNewSituation();
                    break;
                case "Completed":
                    _AdjustTheContextMenuBasedOnTheCompletedSituation();
                    break;
                case "Cancelled":
                    _AdjustTheContextMenuBasedOnTheCancelledSituation();
                        break;
            }
        }

        private void IssueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmIssueDrivingLicenseFirstTime((int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            form.ShowDialog();
            _RefreshLocalApplicationList();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure want to Delete L.D.L.AppID With ID: .:[ {(int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value} ]:."
                , "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int LocalDrivingLicenseApplicationID = (int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;
                
                clsLocalDrivingLicenseApplication localDrivingLicenseApplication 
                    = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

                if (localDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLocalApplicationList();
                }
                else
                {
                    MessageBox.Show("Application was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsLocalDrivingLicenseApplication LocalDrivingApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                if (LocalDrivingApp == null)
                {
                    MessageBox.Show("An error occurred while changing the Application status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (LocalDrivingApp.SetCancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLocalApplicationList();
                }
                else
                {
                    MessageBox.Show("Failed to cancel the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _OpenShowLocalDrivingLicenseApplicationInfoForm()
        {
            Form form = new frmShowLocalDrivingLicenseApplicationInfo((int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            form.ShowDialog();
            _RefreshLocalApplicationList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenShowLocalDrivingLicenseApplicationInfoForm();
        }

        private void dgvAllLocalApplications_DoubleClick(object sender, EventArgs e)
        {
            _OpenShowLocalDrivingLicenseApplicationInfoForm();
        }

        private void ShowLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalAppID = (int)dgvAllLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalAppID);

            if (LocalApp != null)
            {
                int LicenseID = LocalApp.GetActiveLicenseID();

                if (LicenseID != -1)
                {
                    Form frm = new frmShowLocalLicenseInfo(LicenseID);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No active license found for this application.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void showPersonLicenseHestoyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.Find(dgvAllLocalApplications.CurrentRow.Cells["NationalNo"].Value.ToString());
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