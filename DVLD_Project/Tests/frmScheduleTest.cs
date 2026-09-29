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
using static BusinessLayer.clsTestTypes;

namespace DVLD_Project.Tests
{
    public partial class frmScheduleTest : frmBase
    {
        private int _LocalDrivingLicenseAppID = -1;
        private enTestType _TestType = enTestType.VisionTest;
        private int _TestAppointmentID = -1;

        public frmScheduleTest(int LocalDrivingLicenseAppID, enTestType TestType, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;
            _TestType = TestType;
            _TestAppointmentID = TestAppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.LoadData(_LocalDrivingLicenseAppID, _TestType, _TestAppointmentID);
        }
    }
}
