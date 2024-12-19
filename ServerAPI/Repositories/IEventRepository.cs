using Core.Models;

namespace ServerAPI.Repositories
{
    public interface IEventRepository
    {
        // Henter alle events fra databasen
        Task<IEnumerable<Event>> GetAllEventsAsync();

        // Finder et bestemt event baseret på ID
        Task<Event?> GetEventByIdAsync(string id);

        // Tilføjer et nyt event til databasen
        Task<Event> AddEventAsync(Event newEvent);

        // Opdaterer et eksisterende event baseret på ID
        Task<Event?> UpdateEventAsync(string id, Event updatedEvent);

        // Sletter et event fra databasen baseret på ID
        Task<bool> DeleteEventAsync(string id);
    }
}