using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _eventCollection;

        // Constructor – vi henter Event-kollektionen fra MongoDbService
        public EventRepository(MongoDbService mongoDbService)
        {
            _eventCollection = mongoDbService.GetEventCollection();
        }

        // Henter alle events fra databasen
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventCollection.Find(_ => true).ToListAsync(); // Henter alt i kollektionen
        }

        // Finder et specifikt event baseret på dets ID
        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await _eventCollection.Find(e => e.Id == id).FirstOrDefaultAsync(); // Finder det første match
        }

        // Tilføjer et nyt event til databasen
        public async Task<Event> AddEventAsync(Event newEvent)
        {
            // Hvis ID mangler, genererer vi et nyt
            newEvent.Id ??= await GetNextEventIdAsync();
            await _eventCollection.InsertOneAsync(newEvent); // Tilføjer event til kollektionen
            return newEvent;
        }

        // Opdaterer et eksisterende event baseret på ID
        public async Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
            // Erstatter det gamle event med det nye
            var result = await _eventCollection.ReplaceOneAsync(e => e.Id == id, updatedEvent);

            if (result.MatchedCount == 0)
            {
                return null; // Hvis intet match findes, returnerer vi null
            }

            return updatedEvent; // Returnerer det opdaterede event
        }

        // Sletter et event baseret på dets ID
        public async Task<bool> DeleteEventAsync(string id)
        {
            var result = await _eventCollection.DeleteOneAsync(e => e.Id == id); // Slet operation
            return result.DeletedCount > 0; // Returnerer true, hvis noget blev slettet
        }

        // Genererer det næste ID til et event (fx "event1", "event2" osv.)
        private async Task<string> GetNextEventIdAsync()
        {
            var maxId = await _eventCollection
                .Find(_ => true)
                .Project(e => e.Id.Substring(5)) // Ekstraher alt efter "event"
                .ToListAsync();

            var nextId = maxId
                .Where(id => int.TryParse(id, out _)) // Filtrer gyldige numeriske ID'er
                .Select(id => int.Parse(id)) // Konverter til tal
                .DefaultIfEmpty(0) // Hvis der ikke findes ID'er, start med 0
                .Max() + 1; // Tag det højeste tal og tilføj 1

            return $"event{nextId}"; // Returnerer det nye ID som "eventX"
        }
    }
}
