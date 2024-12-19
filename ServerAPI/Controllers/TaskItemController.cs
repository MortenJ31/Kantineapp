using Core.Models;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            return await _taskItemRepository.GetAllTaskItemsAsync();
        }

        [HttpGet("{id}")]
        public async Task<TaskItem?> GetById(string id)
        {
            return await _taskItemRepository.GetTaskItemByIdAsync(id);
        }

        [HttpPost]
        public async Task<TaskItem> Create(TaskItem newTaskItem)
        {
            return await _taskItemRepository.AddTaskItemAsync(newTaskItem);
        }

        [HttpPut("{id}")]
        public async Task<TaskItem?> Update(string id, [FromBody] TaskItem updatedTaskItem)
        {
            if (id != updatedTaskItem.Id)
            {
                throw new ArgumentException("TaskItem ID mismatch.");
            }

            return await _taskItemRepository.UpdateTaskItemAsync(id, updatedTaskItem);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _taskItemRepository.DeleteTaskItemAsync(id);
        }
    }
}