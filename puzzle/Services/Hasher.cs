using System;
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

        public static string Hash(Stream s)
        {
            s.Seek(0, SeekOrigin.Begin);

            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(s));
        }

        public static string HashAndClose(Stream s)
        {
            string hash = Hash(s);
            s.Close();
            return hash;
        }
    }
}
