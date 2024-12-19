using Core.Models;

namespace Kantineapp.Services
{
    public class EventServiceInMemory : IEventService
    {
        private readonly List<Event> _events;

        public EventServiceInMemory()
        {
            // Mockdata
            _events = new List<Event>
            {
                new Event
                {
                    Id = "event1",
                    Name = "Bryllup",
                    Date = DateTime.UtcNow.AddDays(7),
                    Location = "København",
                    Participants = 50,
                    FoodChoice = "Vegetar",
                    SpecialRequests = "Ingen nødder",
                    Customer = "Thomas",
                    UserID = "user1"
                },
                new Event
                {
                    Id = "event2",
                    Name = "Firmafest",
                    Date = DateTime.UtcNow.AddDays(14),
                    Location = "Aarhus",
                    Participants = 100,
                    FoodChoice = "Buffet",
                    SpecialRequests = "Glutenfri dessert",
                    Customer = "Company X",
                    UserID = "user1"
                }
            };
        }

        public Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return Task.FromResult(_events.AsEnumerable());
        }

        public Task<Event?> GetEventByIdAsync(string id)
        {
            return Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
        }

        public Task<Event> AddEventAsync(Event newEvent)
        {
            newEvent.Id = Guid.NewGuid().ToString(); // Simpelt ID
            _events.Add(newEvent);
            return Task.FromResult(newEvent);
        }

        public Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
            var existingEvent = _events.FirstOrDefault(e => e.Id == id);
            if (existingEvent != null)
            {
                _events.Remove(existingEvent); // Fjern den gamle event
                updatedEvent.Id = id; // Bevar ID
                _events.Add(updatedEvent); // Tilføj den opdaterede event
                return Task.FromResult(updatedEvent);
            }
            return Task.FromResult<Event?>(null);
        }

        public Task<bool> DeleteEventAsync(string id)
        {
            var existingEvent = _events.FirstOrDefault(e => e.Id == id);
            if (existingEvent != null)
            {
                _events.Remove(existingEvent);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
