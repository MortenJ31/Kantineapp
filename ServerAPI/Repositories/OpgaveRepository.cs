using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;


namespace ServerAPI.Repositories
{
    public class OpgaveRepository : IOpgaveRepository
    {
        private readonly IMongoCollection<Opgave> _opgaveCollection;

        public OpgaveRepository(MongoDbService mongoDbService)
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
            // Sørg for, at der tildeles et ID
            if (string.IsNullOrEmpty(nyOpgave.Id))
            {
                nyOpgave.Id = await GetNextTaskIdAsync(); // Generer ID, hvis det mangler
            }

            await _opgaveCollection.InsertOneAsync(nyOpgave);
            return nyOpgave;
        }

        public async Task UpdateOpgaveAsync(Opgave updateOpgave)
        {
            await _opgaveCollection.ReplaceOneAsync(o => o.Id == updateOpgave.Id, updateOpgave);
        }

        public async Task<Opgave?> UpdateOpgaveAsync(string id, Opgave updatedOpgave)
        {
            var result = await _opgaveCollection.ReplaceOneAsync(
                o => o.Id == id,
                updatedOpgave
            );

            if (result.MatchedCount == 0)
            {
                return null; // Opgave blev ikke fundet
            }

            return updatedOpgave;
        }

        public async Task<bool> DeleteOpgaveAsync(string id)
        {
           var result = await _opgaveCollection.DeleteOneAsync(o => o.Id == id);
           return result.DeletedCount > 0;
        }

        public async Task<string> GetNextTaskIdAsync()
        {
            var highestIdTask = await _opgaveCollection
                .Find(_ => true)
                .SortByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            if (highestIdTask == null || string.IsNullOrEmpty(highestIdTask.Id))
            {
                return "opgave1"; // Hvis ingen opgaver findes, start med opgave1
            }

            var currentId = int.Parse(highestIdTask.Id.Substring(6)); // Fjern 'opgave'
            var nextId = currentId + 1;

            return $"opgave{nextId}";
        }

    }
}