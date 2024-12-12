using ServerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return await _eventRepository.GetAllAsync();
        }
       

        [HttpGet("{id}")]
        public async Task<Event?> GetById(string id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<Event> Create([FromBody] Event newEvent)
        {
            return await _eventRepository.AddAsync(newEvent);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
           return await _eventRepository.DeleteAsync(id);
        }
    }
}