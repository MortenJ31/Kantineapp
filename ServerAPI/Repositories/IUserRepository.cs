using Core.Models;

namespace ServerAPI.Repositories
{
    public interface IUserRepository
    {
        // Henter alle brugere fra databasen
        Task<IEnumerable<User>> GetAllUsersAsync();

        // Finder en bestemt bruger baseret på ID
        Task<User?> GetUserByIdAsync(string id);

        // Henter alle brugere, der har en bestemt rolle
        Task<IEnumerable<User>> GetUsersByRoleAsync(Role role);

        // Tilføjer en ny bruger til databasen
        Task<User> AddUserAsync(User newUser);
    }
}