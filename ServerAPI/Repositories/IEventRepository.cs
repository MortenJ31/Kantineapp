using Core.Models;


namespace ServerAPI.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(string id);
        Task<Event> AddEventAsync(Event newEvent);
        Task<bool> DeleteEventAsync(string id);
    }
}