using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EsServiceCore.Utility
{
    public class Codes
    {
        public static string Hash(string text)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        public static string ReplaceForSearch(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace(" ", "");
                text = text.Replace("ی", "ي");
                text = text.Replace("ک", "ک");
                text = text.ToLower();
                return text;
            }
            else
            {
                return "";
            }
        }


    }
}
