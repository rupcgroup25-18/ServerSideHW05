using System.Security.Cryptography;
using System.Text;

namespace HW4ServerSide.Models
{
    public class Users
    {
        int id;
        string name;
        string email;
        string password;
        bool active;
        static List<Users> UsersList = new List<Users>();

        public Users(int id, string name, string email, string password, bool active = true)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Active = active;
        }

        public Users() {}

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool Active { get => active; set => active = value; }
        public static List<Users> UsersList1 { get => UsersList; set => UsersList = value; }

        public bool Insert()
        {
            foreach (Users user in UsersList1)
            {
                if (user.Email == Email)
                {
                    return false;
                }
            }

            Id = UsersList1.Count > 0 ? UsersList1.Max(u => u.Id) + 1 : 1;
            Password = HashPassword(Password);

            UsersList1.Add(this);
            return true;
        }

        static public List<Users> Read()
        {
            return UsersList1;
        }

        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }
}