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
    public class CountryManager : AbstractManager, ICountryManager
    {
        private IAddressRepository AddressRepository { get; set; }
        private ICountryRepository CountryRepository { get; set; }

        public CountryManager(IMapper mapper, IAddressRepository addressRepository, 
            ICountryRepository countryRepository) : base(mapper)
        {
            AddressRepository = addressRepository;
            CountryRepository = countryRepository;
        }

        public async Task<IList<DTO.CountryOutput>> GetEntitiesAsync(long? id = null, string name = null, 
            string code = null)
        {
            var countries = await CountryRepository.GetEntitiesAsync(id, name, code);
            return Mapper.Map<IList<DTO.CountryOutput>>(countries);
        }

        public async Task<IDictionary<string, DTO.CountryOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var addresses = await AddressRepository.GetEntitiesByIdAsync(ids, 
                new List<string> {nameof(DAO.Address.Country)});
            return addresses.ToDictionary(key => key.AddressId.ToString(),
                value => Mapper.Map<DTO.CountryOutput>(value.Country));
        }
    }
}