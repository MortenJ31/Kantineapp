using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly IMongoCollection<TaskItem> _taskItemCollection;

        public TaskItemRepository(MongoDbService mongoDbService)
        {
            _taskItemCollection = mongoDbService.GetTaskItemCollection();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync()
        {
            return await _taskItemCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskItemByIdAsync(string id)
        {
            return await _taskItemCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsByEventIdAsync(string eventId)
        {
            return await _taskItemCollection.Find(o => o.EventId == eventId).ToListAsync();
        }

        public async Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem)
        {
            if (string.IsNullOrEmpty(newTaskItem.Id))
            {
                newTaskItem.Id = await GetNextTaskIdAsync();
            }
            await _taskItemCollection.InsertOneAsync(newTaskItem);
            return newTaskItem;
        }

        public async Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem)
        {
            var result = await _taskItemCollection.ReplaceOneAsync(o => o.Id == id, updatedTaskItem);

            if (result.MatchedCount == 0)
            {
                return null;
            }

            return updatedTaskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(string id)
        {
            var result = await _taskItemCollection.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }

        private async Task<string> GetNextTaskIdAsync()
        {
            var highestIdTask = await _taskItemCollection
                .Find(_ => true)
                .SortByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            if (highestIdTask == null || string.IsNullOrEmpty(highestIdTask.Id))
            {
                return "taskitem1";
            }

            var currentId = int.Parse(highestIdTask.Id.Substring(8));
            return $"taskitem{currentId + 1}";
        }
    }
}
