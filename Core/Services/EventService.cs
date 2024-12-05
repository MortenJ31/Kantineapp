using Core.Models;
using MongoDB.Driver;


namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Event> _eventsCollection;

        public EventService(IMongoDatabase database)
        {
            // Forbind til en MongoDB-samling
            _eventsCollection = database.GetCollection<Event>("Events");
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            // Hent alle events fra MongoDB
            return await _eventsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            // Find et specifikt event via ID
            return await _eventsCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddEventAsync(Event newEvent)
        {
            // Tilføj et nyt event
            newEvent.Id = Guid.NewGuid().ToString();
            await _eventsCollection.InsertOneAsync(newEvent);
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            // Opdater et eksisterende event
            await _eventsCollection.ReplaceOneAsync(e => e.Id == updatedEvent.Id, updatedEvent);
        }

        public async Task DeleteEventAsync(string id)
        {
            // Slet et event
            await _eventsCollection.DeleteOneAsync(e => e.Id == id);
        }
    }
}