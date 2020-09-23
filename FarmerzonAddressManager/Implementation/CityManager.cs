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

        private async Task ThrowInCaseOfMissingCity(long cityId)
        {
            var existingCities = await GetEntitiesAsync(id: cityId);
            if (existingCities == null || existingCities.Count != 1)
            {
                throw new BadRequestException($"A city with the id {cityId} does not exist.");
            }
        }

        public async Task<DTO.CityOutput> InsertEntityAsync(DTO.CityInput entity)
        {
            var existingCities = await GetEntitiesAsync(zipCode: entity.ZipCode, name: entity.Name);
            if (existingCities != null && existingCities.Count > 0)
            {
                throw new BadRequestException($"A city with the zip code {entity.ZipCode} and the " + 
                                              $"name {entity.Name} already exists.");
            }
            
            var convertedCity = Mapper.Map<DAO.City>(entity);
            var insertedCity = await CityRepository.InsertEntityAsync(convertedCity);
            return Mapper.Map<DTO.CityOutput>(insertedCity);
        }

        public async Task<IList<DTO.CityOutput>> GetEntitiesAsync(long? id = null, string zipCode = null, 
            string name = null)
        {
            var cities = await CityRepository.GetEntitiesAsync(id, zipCode, name);
            return Mapper.Map<IList<DTO.CityOutput>>(cities);
        }

        public async Task<IDictionary<string, DTO.CityOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var addresses =
                await AddressRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Address.City)});
            return addresses.ToDictionary(key => key.AddressId.ToString(),
                value => Mapper.Map<DTO.CityOutput>(value.City));
        }
        
        public async Task<DTO.CityOutput> UpdateEntityAsync(long id, DTO.CityInput entity)
        {
            await ThrowInCaseOfMissingCity(id);
            var convertedCity = Mapper.Map<DAO.City>(entity);
            var updatedCity = await CityRepository.UpdateEntityAsync(id, convertedCity);
            return Mapper.Map<DTO.CityOutput>(updatedCity);
        }
        
        public async Task<DTO.CityOutput> DeleteEntityAsync(long id)
        {
            await ThrowInCaseOfMissingCity(id);
            var existingRelationshipsForCity = await CityRepository.ExistingRelationshipsForCityAsync(id);
            if (existingRelationshipsForCity)
            {
                throw new BadRequestException($"The city with the id {id} can't be deleted because it " +
                                              "is used by other entries.");
            }
            var deletedCity = await CityRepository.DeleteEntityAsync(id);
            return Mapper.Map<DTO.CityOutput>(deletedCity);
        }
    }
}