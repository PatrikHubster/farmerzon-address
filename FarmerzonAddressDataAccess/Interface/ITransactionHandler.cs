using System.Threading.Tasks;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ITransactionHandler
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task DisposeTransactionAsync();
    }
}