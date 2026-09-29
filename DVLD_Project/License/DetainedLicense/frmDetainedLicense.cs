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

namespace DVLD_Project.License
{
    public partial class frmDetainedLicense : frmBase
    {
        private int _LicenseID = -1;
        private int _DetainID = -1;
        private clsLicense LicenseInfo;
        public frmDetainedLicense()
        {
            InitializeComponent();
            ctrlLocalDriverLicenseWihtFilter1.OnLicenseSelected += OnLicenseSelected;
        }
        private void _LoadApplicationInfoData()
        {
            lbl_DetainID.Text = _DetainID == -1 ? "[????]" : _DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);

            lblLicenseID.Text = LicenseInfo == null ? "[????]" : LicenseInfo.LicenseID.ToString();
            lblApplicationCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }
        private void frmDetainedLicense_Load(object sender, EventArgs e)
        {
            _LoadApplicationInfoData();
        }
        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Please check the red icon(s).",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Detain this license?"
                , "Confirm"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _DetainID = LicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
                if (_DetainID == -1)
                {
                    MessageBox.Show("Failed to Detain License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"License Detained Successfully with Detain ID = {_DetainID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information); _LoadApplicationInfoData();
                    txtFineFees.Text = clsFormat.CurrencyFormat(Convert.ToDecimal(txtFineFees.Text));
                    txtFineFees.ReadOnly = true;

                    ctrlLocalDriverLicenseWihtFilter1.FilterEnable = false;
                    btnDetainLicense.Enabled = false;
                    lblShowLicenseInfo.Enabled = true;
                }
            }
        }

        private void DisableControls()
        {
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnDetainLicense.Enabled = false;
        }
        private void _ResetDefaultUI()
        {
            lbl_DetainID.Text = "[????]";
            lblDetainDate.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblApplicationCreatedBy.Text = "[????]";
            txtFineFees.ReadOnly = false;
            txtFineFees.Text = string.Empty;
        }

        private bool _CheckIfLicenseAlreadyDetain()
        {
            if (LicenseInfo.IsDetained == true)
            {
                MessageBox.Show("Selected License is already detained, please choose another one.",
                                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _DetainID = LicenseInfo.DetainedInfo.DetainID;
                txtFineFees.Text = clsFormat.CurrencyFormat(LicenseInfo.DetainedInfo.FineFees);
                txtFineFees.ReadOnly = true;
                return true;
            }
            return false;
        }

        private bool _CheckIfLicenseIsInActive()
        {
            if (LicenseInfo.IsActive == false)
            {
                MessageBox.Show("Selected License is not active, please choose an active license.",
                                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private void _UpdateUIBasedOnLicenseStatus()
        {
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = true;
            if (_CheckIfLicenseAlreadyDetain() || _CheckIfLicenseIsInActive())
            {
                btnDetainLicense.Enabled = false;
            }
            else
            {
                btnDetainLicense.Enabled = true;
                //lblShowLicenseInfo.Enabled = false;
                //lblShowLicensesHistory.Enabled = true;
            }
        }
        private void OnLicenseSelected(int LicenseID)
        {
            LicenseInfo = null;
            _ResetDefaultUI();

            if (LicenseID == -1)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }

            _LicenseID = LicenseID;
            LicenseInfo = clsLicense.Find(_LicenseID);

            if (LicenseInfo == null)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }
            _UpdateUIBasedOnLicenseStatus();

            _LoadApplicationInfoData();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
            }
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmDriverLicensesHistory(LicenseInfo.DriverInfo.PersonID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmShowLocalLicenseInfo(LicenseInfo.LicenseID);
            form.ShowDialog();
            _LoadApplicationInfoData();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }

            if (!clsValidations.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
        }

        private void frmDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrlLocalDriverLicenseWihtFilter1.FilterFocus();
        }
    }
}
