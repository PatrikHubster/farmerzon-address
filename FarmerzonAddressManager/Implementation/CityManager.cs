using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class CityManager : AbstractManager, ICityManager
    {
        private ICityRepository CityRepository { get; set; }

        public CityManager(IMapper mapper, ICityRepository cityRepository) : base(mapper)
        {
            CityRepository = cityRepository;
        }

        public async Task<IList<DTO.City>> GetEntitiesAsync(long? id, string zipCode, string name)
        {
            var cities = await CityRepository.GetEntitiesAsync(id, zipCode, name);
            return Mapper.Map<IList<DTO.City>>(cities);
        }
    }
}