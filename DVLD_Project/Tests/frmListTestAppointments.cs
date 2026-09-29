using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class frmListTestAppointments : frmBase
    {
        private int _LocalDrivingLicenseApplicationID;
        private DataTable _dtLicenseTestAppointments;
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void _UpdateFormText(string Title)
        {
            this.Text = Title;
            lblTestTitle.Text = Title;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    _UpdateFormText("Vision Test Appointments");
                    pbTestImage.Image = Resources.VisionTest;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    _UpdateFormText("Written Test Appointments");
                    pbTestImage.Image = Resources.WrittenTest;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    _UpdateFormText("Street Test Appointments");
                    pbTestImage.Image = Resources.StreetTest;
                    break;
            }
        }
        private void _RefreshAppointmentsList()
        {
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            _dtLicenseTestAppointments = _dtLicenseTestAppointments.DefaultView.ToTable(false, "TestAppointmentID", "AppointmentDate"
                                                                                             , "PaidFees", "Result", "IsLocked");

            dgvAllTestAppointments.DataSource = _dtLicenseTestAppointments;
            lblDataRecords.Text = dgvAllTestAppointments.Rows.Count.ToString();
        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplicationID);
            _LoadTestTypeImageAndTitle();

            _RefreshAppointmentsList();

            //if (dgvAllTestAppointments.Rows.Count > 0)
            //{
                dgvAllTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvAllTestAppointments.Columns["TestAppointmentID"].Width = 150;

                dgvAllTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvAllTestAppointments.Columns["AppointmentDate"].Width = 200;

                dgvAllTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvAllTestAppointments.Columns["PaidFees"].Width = 140;

                dgvAllTestAppointments.Columns["Result"].HeaderText = "Result";
                dgvAllTestAppointments.Columns["Result"].Width = 100;

                dgvAllTestAppointments.Columns["IsLocked"].HeaderText = "Is Locked";
                dgvAllTestAppointments.Columns["IsLocked"].Width = 100;
            //}
        }
        
        private bool _HandelPassedTheTest()
        {
            if (_LocalDrivingLicenseApplication.DoesPassTestType(_TestType))
            {
                MessageBox.Show("This person already passed this test before, you can only retake failed tests.",
                                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool _HandelActiveTestAppointment()
        {
            int ActiveAppointmentID = _LocalDrivingLicenseApplication.GetActiveTestAppointmentID(_TestType);

            if (ActiveAppointmentID != -1)
            {
                MessageBox.Show($"Person already has an active appointment for this test \nwith ID .:[ {ActiveAppointmentID} ]:., You cannot add a new one.",
                                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool _IsTestLocked()
        {
            return (bool)dgvAllTestAppointments.CurrentRow.Cells["IsLocked"].Value;
        }

        /// <summary>
        /// Having a parameter opens the screen to editing mode
        /// </summary>
        /// <param name="AppointmentID"></param>
        private void _OpenScheduleTestForm(int AppointmentID = -1)
        {
            Form frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, AppointmentID);
            frm.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandelPassedTheTest())
            {
                return;
            }
            if (!_HandelActiveTestAppointment())
            {
                return;
            }

            _OpenScheduleTestForm();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_HandelPassedTheTest())
            {
                return;
            }
            _OpenScheduleTestForm((int)dgvAllTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmTakeTest((int)dgvAllTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value);
            form.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void cmsTakesEditTest_Opening(object sender, CancelEventArgs e)
        {
            bool IsTestLocked = _IsTestLocked();

            editToolStripMenuItem.Enabled = !IsTestLocked;
            takeTestToolStripMenuItem.Text = IsTestLocked ? "Show Test Details" : "Take Test";

            //if (_IsTestLocked())
            //{
            //    takeTestToolStripMenuItem.Text = "Show Test Details";
            //}
            //else
            //{
            //    takeTestToolStripMenuItem.Text = "Take Test";
            //}
        }
    }
}
