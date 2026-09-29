using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinAllowedAge { get; set; }
        public byte ValidityLengthYear { get; set; }
        public decimal ClassFees { get; set; }

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinAllowedAge = 18;
            ValidityLengthYear = 10;
            ClassFees = 0;
        }

        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
                                    byte MinAllowedAge, byte ValidityLengthYear, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinAllowedAge = MinAllowedAge;
            this.ValidityLengthYear = ValidityLengthYear;
            this.ClassFees = ClassFees;
        }

        private bool _UpdateClassInfo()
        {
            return clsLicenseClassData.UpdateClassInfo
                (
                this.LicenseClassID
                , this.ClassName
                , this.ClassDescription
                , this.MinAllowedAge
                , this.ValidityLengthYear
                , this.ClassFees
                );
        }

        public bool Save()
        {
            if (_UpdateClassInfo())
                return true;
            return false;
        }
        static public clsLicenseClass FindLicenseClassByID(int ClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinAllowedAge = 18, ValidityLengthYear = 10;
            decimal ClassFees = 0;
            if (clsLicenseClassData.GetLicenseClassInfoByID(ClassID, ref ClassName, ref ClassDescription, ref MinAllowedAge, ref ValidityLengthYear, ref ClassFees))
            {
                return new clsLicenseClass(ClassID, ClassName, ClassDescription, MinAllowedAge, ValidityLengthYear, ClassFees);
            }
            return null;
        }

        static public clsLicenseClass FindLicenseClassByName(string ClassName)
        {
            int ClassID = -1;
            string ClassDescription = "";
            byte MinAllowedAge = 18, ValidityLengthYear = 10;
            decimal ClassFees = 0;
            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref ClassID, ref ClassDescription, ref MinAllowedAge, ref ValidityLengthYear, ref ClassFees))
            {
                return new clsLicenseClass(ClassID, ClassName, ClassDescription, MinAllowedAge, ValidityLengthYear, ClassFees);
            }
            return null;
        }

        static public DataTable GetClassesList()
        {
            return clsLicenseClassData.GetAllClass();
        }
    }
}