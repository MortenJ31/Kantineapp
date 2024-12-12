using ServerAPI.Models;
using MongoDB.Driver;
using ServerAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ServerAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _eventCollection;

        public EventRepository(MongoDbService mongoDbService)
        {
            _eventCollection = mongoDbService.GetEventCollection();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _eventCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(string id)
        {
            return await _eventCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Event> AddAsync(Event nyEvent)
        {
            await _eventCollection.InsertOneAsync(nyEvent);
            return nyEvent;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _eventCollection.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }

       
    }
}