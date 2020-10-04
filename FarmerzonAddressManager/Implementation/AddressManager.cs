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
    public class AddressManager : AbstractManager, IAddressManager
    {
        private const string OperationNotAllowed = "This address does not exist or is not accessible for this user.";
        private static readonly IList<string> Includes = new List<string>
        {
            nameof(DAO.Address.City),
            nameof(DAO.Address.Country),
            nameof(DAO.Address.State),
            nameof(DAO.Address.Person)
        };
        private IAddressRepository AddressRepository { get; set; }
        private ICityRepository CityRepository { get; set; }
        private ICountryRepository CountryRepository { get; set; }
        private IStateRepository StateRepository { get; set; }
        private IPersonRepository PersonRepository { get; set; }

        public AddressManager(ITransactionHandler transactionHandler, IMapper mapper,
            IAddressRepository addressRepository, ICityRepository cityRepository, 
            ICountryRepository countryRepository, IStateRepository stateRepository, 
            IPersonRepository personRepository) : base(transactionHandler, mapper)
        {
            AddressRepository = addressRepository;
            CityRepository = cityRepository;
            CountryRepository = countryRepository;
            StateRepository = stateRepository;
            PersonRepository = personRepository;
        }

        private async Task<(DAO.City, DAO.Country, DAO.State)> InsertOrGetAddressRelationsAsync(
            DTO.AddressInput entity)
        {
            var convertedCity = Mapper.Map<DAO.City>(entity.City);
            var managedCity = await CityRepository.InsertOrGetEntityAsync(convertedCity);

            var convertedCountry = Mapper.Map<DAO.Country>(entity.Country);
            var managedCountry = await CountryRepository.InsertOrGetEntityAsync(convertedCountry);

            var convertedState = Mapper.Map<DAO.State>(entity.State);
            var managedState = await StateRepository.InsertOrGetEntityAsync(convertedState);

            return (managedCity, managedCountry, managedState);
        }

        private async Task<DAO.Address> CheckAccessRightsAndGetAddressAsync(long id, string userName, 
            string normalizedUserName)
        {
            var foundAddress = await AddressRepository.GetEntityByIdAsync(id, includes: Includes);
            
            if (foundAddress == null)
            {
                throw new UnautherizedException(OperationNotAllowed);
            }

            if (foundAddress.Person.UserName != userName || 
                foundAddress.Person.NormalizedUserName != normalizedUserName)
            {
                throw new UnautherizedException(OperationNotAllowed);
            }

            return foundAddress;
        }

        public async Task<DTO.AddressOutput> InsertEntityAsync(DTO.AddressInput entity, string userName,
            string normalizedUserName)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var person = new DAO.Person
                {
                    UserName = userName,
                    NormalizedUserName = normalizedUserName
                };
                var managedPerson = await PersonRepository.InsertOrGetEntityAsync(person);

                var (city, country, state) = await InsertOrGetAddressRelationsAsync(entity);
                var address = new DAO.Address
                {
                    Person = managedPerson,
                    City = city,
                    Country = country,
                    State = state,
                    DoorNumber = entity.DoorNumber,
                    Street = entity.Street
                };
                var managedAddress = await AddressRepository.InsertEntityAsync(address);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.AddressOutput>(managedAddress);
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

        public async Task<DTO.AddressOutput> UpdateEntityAsync(long id, DTO.AddressInput address, string userName,
            string normalizedUserName)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var foundAddress = await CheckAccessRightsAndGetAddressAsync(id, userName, normalizedUserName);
                var (city, country, state) = await InsertOrGetAddressRelationsAsync(address);
                foundAddress.City = city;
                foundAddress.Country = country;
                foundAddress.State = state;
                foundAddress.DoorNumber = address.DoorNumber;
                foundAddress.Street = address.Street;
                await AddressRepository.UpdateEntityAsync(foundAddress);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.AddressOutput>(foundAddress);
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

        public async Task<DTO.AddressOutput> DeleteEntityByIdAsync(long id, string userName, string normalizedUserName)
        {
            await TransactionHandler.BeginTransactionAsync();
            try
            {
                var foundAddress = await CheckAccessRightsAndGetAddressAsync(id, userName, normalizedUserName);
                var removedAddress = await AddressRepository.RemoveEntityAsync(foundAddress);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.AddressOutput>(removedAddress);
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

        public async Task<IEnumerable<DTO.AddressOutput>> GetEntitiesAsync(long? id = null, string doorNumber = null,
            string street = null)
        {
            var foundAddresses = await AddressRepository.GetEntitiesAsync(filter:
                a => (id == null || a.Id == id) && (doorNumber == null || a.DoorNumber == doorNumber) &&
                     (street == null || a.Street == street), 
                includes: Includes);
            return Mapper.Map<IEnumerable<DTO.AddressOutput>>(foundAddresses);
        }

        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByCityIdAsync(IEnumerable<long> ids)
        {
            var foundAddresses = await AddressRepository.GetEntitiesByCityIdAsync(ids, 
                includes: Includes);
            return Mapper.Map<IDictionary<string, IList<DTO.AddressOutput>>>(foundAddresses);
        }

        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByCountryIdAsync(
            IEnumerable<long> ids)
        {
            var foundAddresses = await AddressRepository.GetEntitiesByCountryIdAsync(ids, 
                includes: Includes);
            return Mapper.Map<IDictionary<string, IList<DTO.AddressOutput>>>(foundAddresses);
        }

        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByStateIdAsync(
            IEnumerable<long> ids)
        {
            var foundAddresses = await AddressRepository.GetEntitiesByStateIdAsync(ids, 
                includes: Includes);
            return Mapper.Map<IDictionary<string, IList<DTO.AddressOutput>>>(foundAddresses);
        }

        public async Task<IDictionary<string, IList<DTO.AddressOutput>>> GetEntitiesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames)
        {
            var foundAddresses = await AddressRepository.GetEntitiesByNormalizedUserNamesAsync(normalizedUserNames, 
                includes: Includes);
            return Mapper.Map<IDictionary<string, IList<DTO.AddressOutput>>>(foundAddresses);
        }
    }
}