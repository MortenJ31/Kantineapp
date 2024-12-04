namespace Core.Services
{
    using Core.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventService : IEventService
    {
        private readonly List<Event> _database = new();

        public Task<List<Event>> GetAllEventsAsync()
        {
            return Task.FromResult(_database);
        }

        public Task<Event?> GetEventByIdAsync(string id)
        {
            var ev = _database.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(ev);
        }

        public Task AddEventAsync(Event newEvent)
        {
            newEvent.Id = Guid.NewGuid().ToString();
            _database.Add(newEvent);
            return Task.CompletedTask;
        }

        public Task UpdateEventAsync(Event updatedEvent)
        {
            var existing = _database.FirstOrDefault(e => e.Id == updatedEvent.Id);
            if (existing != null)
            {
                existing.Name = updatedEvent.Name;
                existing.Dato = updatedEvent.Dato;
                existing.Sted = updatedEvent.Sted;
                existing.DeltagerAntal = updatedEvent.DeltagerAntal;
            }
            return Task.CompletedTask;
        }

        public Task DeleteEventAsync(string id)
        {
            var existing = _database.FirstOrDefault(e => e.Id == id);
            if (existing != null)
            {
                _database.Remove(existing);
            }
            return Task.CompletedTask;
        }
    }
}