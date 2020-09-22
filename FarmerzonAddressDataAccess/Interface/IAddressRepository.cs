using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IAddressRepository : IAbstractRepository<Address>
    {
        public Task<IList<Address>> GetEntitiesAsync(long? id = null, string doorNumber = null, string street = null);
        public Task<IList<Address>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
    }
}