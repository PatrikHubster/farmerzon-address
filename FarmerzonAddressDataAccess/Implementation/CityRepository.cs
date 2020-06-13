using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class CityRepository : AbstractRepository, ICityRepository
    {
        public CityRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<City>> GetEntitiesAsync(long? id, string zipCode, string name)
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
    }
}