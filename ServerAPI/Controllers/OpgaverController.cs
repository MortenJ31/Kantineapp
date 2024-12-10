using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/opgaver")]
    public class OpgaverController : ControllerBase
    {
        private readonly IOpgaverService _service;

        public OpgaverController(IOpgaverService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<Opgaver>> GetAll() => _service.GetAllAsync();

        [HttpGet("{id}")]
        public Task<Opgaver> GetById(int id) => _service.GetByIdAsync(id);

        [HttpPost]
        public Task<Opgaver> Add([FromBody] Opgaver newOpgaver) => _service.AddAsync(newOpgaver);

        [HttpDelete("{id}")]
        public Task<bool> Delete(int id) => _service.DeleteAsync(id);
    }
}