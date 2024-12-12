using Core.Models;
using ServerAPI.Repositories;

namespace ServerAPI.Services
{
    public class OpgaveServiceInMemory : IOpgaveService
    {
        private readonly IOpgaveRepository _opgaveRepository;

        public OpgaveServiceInMemory(IOpgaveRepository opgaveRepository)
        {
            _opgaveRepository = opgaveRepository;
        }

        public  Task<IEnumerable<Opgave>> GetAllOpgaverAsync()
        {
            return _opgaveRepository.GetAllOpgaverAsync();
        }

        public Task<Opgave?> GetOpgaveByIdAsync(string id)
        {
            return _opgaveRepository.GetOpgaveByIdAsync(id);
        }

        public Task<IEnumerable<Opgave>> GetOpgaveByEventIdAsync(string eventId)
        {
            return _opgaveRepository.GetOpgaveByEventIdAsync(eventId);
        }

        public Task<Opgave> AddOpgaveAsync(Opgave nyOpgave)
        {
            return _opgaveRepository.AddOpgaveAsync(nyOpgave);
        }

        public Task UpdateOpgaveAsync(Opgave updatedOpgave)
        {
            return _opgaveRepository.UpdateOpgaveAsync(updatedOpgave);
        }

        public Task<bool> DeleteOpgaveAsync(string id)
        {
            return _opgaveRepository.DeleteOpgaveAsync(id);
        }
    }
}
