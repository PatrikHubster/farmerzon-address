using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class AddressManager : AbstractManager, IAddressManager
    {
        public AddressManager(ITransactionHandler transactionHandler, IMapper mapper) : base(transactionHandler, mapper)
        {
            // nothing to do here
        }
        
        public Task<DTO.AddressOutput> InsertEntityAsync(DTO.AddressInput entity, string userName, 
            string normalizedUserName)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DTO.AddressOutput>> GetEntitiesAsync(long? id = null, string doorNumber = null, 
            string street = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.AddressOutput> UpdateEntityAsync(long id, DTO.AddressInput address, string userName, 
            string normalizedUserName)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.AddressOutput> DeleteEntityByIdAsync(long id, string userName, string normalizedUserName)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByCityIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByCountryIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByStateIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames)
        {
            throw new System.NotImplementedException();
        }
    }
}