using BusinessLayer;
using DVLD_Project.Applications.ReleaseLicenseApplication;
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

namespace DVLD_Project.License.DetainedLicense
{
    public partial class frmManageDetainLicenses : frmBase
    {
        private DataTable _dtDetainedLicenses;
        public frmManageDetainLicenses()
        {
            InitializeComponent();
        }
        private void _CalculateTotalRecords()
        {
            lblDataRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void _RefreshDetainedLicensesList()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            _dtDetainedLicenses = _dtDetainedLicenses.DefaultView.ToTable(false, "DetainID", "LicenseID"
                                             , "DetainDate", "IsReleased", "FineFees"
                                             , "ReleaseDate", "NationalNo", "FullName"
                                             , "ReleaseApplicationID");
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            _CalculateTotalRecords();
        }

        private void frmManageDetainLicenses_Load(object sender, EventArgs e)
        {
            _RefreshDetainedLicensesList();

            cbFilterBy.SelectedIndex = 0;

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns["DetainID"].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns["DetainID"].Width = 60;

                dgvDetainedLicenses.Columns["LicenseID"].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns["LicenseID"].Width = 60;

                dgvDetainedLicenses.Columns["DetainDate"].HeaderText = "Detain Date";
                dgvDetainedLicenses.Columns["DetainDate"].Width = 160;

                dgvDetainedLicenses.Columns["IsReleased"].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns["IsReleased"].Width = 110;

                dgvDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns["FineFees"].Width = 110;

                dgvDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns["ReleaseDate"].Width = 140;

                dgvDetainedLicenses.Columns["NationalNo"].HeaderText = "National No.";
                dgvDetainedLicenses.Columns["NationalNo"].Width = 110;

                dgvDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns["FullName"].Width = 250;

                dgvDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App.ID";
                dgvDetainedLicenses.Columns["ReleaseApplicationID"].Width = 110;
            }
        }

        private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true; // افترض أن اسم الكومبوبوكس الثاني cbIsReleased
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0; // All
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                _CalculateTotalRecords();
                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }

            _CalculateTotalRecords();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    _dtDetainedLicenses.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = 1", FilterColumn);
                    break;
                case "No":
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = 0", FilterColumn);
                    break;
            }
            _CalculateTotalRecords();
        }

        private void txtFilterValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام إذا كان الفلتر مبنياً على المعرفات (IDs)
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        // --- أحداث القائمة المنسدلة (ContextMenuStrip) ---

        private void cmsDetainedLicenses_Opening(object sender, CancelEventArgs e)
        {
            if (dgvDetainedLicenses.Rows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            // تفعيل/تعطيل زر "فك الحجز" بناءً على حالة الرخصة المحددة
            bool IsReleased = (bool)dgvDetainedLicenses.CurrentRow.Cells["IsReleased"].Value;
            releaseDetainedLicenseToolStripMenuItem.Enabled = !IsReleased;
        }

        // أزرار الواجهة العلوية (إذا كان لديك أزرار لحجز وفك حجز رخصة جديدة من نفس الشاشة)

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainedLicense();
            frm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense(); // Constructor بدون باراميترات ليختار المستخدم الرخصة من الشاشة
            frm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value;
            // استدعاء شاشة تفاصيل الشخص بناءً على الرقم الوطني
            Form frm = new frmShowPersonInfo(NationalNo);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;
            Form frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value;
            clsPerson Person = clsPerson.Find(NationalNo);
            if (Person != null)
            {
                Form frm = new frmDriverLicensesHistory(Person.PersonID);
                frm.ShowDialog();
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;

            // فتح شاشة فك الحجز التي صممناها البارحة وتمرير رقم الرخصة لها
            Form frm = new frmReleaseLicense(LicenseID);
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

    }
}