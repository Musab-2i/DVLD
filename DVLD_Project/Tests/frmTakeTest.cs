using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
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
    public partial class frmTakeTest : frmBase
    {
        private int _AppointmentID;
        private clsTestAppointment _TestAppointment;
        private clsTest _Test;

        private int _TestID;

        public frmTakeTest(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
        }

        private void _LoadTestInfo()
        {
            clsTest Test = clsTest.Find(_TestID);
            if (Test == null)
            {
                MessageBox.Show($"Error: Could not find test information with Test ID [{_TestID}].\nPlease contact your system administrator.",
                        "Data Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            if (Test.TestResult == true)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;
            txtNote.Text = string.IsNullOrEmpty(Test.Note) ? "No notes" : Test.Note;
        }
        private void _EnabledControlsBsaedOnMode(bool Enabled)
        {
            btnSave.Enabled = Enabled;
            txtNote.ReadOnly = !Enabled;
            rbPass.Enabled = Enabled;
            rbFail.Enabled = Enabled;
        }
        private void _CheckFormMode()
        {
            _TestAppointment = clsTestAppointment.Find(_AppointmentID);
            if( _TestAppointment == null)
            {
                MessageBox.Show("Appointment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (_TestAppointment.IsLocked)
            {
                _LoadTestInfo();
                _EnabledControlsBsaedOnMode(false);
            }
            else
            {
                _EnabledControlsBsaedOnMode(true);
            }
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTakeTest1.LoadData(_AppointmentID);
            _TestID = ctrlTakeTest1.TestID;

            _CheckFormMode();

            _Test = new clsTest();
        }

        private void ChkVaildResult(object sender, CancelEventArgs e)
        {
            if(rbPass.Checked == false &&  rbFail.Checked == false)
            {
                e.Cancel = true;
                errorProvider1.SetError(rbFail, "You must choose the test result!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(rbFail, null);
            }
        }
        private void _FillInfoInTestRecord()
        {
            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked? true : false;
            _Test.Note = txtNote.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are invalid! Please fix the errors before saving.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return;
            }
            _FillInfoInTestRecord();
            if (_Test.Save())
            {
                // Auto Update In Addnew Test inside clsTestData
                //if (_TestAppointment != null)
                //{
                //    _TestAppointment.IsLocked = true;
                //    _TestAppointment.Save();
                //}

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
