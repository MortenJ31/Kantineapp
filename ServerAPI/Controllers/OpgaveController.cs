using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;


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



        [HttpPut("{id}")]
        public async Task<Opgave?> Update(string id, [FromBody] Opgave updatedOpgave)
        {
            if (id != updatedOpgave.Id)
            {
                throw new ArgumentException("Opgave ID stemmer ikke overens.");
            }

            return await _opgaveRepository.UpdateOpgaveAsync(id, updatedOpgave);
        }


        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _opgaveRepository.DeleteOpgaveAsync(id);
        }
    }
}