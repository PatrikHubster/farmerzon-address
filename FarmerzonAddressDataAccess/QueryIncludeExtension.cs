using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess
{
    public static class QueryIncludeExtension
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> query, IEnumerable<string> includes) where T : class
        {
            if (includes == null)
            {
                return query;
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}