using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        protected override async Task<Person> GetEntityAsync(Person entity)
        {
            return await Context.People
                .Where(p =>  p.NormalizedUserName == entity.NormalizedUserName)
                .FirstOrDefaultAsync();
        }

        public async Task<IDictionary<string, Person>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.Addresses
                .Where(a => ids.Contains(a.Id))
                .IncludeMany(includes, "Person")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Person);
        }
    }
}