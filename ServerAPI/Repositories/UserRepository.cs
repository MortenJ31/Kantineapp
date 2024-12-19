using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(MongoDbService mongoDbService)
        {
            _userCollection = mongoDbService.GetUserCollection();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Role role)
        {
            return await _userCollection.Find(u => u.Role == role).ToListAsync();
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            if (string.IsNullOrEmpty(newUser.Id))
            {
                newUser.Id = await GetNextUserIdAsync();
            }

            await _userCollection.InsertOneAsync(newUser);
            return newUser;
        }

        private async Task<string> GetNextUserIdAsync()
        {
            var highestIdUser = await _userCollection
                .Find(_ => true)
                .SortByDescending(u => u.Id)
                .FirstOrDefaultAsync();

            if (highestIdUser == null || string.IsNullOrEmpty(highestIdUser.Id))
            {
                return "user1";
            }

            var currentId = int.Parse(highestIdUser.Id.Substring(4));
            return $"user{currentId + 1}";
        }
    }
}