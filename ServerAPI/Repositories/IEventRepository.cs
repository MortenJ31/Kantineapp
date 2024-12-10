using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task<Event> AddAsync(Event newEvent);
        Task<bool> DeleteAsync(int id);
    }
}