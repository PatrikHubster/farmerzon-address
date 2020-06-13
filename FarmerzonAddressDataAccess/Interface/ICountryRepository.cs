using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface ICountryRepository
    {
        public Task<IList<Country>> GetEntitiesAsync(long? id, string name, string code);
        public Task<IList<Country>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
    }
}