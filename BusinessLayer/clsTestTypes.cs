using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTestTypes
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public clsTestTypes.enTestType ID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestFees { get; set; } //= 2f;

        public clsTestTypes()
        {
            this.ID = clsTestTypes.enTestType.VisionTest;
            this.TestTypeTitle = string.Empty;
            this.TestTypeDescription = string.Empty;
            this.TestFees = 0;
        }
        private clsTestTypes(clsTestTypes.enTestType ID, string TestTypeTitle, string TestTypeDescription, decimal TestFees)
        {
            this.ID = ID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestFees = TestFees;
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int)this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestFees);
        }
        public bool Save()
        {
            if (_UpdateTestType())
                return true;

            return false;
        }

        public static clsTestTypes Find(clsTestTypes.enTestType TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestFees = 0;
            if (clsTestTypesData.Find((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees))
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
            }
            return null;
        }
        public static DataTable GetAllTests()
        {
            return clsTestTypesData.GetAllTests();
        }
    }
}
