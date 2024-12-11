using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Login
{
    public interface ILoginService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User?> GetCurrentUserAsync();
    }
}
