using Core.Models;
using System.Collections.Concurrent;

namespace Core.Services
{
    public class EventServiceInMemory : IEventService
    {
        private readonly ConcurrentDictionary<string, Events> _events = new();

        public EventServiceInMemory()
        {
            // Mockdata
            var mockData = new List<Events>
            {
                new Events 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Name = "Sommerfest", 
                    Dato = DateTime.Now.AddDays(10), 
                    Lokation = "Parken", 
                    DeltagerAntal = 50, 
                    MadValg = "Grill", 
                    SærligeØnsker = "Vegetarisk mad", 
                    Kunde = "Firma A", 
                    Opgaver = new List<Opgave>() // Tom liste for opgaver
                },
                new Events 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Name = "Julefrokost", 
                    Dato = DateTime.Now.AddMonths(1), 
                    Lokation = "Kantinen", 
                    DeltagerAntal = 30, 
                    MadValg = "Traditionel", 
                    SærligeØnsker = "Ingen alkohol", 
                    Kunde = "Firma B", 
                    Opgaver = new List<Opgave>() // Tom liste for opgaver
                }
            };

            foreach (var ev in mockData)
            {
                _events[ev.Id!] = ev;
            }
        }

        public Task<List<Events>> GetAllEventsAsync()
        {
            return Task.FromResult(_events.Values.ToList());
        }

        public Task<Events?> GetEventByIdAsync(string id)
        {
            _events.TryGetValue(id, out var ev);
            return Task.FromResult(ev);
        }

        public Task AddEventAsync(Events newEvent)
        {
            newEvent.Id = Guid.NewGuid().ToString();
            newEvent.Opgaver = new List<Opgave>(); // Sikrer, at nye events har en tom liste af opgaver
            _events[newEvent.Id!] = newEvent;
            return Task.CompletedTask;
        }

        public Task UpdateEventAsync(Events updatedEvent)
        {
            if (updatedEvent.Id != null && _events.ContainsKey(updatedEvent.Id))
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
