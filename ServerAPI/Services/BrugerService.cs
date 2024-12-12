using Core.Models;
using ServerAPI.Repositories;

namespace ServerAPI.Services
{
    public class BrugerService : IBrugerService
    {
        private readonly IBrugerRepository _brugerRepository;

        public BrugerService(IBrugerRepository brugerRepository)
        {
            _brugerRepository = brugerRepository;
        }

        public async Task<IEnumerable<Bruger>> GetAllUsersAsync()
        {
            return await _brugerRepository.GetAllUsersAsync();
        }

        public async Task<Bruger?> GetUserByIdAsync(string id)
        {
            return await _brugerRepository.GetUserByIdAsync(id);
        }

        public async Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle role)
        {
            return await _brugerRepository.GetUsersByRoleAsync(role);
        }
        public async Task<Bruger> AddUserAsync(Bruger user)
        {
            return await _brugerRepository.AddUserAsync(user);
        }
    }
}