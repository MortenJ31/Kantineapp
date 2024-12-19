using MongoDB.Driver;
using Core.Models;

namespace ServerAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        // Constructor – vi tager en database ind(mongoDB) her og gemmer den i en privat variabel
        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
        }

        // Returnerer "User"-kollektionen fra databasen
        public IMongoCollection<User> GetUserCollection()
        {
            return _database.GetCollection<User>("User");
        }

        // Returnerer "Event"-kollektionen fra databasen
        public IMongoCollection<Event> GetEventCollection()
        {
            return _database.GetCollection<Event>("Event");
        }

        // Returnerer "TaskItem"-kollektionen fra databasen
        public IMongoCollection<TaskItem> GetTaskItemCollection()
        {
            return _database.GetCollection<TaskItem>("TaskItem");
        }
    }
}