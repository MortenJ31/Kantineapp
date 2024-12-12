using ServerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Repositories
{
    public interface IBrugerRepository
    {
        Task<IEnumerable<Bruger>> GetAllUsersAsync();
        Task<Bruger?> GetUserByIdAsync(string id);
        Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle role);
        Task<Bruger> AddUserAsync(Bruger newUser);
        
    }
}