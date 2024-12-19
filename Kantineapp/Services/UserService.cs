using Core.Models;
using System.Net.Http.Json;

namespace Kantineapp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("api/User");
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/User/{id}");
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Role role)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"api/User/role/{role}");
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User", newUser);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}