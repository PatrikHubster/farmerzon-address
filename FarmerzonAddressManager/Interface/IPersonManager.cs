using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IPersonManager
    {
        public Task<IList<DTO.Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
    }
}