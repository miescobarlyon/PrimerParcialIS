using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SecurityService
    {
        private const int SaltLength = 16;

        public static string GenerarSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBuffer = new byte[SaltLength];
                rng.GetBytes(saltBuffer);
                return Convert.ToBase64String(saltBuffer);
            }
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                string passwordWithSalt = password + salt;
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool Verify(string passwordPlano, string salt, string hash)
        {
            string hashGenerado = HashPassword(passwordPlano, salt);
            return hashGenerado == hash;
        }
    }
}
