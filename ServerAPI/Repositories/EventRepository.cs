using Core.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ServerAPI.Services;
using System.Reflection.Metadata.Ecma335;


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
            await _eventCollection.InsertOneAsync(newEvent);
            return newEvent;
        }

        public async Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
           //Kontrollér, at id stemmer overens med updatedEvent.id
            if (id != updatedEvent.Id)
            {
                throw new ArgumentException("EventId stemmer ikke overens");
            }
            

            //udfør opdateringen
            var result = await _eventCollection.ReplaceOneAsync(
                e => e.Id == id, updatedEvent);

            if (result.MatchedCount == 0)
            {
                return null; //Event ikke fundet
            }

            return updatedEvent;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var result = await _eventCollection.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }
    }
}