using Core.Models;
using System.Collections.Concurrent;

namespace Core.Services
{
    public class OpgaveServiceInMemory : IOpgaveService
    {
        private readonly ConcurrentDictionary<string, Opgave> _opgaver = new();

        public OpgaveServiceInMemory()
        {
            // Eksempel på hardcodede event-id'er fra EventServiceInMemory
            var event1Id = Guid.NewGuid().ToString(); // Opdater dette til at matche event 1's Id fra EventServiceInMemory
            var event2Id = Guid.NewGuid().ToString(); // Opdater dette til at matche event 2's Id fra EventServiceInMemory

            // Mockdata
            var mockData = new List<Opgave>
            {
                new Opgave { Id = Guid.NewGuid().ToString(), Beskrivelse = "Sæt borde op", Status = "Ikke startet", StartTid = DateTime.Now, SlutTid = DateTime.Now.AddHours(2), EventId = event1Id },
                new Opgave { Id = Guid.NewGuid().ToString(), Beskrivelse = "Gør rent", Status = "Ikke startet", StartTid = DateTime.Now, SlutTid = DateTime.Now.AddHours(3), EventId = event2Id }
            };

            foreach (var opgave in mockData)
            {
                _opgaver[opgave.Id!] = opgave;
            }
        }

        public Task<List<Opgave>> GetAllOpgaverAsync()
        {
            return Task.FromResult(_opgaver.Values.ToList());
        }

        public Task<Opgave?> GetOpgaveByIdAsync(string id)
        {
            _opgaver.TryGetValue(id, out var opgave);
            return Task.FromResult(opgave);
        }

        public Task<List<Opgave>> GetOpgaverByEventIdAsync(string eventId)
        {
            var opgaverForEvent = _opgaver.Values.Where(o => o.EventId == eventId).ToList();
            return Task.FromResult(opgaverForEvent);
        }

        public Task AddOpgaveAsync(Opgave opgave)
        {
            opgave.Id = Guid.NewGuid().ToString();
            _opgaver[opgave.Id!] = opgave;
            return Task.CompletedTask;
        }

        public Task UpdateOpgaveAsync(Opgave updatedOpgave)
        {
            if (updatedOpgave.Id != null && _opgaver.ContainsKey(updatedOpgave.Id))
            {
                _opgaver[updatedOpgave.Id] = updatedOpgave;
            }
            return Task.CompletedTask;
        }

        public Task DeleteOpgaveAsync(string id)
        {
            _opgaver.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
