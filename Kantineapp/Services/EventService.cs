using Core.Models;
using System.Net.Http.Json;




namespace Kantineapp.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Event>>("api/Event");
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Event>($"api/Event/{id}");
        }

        public async Task<Event> AddEventAsync(Event newEvent)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Event", newEvent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Event/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Event/{id}", updatedEvent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }
    }
}