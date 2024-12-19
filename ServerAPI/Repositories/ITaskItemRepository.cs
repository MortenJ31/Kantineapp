using Core.Models;

namespace ServerAPI.Repositories
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync();
        Task<TaskItem?> GetTaskItemByIdAsync(string id);
        Task<IEnumerable<TaskItem>> GetTaskItemsByEventIdAsync(string eventId);
        Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem);
        Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem);
        Task<bool> DeleteTaskItemAsync(string id);
    }
}