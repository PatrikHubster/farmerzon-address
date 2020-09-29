using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ICityRepository : IBasicRepository<City>
    {
        public Task<IDictionary<string, City>> GetEntitiesByAddressIdAsync(IEnumerable<long> ids);
    }
}