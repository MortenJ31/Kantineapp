using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Name = "Alice", Role = "Admin" },
            new User { Id = 2, Name = "Bob", Role = "Employee" }
        };

        public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult(_users.AsEnumerable());

        public Task<User> GetByIdAsync(int id) => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

        public Task<User> AddAsync(User newUser)
        {
            newUser.Id = _users.Max(u => u.Id) + 1; // Simulate auto-increment
            _users.Add(newUser);
            return Task.FromResult(newUser);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var userToDelete = _users.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null) return Task.FromResult(false);

            _users.Remove(userToDelete);
            return Task.FromResult(true);
        }
    }
}