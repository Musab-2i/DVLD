using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class clsTestData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsTestData");
        }

        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID,
            ref bool TestResult, ref string Note, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestID", TestID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                TestAppointmentID = (int)reader["TestAppointmentID"];
                                TestResult = (bool)reader["TestResult"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];

                                // الحقول التي تسمح بـ NULL
                                Note = reader["Note"] == DBNull.Value ? "" : (string)reader["Note"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetTestInfoByID), ex.Message);
                    }
                }
            }
            return isFound;
        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult,
            string Note, int CreatedByUserID)
        {
            int TestID = -1;
            string query = @"INSERT INTO Tests 
                             (TestAppointmentID, TestResult, Note, CreatedByUserID)
                             VALUES (@TestAppointmentID, @TestResult, @Note, @CreatedByUserID);
    
                             UPDATE TestAppointments
                             SET IsLocked = 1
                             WHERE TestAppointmentID = @TestAppointmentID;

                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("@TestResult", TestResult);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    // الحقول التي تسمح بـ NULL
                    command.Parameters.AddWithValue("@Note", (string.IsNullOrEmpty(Note)) ? (object)DBNull.Value : Note);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(AddNewTest), ex.Message);
                    }
                }
            }
            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID,
            bool TestResult, string Note, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Tests  
                             SET TestAppointmentID = @TestAppointmentID,
                                 TestResult = @TestResult,
                                 Note = @Note,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestID", TestID);
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("@TestResult", TestResult);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    // الحقول التي تسمح بـ NULL
                    command.Parameters.AddWithValue("@Note", (string.IsNullOrEmpty(Note)) ? (object)DBNull.Value : Note);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateTest), ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Passed = false;
            string query = @"SELECT        TOP 1 DoesPassed = 1
                            FROM            LocalDrivingLicenseApplications INNER JOIN
                                                     TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                                     Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE (TestAppointments.TestTypeID = @TestTypeID)
                            AND
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                            AND 
                            (Tests.TestResult = 1)";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null)
                        {
                            Passed = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(DoesPassTestType), ex.Message);
                    }
                }
            }
            return Passed;
        }

        public static int TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int Trials = 0;
            string query = @"SELECT        Count(Tests.TestID)
                            FROM            TestAppointments INNER JOIN
                                                     Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            Where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND TestAppointments.TestTypeID = @TestTypeID
                            AND Tests.TestResult = 0";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    try
                    {
                        connection.Open();

                        object Results = command.ExecuteScalar();
                        if (Results != null && int.TryParse(Results.ToString(), out int TotalTrials))
                        {
                            Trials = TotalTrials;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(TotalTrialsPerTest), ex.Message);
                    }
                }
            }
            return Trials;
        }

        public static int GetTotalPassedTest(int LocalDrivingLicenseApplicationID)
        {
            int TotalPassedTest = 0;
            string query = @"SELECT        Count(Tests.TestID)
                            FROM            TestAppointments INNER JOIN
                                                     Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            Where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND 
                            Tests.TestResult = 1";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null && int.TryParse(Results.ToString(), out int PassedTestCount))
                        {
                            TotalPassedTest = PassedTestCount;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetTotalPassedTest), ex.Message);
                    }
                }
            }
            return TotalPassedTest;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            string query = @"SELECT TestID
                             FROM Tests
                             where TestAppointmentID = @TestAppointmentID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null && int.TryParse(Results.ToString(), out int TestId))
                        {
                            TestID = TestId;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetTestID), ex.Message);
                    }
                }
            }
            return TestID;
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Tests";

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
                                dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetAllTests), ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}