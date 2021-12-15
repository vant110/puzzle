using puzzle.Model;
using System.Text;

namespace puzzle.Services
{
    static class Hasher
    {
        public static void HashPassword(string password)
        {
            User.PasswordHash = Encoding.ASCII.GetBytes(BCrypt.Net.BCrypt.HashPassword(password));
        }
    }
}
