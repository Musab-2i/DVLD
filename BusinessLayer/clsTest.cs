using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Note { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Note = "";
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsTest(int TestID, int TestAppointmentID, bool TestResult,
            string Note, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;

            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);

            this.TestResult = TestResult;
            this.Note = Note;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            bool TestResult = false;
            string Note = "";

            if (clsTestData.GetTestInfoByID(TestID, ref TestAppointmentID,
                ref TestResult, ref Note, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Note, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Note, this.CreatedByUserID);

            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Note, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }

        public static int GetTotalPassedTest(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetTotalPassedTest(LocalDrivingLicenseApplicationID);
        }

        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }
    }
}