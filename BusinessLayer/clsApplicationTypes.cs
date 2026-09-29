using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsApplicationTypes
    {
        public enum enApplicationType
        {
            NewDrivingLicense = 1,
            RenewDrivingLicense = 2,
            ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4,
            ReleaseRetainedDrivingLicense = 5,
            NewInternationalLicense = 6,
            RetakeTest = 7
        }

        public clsApplicationTypes.enApplicationType ID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; } //= 2f;

        public clsApplicationTypes()
        {
            ID = clsApplicationTypes.enApplicationType.NewDrivingLicense;
            ApplicationTypeTitle = string.Empty;
            ApplicationFees = 0;
        }
        private clsApplicationTypes(clsApplicationTypes.enApplicationType ID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ID = ID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        } 

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType((int)this.ID, this.ApplicationTypeTitle, this.ApplicationFees);
        }
        public bool Save()
        {
            if (_UpdateApplicationType()) 
                return true;

            return false;
        }

        public static clsApplicationTypes Find(clsApplicationTypes.enApplicationType ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            decimal ApplicationFees = 0;
            if (clsApplicationTypesData.Find((int)ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            return null;
        }
        public static DataTable GetAllApplications()
        {
            return clsApplicationTypesData.GetAllApplications();
        }
    }
}
