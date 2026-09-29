using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsUsersData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsUsersData");
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            string query = @"INSERT INTO Users
                                       ([PersonID],[UserName],[Password],[IsActive])
                                 VALUES
                                       (@PersonID,@UserName,@Password,@IsActive);
                            SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    try
                    {
                        connection.Open();
                        object Result = command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                        {
                            UserID = InsertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(AddNewUser), ex.Message);
                    }
                }
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID, string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;

            string query = @"UPDATE Users 
                     SET UserName = @UserName, 
                         Password = @Password, 
                         IsActive = @IsActive
                     WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    try
                    {
                        connection.Open();

                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdateUser), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }

        public static bool UpdatePassword(int UserID, string Password)
        {
            int RowAffected = 0;
            string query = @"UPDATE Users 
                             SET Password = @Password
                             WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Password", Password);
                    try
                    {
                        connection.Open();

                        RowAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdatePassword), ex.Message);
                    }
                }
            }
            return (RowAffected > 0);
        }
        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string Username, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM Users
                             WHERE PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                Username = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetUserInfoByPersonID), ex.Message);
                    }
                }
            }
            return IsFound;
        }
        
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string Username, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM Users
                             WHERE UserID = @UserID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                PersonID = (int)reader["PersonID"];
                                Username = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetUserInfoByUserID), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool GetUserInfoByUserName(string UserName, ref int PersonID, ref int UserID, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM Users
                             WHERE UserName = @UserName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetUserInfoByUserName), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool GetUserInfoByUserNameAndPassword(string UserName, string Password, ref int PersonID, ref int UserID, ref bool IsActive)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM Users
                             WHERE UserName = @UserName
                             AND Password = @Password";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetUserInfoByUserNameAndPassword), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            bool IsFound = false;
            string query = @"SELECT Found = 1 From Users 
                           Where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null)
                        {
                            IsFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(IsUserExistByPersonID), ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            bool IsFound = false;
            string query = @"SELECT Found = 1 From Users 
                           Where UserName = @UserName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null)
                        {
                            IsFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(IsUserExistByUserName), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool IsUserExistByLoginScreen(string UserName, string Password)
        {
            bool IsFound = false;
            string query = @"SELECT Found = 1 From Users 
                           Where UserName = @UserName 
                           AND
                           Password = @Password
                           AND
                           IsActive = 1";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null)
                        {
                            IsFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(IsUserExistByLoginScreen), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;
            string query = @"DELETE FROM Users 
                             WHERE UserID = @UserID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                        
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(DeleteUser), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }
        
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT        Users.UserID, Users.PersonID, 
                                           FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL (People.ThirdName + ' ','') + People.LastName, 
                                           Users.UserName, Users.IsActive
                             FROM People INNER JOIN
                                    Users ON People.PersonID = Users.PersonID";
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
                        LocalLogError(nameof(GetAllUsers), ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}
