using MongoDB.Driver;
using ServerAPI.Models;

namespace ServerAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
        }

        //Hent kollektion for Bruger
        public IMongoCollection<Bruger> GetBrugerCollection()
        {
            return _database.GetCollection<Bruger>("Bruger");
        }

        //Hent kollektion for Event
        public IMongoCollection<Event> GetEventCollection()
        {
            return _database.GetCollection<Event>("Event");
        }

        //Hent kollektion for Opgave
        public IMongoCollection<Opgave> GetOpgaveCollection()
        {
            return _database.GetCollection<Opgave>("Opgave");
        }
    }
}
