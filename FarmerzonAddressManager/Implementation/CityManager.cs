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
    public class CityManager : AbstractManager, ICityManager
    {
        private IAddressRepository AddressRepository { get; set; }
        private ICityRepository CityRepository { get; set; }

        public CityManager(IMapper mapper, IAddressRepository addressRepository, ICityRepository cityRepository) : 
            base(mapper)
        {
            AddressRepository = addressRepository;
            CityRepository = cityRepository;
        }

        public async Task<IList<DTO.City>> GetEntitiesAsync(long? id, string zipCode, string name)
        {
            var cities = await CityRepository.GetEntitiesAsync(id, zipCode, name);
            return Mapper.Map<IList<DTO.City>>(cities);
        }

        public async Task<IDictionary<string, DTO.City>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var addresses =
                await AddressRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Address.City)});
            return addresses.ToDictionary(key => key.AddressId.ToString(),
                value => Mapper.Map<DTO.City>(value.City));
        }
    }
}