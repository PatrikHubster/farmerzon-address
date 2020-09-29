using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataTransferModel;
using FarmerzonAddressManager.Interface;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Implementation
{
    public class PersonManager : AbstractManager, IPersonManager
    {
        public PersonManager(ITransactionHandler transactionHandler, IMapper mapper) : base(transactionHandler, mapper)
        {
            // nothing to do here
        }
        
        public Task<IEnumerable<PersonOutput>> GetEntitiesAsync(long? id = null, string userName = null, 
            string normalizedUserName = null)
        {
            throw new System.NotImplementedException();
        }
    }
}