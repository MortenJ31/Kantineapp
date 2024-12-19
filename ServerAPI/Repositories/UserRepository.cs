using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        // Constructor – her henter vi User-kollektionen fra MongoDbService
        public UserRepository(MongoDbService mongoDbService)
        {
            _userCollection = mongoDbService.GetUserCollection();
        }

        // Henter alle brugere fra databasen
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userCollection.Find(_ => true).ToListAsync(); // Returnerer alle brugere
        }

        // Finder en bruger baseret på ID
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync(); // Finder brugeren med det angivne ID
        }

        // Henter brugere med en specifik rolle
        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Role role)
        {
            return await _userCollection.Find(u => u.Role == role).ToListAsync(); // Returnerer brugere med den ønskede rolle
        }

        // Tilføjer en ny bruger til databasen
        public async Task<User> AddUserAsync(User newUser)
        {
            newUser.Id ??= await GetNextUserIdAsync(); // Genererer et nyt ID, hvis det ikke er angivet
            await _userCollection.InsertOneAsync(newUser); // Tilføjer brugeren til databasen
            return newUser;
        }

        // Genererer det næste bruger-ID (fx "user1", "user2" osv.)
        private async Task<string> GetNextUserIdAsync()
        {
            var maxId = await _userCollection
                .Find(_ => true) // Finder alle brugere
                .Project(u => u.Id.Substring(4)) // Ekstraher det numeriske suffiks fra "user123"
                .ToListAsync();

            var nextId = maxId
                .Where(id => int.TryParse(id, out _)) // Filtrerer kun gyldige numeriske ID'er
                .Select(id => int.Parse(id)) // Konverterer suffikset til et heltal
                .DefaultIfEmpty(0) // Hvis der ingen ID'er er, starter vi med 0
                .Max() + 1; // Finder det højeste ID og tilføjer 1

            return $"user{nextId}"; // Returnerer det næste ID som "userX"
        }
    }
}
