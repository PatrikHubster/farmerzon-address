using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IStateManager : IBasicManager<DTO.StateOutput, DTO.StateInput>
    {
        public Task<IEnumerable<DTO.StateOutput>> GetEntitiesAsync(long? id = null, string name = null);
        public Task<IDictionary<string, DTO.StateOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}