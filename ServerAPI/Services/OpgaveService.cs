using Core.Models;
using MongoDB.Driver;
using ServerAPI.Repositories;

namespace ServerAPI.Services
{
    public class OpgaveService : IOpgaveService
    {
        private readonly IOpgaveRepository _opgaveRepository;

        public OpgaveService(IOpgaveRepository opgaveRepository)
        {
           _opgaveRepository = opgaveRepository;
        }

        public async Task<IEnumerable<Opgave>> GetAllOpgaverAsync()
        {
            var opgaver = await _opgaveRepository.GetAllOpgaverAsync();
            return opgaver.ToList();
        }

        public async Task<Opgave?> GetOpgaveByIdAsync(string id)
        {
            return await _opgaveRepository.GetOpgaveByIdAsync(id);
        }

        public async Task<IEnumerable<Opgave>> GetOpgaveByEventIdAsync(string eventId)
        {
            var opgave = await _opgaveRepository.GetOpgaveByEventIdAsync(eventId);
            return opgave.ToList();
        }

        public async Task<Opgave> AddOpgaveAsync(Opgave nyOpgave)
        {
            return await _opgaveRepository.AddOpgaveAsync(nyOpgave);
        }

        public async Task UpdateOpgaveAsync(string id, Opgave updatedOpgave)
        {
            await _opgaveRepository.UpdateOpgaveAsync(id, updatedOpgave);
        }

        public async Task<bool> DeleteOpgaveAsync(string id)
        {
            return await _opgaveRepository.DeleteOpgaveAsync(id);
        }
    }
}