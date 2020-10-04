using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IAddressRepository : IBasicRepository<Address>
    {
        public Task<IDictionary<string, IList<Address>>> GetEntitiesByCityIdAsync(IEnumerable<long> ids,
            IEnumerable<string> includes = null);
        public Task<IDictionary<string, IList<Address>>> GetEntitiesByCountryIdAsync(IEnumerable<long> ids,
            IEnumerable<string> includes = null);
        public Task<IDictionary<string, IList<Address>>> GetEntitiesByStateIdAsync(IEnumerable<long> ids,
            IEnumerable<string> includes = null);
        public Task<IDictionary<string, IList<Address>>> GetEntitiesByNormalizedUserNamesAsync(
            IEnumerable<string> normalizedUserNames, IEnumerable<string> includes = null);
    }
}