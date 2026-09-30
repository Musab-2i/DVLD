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
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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

                    SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        PersonID = (int)outputIdParam.Value;
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

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPersonInfoByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPersonInfoByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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

        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(DeletePerson), ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }

        public static bool CheckPersonExistByID(int PersonID)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_CheckPersonExistByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        IsFound = (int)returnParameter.Value == 1;
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(CheckPersonExistByID), ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static bool CheckPersonExistByNationalNo(string NationalNo)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_CheckPersonExistByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnParameter);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        IsFound = (int)returnParameter.Value == 1;
                    }
                    catch (Exception ex)
                    {
                        LocalLogError(nameof(CheckPersonExistByNationalNo), ex.Message);
                    }
                }
            }
            return IsFound;
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllPeople", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
