using System.Collections.Generic;
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
        private ICityRepository CityRepository { get; set; }
        
        public CityManager(ITransactionHandler transactionHandler, IMapper mapper, ICityRepository cityRepository) : 
            base(transactionHandler, mapper)
        {
            CityRepository = cityRepository;
        }
        
        public async Task<DTO.CityOutput> InsertEntityAsync(DTO.CityInput entity)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
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
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var convertedCity = Mapper.Map<DAO.City>(entity);
                convertedCity.Id = id;

                await CityRepository.UpdateEntityAsync(convertedCity);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CityOutput>(convertedCity);
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
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var deletedCity = await CityRepository.RemoveEntityByIdAsync(id);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CityOutput>(deletedCity);
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