using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        // POST: api/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Authenticate the user
            var user = await _loginRepository.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

            if (user != null)
            {
                // Authentication successful
                return Ok(new LoginResponse
                {
                    IsSuccessful = true,
                    Message = "Login successful.",
                    BrugerId = user
                });
            }

            // Authentication failed
            return Unauthorized(new LoginResponse
            {
                IsSuccessful = false,
                Message = "Invalid email or password."
            });
        }
    }
    
    

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
        public Bruger? BrugerId { get; set; }
    }
}