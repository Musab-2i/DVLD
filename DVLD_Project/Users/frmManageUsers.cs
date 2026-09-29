using BusinessLayer;
using DVLD_Project.Global_Forms;
using DVLD_Project.People;
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
    public partial class frmManageUsers : frmBase
    {
        private DataTable _dtAllUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _CalculatTotalUsers()
        {
            lblDataRecords.Text = dgvAllUsers.Rows.Count.ToString();
        }
        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUsers.GetUsersList();
            _dtAllUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID"
                                                               , "FullName", "UserName"
                                                               , "IsActive");
            dgvAllUsers.DataSource = _dtAllUsers;
            _CalculatTotalUsers();
        }
        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            //dgvAllUsers.DataSource = _dtDrivers;
            cbFilterBy.SelectedIndex = 0;

            if (dgvAllUsers.Rows.Count > 0)
            {
                dgvAllUsers.Columns[0].HeaderText = "User ID";
                dgvAllUsers.Columns[0].Width = 80;

                dgvAllUsers.Columns[1].HeaderText = "Person ID";
                dgvAllUsers.Columns[1].Width = 80;

                dgvAllUsers.Columns[2].HeaderText = "Full Name";
                dgvAllUsers.Columns[2].Width = 390;

                dgvAllUsers.Columns[3].HeaderText = "UserName";
                dgvAllUsers.Columns[3].Width = 100;

                dgvAllUsers.Columns[4].HeaderText = "Is Active";
                dgvAllUsers.Columns[4].Width = 80;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActiveFilter.Visible = true;
                cbIsActiveFilter.Focus();
                cbIsActiveFilter.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActiveFilter.Visible = false;
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                _CalculatTotalUsers();
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            _CalculatTotalUsers();
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            switch (cbIsActiveFilter.Text)
            {
                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = 1", FilterColumn);
                    break;

                case "No":
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = 0", FilterColumn);
                    break;

                default:
                    _dtAllUsers.DefaultView.RowFilter = "";
                    break;
            }
            _CalculatTotalUsers();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
            {
                // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
                }
            }
        }

        private void _OpenAddNewUserForm()
        {
            frmAddUpdateUser form = new frmAddUpdateUser();
            //form.MdiParent = this.MdiParent;
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void btnAddNewEdit_Click(object sender, EventArgs e)
        {
            _OpenAddNewUserForm();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmAddUpdateUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            //form.MdiParent = this.MdiParent;
            form.ShowDialog();
            _RefreshUsersList();
        }
        private void _OpenUserInformationForm()
        {
            Form form = new frmShowUserInfo((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            //form.MdiParent = this.MdiParent;
            form.ShowDialog();
            _RefreshUsersList();
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenUserInformationForm();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmChangeUserPassword((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            //form.MdiParent = this.MdiParent;
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenAddNewUserForm();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure want to Delete User With ID: .:[ {(int)dgvAllUsers.CurrentRow.Cells[0].Value} ]:."
                                , "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsUsers.DeleteUser((int)dgvAllUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvAllUsers_DoubleClick(object sender, EventArgs e)
        {
            _OpenUserInformationForm();
        }
    }
}