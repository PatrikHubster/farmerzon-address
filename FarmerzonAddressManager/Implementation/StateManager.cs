using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class StateManager : AbstractManager, IStateManager
    {
        public StateManager(ITransactionHandler transactionHandler, IMapper mapper) : base(transactionHandler, mapper)
        {
            // nothing to do here
        }
        
        public Task<DTO.StateOutput> InsertEntityAsync(DTO.StateInput entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.StateOutput> UpdateEntityAsync(long id, DTO.StateInput entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.StateOutput> RemoveEntityByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DTO.StateOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.StateOutput> GetEntityByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DTO.StateOutput>> GetEntitiesAsync(long? id = null, string name = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, DTO.StateOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}