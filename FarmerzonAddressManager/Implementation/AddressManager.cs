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
    public class AddressManager : AbstractManager, IAddressManager
    {
        private IAddressRepository AddressRepository { get; set; }
        private ICityRepository CityRepository { get; set; }
        private ICountryRepository CountryRepository { get; set; }
        private IPersonRepository PersonRepository { get; set; }
        private IStateRepository StateRepository { get; set; }

        public AddressManager(IMapper mapper, IAddressRepository addressRepository, ICityRepository cityRepository,
            ICountryRepository countryRepository, IPersonRepository personRepository, 
            IStateRepository stateRepository) : base(mapper)
        {
            AddressRepository = addressRepository;
            CityRepository = cityRepository;
            CountryRepository = countryRepository;
            PersonRepository = personRepository;
            StateRepository = stateRepository;
        }

        public async Task<IList<DTO.AddressOutput>> GetEntitiesAsync(long? id = null, string doorNumber = null, 
            string street = null)
        {
            var articles = await AddressRepository.GetEntitiesAsync(id, doorNumber, street);
            return Mapper.Map<IList<DTO.AddressOutput>>(articles);
        }

        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetAddressesByCityIdAsync(IEnumerable<long> ids) 
        {
            var cities =
                await CityRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.City.Addresses)});

            var addressesForCities = new Dictionary<string, IList<DTO.AddressOutput>>();
            foreach (var city in cities)
            {
                if (!addressesForCities.ContainsKey(city.CityId.ToString()) && city.Addresses.Count > 0)
                {
                    addressesForCities.Add(city.CityId.ToString(), new List<DTO.AddressOutput>());
                    foreach (var address in city.Addresses)
                    {
                        addressesForCities[city.CityId.ToString()].Add(Mapper.Map<DTO.AddressOutput>(address));
                    }
                }
            }

            return addressesForCities;
        }
        
        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetAddressesByCountryIdAsync(IEnumerable<long> ids) 
        {
            var countries =
                await CountryRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Country.Addresses)});

            var addressesForCountries = new Dictionary<string, IList<DTO.AddressOutput>>();
            foreach (var country in countries)
            {
                if (!addressesForCountries.ContainsKey(country.CountryId.ToString()) && country.Addresses.Count > 0)
                {
                    addressesForCountries.Add(country.CountryId.ToString(), new List<DTO.AddressOutput>());
                    foreach (var address in country.Addresses)
                    {
                        addressesForCountries[country.CountryId.ToString()].Add(Mapper.Map<DTO.AddressOutput>(address));
                    }
                }
            }

            return addressesForCountries;
        }

        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetAddressesByStateIdAsync(IEnumerable<long> ids)
        {
            var states =
                await StateRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.State.Addresses)});

            var addressesForStates = new Dictionary<string, IList<DTO.AddressOutput>>();
            foreach (var state in states)
            {
                if (!addressesForStates.ContainsKey(state.StateId.ToString()) && state.Addresses.Count > 0)
                {
                    addressesForStates.Add(state.StateId.ToString(), new List<DTO.AddressOutput>());
                    foreach (var address in state.Addresses)
                    {
                        addressesForStates[state.StateId.ToString()].Add(Mapper.Map<DTO.AddressOutput>(address));
                    }
                }
            }

            return addressesForStates;
        }

        public async Task<IDictionary<string, DTO.AddressOutput>> GetAddressesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames)
        {
            var people = await PersonRepository.GetEntitiesByNormalizedUserNamesAsync(normalizedUserNames,
                new List<string> {nameof(DAO.Person.Address)});
            return people.ToDictionary(key => key.NormalizedUserName,
                value => Mapper.Map<DTO.AddressOutput>(value.Address));
        }
    }
}