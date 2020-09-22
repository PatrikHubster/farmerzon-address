using System.Threading.Tasks;

namespace FarmerzonAddressDataAccess.Implementation
{
    public abstract class AbstractRepository<T>
    {
        protected FarmerzonAddressContext Context { get; set; }

        protected AbstractRepository(FarmerzonAddressContext context)
        {
            Context = context;
        }

        public async Task<T> InsertEntityAsync(T entity)
        {
            var result = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}