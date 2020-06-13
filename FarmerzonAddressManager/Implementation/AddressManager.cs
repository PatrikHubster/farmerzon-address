using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataTransferModel;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class AddressManager : AbstractManager, IAddressManager
    {
        private IAddressRepository AddressRepository { get; set; }

        public AddressManager(IMapper mapper, IAddressRepository addressRepository) : base(mapper)
        {
            AddressRepository = addressRepository;
        }

        public async Task<IList<Address>> GetEntitiesAsync(long? id, string doorNumber, string street)
        {
            var articles = await AddressRepository.GetEntitiesAsync(id, doorNumber, street);
            return Mapper.Map<IList<DTO.Address>>(articles);
        }
    }
}