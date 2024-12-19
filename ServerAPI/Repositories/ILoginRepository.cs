using Core.Models;

namespace ServerAPI.Repositories
{
    public interface ILoginRepository
    {
        // Tjekker brugerens loginoplysninger og returnerer brugeren, hvis email og password matcher
        Task<User?> AuthenticateAsync(string email, string password);
    }
}