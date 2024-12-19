using Microsoft.AspNetCore.Mvc;
using Core.Models;
using ServerAPI.Repositories;

[ApiController]
[Route("api/login")] // Base route for login-relaterede endpoints
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _loginRepository;

    // Constructor – her injecter vi ILoginRepository for at håndtere loginlogik
    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    // Endpoint til login – POST /api/login
    [HttpPost]
    public async Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        // Tjekker om både e-mail og password er angivet
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return new LoginResponse
            {
                IsSuccessful = false,
                Message = "E-mail og Password er nødvendigt" // Fejlbesked hvis data mangler
            };
        }

        // Forsøger at autentificere brugeren
        var user = await _loginRepository.AuthenticateAsync(request.Email, request.Password);

        if (user == null)
        {
            return new LoginResponse
            {
                IsSuccessful = false,
                Message = "Ugyldigt E-mail eller Adgangskode" // Fejlbesked hvis login fejler
            };
        }

        // Returnerer succesfuldt login
        return new LoginResponse
        {
            IsSuccessful = true,
            Role = user.Role.ToString(), // Sender brugerens rolle som en del af svaret
            Message = "Login Vellykket"
        };
    }
}