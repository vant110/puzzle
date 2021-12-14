namespace puzzle.Model
{
    static class User
    {
        private static int id;
        private static string login;
        private static string password;
        private static string passwordHash;

        public static int Id { get => id; set => id = value; }
        public static string Login { get => login; set => login = value; }
        public static string Password { get => password; set => password = value; }
        public static string PasswordHash { get => passwordHash; set => passwordHash = value; }
    }
}
