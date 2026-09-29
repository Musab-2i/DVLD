using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enIssueReason
        {
            FirstTime = 1,
            Renewal = 2,
            DamagedReplacement = 3,
            LostReplacement = 4
        }

        public enMode Mode = enMode.AddNew;

        public int LicenseID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public int ApplicationID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Note { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public clsLicense.enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }
        
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        }


        public clsDriver DriverInfo {  get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public clsDetainedLicense DetainedInfo { get; set; }
        public clsLicense()
        {
            this.LicenseID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.ApplicationID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Note = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = clsLicense.enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID, int DriverID, int LicenseClassID, int ApplicationID,
            DateTime IssueDate, DateTime ExpirationDate, string Note, decimal PaidFees,
            bool IsActive, clsLicense.enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Note = Note;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(LicenseClassID);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }

        public static clsLicense Find(int LicenseID)
        {
            int DriverID = -1, LicenseClassID = -1, ApplicationID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Note = "";
            decimal PaidFees = 0;
            bool IsActive = true;
            byte IssueReason = 1;

            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref DriverID, ref LicenseClassID,
                ref ApplicationID, ref IssueDate, ref ExpirationDate, ref Note,
                ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, DriverID, LicenseClassID, ApplicationID,
                    IssueDate, ExpirationDate, Note, PaidFees, IsActive, (clsLicense.enIssueReason)IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.DriverID, this.LicenseClassID,
                this.ApplicationID, this.IssueDate, this.ExpirationDate, this.Note,
                this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.DriverID, this.LicenseClassID,
                this.ApplicationID, this.IssueDate, this.ExpirationDate, this.Note,
                this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        static public int GetLicenseIDByApplicationID(int ApplicationID)
        {
            return clsLicenseData.GetLicenseIDByApplicationID(ApplicationID);
        }

        static public int GetLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetLicenseIDByPersonID(PersonID,LicenseClassID);
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }
        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }
        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch(IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renewal:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        static public DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        static public bool IsLicenseExist(int PersonID,int LicenseClassID)
        {
            return clsLicenseData.IsLicenseExist(PersonID, LicenseClassID);
        }
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = clsApplicationTypes.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassInfo.ValidityLengthYear;
            
            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Note = Notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;

            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renewal;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }
            DeactivateCurrentLicense();

            return NewLicense;
        }
        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                clsApplicationTypes.enApplicationType.ReplaceDamagedDrivingLicense :
                clsApplicationTypes.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;
            
            if (!Application.Save()) { return null; }


            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();

            return NewLicense;
        }
        public int Detain(decimal FineFees, int CreatedByUserID)
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense();

            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = FineFees;
            DetainedLicense.CreatedByUserID = CreatedByUserID;

            if (!DetainedLicense.Save())
            {
                return -1;
            }

            return DetainedLicense.DetainID;
        }
        public bool ReleaseDetainLicense(int CreatedByUserID, ref int ApplicationID)
        {
            clsApplication ReleaseApplication = new clsApplication();

            ReleaseApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            ReleaseApplication.ApplicationTypeID = clsApplicationTypes.enApplicationType.ReleaseRetainedDrivingLicense;
            ReleaseApplication.ApplicationDate = DateTime.Now;
            ReleaseApplication.LastStatusDate = DateTime.Now;
            ReleaseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            ReleaseApplication.PaidFees = clsApplicationTypes.Find(clsApplicationTypes.enApplicationType.ReleaseRetainedDrivingLicense).ApplicationFees;
            ReleaseApplication.CreatedByUserID = CreatedByUserID;

            if (!ReleaseApplication.Save())
            {
                return false;
            }

            ApplicationID = ReleaseApplication.ApplicationID;

            return this.DetainedInfo.ReleaseDetainedLicense(CreatedByUserID, ReleaseApplication.ApplicationID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }
    }
}