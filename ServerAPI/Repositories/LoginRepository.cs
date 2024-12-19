using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public LoginRepository(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("User");
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            return await _userCollection.Find(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }
    }
}