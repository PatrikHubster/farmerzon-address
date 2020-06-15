using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressDataAccessModel;

namespace FarmerzonAddressDataAccess.Interface
{
    public interface IPersonRepository
    {
        public Task<IList<Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
        public Task<IList<Person>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
        public Task<IList<Person>> GetEntitiesByNormalizedUserNamesAsync(IEnumerable<string> normalizedUserNames,
            IEnumerable<string> includes);
    }
}