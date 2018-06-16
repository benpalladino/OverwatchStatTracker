using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BusinessLogicLayer
{
    public class PasswordLogic
    {
        public static string PasswordHash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public bool ValidatePasswords(string tempPassword, string dbPassword)
        {
            bool isValid = false;
            tempPassword = PasswordHash(tempPassword);

            if (tempPassword == dbPassword)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
