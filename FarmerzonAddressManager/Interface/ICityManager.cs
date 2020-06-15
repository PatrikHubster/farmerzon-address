using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface ICityManager
    {
        public Task<IList<DTO.City>> GetEntitiesAsync(long? id, string zipCode, string name);
        public Task<IDictionary<string, DTO.City>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}