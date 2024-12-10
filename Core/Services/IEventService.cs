namespace Core.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        Task<List<Events>> GetAllEventsAsync();
        Task<Events?> GetEventByIdAsync(string id);
        Task AddEventAsync(Events newEvent);
        Task UpdateEventAsync(Events updatedEvent);
        Task DeleteEventAsync(string id);
    }
}