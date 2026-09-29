using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace DataAccessLayer
{
    public static class clsPersonData
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsDataAccessSettings.LogError($"Error in {FuncName}: {ErrorMessage}", "clsPersonData");
        }
        public static int AddNewPerson(int NationalCountryID, string NationalNo,
                                       string FirstName, string SecondName, string ThirdName,
                                       string LastName, string Address, string PhoneNo,
                                       string Email, string ImagePath, DateTime DateOfBirth,
                                       byte Gender)
        {
            int PersonID = -1;
            string query = @"INSERT INTO People 
                    ([NationalNo],[FirstName],[SecondName],[ThirdName],[LastName],[DateOfBirth],[Gender],[Address],[PhoneNo],[Email],[NationalCountryID],[ImagePath]) 
                    VALUES 
                    (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gender,@Address,@PhoneNo,@Email,@NationalCountryID,@ImagePath);
                    SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                    command.Parameters.AddWithValue("@NationalCountryID", NationalCountryID);

                    // الحقول التي تسمح بـ NULL
                    command.Parameters.AddWithValue("@ThirdName", (string.IsNullOrEmpty(ThirdName)) ? (object)DBNull.Value : ThirdName);
                    command.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(Email)) ? (object)DBNull.Value : Email);
                    command.Parameters.AddWithValue("@ImagePath", (string.IsNullOrEmpty(ImagePath)) ? (object)DBNull.Value : ImagePath);
                    try
                    {
                        connection.Open();
                        object Results = command.ExecuteScalar();
                        if (Results != null && int.TryParse(Results.ToString(), out int InsertedID))
                        {
                            PersonID = InsertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(AddNewPerson), ex.Message);
                    }
                }
            }
            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, int NationalCountryID,
                                       string NationalNo, string FirstName, string SecondName, string ThirdName,
                                       string LastName, string Address, string PhoneNo,
                                       string Email, string ImagePath, DateTime DateOfBirth,
                                       byte Gender)
        {
            int RowsAffected = 0;
            string query = @"UPDATE People 
                     SET NationalNo = @NationalNo,
                         FirstName = @FirstName,
                         SecondName = @SecondName,
                         ThirdName = @ThirdName,
                         LastName = @LastName,
                         DateOfBirth = @DateOfBirth,
                         Gender = @Gender,
                         Address = @Address,
                         PhoneNo = @PhoneNo,
                         Email = @Email,
                         NationalCountryID = @NationalCountryID,
                         ImagePath = @ImagePath
                     WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                    command.Parameters.AddWithValue("@NationalCountryID", NationalCountryID);

                    // التعامل مع الحقول التي تسمح بـ NULL
                    command.Parameters.AddWithValue("@ThirdName", (string.IsNullOrEmpty(ThirdName)) ? (object)DBNull.Value : ThirdName);
                    command.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(Email)) ? (object)DBNull.Value : Email);
                    command.Parameters.AddWithValue("@ImagePath", (string.IsNullOrEmpty(ImagePath)) ? (object)DBNull.Value : ImagePath);

                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(UpdatePerson), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }

        public static bool GetPersonInfoByID(int PersonID, ref int NationalCountryID, ref string NationalNo,
                                       ref string FirstName, ref string SecondName, ref string ThirdName,
                                       ref string LastName, ref string Address, ref string PhoneNo,
                                       ref string Email, ref string ImagePath, ref DateTime DateOfBirth,
                                       ref byte Gender)
        {
            bool Found = false;
            string query = @"SELECT * FROM People 
                           WHERE PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Found = true;

                                NationalCountryID = (int)reader["NationalCountryID"];
                                NationalNo = (string)reader["NationalNo"];
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                LastName = (string)reader["LastName"];
                                Address = (string)reader["Address"];
                                PhoneNo = (string)reader["PhoneNo"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (byte)reader["Gender"];

                                // القيم التي قد تكون NULL
                                ThirdName = (reader["ThirdName"] != DBNull.Value) ? (string)reader["ThirdName"] : "";
                                Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
                                ImagePath = (reader["ImagePath"] != DBNull.Value) ? (string)reader["ImagePath"] : "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetPersonInfoByID), ex.Message);
                    }
                }
                return Found;
            }

        }

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int NationalCountryID, ref int PersonID,
                                       ref string FirstName, ref string SecondName, ref string ThirdName,
                                       ref string LastName, ref string Address, ref string PhoneNo,
                                       ref string Email, ref string ImagePath, ref DateTime DateOfBirth,
                                       ref byte Gender)
        {
            bool Found = false;
            string query = @"SELECT * FROM People 
                           WHERE NationalNo = @NationalNo";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Found = true;

                                PersonID = (int)reader["PersonID"];
                                NationalCountryID = (int)reader["NationalCountryID"];
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                LastName = (string)reader["LastName"];
                                Address = (string)reader["Address"];
                                PhoneNo = (string)reader["PhoneNo"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (byte)reader["Gender"];

                                // القيم التي قد تكون NULL
                                ThirdName = (reader["ThirdName"] != DBNull.Value) ? (string)reader["ThirdName"] : "";
                                Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
                                ImagePath = (reader["ImagePath"] != DBNull.Value) ? (string)reader["ImagePath"] : "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(GetPersonInfoByNationalNo), ex.Message);
                    }
                }
                return Found;
            }

        }

        public static bool DeletePersonByID(int PersonID)
        {
            int RowsAffected = 0;
            string query = @"DELETE FROM People
                             WHERE PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(DeletePersonByID), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }

        public static bool IsPersonExistByID(int PersonID)
        {
            bool IsFound = false;
            string query = @"SELECT Found=1 From People
                         WHERE PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
                        LocalLogError(nameof(IsPersonExistByID), ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool IsFound = false;
            string query = @"SELECT Found=1 From People
                         WHERE NationalNo = @NationalNo";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
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
                        LocalLogError(nameof(IsPersonExistByNationalNo), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT People.PersonID, People.NationalNo
                          , People.FirstName, People.SecondName, People.ThirdName, People.LastName
                          , People.DateOfBirth, People.Gender,
                                CASE Gender
                                    WHEN 0 THEN 'Male'
                                    WHEN 1 THEN 'Female'
                                    ELSE 'Unknown'
                                END AS GendorCaption,
                                People.Address, People.PhoneNo, People.Email, 
                                                     People.NationalCountryID, Countries.CountryName, People.ImagePath
                            FROM            People INNER JOIN
                                                     Countries ON People.NationalCountryID = Countries.CountryID";
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
                        LocalLogError(nameof(GetAllPeople), ex.Message);
                    }
                }

            }
            return dt;
        }
    }
}
