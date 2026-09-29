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

namespace DVLD_Project.Tests
{
    public partial class frmShowTestDetails : frmBase
    {
        private int _TestID;
        private clsTestTypes _TestType;

        public frmShowTestDetails(int TestID)
        {
            InitializeComponent();
            _TestID = TestID;
        }

        private void _LoadTestInfo()
        {
            lblTestID.Text = _TestType.ID.ToString();
            txtTestTitle.Text = _TestType.TestTypeTitle.ToString();
            txtTestDescription.Text = _TestType.TestTypeDescription.ToString();
            txtTestFees.Text = Convert.ToString(_TestType.TestFees);
        }

        private void frmShowTestDetails_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.Find((clsTestTypes.enTestType)_TestID);
            if (_TestType == null)
            {
                MessageBox.Show("Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadTestInfo();
        }
    }
}
