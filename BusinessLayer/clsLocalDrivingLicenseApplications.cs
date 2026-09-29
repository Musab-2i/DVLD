using System;
using System.Data;
using System.Runtime.CompilerServices;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo {  get; set; }
        public string PersonFullName
        {
            get
            {
                return this.PersonInfo.FullName;
                //return PersonInfo.FullName;
            }
        }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicationPersonID,
            clsApplicationTypes.enApplicationType ApplicationTypeID, DateTime ApplicationDate, DateTime LastStatusDate, enApplicationStatus ApplicationStatus,
            decimal PaidFees, int CreatedByUserID, int LicenseClassID)
        : base(ApplicationID, ApplicationPersonID, ApplicationTypeID, ApplicationDate, LastStatusDate, ApplicationStatus, PaidFees, CreatedByUserID)

        {
            // Filling in the properties of the parent class (clsApplication)
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicationPersonID;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationDate = ApplicationDate;
            base.LastStatusDate = LastStatusDate;
            base.ApplicationStatus = ApplicationStatus;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;
            //base.PersonInfo = clsPerson.FindByDetainID(ApplicantPersonID);
            //base.UserInfo = clsUsers.FindByUserID(CreatedByUserID);
            //base.ApplicationTypeInfo = clsApplicationTypes.FindByDetainID(ApplicationTypeID);

            // Filling in the properties of the child class (Local Application)
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(LicenseClassID);

            Mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            // 1. We retrieve the son's information from the database
            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(
                LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                // 2. We retrieve the father's information based on the ApplicationID
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                
                // 3. We merge them together into one object
                if (Application != null)
                {
                    return new clsLocalDrivingLicenseApplication(
                        LocalDrivingLicenseApplicationID, Application.ApplicationID, Application.ApplicantPersonID,
                        Application.ApplicationTypeID, Application.ApplicationDate, Application.LastStatusDate,
                        Application.ApplicationStatus, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
                }
            }
            return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            // we send the ApplicationID to be saved in the Sub's table
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public bool Delete()
        {
            if (clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID))
            {
                if (base.DeleteApplication())
                    return true;
            }
            return false;
        }

        // Hide the parent's Save function (use the word new) to run the child's function
        public new bool Save()
        {
            // We save the Base first in order to get a new ApplicationID
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        base.DeleteApplication();
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }

        /// <summary>
        /// check the appointment booking button so that he can't book an appointment if he passes the test.
        /// </summary>
        /// <param name="TestType"></param>
        /// <returns></returns>
        public bool DoesPassTestType(clsTestTypes.enTestType TestType)
        {
            return clsTestData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public int GetActiveTestAppointmentID(clsTestTypes.enTestType TestType)
        {
            return clsTestAppointmentData.GetActiveTestAppointmentID(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestType)
        {
            return clsTestAppointmentData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType)
        {
            return clsTestAppointmentData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, LicenseClassID);
        }

        public int GetTotalTrialsPerTest(clsTestTypes.enTestType TestType)
        {
            return clsTestData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public bool AttendedTest(clsTestTypes.enTestType TestType)
        {
            return GetTotalTrialsPerTest(TestType) > 0;
        }

        public int GetTotalPassedTest()
        {
            return clsTest.GetTotalPassedTest(this.LocalDrivingLicenseApplicationID);
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetLicenseIDByApplicationID(this.ApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Note,int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;

                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            
            //now we diver is there, so we add new licesnse
            clsLicense IssueNewLicense = new clsLicense();

            IssueNewLicense = new clsLicense();

            IssueNewLicense.DriverID = DriverID;
            IssueNewLicense.LicenseClassID = this.LicenseClassID;
            IssueNewLicense.ApplicationID = this.ApplicationID;
            IssueNewLicense.IssueDate = DateTime.Now;
            IssueNewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.ValidityLengthYear);
            IssueNewLicense.Note = Note;
            IssueNewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            IssueNewLicense.IsActive = true;
            IssueNewLicense.IssueReason = clsLicense.enIssueReason.FirstTime;
            IssueNewLicense.CreatedByUserID = CreatedByUserID;

            if (IssueNewLicense.Save())
            {
                this.SetComplete();

                return IssueNewLicense.LicenseID;
            }

            return -1;
        }

        //public clsTest GetLastTestPerTestType(clsTestTypes.enTestType testType)
        //{
        //    return clsTest.FindLastTestPerPersonAndLicenseClass();
        //}

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

    }
}