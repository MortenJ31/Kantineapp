using Core.Models;
using ServerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;


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

        // Hent alle events
        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        // Hent event med ID
        [HttpGet("{id}")]
        public async Task<Event?> GetEventById(string id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        // Tilføj ny event
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
                throw new ArgumentException("EventID stemmer ikke overens");
            }
            return await _eventRepository.UpdateEventAsync(id, updatedEvent);
        }

        // Slet event med ID
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _eventRepository.DeleteEventAsync(id);
        }
    }
}