using BusinessLayer;
using DVLD_Project.Applications.ApplicationTypes;
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

namespace DVLD_Project.Applications
{
    public partial class frmManageApplicationTypes : frmBase
    {

        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        private void _CalculatTotalUsers()
        {
            lblApplicationTypes.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void _RefreshList()
        {
            DataTable _dtAllApplications = clsApplicationTypes.GetAllApplications();
            dgvApplicationTypes.DataSource = _dtAllApplications;

            _CalculatTotalUsers();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshList();
            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 80;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 430;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 80;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmUpdateApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshList();
        }

        private void dgvApplicationTypes_DoubleClick(object sender, EventArgs e)
        {
            Form form = new frmShowApplicationDetails((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshList();
        }
    }
}
