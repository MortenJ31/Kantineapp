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
            return await _httpClient.GetFromJsonAsync<IEnumerable<Event>>("api/events");
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Event>($"api/events/{id}");
        }

        public async Task<Event> AddEventAsync(Event newEvent)
        {
            var response = await _httpClient.PostAsJsonAsync("api/events", newEvent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/events/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Event?> UpdateEventAsync(string id, Event updatedEvent)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/events/{id}", updatedEvent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }
    }
}