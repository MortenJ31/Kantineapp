namespace Core.Services
{
    using Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(string id);
        Task AddEventAsync(Event newEvent);
        Task UpdateEventAsync(Event updatedEvent);
        Task DeleteEventAsync(string id);
    }
}