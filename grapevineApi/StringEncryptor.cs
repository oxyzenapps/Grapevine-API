using grapevineService.Interfaces;
using grapevineServices.Interfaces;
using System;

namespace grapevineApi
{
    public class StringEncryptor : grapevineService.Interfaces.IStringEncryptor
    {
        public string Encrypt(string clearText)
        {
            if (string.IsNullOrEmpty(clearText)) return clearText;
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(clearText));
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(cipherText);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                return cipherText;
            }
        }
    }
}