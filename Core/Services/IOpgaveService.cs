using Core.Models;

namespace Core.Services
{
    public interface IOpgaveService
    {
        Task<List<Opgave>> GetOpgaverByEventIdAsync(string eventId); // Hent alle opgaver for et event
        Task<List<User>> GetMedarbejdereAsync(); // Hent alle medarbejdere med rollen "Medarbejder"
        Task AddOpgaveAsync(string eventId, Opgave opgave); // Tilføj en opgave til et event
        Task UpdateOpgaveAsync(Opgave opgave); // Opdater en opgave
        Task DeleteOpgaveAsync(string eventId, string opgaveId); // Slet en opgave
    }
}