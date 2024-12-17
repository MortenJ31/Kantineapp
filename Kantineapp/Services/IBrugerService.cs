using Core.Models;

namespace Kantineapp.Services
{
    public interface IBrugerService
    {
        Task<IEnumerable<Bruger>> GetAllUsersAsync();
        Task<Bruger?> GetUserByIdAsync(string id);
        Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle Rolle);
        Task<Bruger> AddUserAsync(Bruger bruger);
        
    }
}