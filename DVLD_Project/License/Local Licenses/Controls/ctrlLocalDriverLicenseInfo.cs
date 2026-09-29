using BusinessLayer;
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
using System.IO;
using DVLD_Project.GlobalClass;

namespace DVLD_Project.License.Controls
{
    public partial class ctrlLocalDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        private clsLicense License;
        public ctrlLocalDriverLicenseInfo()
        {
            InitializeComponent();
        }
        public int LicenseID { get { return _LicenseID; } }
        public clsLicense SelectedLicenseInfo { get { return License; } }
        private void _LoadDriverImage()
        {
            if (License.DriverInfo.PersonInfo.Gender == 0)
                pbDriverImage.Image = Resources.Male_512;
            else
                pbDriverImage.Image = Resources.Female_512;

            string ImagePath = License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbDriverImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _FillLicenseInfoInControls()
        {
            lblClassName.Text = License.LicenseClassInfo.ClassName;
            lblPersonName.Text = License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = License.LicenseID.ToString();

            lblNationalNo.Text = License.DriverInfo.PersonInfo.NationalNo.ToString();
            lblGender.Text = License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(License.IssueDate);

            lblIssueReason.Text = License.IssueReasonText;
            lblNotes.Text = License.Note == "" ? "No Notes" : License.Note;
            lblIsActive.Text = License.IsActive == true ? "Yes" : "No";

            lblDateOfBirth.Text = clsFormat.DateToShort(License.DriverInfo.PersonInfo.DateOfBirth);
            lblExpirationDate.Text = clsFormat.DateToShort(License.ExpirationDate);
            lblDriverID.Text = License.DriverID.ToString();
            lblIsDetained.Text = License.IsDetained == true ? "Yes" : "No";

            _LoadDriverImage();
        }

        private void _ResetDefaultValues()
        {
            lblClassName.Text = "[????]";
            lblPersonName.Text = "[????]";
            lblLicenseID.Text = "[????]";

            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[????]";

            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";

            lblDateOfBirth.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblIsDetained.Text = "[????]";

            pbDriverImage.Image = Resources.Male_512;
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            License = clsLicense.Find(_LicenseID);
            if (License == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("An error occurred; license information could not be displayed."
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            _FillLicenseInfoInControls();
        }

    }
}
