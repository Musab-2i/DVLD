using BusinessLayer;
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

namespace DVLD_Project.Applications.ApplicationTypes
{
    public partial class frmShowApplicationDetails : frmBase
    {
        private int _AppID;
        private clsApplicationTypes _AppType;
        public frmShowApplicationDetails(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;
        }

        private void _LoadAppInfo()
        {
            lblAppID.Text = ((int)_AppType.ID).ToString();
            txtAppTitle.Text = _AppType.ApplicationTypeTitle.ToString();
            txtAppFees.Text = Convert.ToString(_AppType.ApplicationFees);
        }

        private void frmShowApplicationDetails_Load(object sender, EventArgs e)
        {
            _AppType = clsApplicationTypes.Find((clsApplicationTypes.enApplicationType)_AppID);
            if (_AppType == null)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadAppInfo();
        }
    }
}
