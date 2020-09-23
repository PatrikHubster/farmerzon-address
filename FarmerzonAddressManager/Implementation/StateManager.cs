using System.Collections.Generic;
using System.Linq;
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
        private IAddressRepository AddressRepository { get; set; }
        private IStateRepository StateRepository { get; set; }

        public StateManager(IMapper mapper, IAddressRepository addressRepository, 
            IStateRepository stateRepository) : base(mapper)
        {
            AddressRepository = addressRepository;
            StateRepository = stateRepository;
        }
        
        private async Task ThrowInCaseOfMissingState(long stateId)
        {
            var existingStates = await GetEntitiesAsync(id: stateId);
            if (existingStates == null || existingStates.Count != 1)
            {
                throw new BadRequestException($"A state with the id {stateId} does not exist.");
            }
        }

        public async Task<DTO.StateOutput> InsertEntityAsync(DTO.StateInput entity)
        {
            var existingStates = await GetEntitiesAsync(name: entity.Name);
            if (existingStates != null && existingStates.Count > 0)
            {
                throw new BadRequestException($"A state with the name {entity.Name} already exists.");
            }
            
            var convertedState = Mapper.Map<DAO.State>(entity);
            var insertedState = await StateRepository.InsertEntityAsync(convertedState);
            return Mapper.Map<DTO.StateOutput>(insertedState);
        }

        public async Task<IList<DTO.StateOutput>> GetEntitiesAsync(long? id = null, string name = null)
        {
            var states = await StateRepository.GetEntitiesAsync(id, name);
            return Mapper.Map<IList<DTO.StateOutput>>(states);
        }

        public async Task<IDictionary<string, DTO.StateOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var addresses =
                await AddressRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Address.State)});
            return addresses.ToDictionary(key => key.AddressId.ToString(), 
                value => Mapper.Map<DTO.StateOutput>(value.State));
        }

        public async Task<DTO.StateOutput> UpdateEntityAsync(long id, DTO.StateInput entity)
        {
            await ThrowInCaseOfMissingState(id);
            var convertedState = Mapper.Map<DAO.State>(entity);
            var updatedState = await StateRepository.UpdateEntityAsync(id, convertedState);
            return Mapper.Map<DTO.StateOutput>(updatedState);
        }

        public async Task<DTO.StateOutput> DeleteEntityAsync(long id)
        {
            await ThrowInCaseOfMissingState(id);
            var existingRelationshipsForState = await StateRepository.ExistingRelationshipsForStateAsync(id);
            if (existingRelationshipsForState)
            {
                throw new BadRequestException($"The state with the id {id} can't be deleted because it " +
                                              "is used by other entries.");
            }
            var deletedCountry = await StateRepository.DeleteEntityAsync(id);
            return Mapper.Map<DTO.StateOutput>(deletedCountry);
        }
    }
}