using MongoDB.Driver;
using Core.Models;

namespace ServerAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
        }

        //Hent kollektion for User
        public IMongoCollection<User> GetUserCollection()
        {
            return _database.GetCollection<User>("User");
        }

        //Hent kollektion for Event
        public IMongoCollection<Event> GetEventCollection()
        {
            return _database.GetCollection<Event>("Event");
        }

        //Hent kollektion for TaskItem
        public IMongoCollection<TaskItem> GetTaskItemCollection()
        {
            return _database.GetCollection<TaskItem>("TaskItem");
        }
    }
}
