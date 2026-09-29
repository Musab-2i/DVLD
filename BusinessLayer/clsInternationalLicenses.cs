using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsInternationalLicenses : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        //public int CreatedByUserID { get; set; }
        
        public clsDriver DriverInfo { get; set; }

        public int LicenseID
        {
            get
            {
                return IssuedUsingLocalLicenseID;
            }
        }
        public clsInternationalLicenses()
        {
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        private clsInternationalLicenses(int InternationalLicenseID,int ApplicationID, int ApplicationPersonID,
            clsApplicationTypes.enApplicationType ApplicationTypeID, DateTime ApplicationDate, DateTime LastStatusDate, enApplicationStatus ApplicationStatus,
            decimal PaidFees, int CreatedByUserID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
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

            // Filling in the properties of the child class (International License)
            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            this.DriverInfo = clsDriver.FindByDriverID(DriverID);

            Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicense(
                this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicensesData.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID,
                this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate,
                this.IsActive, this.CreatedByUserID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewInternationalLicense())
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
                    return _UpdateInternationalLicense();
            }
            return false;
        }


        public static clsInternationalLicenses Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicensesData.GetInternationalLicenseInfoByID(InternationalLicenseID,
                ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                if (Application != null)
                {
                    return new clsInternationalLicenses(
                            InternationalLicenseID,
                            ApplicationID,
                            Application.ApplicantPersonID,
                            Application.ApplicationTypeID,
                            Application.ApplicationDate,
                            Application.LastStatusDate,
                            Application.ApplicationStatus,
                            Application.PaidFees,
                            CreatedByUserID,
                            DriverID,
                            IssuedUsingLocalLicenseID,
                            IssueDate,
                            ExpirationDate,
                            IsActive
                            );
                }
            }
            return null;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicensesData.GetDriverInternationalLicenses(DriverID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicensesData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }


        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }
        public static bool IsInternationalLicenseExistByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            return clsInternationalLicensesData.IsInternationalLicenseExistByLocalLicenseID(IssuedUsingLocalLicenseID);
        }
        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            return clsInternationalLicensesData.IsInternationalLicenseExist(InternationalLicenseID);
        }
    }
}