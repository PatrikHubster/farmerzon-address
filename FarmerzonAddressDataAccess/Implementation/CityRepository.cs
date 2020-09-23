using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class CityRepository : AbstractRepository<City>, ICityRepository
    {
        public CityRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<City>> GetEntitiesAsync(long? id = null, string zipCode = null, string name = null)
        {
            return await Context.Cities
                .Where(c => id == null || c.CityId == id)
                .Where(c => zipCode == null || c.ZipCode == zipCode)
                .Where(c => name == null || c.Name == name)
                .ToListAsync();
        }

        public async Task<IList<City>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.Cities
                .IncludeMany(includes)
                .Where(c => ids.Contains(c.CityId))
                .ToListAsync();
        }
        
        public async Task<City> UpdateEntityAsync(long id, City entity)
        {
            var city = await Context.Cities.SingleOrDefaultAsync(c => c.CityId == id);
            city.ZipCode = entity.ZipCode;
            city.Name = entity.Name;
            await Context.SaveChangesAsync();
            return city;
        }

        public async Task<bool> ExistingRelationshipsForCityAsync(long id)
        {
            var city = await Context.Cities
                .Include("Addresses")
                .SingleOrDefaultAsync(c => c.CityId == id);
            return city?.Addresses != null && city.Addresses.Count != 0;
        }

        public async Task<City> DeleteEntityAsync(long id)
        {
            var city = await Context.Cities.SingleOrDefaultAsync(c => c.CityId == id);
            Context.Cities.Remove(city);
            await Context.SaveChangesAsync();
            return city;
        }
    }
}