using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsTestTypesData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsTestTypesData");
        }

        public static bool Find(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestFees)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM TestTypes
                            WHERE TestTypeID = @TestTypeID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                TestTypeTitle = (string)reader["TestTypeTitle"];
                                TestTypeDescription = (string)reader["TestTypeDescription"];
                                TestFees = (decimal)reader["TestFees"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(Find), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestFees)
        {
            int RowsAffected = 0;
            string query = @"UPDATE TestTypes
                            SET [TestTypeTitle] = @TestTypeTitle
                            ,[TestFees] = @TestFees
                            ,[TestTypeDescription]  = @TestTypeDescription
                            WHERE TestTypeID = @TestTypeID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                    command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                    command.Parameters.AddWithValue("@TestFees", TestFees);
                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateTestType), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }

        public static DataTable GetAllTests()
        {
            DataTable dataTable = new DataTable();
            string query = @"SELECT * FROM TestTypes";
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
                        LocalLogError(nameof(GetAllTests), ex.Message);
                    }
                }
            }
            return dataTable;
        }
    }
}