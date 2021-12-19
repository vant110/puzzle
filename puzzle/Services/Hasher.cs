using puzzle.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace puzzle.Services
{
    static class Hasher
    {
        public static void HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(User.Login + User.Login);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            User.PasswordHash = Convert.ToBase64String(pbkdf2.GetBytes(44));
        }
    }
}
