using Core.Models;


namespace Kantineapp.Services
{
    public interface IOpgaveService
    {
        Task<IEnumerable<Opgave>> GetAllOpgaverAsync();
        Task<Opgave?> GetOpgaveByIdAsync(string id);
        //Task<IEnumerable<Opgave>> GetOpgaveByEventIdAsync(string eventId);
        Task<Opgave> AddOpgaveAsync(Opgave nyOpgave);
        Task<Opgave?> UpdateOpgaveAsync(string id, Opgave updatedOpgave);
        Task<bool> DeleteOpgaveAsync(string id);
    }
}