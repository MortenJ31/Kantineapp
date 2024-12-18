using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IMongoCollection<Bruger> _brugerCollection;

        public LoginRepository(MongoDbService mongoDbService)
        {
            _brugerCollection = mongoDbService.GetBrugerCollection();
        }

        public async Task<Bruger?> AuthenticateAsync(string email, string password)
        {
            // Find the user with the given email
            var user = await _brugerCollection.Find(b => b.Email == email).FirstOrDefaultAsync();

            // If the user exists, verify the password
            if (user != null && user.Adgangskode == password)
            {
                return user; // Return the authenticated user
            }

            // Return null if authentication fails
            return null;
        }
    }
}