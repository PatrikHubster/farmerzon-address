using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class StateRepository : AbstractRepository, IStateRepository
    {
        public StateRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<State>> GetEntitiesAsync(long? id, string name)
        {
            return await Context.States
                .Where(s => id == null || s.StateId == id)
                .Where(s => name == null || s.Name == name)
                .ToListAsync();
        }

        public async Task<IList<State>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.States
                .IncludeMany(includes)
                .Where(s => ids.Contains(s.StateId))
                .ToListAsync();
        }
    }
}