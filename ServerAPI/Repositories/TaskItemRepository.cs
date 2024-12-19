using Core.Models;
using MongoDB.Driver;
using ServerAPI.Services;

namespace ServerAPI.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly IMongoCollection<TaskItem> _taskItemCollection;

        // Constructor – vi henter TaskItem-kollektionen fra MongoDbService
        public TaskItemRepository(MongoDbService mongoDbService)
        {
            _taskItemCollection = mongoDbService.GetTaskItemCollection();
        }

        // Hent alle TaskItems
        public async Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync()
        {
            return await _taskItemCollection.Find(_ => true).ToListAsync(); // Henter alt fra kollektionen
        }

        // Hent en TaskItem baseret på ID
        public async Task<TaskItem?> GetTaskItemByIdAsync(string id)
        {
            return await _taskItemCollection.Find(o => o.Id == id).FirstOrDefaultAsync(); // Finder det første match på ID
        }

        // Hent TaskItems tilknyttet et bestemt Event baseret på EventId
        public async Task<IEnumerable<TaskItem>> GetTaskItemsByEventIdAsync(string eventId)
        {
            return await _taskItemCollection.Find(o => o.EventId == eventId).ToListAsync(); // Finder TaskItems til et specifikt Event
        }

        // Tilføj en ny TaskItem til databasen
        public async Task<TaskItem> AddTaskItemAsync(TaskItem newTaskItem)
        {
            // Hvis ID mangler, genererer vi et nyt
            newTaskItem.Id ??= await GetNextTaskIdAsync();
            await _taskItemCollection.InsertOneAsync(newTaskItem); // Tilføjer TaskItem til kollektionen
            return newTaskItem;
        }

        // Opdater en eksisterende TaskItem
        public async Task<TaskItem?> UpdateTaskItemAsync(string id, TaskItem updatedTaskItem)
        {
            if (updatedTaskItem == null) throw new ArgumentNullException(nameof(updatedTaskItem)); // Tjek for null-værdi

            // Erstat den eksisterende TaskItem med den opdaterede
            var result = await _taskItemCollection.ReplaceOneAsync(o => o.Id == id, updatedTaskItem);
            if (result.MatchedCount == 0) return null; // Hvis ingen match findes, returner null

            return updatedTaskItem;
        }

        // Slet en TaskItem baseret på ID
        public async Task<bool> DeleteTaskItemAsync(string id)
        {
            var result = await _taskItemCollection.DeleteOneAsync(o => o.Id == id); // Slet baseret på ID
            return result.DeletedCount > 0; // Returner true, hvis noget blev slettet
        }

        // Generer det næste TaskItem ID
        private async Task<string> GetNextTaskIdAsync()
        {
            var maxId = await _taskItemCollection
                .Find(_ => true)
                .Project(t => t.Id.Substring(8)) // Ekstraher det numeriske suffiks fra "taskitem123"
                .ToListAsync();

            var nextId = maxId
                .Where(id => int.TryParse(id, out _)) // Filtrer gyldige numeriske ID'er
                .Select(id => int.Parse(id)) // Konverter til tal
                .DefaultIfEmpty(0) // Hvis ingen ID'er findes, start med 0
                .Max() + 1; // Find det højeste ID og tilføj 1

            return $"taskitem{nextId}"; // Returner det næste ID i rækken
        }
    }
}
