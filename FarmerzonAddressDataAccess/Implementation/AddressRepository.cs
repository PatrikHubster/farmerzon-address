using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        protected override Task<Address> GetEntityAsync(Address entity)
        {
            return Task.FromResult<Address>(null);
        }

        public async Task<IDictionary<string, IList<Address>>> GetEntitiesByCityIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.Cities
                .Where(c => ids.Contains(c.Id))
                .IncludeMany(includes, "Addresses")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Addresses);
        }

        public async Task<IDictionary<string, IList<Address>>> GetEntitiesByCountryIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.Countries
                .Where(c => ids.Contains(c.Id))
                .IncludeMany(includes, "Addresses")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Addresses);
        }

        public async Task<IDictionary<string, IList<Address>>> GetEntitiesByStateIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.States
                .Where(s => ids.Contains(s.Id))
                .IncludeMany(includes, "Addresses")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Addresses);
        }

        public async Task<IDictionary<string, IList<Address>>> GetEntitiesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames, IEnumerable<string> includes = null)
        {
            return await Context.People
                .Where(p => normalizedUserNames.Contains(p.NormalizedUserName))
                .IncludeMany(includes, "Addresses")
                .ToDictionaryAsync(key => key.NormalizedUserName,
                    value => value.Addresses);
        }
    }
}