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

        public PersonManager(IMapper mapper, IPersonRepository personRepository) : base(mapper)
        {
            PersonRepository = personRepository;
        }

        public async Task<IList<DTO.Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName)
        {
            var people = await PersonRepository.GetEntitiesAsync(id, userName, normalizedUserName);
            return Mapper.Map<IList<DTO.Person>>(people);
        }
    }
}