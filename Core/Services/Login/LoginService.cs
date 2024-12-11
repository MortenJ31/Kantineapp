using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Services.Login
{
    public class LoginService : ILoginService
    {

        // Simuler brugere (erstattes med en database)
        private readonly List<User> _mockUsers = new()
        {
        new User { Username = "admin", Password = "password", Role = "administrator" },
        new User { Username = "kantineleder", Password = "password", Role = "kantineleder" },
        new User { Username = "medarbejder", Password = "password", Role = "medarbejder" }
        };

        private User? currentUser; //Gemmer den nuværende bruger

        public Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = _mockUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                currentUser = user; // Gem den aktuelle bruger
            }
            return Task.FromResult(user);
        }

        // Hent den aktuelle bruger
        public Task<User?> GetCurrentUserAsync()
        {
            return Task.FromResult(currentUser);
        }
    }

    // Brugerklasse
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

