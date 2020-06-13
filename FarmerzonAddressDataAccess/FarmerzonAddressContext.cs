using FarmerzonAddressDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonAddressDataAccess
{
    public class FarmerzonAddressContext : DbContext
    {
        public FarmerzonAddressContext(DbContextOptions<FarmerzonAddressContext> options) : base(options)
        {
            // nothing to do here
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<State> States { get; set; }
    }
}