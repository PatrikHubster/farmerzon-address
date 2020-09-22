using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface ICityManager : IAbstractManager<DTO.CityInput, DTO.CityOutput>
    {
        public Task<IList<DTO.CityOutput>> GetEntitiesAsync(long? id = null, string zipCode = null, string name = null);
        public Task<IDictionary<string, DTO.CityOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}