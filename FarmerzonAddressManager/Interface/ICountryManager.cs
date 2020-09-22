using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface ICountryManager
    {
        public Task<IList<DTO.CountryOutput>> GetEntitiesAsync(long? id = null, string name = null, string code = null);
        public Task<IDictionary<string, DTO.CountryOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}