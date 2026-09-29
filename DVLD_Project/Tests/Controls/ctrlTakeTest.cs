using BusinessLayer;
using DVLD_Project.GlobalClass;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLayer.clsTestTypes;

namespace DVLD_Project.Tests.Controls
{
    public partial class ctrlTakeTest : UserControl
    {
        private clsTestAppointment _TestAppointment;
        //private clsTestTypes.enTestType _TestType;
        private clsLocalDrivingLicenseApplication _LocalDrivingApp;
        private int _TestID = -1;

        public int TestID {  get { return _TestID; } }

        public ctrlTakeTest()
        {
            InitializeComponent();
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (/*_TestType*/_TestAppointment.TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    gbTestType.Text = "Vision Test";
                    pbTestImage.Image = Resources.VisionTest;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    gbTestType.Text = "Written Test";
                    pbTestImage.Image = Resources.WrittenTest;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    gbTestType.Text = "Street Test";
                    pbTestImage.Image = Resources.StreetTest;
                    break;
            }
        }

        private void _FillInfoInControls()
        {
            lblLocalApplicationID.Text = _LocalDrivingApp.LocalDrivingLicenseApplicationID.ToString();
            lblLocalLicenseClassName.Text = _LocalDrivingApp.LicenseClassInfo.ClassName;
            lblPersonName.Text = _LocalDrivingApp.PersonFullName;

            //lblTrail.Text = _LocalDrivingApp.GetTotalTrialsPerTest(_TestType).ToString();
            lblTrail.Text = _LocalDrivingApp.GetTotalTrialsPerTest(_TestAppointment.TestTypeID).ToString();

            lblAppointmentFees.Text = _TestAppointment.PaidFees.ToString();
            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblTestID.Text = _TestAppointment.TestID == -1 ? "Not Taken Yet" : _TestAppointment.TestID.ToString();
        }

        public void LoadData(int TestAppointmentID)
        {
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show($"Error: No Test Appointment with ID {TestAppointmentID} ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingApp = 
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_TestAppointment.LocalDrivingLicenseApplicationID);
            if (_LocalDrivingApp == null)
            {
                MessageBox.Show($"Error: No Local Driving License Application with ID {_TestAppointment.LocalDrivingLicenseApplicationID} ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //_TestType = _TestAppointment.TestTypeID;
            _FillInfoInControls();
            _LoadTestTypeImageAndTitle();
            _TestID = _TestAppointment.TestID;
        }
    }
}
