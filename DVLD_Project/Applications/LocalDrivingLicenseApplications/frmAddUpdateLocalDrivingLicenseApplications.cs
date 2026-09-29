using BusinessLayer;
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

namespace DVLD_Project.Applications.LocalDrivingLicenseApplications
{
    public partial class frmAddUpdateLocalDrivingLicenseApplications : Form
    {
        private int _LocalAppID;
        private int _PersonID = -1;
        private clsLocalDrivingLicenseApplication _LocalLicenseApp;
        private clsApplicationTypes _LocalDrivingLicense = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.NewDrivingLicense);
        private enum enMode { AddNew = 1 , Update = 2 }
        private enMode Mode;

        public frmAddUpdateLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicenseApplications(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            Mode = enMode.Update;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage2 /*&& Mode == enMode.AddNew*/)
            {
                if (ctrlPersonCardWithFilter1.PersonID == -1)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select a person first before moving to login information.",
                                    "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                btnSave.Enabled = true;
            }
        }

        private void _UpdateFormStatus()
        {
            if (Mode == enMode.AddNew)
            {
                ctrlPersonCardWithFilter1.FilterEnable = true;
                this.Text = "New Local Driving License Application";
                lblMode.Text = "New Local Driving License Application";
            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnable = false;
                this.Text = "Edit Local Driving License Application";
                lblMode.Text = "Edit Local Driving License Application";
            }
        }
        private void _ResetDefaultValue()
        {
            if (Mode == enMode.AddNew)
            {
                _UpdateFormStatus();
                _LocalLicenseApp = new clsLocalDrivingLicenseApplication();
                _FillDefaultValue();
                return;
            }

        }
        private void _EnablePeopleFilter()
        {
            if (Mode == enMode.AddNew)
                ctrlPersonCardWithFilter1.FilterEnable = true;
            else
                ctrlPersonCardWithFilter1.FilterEnable = false;
        }

        private void _FillClassesInCompoBox()
        {
            DataTable Classes = clsLicenseClass.GetClassesList();
            foreach (DataRow Class in Classes.Rows)
            {
                cbLicenseClasses.Items.Add(Class["ClassName"]);
            }
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString("Class 3 - Ordinary driving license");
        }

        private void _FillDefaultValue()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();

            //New local driving license service fees
            lblApplicationFees.Text = clsFormat.CurrencyFormat(_LocalDrivingLicense.ApplicationFees);
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void _FillApplicationInfoInControls()
        {
            lblApplicationID.Text = _LocalLicenseApp.LocalDrivingLicenseApplicationID.ToString();
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(_LocalLicenseApp.LicenseClassInfo.ClassName);
            _FillDefaultValue();
        }

        private void _LoadData()
        {
            _LocalLicenseApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalAppID);
            if (_LocalLicenseApp == null)
            {
                MessageBox.Show($"No Application With ID: {this._LocalAppID}", "Application Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalLicenseApp.ApplicantPersonID);
                _UpdateFormStatus();
                _FillApplicationInfoInControls();
            }
        }
        private void frmAddUpdateLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            _EnablePeopleFilter();
            _FillClassesInCompoBox();
            if (Mode == enMode.Update)
            {
                _LoadData();
            }
        }
        private bool _CheckLeagleAge()
        {
            // Retrieve the specific License Class information to determine the permitted age.
            clsLicenseClass SelectedLicenseClass = clsLicenseClass.FindLicenseClassByName(cbLicenseClasses.Text);

            clsPerson SelectedPerson = clsPerson.Find(_PersonID);

            if (SelectedPerson != null && SelectedLicenseClass != null)
            {
                // Calculating the person's exact age
                int PersonAge = DateTime.Now.Year - SelectedPerson.DateOfBirth.Year;

                // Adjust the age if his birthday has not yet occurred in the current year
                if (SelectedPerson.DateOfBirth > DateTime.Now.AddYears(-PersonAge))
                {
                    PersonAge--;
                }

                // Compare the age with the age limit for the category
                if (PersonAge < SelectedLicenseClass.MinAllowedAge)
                {
                    MessageBox.Show($@"The selected person age is {PersonAge} years, which is less than the minimum allowed age ({SelectedLicenseClass.MinAllowedAge} years) for this license class.",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; 
                }
            }
            return true;
        }
        private void _FillApplicationInfoInRecord()
        {
            if (Mode == enMode.AddNew)
            {
                _LocalLicenseApp.ApplicantPersonID = _PersonID;
                _LocalLicenseApp.ApplicationDate = DateTime.Now;
                _LocalLicenseApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
                _LocalLicenseApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                _LocalLicenseApp.ApplicationTypeID = _LocalDrivingLicense.ID;
                _LocalLicenseApp.PaidFees = _LocalDrivingLicense.ApplicationFees;
            }

            // These fields are updated in both cases (add and edit)
            _LocalLicenseApp.LicenseClassID = clsLicenseClass.FindLicenseClassByName(cbLicenseClasses.Text).LicenseClassID;
            _LocalLicenseApp.LastStatusDate = DateTime.Now;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int SelectedPersonID = ctrlPersonCardWithFilter1.PersonID;
            int SelectedLicenseClassID = clsLicenseClass.FindLicenseClassByName(cbLicenseClasses.Text).LicenseClassID;

            int ActiveApplicationID = clsLocalDrivingLicenseApplication.GetActiveApplicationIDForLicenseClass(SelectedPersonID, SelectedLicenseClassID);

            // -1 means the Person hasn't active application
            if (ActiveApplicationID != -1 && ActiveApplicationID != _LocalAppID)
            {
                MessageBox.Show($@"Choose another License Class, The Selected Person Already Have and Active Application for Selected Class with ID {ActiveApplicationID}"
                                , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clsLicense.IsLicenseExist(SelectedPersonID, SelectedLicenseClassID))
            {
                MessageBox.Show("This person already owns this license", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!_CheckLeagleAge())
            {
                return;
            }
            
            _FillApplicationInfoInRecord();
            if (_LocalLicenseApp.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Mode = enMode.Update;
                _LocalAppID = _LocalLicenseApp.LocalDrivingLicenseApplicationID;

                lblApplicationID.Text = _LocalAppID.ToString();

                _UpdateFormStatus();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = ctrlPersonCardWithFilter1.PersonID;
        }
        private void frmAddUpdateLocalDrivingLicenseApplications_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
