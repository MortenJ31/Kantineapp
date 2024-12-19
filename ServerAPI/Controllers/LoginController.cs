using Microsoft.AspNetCore.Mvc;
using Core.Models;
using ServerAPI.Repositories;
[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [HttpPost]
    public async Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return new LoginResponse
            {
                IsSuccessful = false,
                Message = "E-mail og Password er nødvendigt"
            };
        }

        var user = await _loginRepository.AuthenticateAsync(request.Email, request.Password);

        if (user == null)
        {
            return new LoginResponse
            {
                IsSuccessful = false,
                Message = "Ugyldigt E-mail eller Adgangskode"
            };
        }

        return new LoginResponse
        {
            IsSuccessful = true,
            Role = user.Role.ToString(),
            Message = "Login Vellykket"
        };
    }
}