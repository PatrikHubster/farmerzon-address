using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        protected override async Task<City> GetEntityAsync(City entity)
        {
            return await Context.Cities
                .Where(c => c.Name == entity.Name && c.ZipCode == entity.ZipCode)
                .FirstOrDefaultAsync();
        }
        
        public async Task<IDictionary<string, City>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.Addresses
                .Where(a => ids.Contains(a.Id))
                .IncludeMany(includes, "City")
                .ToDictionaryAsync(key => key.Id.ToString(), 
                    value => value.City);
        }
    }
}