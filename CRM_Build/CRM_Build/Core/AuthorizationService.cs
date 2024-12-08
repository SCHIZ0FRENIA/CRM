using System.Security.Cryptography;
using System.Text;
using CRM_Build.DB.Repos;

namespace CRM_Build.Core
{
    public class AuthorizationService
    {
        private readonly UsersRepo _usersRepo;

        public AuthorizationService(UsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
        }

        // Verify user credentials and return role
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            var user = await _usersRepo.FindAsync(u => u.Username == username);

            if (user.Count == 0)
            {
                return "User not found";
            }

            if (user[0].PasswordHash == hashedPassword)
            {
                return user[0].Role;
            }

            return "Invalid credentials";
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
