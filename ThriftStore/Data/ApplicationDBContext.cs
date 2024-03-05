using Microsoft.EntityFrameworkCore;
using ThriftStore.Models;

namespace ThriftStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Represents the collection of entities in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }

        // Constructor that takes DbContextOptions as a parameter
        // Automatically applies any pending migrations and creates the database if needed
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
