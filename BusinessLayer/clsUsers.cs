using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsUsers
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        private string _CurrentPassword { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo;
        enMode Mode;

        enum enMode { AddNewUser = 1, UpdateUser = 2 }

        public clsUsers()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.AddNewUser;
        }
        private clsUsers(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(PersonID);
            UserName = userName;
            _CurrentPassword = password;
            Password = password;
            IsActive = isActive;
            Mode = enMode.UpdateUser;
        }

        private static string GetHashPassowrdValue(string Passowrd)
        {
            return clsBusinessUtil.ComputeHash(Passowrd);
        }

        private bool _AddNewUser()
        {
            this.Password = GetHashPassowrdValue(this.Password);

            this.UserID = clsUsersData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }
        private bool _UpdateUsre()
        {
            string PasswordToSave = this.Password;
            //Check if the password has been changed, if yes then hash it before saving
            if (this.Password != _CurrentPassword)
            {
                PasswordToSave = GetHashPassowrdValue(this.Password);
            }

            bool IsUpdated = clsUsersData.UpdateUser(
                this.UserID,
                this.UserName,
                PasswordToSave, 
                this.IsActive
            );

            if (IsUpdated)
            {
                this.Password = PasswordToSave;
                _CurrentPassword = PasswordToSave;
            }

            return IsUpdated;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewUser:
                    if (_AddNewUser())
                    {
                        Mode = enMode.UpdateUser;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateUser:
                    return _UpdateUsre();
            }
            return false;
        }

        public static clsUsers FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string Username = "", Password = "";
            bool IsActive = false;
            if (clsUsersData.GetUserInfoByPersonID(PersonID, ref UserID, ref Username, ref Password, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, Username, Password, IsActive);
            }
            return null;
        }
        public static clsUsers FindByUserID(int UserID)
        {
            int PersonID = -1;
            string Username = "", Password = "";
            bool IsActive = false;
            if (clsUsersData.GetUserInfoByUserID(UserID, ref PersonID, ref Username, ref Password, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, Username, Password, IsActive);
            }
            return null;
        }
        public static clsUsers FindByUsername(string UserName)
        {
            int PersonID = -1, UserID = -1;
            string Password = "";
            bool IsActive = false;
            if (clsUsersData.GetUserInfoByUserName(UserName, ref PersonID, ref UserID, ref Password, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;
        }
        public static clsUsers FindByUsernameAndPassword(string UserName, string Password)
        {
            int PersonID = -1, UserID = -1;
            Password = GetHashPassowrdValue(Password);

            bool IsActive = false;
            if (clsUsersData.GetUserInfoByUserNameAndPassword(UserName, Password, ref PersonID, ref UserID, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistByPersonID(PersonID);
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUsersData.IsUserExistByUserName(UserName);
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public bool UpdatePassword(string NewPassword)
        {
            string HashedPassword = GetHashPassowrdValue(NewPassword);

            bool IsUpdated = clsUsersData.UpdatePassword(this.UserID, HashedPassword);

            if (IsUpdated)
            {
                this.Password = HashedPassword;
            }

            return IsUpdated;
        }
        public static DataTable GetUsersList()
        {
            return clsUsersData.GetAllUsers();
        }
        
    }
}
