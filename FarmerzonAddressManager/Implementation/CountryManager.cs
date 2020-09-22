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
        
        private async Task ThrowInCaseOfMissingCountry(long countryId)
        {
            var existingCountries = await GetEntitiesAsync(id: countryId);
            if (existingCountries == null || existingCountries.Count != 1)
            {
                throw new BadRequestException($"A country with the id {countryId} does not exist.");
            }
        }
        
        public async Task<DTO.CountryOutput> InsertEntityAsync(DTO.CountryInput entity)
        {
            var existingCountries = await GetEntitiesAsync(code: entity.Code, name: entity.Name);
            if (existingCountries != null && existingCountries.Count > 0)
            {
                throw new BadRequestException($"A country with the code {entity.Code} and the " + 
                                              $"name {entity.Name} already exists.");
            }
            
            var convertedCountry = Mapper.Map<DAO.Country>(entity);
            var insertedCountry = await CountryRepository.InsertEntityAsync(convertedCountry);
            return Mapper.Map<DTO.CountryOutput>(insertedCountry);
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

        public async Task<DTO.CountryOutput> UpdateEntityAsync(long id, DTO.CountryInput entity)
        {
            await ThrowInCaseOfMissingCountry(id);
            var convertedCountry = Mapper.Map<DAO.Country>(entity);
            var updatedCountry = await CountryRepository.UpdateEntityAsync(id, convertedCountry);
            return Mapper.Map<DTO.CountryOutput>(updatedCountry);
        }

        public async Task<DTO.CountryOutput> DeleteEntityAsync(long id)
        {
            await ThrowInCaseOfMissingCountry(id);
            var existingRelationshipsForCountry = await CountryRepository.ExistingRelationshipsForCountry(id);
            if (existingRelationshipsForCountry)
            {
                throw new BadRequestException($"The country with the id {id} can't be deleted because it " +
                                              "is used by other entries.");
            }
            var deletedCountry = await CountryRepository.DeleteEntityAsync(id);
            return Mapper.Map<DTO.CountryOutput>(deletedCountry);
        }
    }
}