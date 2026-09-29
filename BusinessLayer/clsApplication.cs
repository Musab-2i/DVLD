using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public enMode Mode = enMode.AddNew;

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsApplicationTypes.enApplicationType ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypeInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public enApplicationStatus ApplicationStatus = enApplicationStatus.New; 
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsPerson PersonInfo { get; set; }
        public clsUsers UserInfo {  get; set; }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationTypeID = clsApplicationTypes.enApplicationType.NewDrivingLicense;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.ApplicationStatus = enApplicationStatus.New;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        protected clsApplication(int ApplicationID, int ApplicationPersonID, clsApplicationTypes.enApplicationType ApplicationTypeID,
            DateTime ApplicationDate, DateTime LastStatusDate, enApplicationStatus ApplicationStatus,
            decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicationPersonID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationStatus = ApplicationStatus;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            this.PersonInfo = clsPerson.Find(ApplicationPersonID);
            this.UserInfo = clsUsers.FindByUserID(CreatedByUserID);
            this.ApplicationTypeInfo = clsApplicationTypes.Find(ApplicationTypeID);
            Mode = enMode.Update;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicationPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 1;
            decimal PaidFees = 0;

            if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicationPersonID, ref ApplicationTypeID,
                ref ApplicationDate, ref LastStatusDate, ref ApplicationStatus, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicationPersonID, (clsApplicationTypes.enApplicationType) ApplicationTypeID,
                    ApplicationDate, LastStatusDate, (enApplicationStatus)ApplicationStatus, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(
                this.ApplicantPersonID, (int)this.ApplicationTypeID, this.ApplicationDate,
                this.LastStatusDate, (byte)this.ApplicationStatus, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(
                this.ApplicationID, this.ApplicantPersonID, (int)this.ApplicationTypeID,
                this.ApplicationDate, this.LastStatusDate, (byte)this.ApplicationStatus,
                this.PaidFees, this.CreatedByUserID);
        }

        public bool DeleteApplication()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)clsApplication.enApplicationStatus.Completed);
        }
        public bool SetCancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)clsApplication.enApplicationStatus.Cancelled);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }
    }
}