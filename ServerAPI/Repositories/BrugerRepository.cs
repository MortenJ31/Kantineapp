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

            //var filter = Builders<Bruger>.Filter.Empty; // Tilsvarende _ => true
            //var result = await _brugerCollection.Find(filter).ToListAsync();

            //Console.WriteLine($"Antal brugere fundet: {result.Count}");
            //return result;




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
        
        
        public async Task<Bruger> AddUserAsync(Bruger bruger)
        {
            //Tilføj en ny bruger til MongoDB
            await _brugerCollection.InsertOneAsync(bruger);
            return bruger;
        }


    }
}