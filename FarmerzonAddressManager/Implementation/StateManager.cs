using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressErrorHandling.CustomException;
using FarmerzonAddressManager.Interface;

using DAO = FarmerzonAddressDataAccessModel;
using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class StateManager : AbstractManager, IStateManager
    {
        private IStateRepository StateRepository { get; set; }

        public StateManager(ITransactionHandler transactionHandler, IMapper mapper, IStateRepository stateRepository) :
            base(transactionHandler, mapper)
        {
            StateRepository = stateRepository;
        }

        public async Task<DTO.StateOutput> InsertEntityAsync(DTO.StateInput entity)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var convertedState = Mapper.Map<DAO.State>(entity);
                var insertedState = await StateRepository.InsertEntityAsync(convertedState);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.StateOutput>(insertedState);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<DTO.StateOutput> UpdateEntityAsync(long id, DTO.StateInput entity)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var foundState = await StateRepository.GetEntityByIdAsync(id);
                if (foundState == null)
                {
                    throw new NotFoundException("This state does not exist.");
                }

                foundState.Name = entity.Name;
                await StateRepository.UpdateEntityAsync(foundState);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.StateOutput>(foundState);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<DTO.StateOutput> RemoveEntityByIdAsync(long id)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var removedState = await StateRepository.RemoveEntityByIdAsync(id);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.StateOutput>(removedState);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<IEnumerable<DTO.StateOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            var foundStates = await StateRepository.GetEntitiesByIdAsync(ids);
            return Mapper.Map<IEnumerable<DTO.StateOutput>>(foundStates);
        }

        public async Task<DTO.StateOutput> GetEntityByIdAsync(long id)
        {
            var foundState = await StateRepository.GetEntityByIdAsync(id);
            return Mapper.Map<DTO.StateOutput>(foundState);
        }

        public async Task<IEnumerable<DTO.StateOutput>> GetEntitiesAsync(long? id = null, string name = null)
        {
            var foundStates = await StateRepository.GetEntitiesAsync(
                s => (id == null || s.Id == id) && (name == null || s.Name == name));
            return Mapper.Map<IEnumerable<DTO.StateOutput>>(foundStates);
        }

        public async Task<IDictionary<string, DTO.StateOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var foundStates = await StateRepository.GetEntitiesByAddressIdAsync(ids);
            return Mapper.Map<IDictionary<string, DTO.StateOutput>>(foundStates);
        }
    }
}