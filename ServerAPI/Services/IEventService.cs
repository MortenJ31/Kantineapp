using Core.Models;

namespace ServerAPI.Services
{    
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(string id);
        Task<Event> AddAsync(Event newEvent);
        Task<bool> DeleteAsync(string id);
    }
    
}