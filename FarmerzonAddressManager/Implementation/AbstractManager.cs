using AutoMapper;
using FarmerzonAddressDataAccess.Interface;

namespace FarmerzonAddressManager.Implementation
{
    public abstract class AbstractManager
    {
        protected ITransactionHandler TransactionHandler { get; set; }
        protected IMapper Mapper { get; set; }
        
        protected AbstractManager(ITransactionHandler transactionHandler, IMapper mapper)
        {
            TransactionHandler = transactionHandler;
            Mapper = mapper;
        } 
    }
}