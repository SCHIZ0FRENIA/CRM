using System.Security.Cryptography;
using System.Text;
using CRM.Database;
using CRM.MVVM.M;
using Microsoft.EntityFrameworkCore;

namespace CRM.Core
{
    public class AuthorizationHelper
    {
        private static DbSet<User> _users = DatabaseHelper.Instance.GetContext().Users;
        private static int _current_user;
        public static int CurrentUser { get; set; }
        public static string GenerateSalt(int size = 16)
        {
            byte[] saltBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                DebugUtils.ShowMessageBox("Password cannot be empty.");
            if (string.IsNullOrWhiteSpace(salt))
                DebugUtils.ShowMessageBox("Salt cannot be empty.");

            using (var sha256 = SHA256.Create())
            {
                string saltedPassword = password + salt;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }
        public static string Authorize(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return "Invalid username or password.";

            string hashedPassword = HashPassword(password, user.Salt);

            if (user.PasswordHash == hashedPassword)
                return "";
            else
                return "Invalid username or password.";
        }
        public static bool UsernameExists(string username)
        {
            return _users.Any(u => u.Username == username);
        }
        public static bool UsernameExistsAdmin(string username)
        {
            return _users.GroupBy(u => u.Username)
                 .Any(group => group.Count() > 1);
        }
        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }
    }
}
