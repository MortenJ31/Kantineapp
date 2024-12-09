using Core.Models;
using MongoDB.Driver;

namespace Core.Services
{
    public class OpgaveService : IOpgaveService
    {
        private readonly IMongoCollection<Opgave> _opgaver;

        public OpgaveService(IMongoDatabase database)
        {
            _opgaver = database.GetCollection<Opgave>("Opgaver");
        }

        public async Task<List<Opgave>> GetAllOpgaverAsync()
        {
            return await _opgaver.Find(_ => true).ToListAsync();
        }

        public async Task<Opgave?> GetOpgaveByIdAsync(string id)
        {
            return await _opgaver.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Opgave>> GetOpgaverByEventIdAsync(string eventId)
        {
            return await _opgaver.Find(o => o.EventId == eventId).ToListAsync();
        }

        public async Task AddOpgaveAsync(Opgave opgave)
        {
            opgave.Id = Guid.NewGuid().ToString();
            await _opgaver.InsertOneAsync(opgave);
        }

        public async Task UpdateOpgaveAsync(Opgave updatedOpgave)
        {
            await _opgaver.ReplaceOneAsync(o => o.Id == updatedOpgave.Id, updatedOpgave);
        }

        public async Task DeleteOpgaveAsync(string id)
        {
            await _opgaver.DeleteOneAsync(o => o.Id == id);
        }
    }
}