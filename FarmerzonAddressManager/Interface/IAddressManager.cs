using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IAddressManager
    {
        public Task<IList<DTO.Address>> GetEntitiesAsync(long? id, string doorNumber, string street);
        public Task<IDictionary<string, IList<DTO.Address>>> GetAddressesByCityIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.Address>>> GetAddressesByCountryIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.Address>>> GetAddressesByStateIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, DTO.Address>> GetAddressesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames);
    }
}