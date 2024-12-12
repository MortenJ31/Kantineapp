using ServerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServerAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Repositories
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly IMongoCollection<Bruger> _brugerCollection;

        public BrugerRepository(MongoDbService mongoDbService)
        {
            _brugerCollection = mongoDbService.GetBrugerCollection();
        }

        public async Task<IEnumerable<Bruger>> GetAllUsersAsync()
        {
            //Hent alle brugere fra MongoDB
            return await _brugerCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Bruger?> GetUserByIdAsync(string id) // Opdaterer int til string
        {
            // Find en bruger med det specifikke ID
            return await _brugerCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Bruger>> GetUsersByRoleAsync(Rolle role)
        {
            return await _brugerCollection.Find(b => b.Rolle == role).ToListAsync();
        }
        
        
        public async Task<Bruger> AddUserAsync(Bruger newUser)
        {
            //Tilføj en ny bruger til MongoDB
            await _brugerCollection.InsertOneAsync(newUser);
            return newUser;
        }


    }
}