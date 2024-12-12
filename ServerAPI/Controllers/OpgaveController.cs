using ServerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpgaveController : ControllerBase
    {
        private readonly IOpgaveRepository _opgaveRepository;

        public OpgaveController(IOpgaveRepository opgaveRepository)
        {
            _opgaveRepository = opgaveRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Opgave>> GetAll()
        {
            return await _opgaveRepository.GetAllOpgaverAsync();
        }

        [HttpGet("{id}")]
        public async Task<Opgave?> GetById(string id)
        {
            return await _opgaveRepository.GetOpgaveByIdAsync(id);
        }

        [HttpPost]
        public async Task<Opgave> Create([FromBody] Opgave nyOpgave)
        {
            return await _opgaveRepository.AddOpgaveAsync(nyOpgave);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _opgaveRepository.DeleteOpgaveAsync(id);
        }
    }
}