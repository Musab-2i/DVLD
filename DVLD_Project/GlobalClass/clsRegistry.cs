using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.GlobalClass
{
    internal class clsRegistry
    {
        private static void LocalLogError(string FuncName, string ErrorMessage)
        {
            clsEventLogger.LogError($"Error in {FuncName}: {ErrorMessage}", "clsRegistry");
        }

        private static readonly string FullKeyPath = @"HKEY_CURRENT_USER\Software\DVLD";
        private static readonly string SubKeyPath = @"Software\DVLD";
        public static void SetRegistryValue(string valueName, string valueData)
        {
            try
            {
                Registry.SetValue(FullKeyPath, valueName, valueData, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                LocalLogError(nameof(SetRegistryValue), ex.Message);
            }
        }
        public static string GetRegistryValue(string valueName)
        {
            try
            {
                string value = Registry.GetValue(FullKeyPath, valueName, null) as string;
                if (value != null)
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LocalLogError(nameof(GetRegistryValue), ex.Message);
                return null;
            }
        }
        public static void DeleteRegistryValue(string valueName)
        {
            try
            {
                // Open the registry Key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(SubKeyPath, true))
                    {
                        if (key != null)
                        {
                            key.DeleteValue(valueName);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                LocalLogError(nameof(DeleteRegistryValue), ex.Message);
                MessageBox.Show("UnauthorizedAccessException: Run the program with administrative privileges.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                LocalLogError(nameof(DeleteRegistryValue), ex.Message);
            }

        }
    }
}
