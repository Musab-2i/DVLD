using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsLocked { get; set; }
        public int CreatedByUserID { get; set; }
        
        public int TestID
        {
            get { return _GetTestID(); }

        }


        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo { get; set; }

        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestTypes.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.IsLocked = false;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID, clsTestTypes.enTestType TestTypeID,
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            decimal PaidFees, bool IsLocked, int CreatedByUserID, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.CreatedByUserID = CreatedByUserID;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            // Protection: We only process Retake Test if the number already exists.
            this.RetakeTestAppInfo = this.RetakeTestApplicationID != -1 ? clsApplication.FindBaseApplication(RetakeTestApplicationID) : null;

            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            byte TestTypeID = 0;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsLocked = false;

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID,
                ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref IsLocked, ref CreatedByUserID, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestTypes.enTestType)TestTypeID,
                    LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, IsLocked, CreatedByUserID, RetakeTestApplicationID);
            }
            return null;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((byte)this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.IsLocked, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (byte)this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.IsLocked, this.CreatedByUserID, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public static clsTestAppointment GetLastTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsLocked = false;
            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (byte)TestType,
                                                          ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref IsLocked, ref CreatedByUserID,
                                                          ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, TestType,
                   LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, IsLocked, CreatedByUserID, RetakeTestApplicationID);
            }
            return null;
        }

        private int _GetTestID()
        {
            return clsTestData.GetTestID(TestAppointmentID);
        }
    }
}