using System.Threading.Tasks;

namespace FarmerzonAddressManager.Interface
{
    public interface IAbstractManager<TEntityInput, TEntityOutput>
    {
        public Task<TEntityOutput> InsertEntityAsync(TEntityInput entity);
        public Task<TEntityOutput> UpdateEntityAsync(long id, TEntityInput entity);
        public Task<TEntityOutput> DeleteEntityAsync(long id);
    }
}