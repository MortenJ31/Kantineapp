using Core.Models;


namespace ServerAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Event> _events = new()
        {
            new Event { Id = 1, Name = "Company Party", Date = DateTime.Now, Location = "Main Hall" },
            new Event { Id = 2, Name = "Team Meeting", Date = DateTime.Now.AddDays(1), Location = "Conference Room" }
        };

        public Task<IEnumerable<Event>> GetAllAsync() => Task.FromResult(_events.AsEnumerable());

        public Task<Event> GetByIdAsync(int id) => Task.FromResult(_events.FirstOrDefault(e => e.Id == id));

        public Task<Event> AddAsync(Event newEvent)
        {
            newEvent.Id = _events.Max(e => e.Id) + 1; // Auto-increment ID
            _events.Add(newEvent);
            return Task.FromResult(newEvent);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var eventToRemove = _events.FirstOrDefault(e => e.Id == id);
            if (eventToRemove == null) return Task.FromResult(false);

            _events.Remove(eventToRemove);
            return Task.FromResult(true);
        }
    }
}