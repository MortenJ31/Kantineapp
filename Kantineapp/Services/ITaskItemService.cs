using Core.Models;

namespace Kantineapp.Services
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync();
        Task<TaskItem?> GetTaskItemByIdAsync(string id);
        Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem);
        Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem);
        Task<bool> DeleteTaskItemAsync(string id);
    }
}