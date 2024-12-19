using Core.Models;

namespace Kantineapp.Services
{
    public class TaskItemServiceInMemory : ITaskItemService
    {
        private readonly List<TaskItem> _taskItems;

        public TaskItemServiceInMemory()
        {
            // Mockdata til test
            _taskItems = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = "taskitem1",
                    Description = "Forbered bordopsætning",
                    StartTime = "09:00",
                    EndTime = "10:30",
                    Status = Status.IkkePåbegyndt,
                    EventId = "event1",
                    ResponsibleForTask = new List<string> { "user3" },
                    TaskType = TaskType.Bordopsætning
                },
                new TaskItem
                {
                    Id = "taskitem2",
                    Description = "Forbered maden",
                    StartTime = "10:00",
                    EndTime = "12:00",
                    Status = Status.Påbegyndt,
                    EventId = "event2",
                    ResponsibleForTask = new List<string> { "user3" },
                    TaskType = TaskType.Madlavning
                }
            };
        }

        public Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync()
        {
            return Task.FromResult(_taskItems.AsEnumerable());
        }

        public Task<TaskItem?> GetTaskItemByIdAsync(string id)
        {
            var taskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(taskItem);
        }

        public Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem)
        {
            newTaskItem.Id = Guid.NewGuid().ToString(); // Generer et unikt ID
            _taskItems.Add(newTaskItem);
            return Task.FromResult(newTaskItem);
        }

        public Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem)
        {
            var existingTaskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            if (existingTaskItem != null)
            {
                _taskItems.Remove(existingTaskItem);
                updatedTaskItem.Id = id; // Bevar det oprindelige ID
                _taskItems.Add(updatedTaskItem);
                return Task.FromResult(updatedTaskItem);
            }
            return Task.FromResult<TaskItem?>(null);
        }

        public Task<bool> DeleteTaskItemAsync(string id)
        {
            var taskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            if (taskItem != null)
            {
                _taskItems.Remove(taskItem);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
