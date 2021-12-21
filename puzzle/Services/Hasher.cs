using puzzle.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace puzzle.Services
{
    static class Hasher
    {
        private static SHA512 shaM = new SHA512Managed();

        public static void HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(User.Login + User.Login);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            User.PasswordHash = Convert.ToBase64String(pbkdf2.GetBytes(44));
        }

        public static string HashImage(Stream imageStream)
        {
            string hash = Convert.ToBase64String(shaM.ComputeHash(imageStream));
            imageStream.Seek(0, SeekOrigin.Begin);
            return hash;
        }

        public static string HashAndCloseImage(MemoryStream imageMemoryStream)
        {
            string hash = Convert.ToBase64String(shaM.ComputeHash(imageMemoryStream));
            imageMemoryStream.Close();
            return hash;
        }
    }
}
