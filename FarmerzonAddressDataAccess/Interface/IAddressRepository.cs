using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IAddressRepository
    {
        public Task<IList<Address>> GetEntitiesAsync(long? id, string doorNumber, string street);
        public Task<IList<Address>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
    }
}