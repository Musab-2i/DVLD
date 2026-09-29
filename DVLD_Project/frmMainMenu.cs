using DVLD_Project.Applications;
using DVLD_Project.Applications.DamagedOrLostApplications;
using DVLD_Project.Applications.InternationalDrivringLiceinseApplications;
using DVLD_Project.Applications.LocalDrivingLicenseApplications;
using DVLD_Project.Applications.ReleaseLicenseApplication;
using DVLD_Project.Applications.RenewDrivingLicenseApplication;
using DVLD_Project.Drivers;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
using DVLD_Project.InternationalLicense;
using DVLD_Project.License;
using DVLD_Project.License.DetainedLicense;
using DVLD_Project.Tests;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmMainMenu : Form
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        void EnableDarkModeTitleBar()
        {
            int darkMode = 1;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));
        }
        public frmMainMenu()
        {
            InitializeComponent();
            EnableDarkModeTitleBar();

        }

        private void OpenMdiChildForm<T>(Func<T> formCreator) where T : Form
        {
            // 1. البحث في الشاشات المفتوحة حالياً عن شاشة من نفس النوع T
            Form formInstance = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (formInstance == null || formInstance.IsDisposed)
            {
                formInstance = formCreator();
                formInstance.MdiParent = this;
                formInstance.Show();
            }
            else
            {
                formInstance.Activate();
                if (formInstance.WindowState == FormWindowState.Minimized)
                    formInstance.WindowState = FormWindowState.Normal;
            }
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManagePeople());
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageUsers());
        }

        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmShowUserInfo(clsGlobal.CurrentUser.UserID));
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmChangeUserPassword(clsGlobal.CurrentUser.UserID));
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageApplicationTypes());
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageTestTypes());
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmAddUpdateLocalDrivingLicenseApplications());
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageLocalLicenseApplications());
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            Application.Restart();
        }

        private void driverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmDrivers());
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmIssueInternationalLicense());
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageInternationalLicenseApplications());
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmRenewDrivingLicense());
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmDamagedOrLostLicense());
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmDetainedLicense());
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmReleaseLicense());
        }

        private void manageDetianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageDetainLicenses());
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmManageLocalLicenseApplications());
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmReleaseLicense());
        }
    }
}
