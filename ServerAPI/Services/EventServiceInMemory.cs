using Core.Models;
using ServerAPI.Repositories;


namespace ServerAPI.Services
{
    public class EventServiceInMemory : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventServiceInMemory(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Task<IEnumerable<Event>> GetAllAsync()
        {
            return _eventRepository.GetAllAsync();
        }

        public Task<Event?> GetByIdAsync(string id)
        {
            return _eventRepository.GetByIdAsync(id);
        }

        public Task<Event> AddAsync(Event newEvent)
        {
            return _eventRepository.AddAsync(newEvent);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return _eventRepository.DeleteAsync(id);
        }
    }
}
