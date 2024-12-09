using Core.Models;
using System.Collections.Concurrent;

namespace Core.Services
{
    public class UserServiceInMemory : IUserService
    {
        private readonly ConcurrentDictionary<string, User> _users = new();

        public UserServiceInMemory()
        {
            // Mock data for users
            var mockUsers = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Navn = "Thomas",
                    Email = "Thomas@example.com",
                    Adgangskode = "password123",
                    BrugerRolle = Rolle.Medarbejder
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Navn = "Bob",
                    Email = "bob@example.com",
                    Adgangskode = "securepassword",
                    BrugerRolle = Rolle.Administrator
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Navn = "Hans",
                    Email = "hans@example.com",
                    Adgangskode = "mypassword",
                    BrugerRolle = Rolle.Kantineleder
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Navn = "Anna",
                    Email = "anna@example.com",
                    Adgangskode = "password456",
                    BrugerRolle = Rolle.Medarbejder
                }
            };

            foreach (var user in mockUsers)
            {
                _users[user.Id!] = user;
            }
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.Values.ToList());
        }

        public Task<List<User>> GetUsersByRoleAsync(Rolle role)
        {
            var usersByRole = _users.Values.Where(u => u.BrugerRolle == role).ToList();
            return Task.FromResult(usersByRole);
        }

        public Task<User?> GetUserByIdAsync(string id)
        {
            _users.TryGetValue(id, out var user);
            return Task.FromResult(user);
        }

        public Task AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _users[user.Id!] = user;
            return Task.CompletedTask;
        }

        public Task UpdateUserAsync(User updatedUser)
        {
            if (updatedUser.Id != null && _users.ContainsKey(updatedUser.Id))
            {
                _users[updatedUser.Id] = updatedUser;
            }
            return Task.CompletedTask;
        }

        public Task DeleteUserAsync(string id)
        {
            _users.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
