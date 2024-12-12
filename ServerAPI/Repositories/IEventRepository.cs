using ServerAPI.Models;


namespace ServerAPI.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(string id);
        Task<Event> AddAsync(Event newEvent);
        Task<bool> DeleteAsync(string id);
    }
}