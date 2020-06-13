using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IStateRepository
    {
        public Task<IList<State>> GetEntitiesAsync(long? id, string name);
        public Task<IList<State>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
    }
}