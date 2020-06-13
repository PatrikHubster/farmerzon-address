using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ICityRepository
    {
        public Task<IList<City>> GetEntitiesAsync(long? id, string zipCode, string name);
        public Task<IList<City>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
    }
}