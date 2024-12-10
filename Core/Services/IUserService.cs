namespace Core.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetUsersByRoleAsync(Rolle role);
        Task<User?> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User updatedUser);
        Task DeleteUserAsync(string id);
    }
}