using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmerzonAddressManager.Interface
{
    public interface IBasicManager<TEntityOutput, TEntityInput>
    {
        public Task<TEntityOutput> InsertEntityAsync(TEntityInput entity);
        public Task<TEntityOutput> UpdateEntityAsync(long id, TEntityInput entity);
        public Task<TEntityOutput> RemoveEntityByIdAsync(long id);
        public Task<IEnumerable<TEntityOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids);
        public Task<TEntityOutput> GetEntityByIdAsync(long id);
    }
}