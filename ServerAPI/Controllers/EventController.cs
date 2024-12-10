using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<Event>> GetAll() => _service.GetAllAsync();

        [HttpGet("{id}")]
        public Task<Event> GetById(int id) => _service.GetByIdAsync(id);

        [HttpPost]
        public Task<Event> Add([FromBody] Event newEvent) => _service.AddAsync(newEvent);

        [HttpDelete("{id}")]
        public Task<bool> Delete(int id) => _service.DeleteAsync(id);
    }
}