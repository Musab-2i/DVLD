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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.License
{
    public partial class frmDriverLicensesHistory : frmBase
    {
        private int _PersonID;
        public frmDriverLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmDriverLicensesHistory_Load(object sender, EventArgs e)
        {
            clsDriver Driver = clsDriver.FindByPersonID(_PersonID);

            if (Driver != null)
            {
                ctrlDriverLicenses1.LoadLicensesHistory(Driver.DriverID);
            }
            else
            {
                MessageBox.Show("No licenses have been issued to this person yet, so there is no license record for his show.",
                        "No record",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                //this.Close();
                //return;
            }
        }

        private void lblShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new frmShowPersonInfo(_PersonID);
            form.ShowDialog();
        }
    }
}
