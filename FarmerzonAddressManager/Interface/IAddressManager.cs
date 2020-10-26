using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IAddressManager
    {
        public Task<DTO.AddressOutput> InsertEntityAsync(DTO.AddressInput entity, string userName,
            string normalizedUserName);
        public Task<IEnumerable<DTO.AddressOutput>> GetEntitiesAsync(long? id = null, string doorNumber = null,
            string street = null);
        public Task<DTO.AddressOutput> UpdateEntityAsync(long id, DTO.AddressInput address, string userName,
            string normalizedUserName);
        public Task<DTO.AddressOutput> RemoveEntityByIdAsync(long id, string userName, string normalizedUserName);
        
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByCityIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByCountryIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByStateIdAsync(IEnumerable<long> ids);
        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames);
    }
}