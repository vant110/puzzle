using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace puzzle.Services
{
    static class Hasher
    {
        public static string HashPassword(string password, string login)
        {
            byte[] salt = Encoding.ASCII.GetBytes(login + login);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            return Convert.ToBase64String(pbkdf2.GetBytes(44));
        }

        public static string HashImage(Stream imageStream)
        {
            imageStream.Seek(0, SeekOrigin.Begin);

            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(imageStream));
        }

        public static string HashImageAndClose(Stream imageStream)
        {
            string hash = HashImage(imageStream);
            imageStream.Close();
            return hash;
        }
    }
}
