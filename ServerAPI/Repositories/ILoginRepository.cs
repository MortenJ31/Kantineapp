using Core.Models;

namespace ServerAPI.Repositories
{
    public interface ILoginRepository
    {
        Task<User?> AuthenticateAsync(string email, string password);
    }
}