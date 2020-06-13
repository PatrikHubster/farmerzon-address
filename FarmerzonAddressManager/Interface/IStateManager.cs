using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IStateManager
    {
        public Task<IList<DTO.State>> GetEntitiesAsync(long? id, string name);
    }
}