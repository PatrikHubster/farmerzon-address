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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // City
            builder.Entity<City>()
                .HasIndex(c => new {c.Name, c.ZipCode})
                .IsUnique();

            // Country
            builder.Entity<Country>()
                .HasIndex(c => c.Code)
                .IsUnique();

            builder.Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Person
            builder.Entity<Person>()
                .HasIndex(p => p.NormalizedUserName)
                .IsUnique();

            builder.Entity<Person>()
                .HasIndex(p => p.UserName)
                .IsUnique();

            // State
            builder.Entity<State>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<State> States { get; set; }
    }
}