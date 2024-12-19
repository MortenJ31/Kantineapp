using Core.Models;

namespace Kantineapp.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(string id);
        Task<Event> AddEventAsync(Event newEvent);
        Task<Event?> UpdateEventAsync(string id, Event updatedEvent);
        Task<bool> DeleteEventAsync(string id);
    }
}