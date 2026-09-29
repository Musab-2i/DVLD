using BusinessLayer;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.Applications.LocalDrivingLicenseApplications.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalApplication;
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _VisibleLicenseInfoLabel()
        {
            int _LicenseID = _LocalApplication.GetActiveLicenseID();
            lblShowLicenseInfo.Visible = (_LicenseID != -1);
        }
        private void _ResetLocalApplicationInfo()
        {
            ctrlApplicationBasicInfo1.ResetApplicationInfo();

            lblLocalApplicationID.Text = "[????]";
            lblPassedTest.Text = "[????]";
            lblLocalLicenseClassName.Text = "[????]";
        }
        private void _FillLocalApplicationInfo()
        {
            _VisibleLicenseInfoLabel();
            lblLocalApplicationID.Text = _LocalApplication.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTest.Text = (_LocalApplication.GetTotalPassedTest() + " / 3").ToString();
            lblLocalLicenseClassName.Text = _LocalApplication.LicenseClassInfo.ClassName;
        }

        public void LoadApplicationInfo(int LocalAppID)
        {
            _LocalApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalAppID);
            if (_LocalApplication == null)
            {
                _ResetLocalApplicationInfo();
                MessageBox.Show("No Local Application with ID : " + LocalAppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalApplication.ApplicationID);
            _FillLocalApplicationInfo();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (_LocalApplication != null)
            {
                int LicenseID = _LocalApplication.GetActiveLicenseID();

                if (LicenseID != -1)
                {
                    Form frm = new frmShowLocalLicenseInfo(LicenseID);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No active license found for this application.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
