using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
using DVLD_Project.License;
using DVLD_Project.License.Controls;
using DVLD_Project.License.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.InternationalLicense
{
    public partial class frmIssueInternationalLicense : frmBase
    {

        private clsLicense _LocalLicense;
        private clsInternationalLicenses _InternationalLicenses;
        private int _LocalLicenseID;
        private decimal IssueInternationalLicenseFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.NewInternationalLicense).ApplicationFees;
        public frmIssueInternationalLicense()
        {
            InitializeComponent();

            ctrlDriverLicenseWihtFilter1.OnLicenseSelected += OnLicenseSelected;
        }

        private void _LoadApplicationInfoData()
        {
            lbl_I_L_ApplicationID.Text = _InternationalLicenses == null ? "[????]" : _InternationalLicenses.ApplicationID.ToString();
            lbl_I_L_LicenseID.Text = _InternationalLicenses == null ? "[????]" : _InternationalLicenses.InternationalLicenseID.ToString();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblLocalLicenseID.Text = _LocalLicense == null ? "[????]" : _LocalLicense.LicenseID.ToString();

            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));

            lblApplicationCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationFees.Text = clsFormat.CurrencyFormat(IssueInternationalLicenseFees);
        }
        private void frmIssueInternationalLicense_Load(object sender, EventArgs e)
        {
            _LoadApplicationInfoData();
        }

        private bool _CheckIfPersonHaveInternationLicense()
        {
            int InternationalLicenseID = clsInternationalLicenses.GetActiveInternationalLicenseIDByDriverID(_LocalLicense.DriverID);

            if (InternationalLicenseID != -1)
            {
                MessageBox.Show($"Person Already have an active International License \nWith ID .:[ {InternationalLicenseID} ]:."
                  , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _InternationalLicenses = clsInternationalLicenses.Find(InternationalLicenseID);

                return false;
            }
            return true;
        }

        private bool _CheckLicenseClass()
        {
            return (_LocalLicense.LicenseClassID == 3);
        }
        private bool _CheckIfLocalLicenseIsActive()
        {
            return (_LocalLicense.IsActive == true);
        }
        private bool _CheckLicenseExpirationDate()
        {
            return (_LocalLicense.ExpirationDate > DateTime.Now);
        }
        private bool _CheckAllConstraints()
        {
            if (!_CheckLicenseClass())
            {
                MessageBox.Show("The selected license must be of Class 3 (Ordinary driving license) to issue an International License.",
                          "Invalid License Class",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                return false;
            }
            if (!_CheckIfLocalLicenseIsActive())
            {
                MessageBox.Show("The selected local license is currently inactive. Please choose an active license to proceed.",
                          "Inactive License",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                return false;
            }

            if (!_CheckLicenseExpirationDate())
            {
                MessageBox.Show("The selected local license has expired. Please renew the local license before applying for an international one.",
                          "License Expired",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void _FillInfoInInternationalLicenseRecord()
        {
            _InternationalLicenses = new clsInternationalLicenses();

            // Create New Application
            _InternationalLicenses.ApplicantPersonID = _LocalLicense.DriverInfo.PersonID;
            _InternationalLicenses.ApplicationDate = DateTime.Now;
            _InternationalLicenses.ApplicationTypeID = clsApplicationTypes.enApplicationType.NewInternationalLicense;
            _InternationalLicenses.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _InternationalLicenses.LastStatusDate = DateTime.Now;
            _InternationalLicenses.PaidFees = IssueInternationalLicenseFees;


            _InternationalLicenses.DriverID = _LocalLicense.DriverID;
            _InternationalLicenses.IssuedUsingLocalLicenseID = _LocalLicense.LicenseID;
            _InternationalLicenses.IssueDate = DateTime.Now;
            _InternationalLicenses.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicenses.IsActive = true;

            _InternationalLicenses.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?"
              , "Confirm"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question) == DialogResult.Yes)
            {

                _FillInfoInInternationalLicenseRecord();

                if (_InternationalLicenses.Save())
                {
                    MessageBox.Show($"International License Issued Successfully with ID .:[ {_InternationalLicenses.InternationalLicenseID} ]:."
                      , "License Issued"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Information);

                    _LoadApplicationInfoData();

                    ctrlDriverLicenseWihtFilter1.FilterEnable = false;
                    btnIssueLicense.Enabled = false;
                    lblShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("International License Issuance Failed!"
                      , "Error"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Error);

                }
            }
        }
        private void frmIssueInternationalLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWihtFilter1.FilterFocus();
        }


        private void DisableControls()
        {
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnIssueLicense.Enabled = false;
        }
        private void _ResetDefaultUI()
        {
            lbl_I_L_ApplicationID.Text = "[????]";
            lbl_I_L_LicenseID.Text = "[????]";
            lblApplicationDate.Text = "[????]";
            lblLocalLicenseID.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblApplicationCreatedBy.Text = "[????]";
            lblApplicationFees.Text = "[????]";

        }
        private void _UpdateUIBasedOnLicenseStatus()
        {
            lblShowLicensesHistory.Enabled = true;

            if (!_CheckAllConstraints())
            {
                btnIssueLicense.Enabled = false;
                lblShowLicenseInfo.Enabled = false;
            }
            else if (!_CheckIfPersonHaveInternationLicense())
            {
                btnIssueLicense.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
            }
            else
            {
                btnIssueLicense.Enabled = true;
                lblShowLicenseInfo.Enabled = false;
            }
        }

        private void OnLicenseSelected(int LicenseID)
        {
            _InternationalLicenses = null;
            if (LicenseID == -1)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }

            _LocalLicenseID = LicenseID;
            _LocalLicense = clsLicense.Find(_LocalLicenseID);

            if (_LocalLicense == null)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }

            _UpdateUIBasedOnLicenseStatus();

            _LoadApplicationInfoData();
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmDriverLicensesHistory(_LocalLicense.DriverInfo.PersonID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmShowInternationalLicenseInfo(_InternationalLicenses.InternationalLicenseID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

    }
}