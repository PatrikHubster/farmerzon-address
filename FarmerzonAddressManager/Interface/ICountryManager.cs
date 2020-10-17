using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface ICountryManager : IBasicManager<DTO.CountryOutput, DTO.CountryInput>
    {
        public Task<IEnumerable<DTO.CountryOutput>> GetEntitiesAsync(long? id = null, string code = null, 
            string name = null);
        public Task<IDictionary<string, DTO.CountryOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}