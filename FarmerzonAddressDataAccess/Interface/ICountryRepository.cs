using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ICountryRepository : IBasicRepository<Country>
    {
        public Task<IDictionary<string, Country>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null);
    }
}