using System.Security.Cryptography;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Helper
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Pbkdf2Iterations = 10000;

        public static string HashPasswordWithSalt(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static bool IsCorrectPassword(User user, string password)
        {
            byte[] salt = Convert.FromBase64String(user.Salt);
            string passwordHash = HashPasswordWithSalt(password, salt);
            return passwordHash == user.PasswordHash;
        }
    }
}
