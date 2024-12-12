using Core.Models;
using ServerAPI.Repositories;




namespace ServerAPI.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        public async Task<Event?> GetByIdAsync(string id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        public async Task<Event> AddAsync(Event nyEvent)
        {
            return await _eventRepository.AddEventAsync(nyEvent);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _eventRepository.DeleteEventAsync(id);
        }
    }
}