using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class clsApplicationData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsApplicationData");
        }

        public static bool GetApplicationInfoByID(int ApplicationID,
            ref int ApplicationPersonID, ref int ApplicationTypeID, ref DateTime ApplicationDate,
            ref DateTime LastStatusDate, ref byte ApplicationStatus, ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                ApplicationPersonID = (int)reader["ApplicationPersonID"];
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                LastStatusDate = (DateTime)reader["LastStatusDate"];
                                ApplicationStatus = (byte)reader["ApplicationStatus"];
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetApplicationInfoByID), ex.Message);
                    }
                }
            }
            return isFound;
        }

        public static int AddNewApplication(int ApplicationPersonID, int ApplicationTypeID,
            DateTime ApplicationDate, DateTime LastStatusDate, byte ApplicationStatus,
            decimal PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            string query = @"INSERT INTO Applications 
                             (ApplicationPersonID, ApplicationTypeID, ApplicationDate, LastStatusDate, ApplicationStatus, PaidFees, CreatedByUserID)
                             VALUES 
                             (@ApplicationPersonID, @ApplicationTypeID, @ApplicationDate, @LastStatusDate, @ApplicationStatus, @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ApplicationID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(AddNewApplication), ex.Message);
                    }
                }
            }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicationPersonID, int ApplicationTypeID,
            DateTime ApplicationDate, DateTime LastStatusDate, byte ApplicationStatus,
            decimal PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Applications  
                             SET ApplicationPersonID = @ApplicationPersonID,
                                 ApplicationTypeID = @ApplicationTypeID,
                                 ApplicationDate = @ApplicationDate,
                                 LastStatusDate = @LastStatusDate,
                                 ApplicationStatus = @ApplicationStatus,
                                 PaidFees = @PaidFees,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateApplication), ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;
            string query = @"DELETE FROM Applications 
                             WHERE ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(DeleteApplication), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }

        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            int RowsAffected = 0;
            string query = @"UPDATE Applications
                             SET 
                                  ApplicationStatus = @NewStatus 
                                , LastStatusDate = @LastStatusDate
                             WHERE ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@NewStatus", NewStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                    
                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateStatus), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }
        
        public static bool IsApplicationExist(int ApplicationID)
        {
            bool IsFound = false;
            string query = @"SELECT Top 1 Found = 1 FROM Applications WHERE ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    try
                    {
                        connection.Open();

                        object Result = command.ExecuteScalar();
                        if (Result != null)
                        {
                            IsFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(IsApplicationExist), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static int GetActiveApplicationID(int ApplicationPersonID, byte ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            byte NewApplicationStatus = 1;

            string query = @"SELECT ActiveApplicationID = ApplicationID 
                             FROM Applications
                             Where ApplicationPersonID = @ApplicationPersonID
                             And ApplicationTypeID = @ApplicationTypeID
                             And ApplicationStatus = @NewApplicationStatus";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@NewApplicationStatus", NewApplicationStatus);
                    try
                    {
                        connection.Open();
                        object Result = command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(),out int result))
                        {
                            ActiveApplicationID = result;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetActiveApplicationID), ex.Message);
                    }
                }
            }
            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            byte NewApplicationStatus = 1;

            string query = @"SELECT Applications.ApplicationID
                            FROM Applications 
                            INNER JOIN LocalDrivingLicenseApplications 
                                                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE Applications.ApplicationPersonID = @PersonID 
                              AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID 
                              AND Applications.ApplicationStatus = @NewApplicationStatus";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    command.Parameters.AddWithValue("@NewApplicationStatus", NewApplicationStatus);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetActiveApplicationIDForLicenseClass), ex.Message);
                    }
                }
            }
            return ActiveApplicationID;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Applications ORDER BY ApplicationDate DESC";

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
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetAllApplications), ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}