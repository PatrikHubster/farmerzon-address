using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess
{
    public static class QueryExtension
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> query, 
            IEnumerable<string> includes) where T : class
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