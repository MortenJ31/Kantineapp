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
            newTaskItem.Id ??= await GetNextTaskIdAsync();
            await _taskItemCollection.InsertOneAsync(newTaskItem);
            return newTaskItem;
        }

        public async Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem)
        {
            if (updatedTaskItem == null) throw new ArgumentNullException(nameof(updatedTaskItem));
            
            var result = await _taskItemCollection.ReplaceOneAsync(o => o.Id == id, updatedTaskItem);
            if (result.MatchedCount == 0) return null;

            return updatedTaskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(string id)
        {
            var result = await _taskItemCollection.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }

        private async Task<string> GetNextTaskIdAsync()
        {
            var maxId = await _taskItemCollection
                .Find(_ => true)
                .Project(t => t.Id.Substring(8)) // Ekstraher suffikset
                .ToListAsync();

            var nextId = maxId
                .Where(id => int.TryParse(id, out _)) // Kun gyldige tal
                .Select(id => int.Parse(id))
                .DefaultIfEmpty(0)
                .Max() + 1;

            return $"taskitem{nextId}";
        }
    }
}
