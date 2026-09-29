using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsCountries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountries()
        {
            CountryID = -1;
            CountryName = "";
        }
        private clsCountries(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }

        static public clsCountries Find(int CountryID)
        {
            if (CountryID > 0)
            {
                string CountryName = "";
                if (clsCountriesData.GetCountryInfoByID(CountryID, ref CountryName))
                {
                    return new clsCountries(CountryID, CountryName);
                }
            }
            return null;
        }
        static public clsCountries Find(string CountryName)
        {
            if (!string.IsNullOrEmpty(CountryName))
            {
                int CountryID = -1;
                if (clsCountriesData.GetCountryInfoByName(CountryName, ref CountryID))
                {
                    return new clsCountries(CountryID, CountryName);
                }
            }
            return null;
        }

        static public DataTable GetCountriesList()
        {
            return clsCountriesData.GetAllCountries();
        }
    }
}
