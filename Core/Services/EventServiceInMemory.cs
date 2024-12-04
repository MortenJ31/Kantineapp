namespace Core.Services
{
    using Core.Models;
    using System.Collections.Concurrent;

    public class EventServiceInMemory : IEventService
    {
        private readonly ConcurrentDictionary<string, Event> _events = new();

        public EventServiceInMemory()
        {
            // Mock data
            var mockData = new List<Event>
            {
                new Event { Id = Guid.NewGuid().ToString(), Name = "Sommerfest", Dato = DateTime.Now.AddDays(10), Sted = "Parken", DeltagerAntal = 50 },
                new Event { Id = Guid.NewGuid().ToString(), Name = "Julefrokost", Dato = DateTime.Now.AddMonths(1), Sted = "Kantinen", DeltagerAntal = 30 },
            };

            foreach (var ev in mockData)
            {
                _events[ev.Id!] = ev;
            }
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            return Task.FromResult(_events.Values.ToList());
        }

        public Task<Event?> GetEventByIdAsync(string id)
        {
            _events.TryGetValue(id, out var ev);
            return Task.FromResult(ev);
        }

        public Task AddEventAsync(Event newEvent)
        {
            newEvent.Id = Guid.NewGuid().ToString();
            _events[newEvent.Id!] = newEvent;
            return Task.CompletedTask;
        }

        public Task UpdateEventAsync(Event updatedEvent)
        {
            if (!string.IsNullOrEmpty(updatedEvent.Id) && _events.ContainsKey(updatedEvent.Id))
            {
                _events[updatedEvent.Id] = updatedEvent;
            }
            return Task.CompletedTask;
        }

        public Task DeleteEventAsync(string id)
        {
            _events.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}