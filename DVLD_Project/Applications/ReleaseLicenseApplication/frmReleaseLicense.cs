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
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.ReleaseLicenseApplication
{
    public partial class frmReleaseLicense : frmBase
    {
        private int _ApplicationID = -1;
        private clsLicense LicenseInfo;
        private struct stFees
        {
            public decimal ApplicationFees;
            public decimal FineFees;

            public decimal TotalFees
            {
                get
                {
                    return (ApplicationFees + FineFees);
                }
            }
        }
        private stFees _AppFees;

        public frmReleaseLicense()
        {
            InitializeComponent();
            ctrlLocalDriverLicenseWihtFilter1.OnLicenseSelected += OnLicenseSelected;
        }
        public frmReleaseLicense(int LicenseID)
        {
            InitializeComponent();
            //_LicenseID = LicenseID;
            ctrlLocalDriverLicenseWihtFilter1.OnLicenseSelected += OnLicenseSelected;

            ctrlLocalDriverLicenseWihtFilter1.LoadLicenseInfo(LicenseID);
            ctrlLocalDriverLicenseWihtFilter1.FilterEnable = false;
            //LoadDataFromParamitrizeConstructor();
        }

        //private void LoadDataFromParamitrizeConstructor()
        //{
        //    if (_LicenseID == -1)
        //    {
        //        _ResetDefaultUI();
        //        return;
        //    }

        //    LicenseInfo = clsLicense.Find(_LicenseID);

        //    if (LicenseInfo == null)
        //    {
        //        _ResetDefaultUI();
        //        return;
        //    }
        //    ctrlLocalDriverLicenseWihtFilter1.LoadLicenseInfo(_LicenseID);
        //    ctrlLocalDriverLicenseWihtFilter1.FilterEnable = false;

        //    _UpdateUIBasedOnLicenseStatus();
        //    _LoadApplicationInfoData();
        //}

        private void _LoadFees()
        {
            _AppFees.ApplicationFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.ReleaseRetainedDrivingLicense).ApplicationFees;
            _AppFees.FineFees = LicenseInfo.DetainedInfo.FineFees;
        }
        private void _LoadApplicationInfoData()
        {

            lbl_DetainID.Text = LicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(LicenseInfo.DetainedInfo.DetainDate);
            lblLicenseID.Text = LicenseInfo.LicenseID.ToString();
            lbl_ApplicationID.Text = _ApplicationID == -1 ? "[????]" : _ApplicationID.ToString();
            lblApplicationFees.Text = clsFormat.CurrencyFormat(_AppFees.ApplicationFees);
            lblFineFees.Text = clsFormat.CurrencyFormat(_AppFees.FineFees);
            lblTotalFees.Text = clsFormat.CurrencyFormat(_AppFees.TotalFees);

            lblDetainCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }
        private bool _CheckIfPersonHaveDetainLicense()
        {
            if (!LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _LoadFees();
            return true;
        }
        private void DisableControls()
        {
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnReleaseLicense.Enabled = false;
        }
        private void _ResetDefaultUI()
        {


            lbl_DetainID.Text = "[????]";
            lblDetainDate.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lbl_ApplicationID.Text = "[????]";
            lblApplicationFees.Text = "[????]";
            lblFineFees.Text = "[????]";
            lblTotalFees.Text = "[????]";

        }
        private void _UpdateUIBasedOnLicenseStatus()
        {
            lblShowLicensesHistory.Enabled = true;
            lblShowLicenseInfo.Enabled = false;

            btnReleaseLicense.Enabled = _CheckIfPersonHaveDetainLicense();

        }
        private void OnLicenseSelected(int LicenseID)
        {
            //LicenseInfo = null;
            if (LicenseID == -1)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }
             
            LicenseInfo = clsLicense.Find(LicenseID);

            if (LicenseInfo == null)
            {
                DisableControls();
                _ResetDefaultUI();
                return;
            }
            _UpdateUIBasedOnLicenseStatus();
            if (!LicenseInfo.IsDetained)
            {
                _ResetDefaultUI();
                return;
            }
            _LoadApplicationInfoData();

        }
        private void frmReleaseLicense_Activated(object sender, EventArgs e)
        {
            ctrlLocalDriverLicenseWihtFilter1.FilterFocus();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                bool IsRelease = LicenseInfo.ReleaseDetainLicense(clsGlobal.CurrentUser.UserID, ref _ApplicationID);

                if (IsRelease)
                {
                    MessageBox.Show($"Detained License released successfully with Application ID: {_ApplicationID}",
                                            "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _LoadApplicationInfoData();
                    ctrlLocalDriverLicenseWihtFilter1.FilterEnable = false;
                    btnReleaseLicense.Enabled = false;
                    lblShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Failed to release the Detained License.", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
    }
}
