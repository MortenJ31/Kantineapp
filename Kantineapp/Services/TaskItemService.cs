using Core.Models;
using MongoDB.Driver;
using System.Net.Http.Json;

namespace Kantineapp.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly HttpClient _httpClient;

        public TaskItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TaskItem>>("api/TaskItem");
        }

        public async Task<TaskItem?> GetTaskItemByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<TaskItem>($"api/TaskItem/{id}");
        }

        public async Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TaskItem", newTaskItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/TaskItem/{id}", updatedTaskItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task<bool> DeleteTaskItemAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/TaskItem/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}