using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IStateRepository : IBasicRepository<State>
    {
        public Task<IDictionary<string, State>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null);
    }
}