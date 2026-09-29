using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer
{
    static public class clsBusinessUtil
    {
        static public string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "");

            }
        }
    }
}
