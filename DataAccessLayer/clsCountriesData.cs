using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsCountriesData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsCountriesData");
        }

        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool Found = false;
            string query = @"SELECT * FROM Countries
                            WHERE CountryID = @CountryID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", CountryID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Found = true;
                                CountryName = (string)reader["CountryName"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetCountryInfoByID), ex.Message);
                    }
                }
            }
            return Found;
        }

        public static bool GetCountryInfoByName(string CountryName,ref int CountryID)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM Countries
                            WHERE CountryName = @CountryName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", CountryName);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                CountryID = (int)reader["CountryID"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetCountryInfoByName), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dataTable = new DataTable();
            string query = @"SELECT * FROM Countries";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetAllCountries), ex.Message);
                    }
                }
            }
            return dataTable;
        }
    }
}
