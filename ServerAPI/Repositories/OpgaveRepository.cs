using ServerAPI.Models;
using MongoDB.Driver;
using ServerAPI.Services;


namespace ServerAPI.Repositories
{
    public class OpgaveRepository : IOpgaveRepository
    {
        private readonly IMongoCollection<Opgave> _opgaveCollection;

        public  OpgaveRepository(MongoDbService mongoDbService)
        {
            _opgaveCollection = mongoDbService.GetOpgaveCollection();
        }

       public async Task<IEnumerable<Opgave>> GetAllOpgaverAsync()
        {
            return await _opgaveCollection.Find(_ => true).ToListAsync();
        }
       
        public async Task<Opgave?> GetOpgaveByIdAsync(string id)
        {
            return await _opgaveCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Opgave>> GetOpgaveByEventIdAsync(string eventId)
        {
           return await _opgaveCollection.Find(o => o.EventId == eventId).ToListAsync();
        }

        public async Task<Opgave> AddOpgaveAsync(Opgave nyOpgave)
        {
            await _opgaveCollection.InsertOneAsync(nyOpgave);
            return nyOpgave;
        }

        public async Task UpdateOpgaveAsync(Opgave updateOpgave)
        {
            await _opgaveCollection.ReplaceOneAsync(o => o.Id == updateOpgave.Id, updateOpgave);
        }

        public async Task<bool> DeleteOpgaveAsync(string id)
        {
           var result = await _opgaveCollection.DeleteOneAsync(o => o.Id == id);
           return result.DeletedCount > 0;
        }
    }
}