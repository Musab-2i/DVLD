using BusinessLayer;
using DVLD_Project.GlobalClass;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLayer.clsApplicationTypes;
using static BusinessLayer.clsTestTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DVLD_Project.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        private enum enMode { AddNew = 1, Update = 2 }
        private enum enCreationMode { FirstTimeSchedule = 1, RetakeTestSchedule = 2 }
        private enMode Mode = enMode.AddNew;
        private enCreationMode CreationMode = enCreationMode.FirstTimeSchedule;

        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseAppID = -1;
        private decimal _TestFees = decimal.Zero;
        private decimal _RetakeTestApplicationFees = decimal.Zero;

        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingApp;
        private clsTestAppointment _TestAppointment;

        private bool _RetakeEnable = true;
        public bool RetakeEnable
        {
            get { return _RetakeEnable; }
            set
            {
                _RetakeEnable = value;
                gbRetakeInfo.Enabled = _RetakeEnable;
            }
        }

        public clsTestTypes.enTestType TestType
        {
            get { return _TestType; }
            set
            {
                _TestType = value;
                _LoadTestTypeImageAndTitle();
            }
        }
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
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

        private void _FillInfoInControlsIfRetakeTest()
        {
            lblRetakeAppFees.Text = clsFormat.CurrencyFormat(_RetakeTestApplicationFees);
            RetakeEnable = true;
            lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID == -1 ? "N/A" : _TestAppointment.RetakeTestApplicationID.ToString();
        }

        private decimal _CalculateTotalFees()
        {
            decimal totalFees = 0;
            if (CreationMode == enCreationMode.RetakeTestSchedule)
            {
                totalFees = _TestFees
                +
                _RetakeTestApplicationFees;
            }
            else
            {
                totalFees = _TestFees;
            }
            return totalFees;
        }

        private void _FixedDate()
        {
            dtpTestDate.MinDate = DateTime.Now;
            dtpTestDate.Format = DateTimePickerFormat.Custom;
            dtpTestDate.CustomFormat = "dd/MMM/yyyy";

            if (Mode == enMode.Update)
            {
                if (DateTime.Compare(_TestAppointment.AppointmentDate, DateTime.Now) < 0)
                {
                    dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
                }
                //dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
                dtpTestDate.Value = _TestAppointment.AppointmentDate;
            }
            else
            {
                dtpTestDate.Value = DateTime.Now;
            }
        }
        private void _DetermineCreationModeBasedOnTrials(int Trials)
        {
            if (Trials == 0)
            {
                CreationMode = enCreationMode.FirstTimeSchedule;
            }
            else
            {
                CreationMode = enCreationMode.RetakeTestSchedule;
            }
        }

        private void _FillInfoInControls()
        {
            RetakeEnable = false;

            lblLocalApplicationID.Text = _LocalDrivingApp.LocalDrivingLicenseApplicationID.ToString();
            lblLocalLicenseClassName.Text = clsLicenseClass.FindLicenseClassByID(_LocalDrivingApp.LicenseClassID).ClassName;
            lblPersonName.Text = _LocalDrivingApp.PersonFullName;

            _FixedDate();
            int Trials = _LocalDrivingApp.GetTotalTrialsPerTest(_TestType);
            lblTrial.Text = Trials.ToString();

            dtpTestDate.Value = Mode == enMode.AddNew ? DateTime.Now : clsTestAppointment.Find(_TestAppointmentID).AppointmentDate;
            lblTestFees.Text = clsFormat.CurrencyFormat(_TestFees);


            _DetermineCreationModeBasedOnTrials(Trials);
            if (CreationMode == enCreationMode.RetakeTestSchedule)
            {
                _FillInfoInControlsIfRetakeTest();
            }
            lblTotalFees.Text = clsFormat.CurrencyFormat(_CalculateTotalFees());
        }

        private void _CustomizeTheAppearanceOfControlsBasedOnConstraints(bool show)
        {
            lblUserMessage.Visible = !show;
            btnSave.Enabled = show;
            dtpTestDate.Enabled = show;
        }
        private bool _HandlePreviousTestConstraint()
        {
            switch (_TestType)
            {
                case enTestType.VisionTest:
                    _CustomizeTheAppearanceOfControlsBasedOnConstraints(true);
                    return true;

                case enTestType.WrittenTest:
                    if (_LocalDrivingApp.DoesPassTestType(enTestType.VisionTest))
                    {
                        _CustomizeTheAppearanceOfControlsBasedOnConstraints(true);
                        return true;
                    }
                    _CustomizeTheAppearanceOfControlsBasedOnConstraints(false);
                    lblUserMessage.Text = "Cannot Schedule, Vision Test should be passed first";
                    return false;

                case enTestType.StreetTest:
                    if (_LocalDrivingApp.DoesPassTestType(enTestType.WrittenTest))
                    {
                        _CustomizeTheAppearanceOfControlsBasedOnConstraints(true);
                        return true;
                    }
                    _CustomizeTheAppearanceOfControlsBasedOnConstraints(false);
                    lblUserMessage.Text = "Cannot Schedule, Written Test should be passed first";
                    return false;
            }
            return false;
        }
        private bool _HandleAppointmentLockedConstraint()
        {
            if (Mode == enMode.AddNew)
                return true;

            if (_TestAppointment.IsLocked)
            {
                _CustomizeTheAppearanceOfControlsBasedOnConstraints(false);
                lblUserMessage.Text = "Person already sat for the test, appointment locked.";
                return false;
            }
            _CustomizeTheAppearanceOfControlsBasedOnConstraints(true);
            return true;
        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (Mode == enMode.Update)
                return true;

            if (clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseAppID,_TestType))
            {
                _CustomizeTheAppearanceOfControlsBasedOnConstraints(false);
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                return false;
            }
            _CustomizeTheAppearanceOfControlsBasedOnConstraints(true);
            return true;
        }

        private bool _CheckConstraints()
        {
            if (!_HandlePreviousTestConstraint())
            {
                MessageBox.Show("There is an error in the order of the tests. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!_HandleAppointmentLockedConstraint())
            {
                MessageBox.Show("This appointment is locked.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!_HandleActiveTestAppointmentConstraint())
            {
                MessageBox.Show("The user already has an active appointment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public void LoadData(int LocalDrivingLicenseAppID, clsTestTypes.enTestType TestType, int TestAppointmentID = -1)
        {
            _LocalDrivingApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppID);
            if (_LocalDrivingApp == null)
            {
                MessageBox.Show("Error! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType = TestType;
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;
            _TestFees = clsTestTypes.Find(_TestType).TestFees;
            _RetakeTestApplicationFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.RetakeTest).ApplicationFees;

            if (TestAppointmentID == -1)
            {
                Mode = enMode.AddNew;
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                Mode = enMode.Update;
                _TestAppointmentID = TestAppointmentID;
                _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            }

            _LoadTestTypeImageAndTitle();
            _FillInfoInControls();
            _CheckConstraints();
        }

        private void _FillControlsInfoInRecord()
        {
            _TestAppointment.TestTypeID = _TestType;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingApp.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = _TestFees;
            _TestAppointment.IsLocked = false;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private bool _HandleRetakeApplication()
        {
            if (Mode == enMode.Update || CreationMode == enCreationMode.FirstTimeSchedule)
                return true;
            
            clsApplication RetakeApplication = new clsApplication();
            RetakeApplication.ApplicantPersonID = _LocalDrivingApp.ApplicantPersonID;
            RetakeApplication.ApplicationTypeID = clsApplicationTypes.enApplicationType.RetakeTest;
            RetakeApplication.ApplicationDate = DateTime.Now;
            RetakeApplication.LastStatusDate = DateTime.Now;
            RetakeApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            RetakeApplication.PaidFees = _RetakeTestApplicationFees;
            RetakeApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!RetakeApplication.Save())
            {
                _TestAppointment.RetakeTestApplicationID = -1;
                MessageBox.Show("Failed to Create Retake Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _TestAppointment.RetakeTestApplicationID = RetakeApplication.ApplicationID;
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _FillControlsInfoInRecord();
            if (_TestAppointment.Save())
            {
                Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
