using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class TransactionHandler : AbstractRepository, ITransactionHandler
    {
        private IDbContextTransaction _transaction = null;
        
        public TransactionHandler(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await Context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}