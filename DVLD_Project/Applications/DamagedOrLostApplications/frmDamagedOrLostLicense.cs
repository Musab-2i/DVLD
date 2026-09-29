using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
using DVLD_Project.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.DamagedOrLostApplications
{
    public partial class frmDamagedOrLostLicense : frmBase
    {
        private clsLicense _ExistLicenseInfo;
        private clsLicense _NewLicenseInfo;
        private clsLicense.enIssueReason IssueReason;
        private int _LicenseID = -1;
        private clsApplicationTypes ApplicationType;
        decimal ApplicationFees;
        private enum enMode { DamagedLicense = 1, LostLicense = 2 }
        private enMode Mode;
        private struct stApplicationModeFees
        {
            public decimal DamagedFees;
            public decimal LostFees;
        }
        private stApplicationModeFees _AppFees;

        public frmDamagedOrLostLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseWihtFilter1.OnLicenseSelected += OnLicenseSelected;
            _LoadFees();
        }
        private void _LoadFees()
        {
            _AppFees.DamagedFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees;
            _AppFees.LostFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees;
        }

        private void _LoadApplicationInfoData()
        {
            lbl_R_L_ApplicationID.Text = _NewLicenseInfo == null ? "[????]" : _NewLicenseInfo.ApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);

            lblOldLicenseID.Text = _ExistLicenseInfo == null ? "[????]" : _ExistLicenseInfo.LicenseID.ToString();
            lblNewLicenseID.Text = _NewLicenseInfo == null ? "[????]" : _NewLicenseInfo.LicenseID.ToString();
            lblApplicationCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblApplicationFees.Text = clsFormat.CurrencyFormat(ApplicationFees);
        }
        private void frmDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            _UpdateFormSettings();
            _LoadApplicationInfoData();
        }

        private bool _CheckIfLocalLicenseIsActive()
        {
            if (!_ExistLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not active, it might be already renewed or replaced.\nPlease choose an active license."
                                        , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void DisableControls()
        {
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnIssueLicense.Enabled = false;
        }
        private void _ResetDefaultUI()
        {
            lbl_R_L_ApplicationID.Text = "[????]";
            lblApplicationDate.Text = "[????]";
            lblOldLicenseID.Text = "[????]";
            lblNewLicenseID.Text = "[????]";
            lblApplicationCreatedBy.Text = "[????]";
            lblApplicationFees.Text = "[????]";
        }
        private void _UpdateUIBasedOnLicenseStatus()
        {
            lblShowLicensesHistory.Enabled = true;

            if (!_CheckIfLocalLicenseIsActive())
            {
                btnIssueLicense.Enabled = false;
                lblShowLicenseInfo.Enabled = false;
                lblShowLicensesHistory.Enabled = true;
            }
            else
            {
                btnIssueLicense.Enabled = true;
                lblShowLicenseInfo.Enabled = false;
                lblShowLicensesHistory.Enabled = true;
            }
        }

        private void OnLicenseSelected(int LicenseID)
        {
            _ExistLicenseInfo = null;
            _NewLicenseInfo = null;

            if (LicenseID == -1)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }

            _LicenseID = LicenseID;
            _ExistLicenseInfo = clsLicense.Find(_LicenseID);

            if (_ExistLicenseInfo == null)
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
            Form form = new frmDriverLicensesHistory(_ExistLicenseInfo.DriverInfo.PersonID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmShowLocalLicenseInfo(_NewLicenseInfo.LicenseID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?"
                , "Confirm"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question) == DialogResult.Yes)
            {

                _NewLicenseInfo = _ExistLicenseInfo.Replace(IssueReason,clsGlobal.CurrentUser.UserID);

                if (_NewLicenseInfo != null)
                {
                    MessageBox.Show($"New License Issued Successfully with ID .:[ {_NewLicenseInfo.LicenseID} ]:."
                        , "License Issued"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);

                    _LoadApplicationInfoData();

                    ctrlDriverLicenseWihtFilter1.FilterEnable = false;
                    gbReplaceMode.Enabled = false;
                    btnIssueLicense.Enabled = false;
                    lblShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("New License Issuance Failed!"
                        , "Error"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Error);

                }
            }
        }

        private void _UpdateFormSettings()
        {
            string FormTitle = "Replacement For ";
            if (rbDamagedLicense.Checked)
            {
                Mode = enMode.DamagedLicense;
                IssueReason = clsLicense.enIssueReason.DamagedReplacement;
                ApplicationFees = _AppFees.DamagedFees;
                this.Text = FormTitle + "Damaged License";
            }
            else
            {
                Mode = enMode.LostLicense;
                IssueReason = clsLicense.enIssueReason.LostReplacement;
                ApplicationFees = _AppFees.LostFees;
                this.Text = FormTitle + "Lost License";
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateFormSettings();
            _LoadApplicationInfoData();
        }
    }
}
