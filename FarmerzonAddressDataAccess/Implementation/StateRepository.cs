using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        protected override async Task<State> GetEntityAsync(State entity)
        {
            return await Context.States
                .Where(c => c.Name == entity.Name)
                .FirstOrDefaultAsync();
        }
        
        public async Task<IDictionary<string, State>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.Addresses
                .Where(a => ids.Contains(a.Id))
                .IncludeMany(includes, "State")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.State);
        }
    }
}