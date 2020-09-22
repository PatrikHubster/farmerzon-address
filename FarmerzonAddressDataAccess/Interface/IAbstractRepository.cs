using System.Threading.Tasks;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IAbstractRepository<T>
    {
        public Task<T> InsertEntityAsync(T entity);
        public Task<T> UpdateEntityAsync(long id, T entity);
        public Task<T> DeleteEntityAsync(long id);
    }
}