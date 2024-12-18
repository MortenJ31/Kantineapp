using Core.Models;

namespace ServerAPI.Repositories
{
    public interface ILoginRepository
    {
        Task<Bruger?> AuthenticateAsync(string email, string password);
    }
}