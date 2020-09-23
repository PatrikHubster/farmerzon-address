using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class CountryRepository : AbstractRepository<Country>, ICountryRepository
    {
        public CountryRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<Country>> GetEntitiesAsync(long? id = null, string name = null, string code = null)
        {
            return await Context.Countries
                .Where(c => id == null || c.CountryId == id)
                .Where(c => name == null || c.Name == name)
                .Where(c => code == null || c.Code == code)
                .ToListAsync();
        }

        public async Task<IList<Country>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.Countries
                .IncludeMany(includes)
                .Where(c => ids.Contains(c.CountryId))
                .ToListAsync();
        }

        public async Task<Country> UpdateEntityAsync(long id, Country entity)
        {
            var country = await Context.Countries.SingleOrDefaultAsync(c => c.CountryId == id);
            country.Code = entity.Code;
            country.Name = entity.Name;
            await Context.SaveChangesAsync();
            return country;
        }
        
        public async Task<bool> ExistingRelationshipsForCountryAsync(long id)
        {
            var country = await Context.Countries
                .Include("Addresses")
                .SingleOrDefaultAsync(c => c.CountryId == id);
            return country?.Addresses != null && country.Addresses.Count != 0;
        }

        public async Task<Country> DeleteEntityAsync(long id)
        {
            var country = await Context.Countries.SingleOrDefaultAsync(c => c.CountryId == id);
            Context.Countries.Remove(country);
            await Context.SaveChangesAsync();
            return country;
        }
    }
}