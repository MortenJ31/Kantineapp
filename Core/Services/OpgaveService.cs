using Core.Models;
using MongoDB.Driver;


namespace Core.Services
{
    public class OpgaveService : IOpgaveService
    {
        private readonly IMongoCollection<Opgave> _opgaverCollection;
        private readonly IMongoCollection<User> _usersCollection; 
        public OpgaveService(IMongoDatabase database)
        {
            _opgaverCollection = database.GetCollection<Opgave>("Opgaver");
            _usersCollection = database.GetCollection<User>("Users"); 
        }

        public async Task<List<Opgave>> GetOpgaverByEventIdAsync(string eventId)
        {
            return await _opgaverCollection.Find(o => o.EventId == eventId).ToListAsync();
        }

        public async Task<Opgave?> GetOpgaveByIdAsync(string opgaveId)
        {
            return await _opgaverCollection.Find(o => o.OpgaveId == opgaveId).FirstOrDefaultAsync();
        }

        public async Task AddOpgaveAsync(string eventId, Opgave opgave)
        {
            opgave.OpgaveId = Guid.NewGuid().ToString();
            opgave.EventId = eventId;
            await _opgaverCollection.InsertOneAsync(opgave);
        }

        public async Task UpdateOpgaveAsync(Opgave opgave)
        {
            await _opgaverCollection.ReplaceOneAsync(o => o.OpgaveId == opgave.OpgaveId, opgave);
        }

        public async Task DeleteOpgaveAsync(string eventId, string opgaveId)
        {
            await _opgaverCollection.DeleteOneAsync(o => o.EventId == eventId && o.OpgaveId == opgaveId);
        }

        public async Task<List<User>> GetMedarbejdereAsync()
        {
            return await _usersCollection.Find(u => u.Rolle == "Medarbejder").ToListAsync();
        }
    }
}