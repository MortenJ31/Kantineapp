using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Event?> GetEventById(string id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        [HttpPost]
        public async Task<Event> Create([FromBody] Event newEvent)
        {
            return await _eventRepository.AddEventAsync(newEvent);
        }

        [HttpPut("{id}")]
        public async Task<Event?> Update(string id, [FromBody] Event updatedEvent)
        {
            if (id != updatedEvent.Id)
            {
                throw new ArgumentException("Event ID mismatch");
            }

            return await _eventRepository.UpdateEventAsync(id, updatedEvent);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _eventRepository.DeleteEventAsync(id);
        }
    }
}