using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class StateRepository : AbstractRepository<State>, IStateRepository
    {
        public StateRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<State>> GetEntitiesAsync(long? id = null, string name = null)
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

        public async Task<State> UpdateEntityAsync(long id, State entity)
        {
            var state = await Context.States.SingleOrDefaultAsync(s => s.StateId == id);
            state.Name = entity.Name;
            await Context.SaveChangesAsync();
            return state;
        }
        
        public async Task<bool> ExistingRelationshipsForStateAsync(long id)
        {
            var state = await Context.States
                .Include("Addresses")
                .SingleOrDefaultAsync(s => s.StateId == id);
            return state?.Addresses != null && state.Addresses.Count != 0;
        }

        public async Task<State> DeleteEntityAsync(long id)
        {
            var state = await Context.States.SingleOrDefaultAsync(s => s.StateId == id);
            Context.States.Remove(state);
            await Context.SaveChangesAsync();
            return state;
        }
    }
}