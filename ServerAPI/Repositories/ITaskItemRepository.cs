using Core.Models;

namespace ServerAPI.Repositories
{
    public interface ITaskItemRepository
    {
        // Henter alle TaskItems fra databasen
        Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync();

        // Finder en specifik TaskItem baseret på dens ID
        Task<TaskItem?> GetTaskItemByIdAsync(string id);

        // Henter alle TaskItems, der er tilknyttet et bestemt Event via EventId
        Task<IEnumerable<TaskItem>> GetTaskItemsByEventIdAsync(string eventId);

        // Tilføjer en ny TaskItem til databasen
        Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem);

        // Opdaterer en eksisterende TaskItem baseret på dens ID
        Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem);

        // Sletter en TaskItem fra databasen baseret på dens ID
        Task<bool> DeleteTaskItemAsync(string id);
    }
}