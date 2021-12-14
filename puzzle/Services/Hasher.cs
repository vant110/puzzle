using puzzle.Model;

namespace puzzle.Services
{
    static class Hasher
    {
        public static void HashPassword(string password)
        {
            User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
