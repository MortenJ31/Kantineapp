using Core.Models;

namespace Kantineapp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(Role role);
        Task<User> AddUserAsync(User user);
    }
}