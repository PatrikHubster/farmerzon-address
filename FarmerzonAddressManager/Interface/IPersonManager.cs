using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IPersonManager
    {
        public Task<IDictionary<string, DTO.PersonOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> addressIds);
    }
}