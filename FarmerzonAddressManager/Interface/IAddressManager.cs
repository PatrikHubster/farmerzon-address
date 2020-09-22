using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IAddressManager
    {
        public Task<IList<DTO.AddressOutput>> GetEntitiesAsync(long? id = null, string doorNumber = null, 
            string street = null);
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetAddressesByCityIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetAddressesByCountryIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetAddressesByStateIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, DTO.AddressOutput>> GetAddressesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames);
    }
}