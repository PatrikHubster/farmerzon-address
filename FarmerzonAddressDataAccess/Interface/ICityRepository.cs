using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ICityRepository : IAbstractRepository<City>
    {
        public Task<IList<City>> GetEntitiesAsync(long? id = null, string zipCode = null, string name = null);
        public Task<IList<City>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
        public Task<bool> ExistingRelationshipsForCity(long id);
    }
}