using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/events")] // Base route for endpoints relateret til events
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        // Constructor – injecter IEventRepository for at kunne udføre event-operationer
        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // Henter alle events – GET /api/events
        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _eventRepository.GetAllEventsAsync(); // Henter alt fra event-kollektionen
        }

        // Henter et specifikt event baseret på ID – GET /api/events/{id}
        [HttpGet("{id}")]
        public async Task<Event?> GetEventById(string id)
        {
            return await _eventRepository.GetEventByIdAsync(id); // Finder eventet med det angivne ID
        }

        // Opretter et nyt event – POST /api/events
        [HttpPost]
        public async Task<Event> Create([FromBody] Event newEvent)
        {
            return await _eventRepository.AddEventAsync(newEvent); // Tilføjer et nyt event til databasen
        }

        // Opdaterer et eksisterende event – PUT /api/events/{id}
        [HttpPut("{id}")]
        public async Task<Event?> Update(string id, [FromBody] Event updatedEvent)
        {
            if (id != updatedEvent.Id)
            {
                throw new ArgumentException("Event ID mismatch"); // Sikrer at ID'erne matcher
            }

            return await _eventRepository.UpdateEventAsync(id, updatedEvent); // Opdaterer eventet
        }

        // Sletter et event – DELETE /api/events/{id}
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _eventRepository.DeleteEventAsync(id); // Sletter eventet baseret på ID
        }
    }
}
