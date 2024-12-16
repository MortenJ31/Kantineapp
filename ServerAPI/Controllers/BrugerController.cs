using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
using Core.Models;


namespace ServerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrugerController : ControllerBase 
{
    private readonly IBrugerRepository _brugerRepository;

    public BrugerController(IBrugerRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }

    //Hent alle brugere
    [HttpGet]
    public async Task<IEnumerable<Bruger>> GetAll()
    {
        return await _brugerRepository.GetAllUsersAsync();
    }

    //Hent bruger med ID
    [HttpGet("{id}")]
    public async Task<Bruger?> GetById(string id)
    {
        return await _brugerRepository.GetUserByIdAsync(id);
    }

    //Tilf�j ny bruger
    [HttpPost]
    public async Task<Bruger> Create([FromBody] Bruger newUser)
    {
        return await _brugerRepository.AddUserAsync(newUser);
    }

    // Hent brugere baseret på rolle
    [HttpGet("rolle/{rolle}")]
    public async Task<IEnumerable<Bruger>> GetByRole(Rolle rolle)
    {
        return await _brugerRepository.GetUsersByRoleAsync(rolle);
    }

}
