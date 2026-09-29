using BusinessLayer;
using DVLD_Project.Global_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.LocalDrivingLicenseApplications
{
    public partial class frmShowLocalDrivingLicenseApplicationInfo : frmBase
    {
        private int _LocalAppID;
        private clsLocalDrivingLicenseApplication _LocalDrivingApp;
        public frmShowLocalDrivingLicenseApplicationInfo(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            _LocalDrivingApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalAppID);
        }

        private void frmShowLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            if (_LocalDrivingApp == null)
            {
                MessageBox.Show("No application was found with the selected ID. The record might be corrupted or missing.",
                                "Data Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalDrivingApp.LocalDrivingLicenseApplicationID);
        }
    }
}
