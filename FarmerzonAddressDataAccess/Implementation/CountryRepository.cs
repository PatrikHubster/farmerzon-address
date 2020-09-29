using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        protected override async Task<Country> GetEntityAsync(Country entity)
        {
            return await Context.Countries
                .Where(c => c.Code == entity.Code || c.Name == entity.Name)
                .FirstOrDefaultAsync();
        }
        
        public async Task<IDictionary<string, Country>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids)
        {
            return await Context.Addresses
                .Where(a => ids.Contains(a.Id))
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Country);
        }
    }
}