using BusinessLayer;
using DVLD_Project.Applications;
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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _CalculatTotalUsers()
        {
            lblTestTypes.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void _RefreshList()
        {
            DataTable _dtAllTests = clsTestTypes.GetAllTests();
            dgvTestTypes.DataSource = _dtAllTests;

            _CalculatTotalUsers();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshList();
            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 30;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 120;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 390;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 50;
            }
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form form = new frmUpdateTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshList();
        }

        private void dgvTestTypes_DoubleClick(object sender, EventArgs e)
        {
            Form form = new frmShowTestDetails((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshList();
        }
    }
}
