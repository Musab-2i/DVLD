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

namespace DVLD_Project.License.International_Licenses.Controls
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private clsInternationalLicenses InternationalLicense;
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadDriverImage()
        {
            if (InternationalLicense.PersonInfo.Gender == 0)
                pbDriverImage.Image = Resources.Male_512;
            else
                pbDriverImage.Image = Resources.Female_512;

            string ImagePath = InternationalLicense.PersonInfo.ImagePath;
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
            lblPersonName.Text = InternationalLicense.PersonInfo.FullName;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = InternationalLicense.LicenseID.ToString();
            
            lblNationalNo.Text = InternationalLicense.PersonInfo.NationalNo;
            lblGender.Text = InternationalLicense.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(InternationalLicense.IssueDate);

            lblIsActive.Text = InternationalLicense.IsActive == true ? "Yes" : "No";

            lblDateOfBirth.Text = clsFormat.DateToShort(InternationalLicense.PersonInfo.DateOfBirth);
            lblExpirationDate.Text = clsFormat.DateToShort(InternationalLicense.ExpirationDate);
            lblDriverID.Text = InternationalLicense.DriverID.ToString();

            _LoadDriverImage();
        }

        public void LoadLicenseInfo(int InternationalLicenseID)
        {
            InternationalLicense = clsInternationalLicenses.Find(InternationalLicenseID);
            if (InternationalLicense == null)
            {
                MessageBox.Show("An error occurred; license information could not be displayed."
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InternationalLicenseID = -1;
                return;
            }
            _FillLicenseInfoInControls();
        }

    }
}
