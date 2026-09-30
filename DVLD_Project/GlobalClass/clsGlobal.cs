using BusinessLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.GlobalClass
{
    public class clsGlobal
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsEventLogger.LogError($"Error in {FuncName}: {ErrorMessage}", "clsGlobal");
        }

        public static clsUsers CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                // If a blank username is passed, it means "clear my data" (cancel remember me).
                if (string.IsNullOrWhiteSpace(Username))
                {
                    if (clsRegistry.GetRegistryValue("Username") != null)
                    {
                        clsRegistry.DeleteRegistryValue("Username");
                        clsRegistry.DeleteRegistryValue("Password");
                    }
                    return true;
                }

                string EncryptedPassword = clsEncryptionDecryption.Encrypt(Password);

                clsRegistry.SetRegistryValue("Username", Username);
                clsRegistry.SetRegistryValue("Password", EncryptedPassword);
                return true;
            }
            catch (Exception ex)
            {
                LocalLogError(nameof(RememberUsernameAndPassword), ex.Message);
                return false;
            }
        }
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                if (clsRegistry.GetRegistryValue("Username") != null)
                {
                    Username = clsRegistry.GetRegistryValue("Username");
                    Password = clsEncryptionDecryption.Decrypt(clsRegistry.GetRegistryValue("Password"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LocalLogError(nameof(GetStoredCredential), ex.Message);
                return false;
            }
        }
    }
}


