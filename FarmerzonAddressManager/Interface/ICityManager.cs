using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface ICityManager : IBasicManager<DTO.CityOutput, DTO.CityInput>
    {
        public Task<IEnumerable<DTO.CityOutput>> GetEntitiesAsync(long? id = null, string zipCode = null, 
            string name = null);
        public Task<IDictionary<string, DTO.CityOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}