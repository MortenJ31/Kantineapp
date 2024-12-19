using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/User")] // Angiver base-URL'en for denne controller
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        // Constructor – her injecter vi IUserRepository, så vi kan bruge det i controlleren
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Henter alle brugere – GET /api/User
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Henter en specifik bruger baseret på ID – GET /api/User/{id}
        [HttpGet("{id}")]
        public async Task<User?> GetById(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        // Opretter en ny bruger – POST /api/User
        [HttpPost]
        public async Task<User> Create([FromBody] User newUser)
        {
            return await _userRepository.AddUserAsync(newUser);
        }

        // Henter brugere baseret på deres rolle – GET /api/User/role/{role}
        [HttpGet("role/{role}")]
        public async Task<IEnumerable<User>> GetByRole(Role role)
        {
            return await _userRepository.GetUsersByRoleAsync(role);
        }
    }
}