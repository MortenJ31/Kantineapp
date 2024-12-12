using Core.Models;
using MongoDB.Driver;
using System.Net.Http.Json;


namespace Kantineapp.Services
{
    public class OpgaveService : IOpgaveService
    {
        private readonly HttpClient _httpClient;

        public OpgaveService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Opgave>> GetAllOpgaverAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Opgave>>("api/Opgave");
        }

        public async Task<Opgave?> GetOpgaveByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Opgave>($"api/Opgave/{id}");
        }

        public async Task<Opgave> AddOpgaveAsync(Opgave newOpgave)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Opgave", newOpgave);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Opgave>();
        }

        

        public async Task<Opgave?> UpdateOpgaveAsync(string id, Opgave updatedOpgave)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Opgave/{id}", updatedOpgave);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Opgave>();
        }

        public async Task<bool> DeleteOpgaveAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Opgave/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}