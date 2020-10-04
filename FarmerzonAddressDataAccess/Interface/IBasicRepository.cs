using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IBasicRepository<T>
    {
        public Task<T> InsertEntityAsync(T entity);
        public Task<T> InsertOrGetEntityAsync(T entity);
        public Task UpdateEntityAsync(T entity);
        public Task<T> RemoveEntityByIdAsync(long id);
        public Task<T> RemoveEntityAsync(T entity);
        public Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includes = null);
        public Task<T> GetEntityAsync(Expression<Func<T, bool>> filter = null, IEnumerable<string> includes = null);
        public Task<IEnumerable<T>> GetEntitiesByIdAsync(IEnumerable<long> ids, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includes = null);
        public Task<T> GetEntityByIdAsync(long id, IEnumerable<string> includes = null);
    }
}