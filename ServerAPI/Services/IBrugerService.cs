using Core.Models;

namespace ServerAPI.Services
{
    public interface IBrugerService
    {
        Task<IEnumerable<Bruger>> GetAllUsersAsync();
        Task<Bruger?> GetUserByIdAsync(string id);
        Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle role);
        Task<Bruger> AddUserAsync(Bruger newUser);
    }
}