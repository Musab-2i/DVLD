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
    public partial class frmIssueDrivingLicenseFirstTime : frmBase
    {
        private int _LocalAppID;
        private clsLocalDrivingLicenseApplication _LocalDrivingApp;

        public frmIssueDrivingLicenseFirstTime(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
        }

        private void frmIssueDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNote.Focus();

            _LocalDrivingApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalAppID);
            if (_LocalDrivingApp == null)
            {
                MessageBox.Show($"No Applicaiton with ID {_LocalAppID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingApp.GetActiveLicenseID();
            if (LicenseID != -1)
            {
                MessageBox.Show($"Person already has License before with License ID {LicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalAppID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingApp.IssueLicenseForTheFirtTime(txtNote.Text.Trim(), clsGlobal.CurrentUser.UserID);
            
            if (LicenseID != -1)
            {
                MessageBox.Show($"License Issued Successfully with License ID {LicenseID}"
                                , "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
