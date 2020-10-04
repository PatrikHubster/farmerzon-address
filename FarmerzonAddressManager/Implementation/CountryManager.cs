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
    public class CountryManager : AbstractManager, ICountryManager
    {
        private ICountryRepository CountryRepository { get; set; }
        
        public CountryManager(ITransactionHandler transactionHandler, IMapper mapper, 
            ICountryRepository countryRepository) : base(transactionHandler, mapper)
        {
            CountryRepository = countryRepository;
        }
        
        public async Task<DTO.CountryOutput> InsertEntityAsync(DTO.CountryInput entity)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var convertedCountry = Mapper.Map<DAO.Country>(entity);
                var insertedCountry = await CountryRepository.InsertEntityAsync(convertedCountry);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CountryOutput>(insertedCountry);
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

        public async Task<DTO.CountryOutput> UpdateEntityAsync(long id, DTO.CountryInput entity)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var foundCountry = await CountryRepository.GetEntityByIdAsync(id);
                if (foundCountry == null)
                {
                    throw new NotFoundException("This country does not exist.");
                }

                foundCountry.Code = entity.Code;
                foundCountry.Name = entity.Name;

                await CountryRepository.UpdateEntityAsync(foundCountry);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CountryOutput>(foundCountry);
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

        public async Task<DTO.CountryOutput> RemoveEntityByIdAsync(long id)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var removedCountry = await CountryRepository.RemoveEntityByIdAsync(id);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.CountryOutput>(removedCountry);
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

        public async Task<IEnumerable<DTO.CountryOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            var foundCountries = await CountryRepository.GetEntitiesByIdAsync(ids);
            return Mapper.Map<IEnumerable<DTO.CountryOutput>>(foundCountries);
        }

        public async Task<DTO.CountryOutput> GetEntityByIdAsync(long id)
        {
            var foundCountry = await CountryRepository.GetEntityByIdAsync(id);
            return Mapper.Map<DTO.CountryOutput>(foundCountry);
        }

        public async Task<IEnumerable<DTO.CountryOutput>> GetEntitiesAsync(long? id = null, string code = null, 
            string name = null)
        {
            var foundCountries = await CountryRepository.GetEntitiesAsync(
                filter: c => (id == null || c.Id == id) && (code == null || c.Code == code) &&
                             (name == null || c.Name == name));
            return Mapper.Map<IEnumerable<DTO.CountryOutput>>(foundCountries);
        }

        public async Task<IDictionary<string, DTO.CountryOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            var foundCountries = await CountryRepository.GetEntitiesByAddressIdAsync(ids);
            return Mapper.Map<IDictionary<string, DTO.CountryOutput>>(foundCountries);
        }
    }
}