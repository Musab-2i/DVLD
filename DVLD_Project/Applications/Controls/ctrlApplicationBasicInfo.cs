using BusinessLayer;
using DVLD_Project.GlobalClass;
using DVLD_Project.People;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        
        public void ResetApplicationInfo()
        {
            _ResetApplicationInfo();
        }

        private void _ResetApplicationInfo()
        {
            lblApplicationID.Text = "[????]";
            lblApplicationStatus.Text = "[????]";
            lblApplicationFees.Text = "[????]";
            lblApplicationType.Text = "[????]";
            lblApplicationPersonName.Text = "[????]";
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationStatusDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationCreatedBy.Text = "[????]";
        }

        private void _FillApplicationInfo()
        {
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblApplicationStatus.Text = _Application.ApplicationStatus.ToString();
            lblApplicationFees.Text = clsFormat.CurrencyFormat(_Application.PaidFees);

            lblApplicationType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicationPersonName.Text = _Application.PersonInfo.FullName;
            lblApplicationDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);

            lblApplicationStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblApplicationCreatedBy.Text = _Application.UserInfo.UserName;
            
        }

        public void LoadApplicationInfo(int AppID)
        {
            _Application = clsApplication.FindBaseApplication(AppID);
            if (_Application == null)
            {
                _ResetApplicationInfo();
                MessageBox.Show("No Application with ID : " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillApplicationInfo();
        }

        private void lblShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmShowPersonInfo(_Application.ApplicantPersonID);
            form.ShowDialog();
            _FillApplicationInfo();
        }
    }
}
