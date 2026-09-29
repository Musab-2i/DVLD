using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsLicenseClassData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsLicenseClassData");
        }

        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription
                                , ref byte MinAllowedAge, ref byte ValidityLengthYear, ref decimal ClassFees)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM LicenseClass
                     WHERE LicenseClassID = @LicenseClassID";
            
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ClassName = (string)reader["ClassName"];
                                ClassDescription = (string)reader["ClassDescription"];
                                MinAllowedAge = (byte)reader["MinAllowedAge"];
                                ValidityLengthYear = (byte)reader["ValidityLengthYear"];
                                ClassFees = Convert.ToDecimal(reader["ClassFees"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetLicenseClassInfoByID), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool GetLicenseClassInfoByClassName(string ClassName,ref int LicenseClassID, ref string ClassDescription
                        , ref byte MinAllowedAge, ref byte ValidityLengthYear, ref decimal ClassFees)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM LicenseClass
                     WHERE ClassName = @ClassName";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", ClassName);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                LicenseClassID = (int)reader["LicenseClassID"];
                                ClassDescription = (string)reader["ClassDescription"];
                                MinAllowedAge = (byte)reader["MinAllowedAge"];
                                ValidityLengthYear = (byte)reader["ValidityLengthYear"];
                                ClassFees = Convert.ToDecimal(reader["ClassFees"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetLicenseClassInfoByClassName), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool UpdateClassInfo(int LicenseClassID, string ClassName, string ClassDescription, byte MinAllowedAge, byte ValidityLengthYear, decimal ClassFees)
        {
            int rowsAffected = 0;

            string query = @"UPDATE LicenseClass
                     SET ClassName = @ClassName,
                         ClassDescription = @ClassDescription,
                         MinAllowedAge = @MinAllowedAge,
                         ValidityLengthYear = @ValidityLengthYear,
                         ClassFees = @ClassFees
                     WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    command.Parameters.AddWithValue("@ClassName", ClassName);
                    command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                    command.Parameters.AddWithValue("@MinAllowedAge", MinAllowedAge);
                    command.Parameters.AddWithValue("@ValidityLengthYear", ValidityLengthYear);
                    command.Parameters.AddWithValue("@ClassFees", ClassFees);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateClassInfo), ex.Message);
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllClass()
        {
            DataTable dataTable = new DataTable();
            string query = @"SELECT * FROM LicenseClass";
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
                        LocalLogError(nameof(GetAllClass), ex.Message);
                    }
                }
            }
            return dataTable;
        }
    }
}
