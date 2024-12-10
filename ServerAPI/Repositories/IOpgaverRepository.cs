using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Repositories
{
    public interface IOpgaverRepository
    {
        Task<IEnumerable<Opgaver>> GetAllAsync();
        Task<Opgaver> GetByIdAsync(int id);
        Task<Opgaver> AddAsync(Opgaver newOpgaver);
        Task<bool> DeleteAsync(int id);
    }
}