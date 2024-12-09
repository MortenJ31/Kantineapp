namespace Core.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOpgaveService
    {
        Task<List<Opgave>> GetAllOpgaverAsync();
        Task<Opgave?> GetOpgaveByIdAsync(string id);
        Task<List<Opgave>> GetOpgaverByEventIdAsync(string eventId); // Ny metode
        Task AddOpgaveAsync(Opgave opgave);
        Task UpdateOpgaveAsync(Opgave updatedOpgave);
        Task DeleteOpgaveAsync(string id);
    }
}