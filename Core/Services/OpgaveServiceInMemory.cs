using Core.Models;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OpgaveServiceInMemory : IOpgaveService
    {
        private readonly ConcurrentDictionary<string, List<Opgave>> _opgaverByEvent = new();
        private readonly List<User> _users = new();

        public OpgaveServiceInMemory()
        {
            // Mock data for brugere
            _users = new List<User>
            {
                new User { Id = "1", Navn = "Anna", Kompetence = "Organisering", Rolle = "Medarbejder" },
                new User { Id = "2", Navn = "Jonas", Kompetence = "Madlavning", Rolle = "Medarbejder" },
                new User { Id = "3", Navn = "Emma", Kompetence = "Servering", Rolle = "Admin" }
            };

            // Mock data for opgaver
            var event1Id = Guid.NewGuid().ToString();
            _opgaverByEvent[event1Id] = new List<Opgave>
            {
                new Opgave
                {
                    OpgaveId = Guid.NewGuid().ToString(),
                    EventId = event1Id,
                    MedarbejderIds = new List<string> { "1", "2" },
                    Navn = "Opsætning af borde",
                    Status = "Ikke Påbegyndt"
                }
            };
        }

        public Task<List<Opgave>> GetOpgaverByEventIdAsync(string eventId)
        {
            if (_opgaverByEvent.TryGetValue(eventId, out var opgaver))
            {
                return Task.FromResult(opgaver);
            }
            return Task.FromResult(new List<Opgave>());
        }

        public Task<List<User>> GetMedarbejdereAsync()
        {
            // Returner kun brugere med rollen "Medarbejder"
            return Task.FromResult(_users.Where(u => u.Rolle == "Medarbejder").ToList());
        }

        public Task AddOpgaveAsync(string eventId, Opgave opgave)
        {
            if (!_opgaverByEvent.ContainsKey(eventId))
            {
                _opgaverByEvent[eventId] = new List<Opgave>();
            }

            opgave.OpgaveId = Guid.NewGuid().ToString();
            opgave.EventId = eventId;
            _opgaverByEvent[eventId].Add(opgave);
            return Task.CompletedTask;
        }

        public Task UpdateOpgaveAsync(Opgave opgave)
        {
            if (!string.IsNullOrEmpty(opgave.EventId) &&
                _opgaverByEvent.TryGetValue(opgave.EventId, out var opgaver))
            {
                var index = opgaver.FindIndex(o => o.OpgaveId == opgave.OpgaveId);
                if (index != -1)
                {
                    opgaver[index] = opgave;
                }
            }
            return Task.CompletedTask;
        }

        public Task DeleteOpgaveAsync(string eventId, string opgaveId)
        {
            if (_opgaverByEvent.TryGetValue(eventId, out var opgaver))
            {
                opgaver.RemoveAll(o => o.OpgaveId == opgaveId);
            }
            return Task.CompletedTask;
        }
    }
}
