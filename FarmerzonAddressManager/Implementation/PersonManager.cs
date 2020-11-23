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

        public async Task<IDictionary<string, DTO.PersonOutput>> GetEntitiesByAddressIdAsync(IEnumerable<long> addressIds)
        {
            var foundPeople = await PersonRepository.GetEntitiesByAddressIdAsync(addressIds);
            return Mapper.Map<IDictionary<string, DTO.PersonOutput>>(foundPeople);
        }
    }
}