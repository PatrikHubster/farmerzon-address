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
        private IStateRepository StateRepository { get; set; }

        public StateManager(IMapper mapper, IStateRepository stateRepository) : base(mapper)
        {
            StateRepository = stateRepository;
        }

        public async Task<IList<DTO.State>> GetEntitiesAsync(long? id, string name)
        {
            var states = await StateRepository.GetEntitiesAsync(id, name);
            return Mapper.Map<IList<DTO.State>>(states);
        }
    }
}