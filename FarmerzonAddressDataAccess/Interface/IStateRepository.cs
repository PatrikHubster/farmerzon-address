using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IStateRepository : IAbstractRepository<State>
    {
        public Task<IList<State>> GetEntitiesAsync(long? id = null, string name = null);
        public Task<IList<State>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
    }
}