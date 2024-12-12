using Core.Models;


namespace ServerAPI.Repositories
{
    public interface IBrugerRepository
    {
        Task<IEnumerable<Bruger>> GetAllUsersAsync();
        Task<Bruger?> GetUserByIdAsync(string id);
        Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle Rolle);
        Task<Bruger> AddUserAsync(Bruger newUser);
        
    }
}