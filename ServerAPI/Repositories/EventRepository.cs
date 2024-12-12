using Core.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ServerAPI.Services;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;


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
            if (string.IsNullOrEmpty(newEvent.Id))
            {
                //Genererer næste Id
                newEvent.Id = await GetNextEventIdAsync(); 
            }
            
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

        public async Task<string> GetNextEventIdAsync()
        {
            // Hent det dokument med det højeste ID
            var highestIdEvent = await _eventCollection
                .Find(_ => true) // Find alle dokumenter
                .SortByDescending(e => e.Id) // Sortér i faldende rækkefølge efter ID
                .FirstOrDefaultAsync(); // Tag det første dokument

            if (highestIdEvent == null || string.IsNullOrEmpty(highestIdEvent.Id))
            {
                return "event1"; // Hvis ingen events findes, start med event1
            }

            // Ekstrahér nummeret fra det højeste ID og tæl op
            var currentId = int.Parse(highestIdEvent.Id.Substring(5)); // Fjern 'event'
            var nextId = currentId + 1;

            return $"event{nextId}";
        }
    }
}