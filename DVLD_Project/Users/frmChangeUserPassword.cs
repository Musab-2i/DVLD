using BusinessLayer;
using DVLD_Project.Global_Forms;
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
using static DVLD_Project.People.frmAddUpdatePerson;

namespace DVLD_Project.Users
{
    public partial class frmChangeUserPassword : frmBase
    {
        private int _UserID;
        private clsUsers _User;

        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtCurrentPassword.Focus();
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUsers.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show($"No User With ID: {this._UserID}", "User No Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void _TextBoxesChar(char Char)
        {
            txtCurrentPassword.PasswordChar = Char;
            txtNewPassword.PasswordChar = Char;
            txtConfirmPassword.PasswordChar = Char;
        }

        private void btnShowHidePassword_Click(object sender, EventArgs e)
        {
            if (txtCurrentPassword.PasswordChar == '*')
            {
                btnShowHidePassword.Image = Resources.ButtonHidePassword;
                _TextBoxesChar('\0');
            }
            else
            {
                btnShowHidePassword.Image = Resources.ButtonShowPassword;
                _TextBoxesChar('*');
            }
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

            if (_User.UpdatePassword(txtNewPassword.Text.Trim())) 
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool _ValidateEmptyTextBox(TextBox txt, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "This field is required!");
                return true; // أي أنه فارغ وفيه خطأ
            }
            return false; // ليس فارغاً
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_ValidateEmptyTextBox(txtCurrentPassword, e)) return;
            
            if (clsBusinessUtil.ComputeHash(txtCurrentPassword.Text.Trim()) != _User.Password.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_ValidateEmptyTextBox(txtNewPassword, e)) return;

            e.Cancel = false;
            errorProvider1.SetError(txtNewPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_ValidateEmptyTextBox(txtConfirmPassword, e)) return;

            if (txtConfirmPassword.Text != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "The password does not match!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}
