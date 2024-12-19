using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        // Constructor – her sørger vi for at hente User-kollektionen direkte fra databasen
        public LoginRepository(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("User");
        }

        // Tjekker, om en bruger kan logge ind baseret på email og password
        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            // Finder den første bruger, der matcher både email og password
            return await _userCollection.Find(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }
    }
}