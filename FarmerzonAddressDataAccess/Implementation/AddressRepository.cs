using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonAddressDataAccess.Interface;
using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess.Implementation
{
    public class AddressRepository : AbstractRepository<Address>, IAddressRepository
    {
        public AddressRepository(FarmerzonAddressContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<Address>> GetEntitiesAsync(long? id = null, string doorNumber = null, 
            string street = null)
        {
            return await Context.Addresses
                .Where(a => id == null || a.AddressId == id)
                .Where(a => doorNumber == null || a.DoorNumber == doorNumber)
                .Where(a => street == null || a.Street == street)
                .ToListAsync();
        }

        public async Task<IList<Address>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.Addresses
                .IncludeMany(includes)
                .Where(a => ids.Contains(a.AddressId))
                .ToListAsync();
        }

        public Task<Address> UpdateEntityAsync(long id, Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<Address> DeleteEntityAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}