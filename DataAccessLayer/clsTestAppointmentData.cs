using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class clsTestAppointmentData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsTestAppointmentData");
        }

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
            ref byte TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref decimal PaidFees,
            ref bool IsLocked, ref int CreatedByUserID, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                TestTypeID = (byte)reader["TestTypeID"];
                                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                                AppointmentDate = (DateTime)reader["AppointmentDate"];
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                                IsLocked = (bool)reader["IsLocked"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];

                                RetakeTestApplicationID = reader["RetakeTestApplicationID"] == DBNull.Value ? -1 : (int)reader["RetakeTestApplicationID"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetTestAppointmentInfoByID), ex.Message);
                    }
                }
            }
            return isFound;
        }

        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID, byte TestTypeID,
            ref int TestAppointmentID, ref DateTime AppointmentDate, ref decimal PaidFees,
            ref bool IsLocked, ref int CreatedByUserID, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;
            string query = @"SELECT TOP 1 * 
                             FROM   TestAppointments
                             WHERE  (TestTypeID = @TestTypeID)
                             AND    (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                             ORDER BY TestAppointmentID DESC";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                TestAppointmentID = (int)reader["TestAppointmentID"];
                                AppointmentDate = (DateTime)reader["AppointmentDate"];
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                                IsLocked = (bool)reader["IsLocked"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];

                                RetakeTestApplicationID = reader["RetakeTestApplicationID"] == DBNull.Value ? -1 : (int)reader["RetakeTestApplicationID"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetLastTestAppointment), ex.Message);
                    }
                }
            }
            return IsFound;
        }
        
        public static int AddNewTestAppointment(byte TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, bool IsLocked, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            string query = @"INSERT INTO TestAppointments 
                             (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, IsLocked, CreatedByUserID, RetakeTestApplicationID)
                             VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @IsLocked, @CreatedByUserID, @RetakeTestApplicationID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@IsLocked", IsLocked);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestAppointmentID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(AddNewTestAppointment), ex.Message);
                    }
                }
            }
            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, byte TestTypeID,
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            decimal PaidFees, bool IsLocked, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE TestAppointments  
                             SET TestTypeID = @TestTypeID,
                                 LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                 AppointmentDate = @AppointmentDate,
                                 PaidFees = @PaidFees,
                                 IsLocked = @IsLocked,
                                 CreatedByUserID = @CreatedByUserID,
                                 RetakeTestApplicationID = @RetakeTestApplicationID
                             WHERE TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@IsLocked", IsLocked);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateTestAppointment), ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static int GetActiveTestAppointmentID(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            int ActiveTestAppointmentID = -1;
            string query = @"SELECT TOP 1 TestAppointmentID 
                             FROM TestAppointments 
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                               AND TestTypeID = @TestTypeID 
                               AND IsLocked = 0
                             ORDER BY TestAppointmentID DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int AppointmentID))
                        {
                            ActiveTestAppointmentID = AppointmentID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetActiveTestAppointmentID), ex.Message);
                    }
                }
            }
            return ActiveTestAppointmentID;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            bool IsFound = false;
            string query = @"SELECT TOP 1 Found = 1 
                             FROM TestAppointments 
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                               AND TestTypeID = @TestTypeID 
                               AND IsLocked = 0";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            IsFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(IsThereAnActiveScheduledTest), ex.Message);
                    }
                }
            }
            return IsFound;
        }
        
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, byte TestType)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT         TestAppointments.TestAppointmentID, TestAppointments.AppointmentDate, TestAppointments.PaidFees,
                                            CASE 
                                            WHEN Tests.TestResult = 0 THEN 'Failed'
                                            WHEN Tests.TestResult = 1 THEN 'Passed'
                                            ELSE 'Not Taken'
                                            END AS Result,
                                            TestAppointments.IsLocked
                             FROM            TestAppointments LEFT JOIN
                                                    Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             WHERE (TestTypeID = @TestType)
                             AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                             ORDER BY AppointmentDate DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestType", TestType);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //if (reader.HasRows)
                            //{
                                dt.Load(reader);
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetApplicationTestAppointmentsPerTestType), ex.Message);
                    }
                }
            }
            return dt;
        }
    
        public static DataTable GetAllTestAppointments()
        {
            DataTable dataTable = new DataTable();
            string query = @"SELECT * FROM TestAppointments_View
                             ORDER BY AppointmentDate DESC";
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
                        LocalLogError(nameof(GetAllTestAppointments), ex.Message);
                    }
                }
            }
            return dataTable;
        }
    
    }
}