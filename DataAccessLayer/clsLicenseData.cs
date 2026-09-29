using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class clsLicenseData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsLicenseData");
        }

        public static bool GetLicenseInfoByID(int LicenseID, ref int DriverID, ref int LicenseClassID,
            ref int ApplicationID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Note,
            ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM License WHERE LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            DriverID = (int)reader["DriverID"];
                            LicenseClassID = (int)reader["LicenseClassID"];
                            ApplicationID = (int)reader["ApplicationID"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            ExpirationDate = (DateTime)reader["ExpirationDate"];

                            Note = reader["Note"] != DBNull.Value ? (string)reader["Note"] : "";

                            PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                            IsActive = (bool)reader["IsActive"];
                            IssueReason = (byte)reader["IssueReason"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    LocalLogError(nameof(GetLicenseInfoByID), ex.Message);
                }
            }
            return isFound;
        }

        public static int AddNewLicense(int DriverID, int LicenseClassID, int ApplicationID,
            DateTime IssueDate, DateTime ExpirationDate, string Note, decimal PaidFees,
            bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            string query = @"INSERT INTO License 
                             (DriverID, LicenseClassID, ApplicationID, IssueDate, ExpirationDate, Note, PaidFees, IsActive, IssueReason, CreatedByUserID)
                             VALUES 
                             (@DriverID, @LicenseClassID, @ApplicationID, @IssueDate, @ExpirationDate, @Note, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                command.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note) ? (object)DBNull.Value : Note);

                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        LicenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    LocalLogError(nameof(AddNewLicense), ex.Message);
                }
            }

            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int DriverID, int LicenseClassID,
            int ApplicationID, DateTime IssueDate, DateTime ExpirationDate, string Note,
            decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE License 
                             SET DriverID = @DriverID,
                                 LicenseClassID = @LicenseClassID,
                                 ApplicationID = @ApplicationID,
                                 IssueDate = @IssueDate,
                                 ExpirationDate = @ExpirationDate,
                                 Note = @Note,
                                 PaidFees = @PaidFees,
                                 IsActive = @IsActive,
                                 IssueReason = @IssueReason,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                // الشورت هاند
                command.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note) ? (object)DBNull.Value : Note);

                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LocalLogError(nameof(UpdateLicense), ex.Message);
                }
            }
            return (rowsAffected > 0);
        }

        public static int RenewLicense(int ApplicantPersonID, decimal ApplicationFees, int DriverID, int LicenseClassID,
                               DateTime IssueDate, DateTime ExpirationDate, decimal LicenseFees,
                               string Notes, int CreatedByUserID, int OldLicenseID)
        {
            int NewLicenseID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_RenewLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                    command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
                    command.Parameters.AddWithValue("@DriverID", DriverID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    command.Parameters.AddWithValue("@LicenseFees", LicenseFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@OldLicenseID", OldLicenseID);

                    if (string.IsNullOrEmpty(Notes))
                        command.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Notes", Notes);

                    // تجهيز متغير الـ OUTPUT لاستقبال رقم الرخصة الجديدة
                    SqlParameter outputIdParam = new SqlParameter("@NewLicenseID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        NewLicenseID = (int)outputIdParam.Value;
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(RenewLicense), ex.Message);
                    }
                }
            }

            return NewLicenseID;
        }

        public static int GetLicenseIDByApplicationID(int ApplicationID)
        {
            int LicenseID = -1;
            string query = @"SELECT LicenseID FROM License 
                            Where ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    try
                    {
                        connection.Open();
                        object Result = command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int licenseID))
                        {
                            LicenseID = licenseID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetLicenseIDByApplicationID), ex.Message);
                    }
                }
            }
            return LicenseID;
        }
        
        public static int GetLicenseIDByPersonID(int PersonID,int LicenseClass)
        {
            int LicenseID = -1;
            string query = @"SELECT License.LicenseID
                             FROM   License INNER JOIN
                                            Drivers ON License.DriverID = Drivers.DriverID
                             WHERE  
                             License.LicenseClassID = @LicenseClass
                             AND Drivers.PersonID = @PersonID
                             And IsActive = 1;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        object Result = command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int licenseID))
                        {
                            LicenseID = licenseID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetLicenseIDByPersonID), ex.Message);
                    }
                }
            }
            return LicenseID;
        }

        public static bool IsLicenseExist(int PersonID, int LicenseClass)
        {
            bool IsFound = false;
            string query = @"SELECT Found = 1
                             FROM   License INNER JOIN
                                            Drivers ON License.DriverID = Drivers.DriverID
                             Where
                                    License.LicenseClassID = @LicenseClass 
                                    AND Drivers.PersonID = @PersonID
                                    And IsActive = 1;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
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
                        LocalLogError(nameof(IsLicenseExist), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;
            string query = @"SELECT     License.LicenseID
                             FROM        License INNER JOIN
                                               Drivers ON License.DriverID = Drivers.DriverID
                             WHERE License.LicenseClassID = @LicenseClassID
                             AND Drivers.PersonID = @PersonID
                             AND License.IsActive = 1;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                    try
                    {
                        connection.Open();
                        object Result = command.ExecuteScalar();
                        if (Result != null && int.TryParse(Result.ToString(),out int Licenseid))
                        {
                            LicenseID = Licenseid;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetActiveLicenseIDByPersonID), ex.Message);
                    }
                }
            }
            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            int RowsAffected = 0;
            string query = @"UPDATE License
                             SET IsActive = 0
                             WHERE LicenseID = @LicenseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(DeactivateLicense), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dataTable = new DataTable();
            string query = @"SELECT        License.LicenseID, License.ApplicationID, LicenseClass.ClassName
                                         , License.IssueDate, License.ExpirationDate, License.IsActive
                             FROM            License INNER JOIN
                                                      LicenseClass ON License.LicenseClassID = LicenseClass.LicenseClassID
                             where DriverID = @DriverID
                             ORDER BY IsActive DESC;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", DriverID);

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
                        LocalLogError(nameof(GetDriverLicenses), ex.Message);
                    }
                }
            }
            return dataTable;
        }
    }
}