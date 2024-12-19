using Core.Models;

namespace ServerAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(Role role);
        Task<User> AddUserAsync(User newUser);
    }
}