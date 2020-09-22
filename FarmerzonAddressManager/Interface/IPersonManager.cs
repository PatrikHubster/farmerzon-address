using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IPersonManager
    {
        public Task<IList<DTO.PersonOutput>> GetEntitiesAsync(long? id = null, string userName = null, 
            string normalizedUserName = null);
    }
}