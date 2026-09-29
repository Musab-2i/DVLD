using DVLD_Project.Global_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.License.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : frmBase
    {
        private int _InternationalLicense = -1;
        public frmShowInternationalLicenseInfo(int internationalLicense)
        {
            InitializeComponent();
            _InternationalLicense = internationalLicense;
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfo1.LoadLicenseInfo(_InternationalLicense);
        }
    }
}
