using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
using DVLD_Project.License;
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

namespace DVLD_Project.Applications.RenewDrivingLicenseApplication
{
    public partial class frmRenewDrivingLicense : frmBase
    {
        private clsLicense _ExistLicenseInfo;
        private clsLicense _NewLicenseInfo;
        private int _LicenseID = -1;
        private struct stFees
        {
            public decimal RenewFees;
            public decimal LicenseFees;

            public decimal TotalFees 
            {
                get
                {
                    return (RenewFees + LicenseFees);
                }
            }
        }

        private stFees _AppFees;
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseWihtFilter1.OnLicenseSelected += OnLicenseSelected;
            _LoadFees();
        }

        private void _LoadFees()
        {
            _AppFees.RenewFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.RenewDrivingLicense).ApplicationFees;
            _AppFees.LicenseFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.NewDrivingLicense).ApplicationFees;
        }
        private void _LoadApplicationInfoData()
        {
            lbl_R_L_ApplicationID.Text = _NewLicenseInfo == null ? "[????]" : _NewLicenseInfo.ApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);

            lblOldLicenseID.Text = _ExistLicenseInfo == null ? "[????]" : _ExistLicenseInfo.LicenseID.ToString();
            lblNewLicenseID.Text = _NewLicenseInfo == null ? "[????]" : _NewLicenseInfo.LicenseID.ToString();

            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = _ExistLicenseInfo == null ? "[????]" : clsFormat.DateToShort(DateTime.Now.AddYears(_ExistLicenseInfo.LicenseClassInfo.ValidityLengthYear));
            lblApplicationCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblApplicationFees.Text = clsFormat.CurrencyFormat(_AppFees.RenewFees);
            lblLicenseFees.Text = clsFormat.CurrencyFormat(_AppFees.LicenseFees);
            lblTotalFees.Text = clsFormat.CurrencyFormat(_AppFees.TotalFees);

            if (_ExistLicenseInfo != null)
                txtNote.Text = string.IsNullOrEmpty(_ExistLicenseInfo.Note) ? "" : _ExistLicenseInfo.Note;
        }
        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            _LoadApplicationInfoData();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?"
                , "Confirm"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question) == DialogResult.Yes)
            {

                _NewLicenseInfo = _ExistLicenseInfo.RenewLicense(txtNote.Text.Trim(), clsGlobal.CurrentUser.UserID);

                if (_NewLicenseInfo != null)
                {
                    MessageBox.Show($"New License Issued Successfully with ID .:[ {_NewLicenseInfo.LicenseID} ]:."
                        , "License Issued"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);

                    _LoadApplicationInfoData();

                    ctrlDriverLicenseWihtFilter1.FilterEnable = false;
                    btnRenewLicense.Enabled = false;
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

        private void frmRenewDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWihtFilter1.FilterFocus();
        }

        private bool _IsLicenseNotExpired()
        {
            if (!_ExistLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"Selected License is not yet expired, It will Expire on .:[{clsFormat.DateToShort(_ExistLicenseInfo.ExpirationDate)}]:."
                                , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }
            return false;
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
            btnRenewLicense.Enabled = false;
        }
        private void _ResetDefaultUI()
        {
            lbl_R_L_ApplicationID.Text = "[????]";
            lblApplicationDate.Text = "[????]";
            lblOldLicenseID.Text = "[????]";
            lblNewLicenseID.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblApplicationCreatedBy.Text = "[????]";
            lblApplicationFees.Text = "[????]";
            lblLicenseFees.Text = "[????]";
            lblTotalFees.Text = "[????]";
        }

        private void _UpdateUIBasedOnLicenseStatus()
        {
            lblShowLicensesHistory.Enabled = true;
            lblShowLicenseInfo.Enabled = false;

            if (_IsLicenseNotExpired() || !_CheckIfLocalLicenseIsActive())
            {
                btnRenewLicense.Enabled = false;
            }
            else
            {
                btnRenewLicense.Enabled = true;
                //lblShowLicenseInfo.Enabled = false;
                //lblShowLicensesHistory.Enabled = true;
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

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmShowLocalLicenseInfo(_NewLicenseInfo.LicenseID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmDriverLicensesHistory(_ExistLicenseInfo.DriverInfo.PersonID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }


    }
}