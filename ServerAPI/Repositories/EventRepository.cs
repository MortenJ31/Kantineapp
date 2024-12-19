using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _eventCollection;

        public EventRepository(MongoDbService mongoDbService)
        {
            _eventCollection = mongoDbService.GetEventCollection();
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await _eventCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Event> AddEventAsync(Event newEvent)
        {
            newEvent.Id ??= await GetNextEventIdAsync();
            await _eventCollection.InsertOneAsync(newEvent);
            return newEvent;
        }

        public async Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
            var result = await _eventCollection.ReplaceOneAsync(e => e.Id == id, updatedEvent);

            if (result.MatchedCount == 0)
            {
                return null;
            }

            return updatedEvent;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var result = await _eventCollection.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }

        private async Task<string> GetNextEventIdAsync()
        {
            var maxId = await _eventCollection
                .Find(_ => true)
                .Project(e => e.Id.Substring(5)) // Ekstraher suffikset (alt efter 'event')
                .ToListAsync();

            var nextId = maxId
                .Where(id => int.TryParse(id, out _)) // Kun gyldige tal
                .Select(id => int.Parse(id))
                .DefaultIfEmpty(0)
                .Max() + 1;

            return $"event{nextId}";
        }


    }
}
