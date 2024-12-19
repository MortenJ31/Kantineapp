using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Base route for denne controller, bruger controller-navnet (TaskItem)
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;

        // Constructor – injecter ITaskItemRepository for at kunne bruge det i controlleren
        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        // Henter alle TaskItems – GET /api/TaskItem
        [HttpGet]
        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            return await _taskItemRepository.GetAllTaskItemsAsync();
        }

        // Henter en specifik TaskItem baseret på ID – GET /api/TaskItem/{id}
        [HttpGet("{id}")]
        public async Task<TaskItem?> GetById(string id)
        {
            return await _taskItemRepository.GetTaskItemByIdAsync(id);
        }

        // Opretter en ny TaskItem – POST /api/TaskItem
        [HttpPost]
        public async Task<TaskItem> Create(TaskItem newTaskItem)
        {
            return await _taskItemRepository.AddTaskItemAsync(newTaskItem);
        }

        // Opdaterer en eksisterende TaskItem – PUT /api/TaskItem/{id}
        [HttpPut("{id}")]
        public async Task<TaskItem?> Update(string id, [FromBody] TaskItem updatedTaskItem)
        {
            if (id != updatedTaskItem.Id)
            {
                throw new ArgumentException("TaskItem ID mismatch."); // Tjekker om ID'et matcher
            }

            return await _taskItemRepository.UpdateTaskItemAsync(id, updatedTaskItem);
        }

        // Sletter en TaskItem – DELETE /api/TaskItem/{id}
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _taskItemRepository.DeleteTaskItemAsync(id);
        }
    }
}
