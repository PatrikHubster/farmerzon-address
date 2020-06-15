using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressManager.Interface;

using DAO = FarmerzonAddressDataAccessModel;
using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class StateManager : AbstractManager, IStateManager
    {
        private IAddressRepository AddressRepository { get; set; }
        private IStateRepository StateRepository { get; set; }

        public StateManager(IMapper mapper, IAddressRepository addressRepository, IStateRepository stateRepository) : base(mapper)
        {
            AddressRepository = addressRepository;
            StateRepository = stateRepository;
        }

        public async Task<IList<DTO.State>> GetEntitiesAsync(long? id, string name)
        {
            var states = await StateRepository.GetEntitiesAsync(id, name);
            return Mapper.Map<IList<DTO.State>>(states);
        }

        public async Task<IDictionary<string, DTO.State>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var addresses =
                await AddressRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Address.State)});
            return addresses.ToDictionary(key => key.AddressId.ToString(), value => Mapper.Map<DTO.State>(value.State));
        }
    }
}