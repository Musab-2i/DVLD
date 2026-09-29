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

namespace DVLD_Project.Users
{
    public partial class frmAddUpdateUser : frmBase
    {
        enum enMode { AddNewUser = 1, UpdateUser = 2 }
        private enMode Mode;
        private int _UserID = -1;
        private clsUsers _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            Mode = enMode.AddNewUser;
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            Mode = enMode.UpdateUser;
            this._UserID = UserID;
        }

        private void _EnablePeopleFilter()
        {
            if (Mode == enMode.AddNewUser)
                ctrlPersonCardWithFilter1.FilterEnable = true;
            else
                ctrlPersonCardWithFilter1.FilterEnable = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
        }

        private void _FillUserInfoInControls()
        {
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            // Because Get HashValue 
            //txtPassword.Text = _User.Password;
            //txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
        }

        private void _ResetDefaultValue()
        {
            if (Mode == enMode.AddNewUser)
            {
                this.Text = "Add New User";
                lblMode.Text = "Add New User";
                _User = new clsUsers();
                return;
            }
        }

        private void _LoadData()
        {
            _User = clsUsers.FindByUserID(this._UserID);

            if (_User == null)
            {
                MessageBox.Show($"No User With ID: {this._UserID}", "User No Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            this.Text = "Edit User";
            lblMode.Text = $"Edit User With ID: {this._UserID}";

            _FillUserInfoInControls();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
            _ResetDefaultValue();
            _EnablePeopleFilter();
            if (Mode == enMode.UpdateUser)
            {
                _LoadData();
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage2 && Mode == enMode.AddNewUser)
            {
                if (ctrlPersonCardWithFilter1.PersonID == -1)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select a person first before moving to login information.",
                                    "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (clsUsers.IsUserExistByPersonID(_User.PersonID))
                {
                    e.Cancel = true;
                    MessageBox.Show("Selected person is alredy has user, Choose another one!",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void _FillUserInfoInRecord()
        {
            _User.UserName = txtUserName.Text.Trim();
            //_User.PersonID = this._PersonID;
            _User.Password = ((Mode == enMode.UpdateUser && txtPassword.Text == "")? _User.Password : txtPassword.Text.Trim());
            _User.IsActive = chkIsActive.Checked;
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

            _FillUserInfoInRecord();
            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Text = "Edit User";
                Mode = enMode.UpdateUser;
                lblMode.Text = $"Edit User With ID: {_User.UserID}";
                lblUserID.Text = _User.UserID.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _TextBoxesChar(char Char)
        {
            txtPassword.PasswordChar = Char;
            txtConfirmPassword.PasswordChar = Char;
        }

        private void btnShowHidePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
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

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (Mode == enMode.UpdateUser && txtPassword.Text == "") return;

            if (_ValidateEmptyTextBox(txtConfirmPassword, e)) return;

            else if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "The password does not match!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (_ValidateEmptyTextBox(txtUserName, e)) return;

            if (txtUserName.Text.Trim() != _User.UserName)
            {
                if (clsUsers.IsUserExistByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "Username is already used for another person!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtUserName, "");
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (Mode == enMode.UpdateUser) return;
            if (_ValidateEmptyTextBox(txtPassword, e)) return;

            e.Cancel = false;
            errorProvider1.SetError(txtPassword, null);
        }
    }
}
