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
    public partial class frmUpdateTestType : frmBase
    {
        private int _TestID;
        private clsTestTypes _TestType;

        public frmUpdateTestType(int TestID)
        {
            InitializeComponent();
            _TestID = TestID;
        }
        
        private void _ShowTestInfo()
        {
            lblTestID.Text = _TestType.ID.ToString();
            txtTestTitle.Text = _TestType.TestTypeTitle.ToString();
            txtTestDescription.Text = _TestType.TestTypeDescription.ToString();
            txtTestFees.Text = Convert.ToString(_TestType.TestFees);
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.Find((clsTestTypes.enTestType)_TestID);
            if (_TestType != null)
            {
                _ShowTestInfo();
            }
        }

        private void _FillTestTypeInfoInRecord()
        {
            _TestType.TestTypeTitle = txtTestTitle.Text.Trim();
            _TestType.TestTypeDescription = txtTestDescription.Text.Trim();
            _TestType.TestFees = Convert.ToDecimal(txtTestFees.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are invalid! Please fix the errors before saving.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            _FillTestTypeInfoInRecord();
            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTestFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTestTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestDescription, "Title cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestDescription, null);
            }
        }

        private void txtTestDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestDescription, "Description cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestDescription, null);
            }
        }

        private void txtTestFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Fees cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestFees, null);
            }
        }

    }
}
