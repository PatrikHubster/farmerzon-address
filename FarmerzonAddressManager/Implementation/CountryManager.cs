using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class CountryManager : AbstractManager, ICountryManager
    {
        private ICountryRepository CountryRepository { get; set; }

        public CountryManager(IMapper mapper, ICountryRepository countryRepository) : base(mapper)
        {
            CountryRepository = countryRepository;
        }

        public async Task<IList<DTO.Country>> GetEntitiesAsync(long? id, string name, string code)
        {
            var countries = await CountryRepository.GetEntitiesAsync(id, name, code);
            return Mapper.Map<IList<DTO.Country>>(countries);
        }
    }
}