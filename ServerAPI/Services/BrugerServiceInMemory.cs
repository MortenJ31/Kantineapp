using ServerAPI.Models;
using ServerAPI.Repositories;
using System.Collections.Concurrent;

namespace ServerAPI.Services
{
    public class BrugerServiceInMemory : IBrugerService
    {
        private readonly IBrugerRepository _brugerRepository;

        public BrugerServiceInMemory(IBrugerRepository brugerRepository)
        {
            _brugerRepository = brugerRepository;
        }

        public Task<IEnumerable<Bruger>> GetAllUsersAsync()
        {
            return _brugerRepository.GetAllUsersAsync();
        }

        public Task<Bruger?> GetUserByIdAsync(string id)
        {
            return _brugerRepository.GetUserByIdAsync(id);
        }

        public Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle role)
        {
            return _brugerRepository.GetUsersByRoleAsync(role);
        }

        public Task<Bruger> AddUserAsync(Bruger user)
        {
            return _brugerRepository.AddUserAsync(user);
        }
    }
}
