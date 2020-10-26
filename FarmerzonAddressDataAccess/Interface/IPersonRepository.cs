using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IPersonRepository : IBasicRepository<Person>
    {
        public Task<IDictionary<string, Person>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}