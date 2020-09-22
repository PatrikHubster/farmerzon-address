using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ICountryRepository : IAbstractRepository<Country>
    {
        public Task<IList<Country>> GetEntitiesAsync(long? id = null, string name = null, string code = null);
        public Task<IList<Country>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
        public Task<bool> ExistingRelationshipsForCountry(long id);
    }
}