using Core.Models;
using MongoDB.Driver;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Events> _events;

        public EventService(IMongoDatabase database)
        {
            _events = database.GetCollection<Events>("Events");
        }

        public async Task<List<Events>> GetAllEventsAsync()
        {
            return await _events.Find(_ => true).ToListAsync();
        }

        public async Task<Events?> GetEventByIdAsync(string id)
        {
            return await _events.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddEventAsync(Events newEvent)
        {
            newEvent.Id = Guid.NewGuid().ToString();
            await _events.InsertOneAsync(newEvent);
        }

        public async Task UpdateEventAsync(Events updatedEvent)
        {
            await _events.ReplaceOneAsync(e => e.Id == updatedEvent.Id, updatedEvent);
        }

        public async Task DeleteEventAsync(string id)
        {
            await _events.DeleteOneAsync(e => e.Id == id);
        }
    }
}