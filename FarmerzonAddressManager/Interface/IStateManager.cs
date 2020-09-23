using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IStateManager : IAbstractManager<DTO.StateInput, DTO.StateOutput>
    {
        public Task<IList<DTO.StateOutput>> GetEntitiesAsync(long? id = null, string name = null);
        public Task<IDictionary<string, DTO.StateOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}