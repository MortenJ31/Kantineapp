using Core.Models;
using MongoDB.Driver;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<List<User>> GetUsersByRoleAsync(Rolle role)
        {
            return await _users.Find(u => u.BrugerRolle == role).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            await _users.ReplaceOneAsync(u => u.Id == updatedUser.Id, updatedUser);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.Id == id);
        }
    }
}