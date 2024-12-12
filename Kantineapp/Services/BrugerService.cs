using Core.Models;
using System.Net.Http.Json;


namespace Kantineapp.Services
{
    public class BrugerService : IBrugerService
    {
        private readonly HttpClient _httpClient;

        public BrugerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Bruger>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Bruger>>("api/Bruger");
        }

        public async Task<Bruger?> GetUserByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Bruger>($"api/Bruger/{id}");
        }

        public async Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle Rolle)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Bruger>>($"api/Bruger/rolle/{Rolle}");
        }

        public async Task<Bruger> AddUserAsync(Bruger newBruger)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Bruger", newBruger);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Bruger>();
        }

        
    }
}