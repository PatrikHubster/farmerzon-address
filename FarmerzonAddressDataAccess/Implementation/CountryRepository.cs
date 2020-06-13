using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class CountryRepository : AbstractRepository, ICountryRepository
    {
        public CountryRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<Country>> GetEntitiesAsync(long? id, string name, string code)
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
    }
}