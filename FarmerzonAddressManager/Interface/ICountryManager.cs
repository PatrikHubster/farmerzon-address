using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface ICountryManager
    {
        public Task<IList<DTO.Country>> GetEntitiesAsync(long? id, string name, string code);
    }
}