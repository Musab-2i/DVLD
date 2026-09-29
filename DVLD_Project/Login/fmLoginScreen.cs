using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.GlobalClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class fmLoginScreen : frmBase
    {
        public fmLoginScreen()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(txt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "This field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, null);
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = clsUsers.FindByUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            if (clsGlobal.CurrentUser == null)
            {
                MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (clsGlobal.CurrentUser.IsActive == false)
            {
                MessageBox.Show("Your account is Inactive, Contact your Admin.", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    // Sending empty text means we are asking the function to delete the file
                    clsGlobal.RememberUsernameAndPassword("", "");
                }
                // 1. We signal to the system that the operation was successful
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void fmLoginScreen_Load(object sender, EventArgs e)
        {
            string Username = "";
            string Password = "";

            if (clsGlobal.GetStoredCredential(ref Username, ref Password))
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
        }
    }
}