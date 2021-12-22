using puzzle.Model;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace puzzle.Services
{
    static class Hasher
    {
        private static SHA512 shaM = new SHA512Managed();

        public static string HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(UserDTO.Login + UserDTO.Login);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            return Convert.ToBase64String(pbkdf2.GetBytes(44));
        }

        public static string HashImage(Stream imageStream, bool close)
        {
            string hash = Convert.ToBase64String(shaM.ComputeHash(imageStream));
            if (close)
            {
                imageStream.Close();
            }
            else
            {
                imageStream.Seek(0, SeekOrigin.Begin);
            }
            return hash;
        }
    }
}
