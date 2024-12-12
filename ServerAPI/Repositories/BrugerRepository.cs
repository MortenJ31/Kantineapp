using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;


namespace ServerAPI.Repositories
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly IMongoCollection<Bruger> _brugerCollection;

        public BrugerRepository(MongoDbService mongoDbService)
        {
            _brugerCollection = mongoDbService.GetBrugerCollection();
        }

        public async Task<IEnumerable<Bruger>> GetAllUsersAsync()
        {
            //Hent alle brugere fra MongoDB
            return await _brugerCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Bruger?> GetUserByIdAsync(string id) // Opdaterer int til string
        {
            // Find en bruger med det specifikke ID
            return await _brugerCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle role)
        {
            return await _brugerCollection.Find(b => b.Rolle == role).ToListAsync();
        }
        
        
        public async Task<Bruger> AddUserAsync(Bruger newUser)
        {
            //
            if (string.IsNullOrEmpty(newUser.Id))
            {
                newUser.Id = await GetNextUserIdAsync(); // Generér næste ID
            }


            //Tilføj en ny bruger til MongoDB
            await _brugerCollection.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task<string> GetNextUserIdAsync()
        {
            var highestIdUser = await _brugerCollection
                .Find(_ => true)
                .SortByDescending(u => u.Id)
                .FirstOrDefaultAsync();

            if (highestIdUser == null || string.IsNullOrEmpty(highestIdUser.Id))
            {
                return "medarbejder1"; // Hvis ingen brugere findes, start med user1
            }

            var currentId = int.Parse(highestIdUser.Id.Substring(11)); // Fjerner 'medarbejder'
            var nextId = currentId + 1;

            return $"medarbejder{nextId}";
        }

    }
}