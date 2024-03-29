using System.Collections.Generic;
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
        private ICityRepository CityRepository { get; set; }

        private static readonly IList<string> Includes = new List<string>
        {
            nameof(DAO.City.Addresses)
        };
        
        public CityManager(ITransactionHandler transactionHandler, IMapper mapper, ICityRepository cityRepository) : 
            base(transactionHandler, mapper)
        {
            CityRepository = cityRepository;
        }
        
        public async Task<DTO.CityOutput> InsertEntityAsync(DTO.CityInput entity)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var convertedCity = Mapper.Map<DAO.City>(entity);
                var insertedCity = await CityRepository.InsertEntityAsync(convertedCity);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CityOutput>(insertedCity);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<DTO.CityOutput> UpdateEntityAsync(long id, DTO.CityInput entity)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var foundCity = await CityRepository.GetEntityByIdAsync(id);
                if (foundCity == null)
                {
                    throw new NotFoundException("This city does not exist.");
                }
                
                foundCity.ZipCode = entity.ZipCode;
                foundCity.Name = entity.Name;

                await CityRepository.UpdateEntityAsync(foundCity);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CityOutput>(foundCity);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<DTO.CityOutput> RemoveEntityByIdAsync(long id)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var cityToRemove = await CityRepository.GetEntityAsync(filter: c => c.Id == id, includes: Includes);
                if (cityToRemove.Addresses != null && cityToRemove.Addresses.Count > 0)
                {
                    throw new BadRequestException("This city is used by another address.");
                }
                
                var removedCity = await CityRepository.RemoveEntityAsync(cityToRemove);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CityOutput>(removedCity);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<IEnumerable<DTO.CityOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            var foundCities = await CityRepository.GetEntitiesByIdAsync(ids);
            return Mapper.Map<IEnumerable<DTO.CityOutput>>(foundCities);
        }

        public async Task<DTO.CityOutput> GetEntityByIdAsync(long id)
        {
            var foundCity = await CityRepository.GetEntityByIdAsync(id);
            return Mapper.Map<DTO.CityOutput>(foundCity);
        }

        public async Task<IEnumerable<DTO.CityOutput>> GetEntitiesAsync(long? id = null, string zipCode = null, 
            string name = null)
        {
            var foundCities = await CityRepository.GetEntitiesAsync(
                filter: c => (id == null || c.Id == id) && (zipCode == null || c.ZipCode == zipCode) &&
                             (name == null || c.Name == name));
            return Mapper.Map<IEnumerable<DTO.CityOutput>>(foundCities);
        }

        public async Task<IDictionary<string, DTO.CityOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var foundCities = await CityRepository.GetEntitiesByAddressIdAsync(ids);
            return Mapper.Map<IDictionary<string, DTO.CityOutput>>(foundCities);
        }
    }
}