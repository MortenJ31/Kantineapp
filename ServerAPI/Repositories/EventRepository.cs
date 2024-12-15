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
                newEvent.Id = await GetNextEventIdAsync();
            }

            try
            {
                await _eventCollection.InsertOneAsync(newEvent);
                return newEvent;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                Console.WriteLine($"Duplikat ID-fejl: {ex.Message}");
                throw new Exception("Event ID allerede eksisterer. Prøv igen.");
            }
        }


        public async Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
           //Kontroll�r, at id stemmer overens med updatedEvent.id
            if (id != updatedEvent.Id)
            {
                throw new ArgumentException("EventId stemmer ikke overens");
            }
            

            //udf�r opdateringen
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
            // Find det højeste ID i formatet "eventX"
            var allIds = await _eventCollection
                .Find(_ => true)
                .Project(e => e.Id) // Hent kun ID-feltet
                .ToListAsync();

            // Filtrer ID'er, der starter med "event" og slutter med et tal
            var maxId = allIds
                .Where(id => id != null && id.StartsWith("event") && id.Length > 5)
                .Select(id =>
                {
                    var numPart = id.Substring(5); // Fjern "event"
                    return int.TryParse(numPart, out var num) ? num : 0; // Parse det numeriske
                })
                .DefaultIfEmpty(0) // Hvis ingen ID'er findes, brug 0
                .Max(); // Find det højeste ID

            // Generer det næste ID
            return $"event{maxId + 1}";
        }

    }
}