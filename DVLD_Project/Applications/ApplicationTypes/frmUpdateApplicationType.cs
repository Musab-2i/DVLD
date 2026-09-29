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

namespace DVLD_Project.Applications
{
    public partial class frmUpdateApplicationType : frmBase
    {
        private int _AppID;
        clsApplicationTypes _AppType;
        public frmUpdateApplicationType(int ApplicationID)
        {
            InitializeComponent();
            _AppID = ApplicationID;
        }

        private void _ShowAppInfo()
        {
            lblAppID.Text = _AppType.ID.ToString();
            txtAppTitle.Text = _AppType.ApplicationTypeTitle.ToString();
            txtAppFees.Text = clsFormat.CurrencyFormat(_AppType.ApplicationFees);
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _AppType = clsApplicationTypes.Find((clsApplicationTypes.enApplicationType)_AppID);
            if (_AppType == null)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ShowAppInfo();
        }

        private void txtAppFees_KeyPress(object sender, KeyPressEventArgs e)
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

        private void _FillAppTypeInfoInRecord()
        {
            _AppType.ApplicationTypeTitle = txtAppTitle.Text.Trim();
            _AppType.ApplicationFees = Convert.ToDecimal(txtAppFees.Text);
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

            _FillAppTypeInfoInRecord();
            if (_AppType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppTitle, "Title cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAppTitle, null);
            }
        }
        private void txtAppFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppFees, "Fees cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAppFees, null);
            }
        }

    }
}
