using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class PersonManager : AbstractManager, IPersonManager
    {
        private IPersonRepository PersonRepository { get; set; }

        public PersonManager(ITransactionHandler transactionHandler, IMapper mapper,
            IPersonRepository personRepository) : base(transactionHandler, mapper)
        {
            PersonRepository = personRepository;
        }

        public async Task<IEnumerable<DTO.PersonOutput>> GetEntitiesAsync(long? id = null, string userName = null,
            string normalizedUserName = null)
        {
            var foundPeople = await PersonRepository.GetEntitiesAsync(
                p => (id == null || p.Id == id) && (userName == null || p.UserName == userName) &&
                     (normalizedUserName == null || p.NormalizedUserName == normalizedUserName));
            return Mapper.Map<IEnumerable<DTO.PersonOutput>>(foundPeople);
        }

        public async Task<IDictionary<string, DTO.PersonOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> addressIds)
        {
            var foundPeople = await PersonRepository.GetEntitiesByAddressIdAsync(addressIds);
            return Mapper.Map<IDictionary<string, DTO.PersonOutput>>(foundPeople);
        }
    }
}