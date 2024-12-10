using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Repositories
{
    public class OpgaverRepository : IOpgaverRepository
    {
        private readonly List<Opgaver> _opgaver = new()
        {
            new Opgaver { Id = 1, Description = "Setup tables", Status = "Not Started", EventId = 1 },
            new Opgaver { Id = 2, Description = "Prepare food", Status = "In Progress", EventId = 2 }
        };

        public Task<IEnumerable<Opgaver>> GetAllAsync() => Task.FromResult(_opgaver.AsEnumerable());

        public Task<Opgaver> GetByIdAsync(int id) => Task.FromResult(_opgaver.FirstOrDefault(o => o.Id == id));

        public Task<Opgaver> AddAsync(Opgaver newOpgaver)
        {
            newOpgaver.Id = _opgaver.Max(o => o.Id) + 1; // Simulate auto-increment
            _opgaver.Add(newOpgaver);
            return Task.FromResult(newOpgaver);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var opgaverToRemove = _opgaver.FirstOrDefault(o => o.Id == id);
            if (opgaverToRemove == null) return Task.FromResult(false);

            _opgaver.Remove(opgaverToRemove);
            return Task.FromResult(true);
        }
    }
}