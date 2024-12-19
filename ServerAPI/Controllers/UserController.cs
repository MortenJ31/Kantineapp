using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<User?> GetById(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        [HttpPost]
        public async Task<User> Create([FromBody] User newUser)
        {
            return await _userRepository.AddUserAsync(newUser);
        }

        [HttpGet("role/{role}")]
        public async Task<IEnumerable<User>> GetByRole(Role role)
        {
            return await _userRepository.GetUsersByRoleAsync(role);
        }
    }
}